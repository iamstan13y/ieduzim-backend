using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Enums;
using IEduZimAPI.Models.Local;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class HubRepository : IHubRepository
    {
        private readonly AppDbContext _context;

        public HubRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Hub>> AddAsync(Hub hub)
        {
            try
            {
                await _context.Hubs.AddAsync(hub);
                await _context.SaveChangesAsync();

                return new Result<Hub>(hub);
            }
            catch (Exception)
            {
                return new Result<Hub>(false, "Failed to save", null);
            }
        }

        public async Task<Result<Hub>> DeleteAsync(int id)
        {
            var hub = await _context.Hubs.FindAsync(id);
            if (hub == null) return new Result<Hub>(false, "Hub not found.", null);

            _context.Remove(hub);
            await _context.SaveChangesAsync();

            return new Result<Hub>(hub);
        }

        public async Task<Result<IEnumerable<Hub>>> GetAllAsync()
        {
            var hubs = await _context.Hubs.Include(x => x.Location).ToListAsync();
            return new Result<IEnumerable<Hub>>(hubs);
        }

        public async Task<Result<Hub>> GetByIdAsync(int id)
        {
            var hub = await _context.Hubs.Include(x => x.Location).FirstOrDefaultAsync(x => x.Id == id);
            if (hub == null) return new Result<Hub>(false, "Hub not found.", null);

            return new Result<Hub>(hub);
        }

        public async Task<Result<IEnumerable<HubSearchResponse>>> SearchAsync(int SubjectId)
        {
            var subject = await _context.Subjects
                .Include(x => x.Level)
                .Include(x => x.LessonLocation)
                .Where(x => x.Id == SubjectId)
                .FirstOrDefaultAsync();

            if (subject != null) subject.ZwlPrice = CalculateZwlPrice(subject.Price);

            var schedules = await _context.HubLessonSchedules.Where(x => x.SubjectId == SubjectId).ToListAsync();
            schedules.ForEach(x =>
            {
                Day day = (Day)Convert.ToInt32(x.LessonDay);
                x.LessonDay = x.LessonDay == "1" ? day.ToString() : "Every " + day.ToString();
            });

            var response = new List<HubSearchResponse>();

            var hubIds = schedules.Select(x => x.HubId).Distinct().ToList();
            hubIds.ForEach(x =>
            {
                response.Add(new HubSearchResponse
                {
                    Hub = _context.Hubs.Include(u => u.Location).First(y => y.Id == x),
                    LessonSchedules = schedules.Where(y => y.HubId == x).ToList(),
                    Subject = subject
                });
            });

            return new Result<IEnumerable<HubSearchResponse>>(response);
        }

        public async Task<Result<Hub>> UpdateAsync(Hub hub)
        {
            _context.Hubs.Update(hub);
            await _context.SaveChangesAsync();

            return new Result<Hub>(hub);
        }

        private double CalculateZwlPrice(string UsdPrice)
        {
            var rate = _context.ExchangeRates.Where(x => x.CurrencyId == 2).FirstOrDefault().Rate;

            return Math.Round(Convert.ToDouble(UsdPrice) * rate, 2);
        }
    }
}