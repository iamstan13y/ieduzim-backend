using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IEduZimAPI.Services
{
    public class SearchLocationsService:BaseService<Address>
    {
        private IEduContext context;
        private readonly AppDbContext _appDbContext;

        public SearchLocationsService(IEduContext eduContext, AppDbContext appDbContext)
        {
            this.context = eduContext;
            _appDbContext = appDbContext;
        }

        public Paginator<Address> GetPagedLocationsByCriteria(LocationSearchRequest request)
        {
            List<Address> addresses = new List<Address>();
            var subjects = GetRequestedSubjects(request);
            if (subjects.Count() == 0) throw new System.Exception("No subject offers found.");
            if (request.PageNumber == 0) request.PageNumber = 1;
            if (request.PageSize == 0) request.PageSize = 10;
            var req = context.Addresses.Include(a=> a.City).Include(b=> b.Province).Include(t=> t.Teacher).ThenInclude(s=> s.User).Include(a=> a.Teacher).ThenInclude(b=> b.Title).Where(w=> w.IsLearningLocation == true).AsQueryable();
            if (request.ProvinceId != 0)
                req = req.Where(a => a.ProvinceId == request.ProvinceId);
            if (request.CityId != 0)
                req = req.Where(a => a.CityId == request.CityId);
            if(request.Area != null)
                req = req.Where(p => p.Area.Contains(request.Area));
            if (request.SortParam != null)
                req = Sort(req, request);
            var total = req.CountAsync().Result;
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            req.ToList().ForEach(a =>
            {
                if (subjects.Exists(b => b.TeacherId.Equals(a.UserId)))
                {
                    a.subjects = new List<LessonStructure>();
                    var subs = subjects.Where(w => w.TeacherId.Equals(a.UserId)).AsQueryable();
                    a.subjects.AddRange(subs);
                    addresses.Add(a);
                }
            });
            return new Paginator<Address>(request, total, addresses);
        }


        public List<LessonStructure> GetRequestedSubjects(LocationSearchRequest request)
        {
            var subjects = context.LessonStructures.Include(l => l.Level).Include(s => s.Subject).ThenInclude(a => a.Currency)
                .Where(a => a.LevelId == request.LevelId).AsQueryable();

            if (request.AutoLoad == true) return subjects.ToList();
            List<LessonStructure> subsList = new List<LessonStructure>();
            if (request.Subjects?.Count > 0)
                request.Subjects.ForEach(a =>
                {
                    subsList.AddRange(GetSubject(subjects, a, request.ExamType, request.LessonLocation));
                });

            foreach (var subject in subsList) subject.Subject.ZwlPrice = CalculateZwlPrice(subject.Subject.Price);
            
            return subsList;
        }

        public List<LessonStructure> GetSubject(IQueryable<LessonStructure> subjects, int subjectId, int examType, int lessonLocation)
        {
            var sub = subjects.Include(c => c.Level).Include(b => b.Subject).Where(a => a.SubjectId == subjectId).ToList();
            if (examType != 0) sub = sub.Where(e => e.ExamTypeId.Contains(examType.ToString())).ToList();
            if (lessonLocation != 0) sub = sub.Where(l => l.LessonLocationId.Contains(lessonLocation.ToString())).ToList();
            
            foreach(var subject in sub) subject.Subject.ZwlPrice = CalculateZwlPrice(subject.Subject.Price);

            return sub;
        }

        private double CalculateZwlPrice(string UsdPrice)
        {
            var rate = _appDbContext.ExchangeRates.Where(x => x.CurrencyId == 2).FirstOrDefault().Rate;

            return Math.Round(Convert.ToDouble(UsdPrice) * rate, 2);
        }
    }
}
