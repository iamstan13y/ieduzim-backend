using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IEduContext _context;
        private readonly AppDbContext _appDbContext;

        public SubjectRepository(IEduContext context, AppDbContext appDbContext)
        {
            _context = context;
            _appDbContext = appDbContext;
        }

        public async Task<Result<Subject>> AddAsync(HubSubjectRequest request)
        {
            try
            {
                var subject = new Subject
                {
                    Active = true,
                    CurrencyId = request.CurrencyId,
                    LevelId = request.LevelId,
                    Name = request.Name,
                    Price = request.Price,
                    LessonLocationId = request.LessonLocationId
                };

                if (request.LessonLocationId == 1)
                {
                    subject.StartTime = default;
                    subject.EndTime = default;
                }
                else
                {
                    subject.StartTime = DateTime.ParseExact(request.StartTime, "HH:mm", CultureInfo.InvariantCulture);
                    subject.EndTime = DateTime.ParseExact(request.EndTime, "HH:mm", CultureInfo.InvariantCulture);
                }

                await _appDbContext.Subjects.AddAsync(subject);

                if (request.LessonLocationId != 1)
                {
                    HybridLessonSchedule schedule = null;

                    request.LessonDays.ForEach(x =>
                    {
                        var scheduleInDb = _appDbContext.HybridLessonSchedules.Where(y => y.LessonDay == x && y.StartTime.ToString().Contains(request.StartTime) && y.EndTime.ToString().Contains(request.EndTime)).FirstOrDefault();
                        if (scheduleInDb != null) schedule = scheduleInDb;
                    });

                    if (schedule != null) return new Result<Subject>(false, "Schedule is already taken.", null);
                    await _appDbContext.SaveChangesAsync();

                    request.LessonDays.ForEach(lessonDay =>
                    {
                        _appDbContext.HybridLessonSchedules.Add(new HybridLessonSchedule
                        {
                            SubjectId = subject.Id,
                            StartTime = subject.StartTime,
                            EndTime = subject.EndTime,
                            LessonDay = lessonDay
                        });
                    });
                }

                await _appDbContext.SaveChangesAsync();

                return new Result<Subject>(subject);
            }
            catch (Exception)
            {
                return new Result<Subject>(false, "An error occured while saving subject.", null);
            }
        }

        public async Task<Result<IEnumerable<Subject>>> GetAllSubjectsAsync()
        {
            var subjects = await _appDbContext.Subjects.Include(x => x.Level).ToListAsync();

            subjects.ForEach(x => x.ZwlPrice = CalculateZwlPrice(x.Price));

            subjects.ForEach(x =>
            {
                x.LessonSchedules = new();
                var schedules = _appDbContext.HybridLessonSchedules.Where(y => y.SubjectId == x.Id).ToList();
                if (schedules.Count != 0) x.LessonSchedules = schedules;
            });

            return new Result<IEnumerable<Subject>>(subjects);
        }

        public Task<Paginator<Subject>> GetAllSubjectsPagedAsync(PageRequest request)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = _context.Set<Subject>().Include(x => x.Level).AsQueryable().ToList();
            if (request.SortParam != null)
                req = Sort((IQueryable<Subject>)req, request).ToList();
            var total = req.Count;
            req.ForEach(x => x.ZwlPrice = CalculateZwlPrice(x.Price));
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
            return Task.FromResult(new Paginator<Subject>(request, total, req));
        }

        public async Task<Result<IEnumerable<Subject>>> GetPageByCriteriaAsync(int levelId, int lessonLocationId)
        {
            var subjects = await _appDbContext.Subjects
                .Include(x => x.LessonLocation)
                .Include(x => x.Level)
                .Where(x => x.LevelId == levelId && x.LessonLocationId == lessonLocationId).ToListAsync();

            subjects.ForEach(x => x.ZwlPrice = CalculateZwlPrice(x.Price));

            subjects.ForEach(x =>
            {
                x.LessonSchedules = _appDbContext.HybridLessonSchedules.Where(y => y.SubjectId == x.Id).ToList();
            });

            return new Result<IEnumerable<Subject>>(subjects);
        }

        public IQueryable<Subject> Sort(IQueryable<Subject> req, PageRequest request)
        {
            var param = Expression.Parameter(typeof(Subject), "item");
            var sortExpression = Expression.Lambda<Func<Subject, object>>
                    (Expression.Convert(Expression.Property(param, request.SortParam), typeof(object)), param);
            if (request.Desc)
                req = req.OrderByDescending(sortExpression);
            else req = req.OrderBy(sortExpression);
            return req;
        }

        private double CalculateZwlPrice(string UsdPrice)
        {
            var rate = _appDbContext.ExchangeRates.Where(x => x.CurrencyId == 2).FirstOrDefault().Rate;

            return Math.Round(Convert.ToDouble(UsdPrice) * rate, 2);
        }


    }
}