using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly AppDbContext _context;

        public LocationsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Location>> AddAsync(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            return new Result<Location>(location);
        }

        public async Task<Result<IEnumerable<Location>>> GetAllAsync()
        {
            var locations = await _context.Locations.ToListAsync();
            return new Result<IEnumerable<Location>>(locations);
        }

        public Task<Paginator<Location>> GetAllPagedAsync(PageRequest request)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = _context.Set<Location>().AsQueryable<Location>();
            if (request.SortParam != null)
                req = Sort(req, request);
            var total = req.CountAsync().Result;
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            return Task.FromResult(new Paginator<Location>(request, total, req));
        }

        public IQueryable<Location> Sort(IQueryable<Location> req, PageRequest request)
        {
            var param = Expression.Parameter(typeof(Location), "item");
            var sortExpression = Expression.Lambda<Func<Location, object>>
                    (Expression.Convert(Expression.Property(param, request.SortParam), typeof(object)), param);
            if (request.Desc)
                req = req.OrderByDescending(sortExpression);
            else req = req.OrderBy(sortExpression);
            return req;
        }

        public async Task<Result<IEnumerable<Location>>> GetByCityIdAsync(int cityId)
        {
            var locations = await _context.Locations.Where(x => x.CityId == cityId).ToListAsync();
            return new Result<IEnumerable<Location>>(locations);
        }

        public async Task<Result<Location>> GetByIdAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            return new Result<Location>(location);
        }

        public Task<Result<Location>> UpdateAsync(Location location)
        {
            throw new System.NotImplementedException();
        }
    }
}