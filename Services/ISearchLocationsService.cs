﻿using IEduZimAPI.CoreClasses.Pagination;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using System.Threading.Tasks;

namespace IEduZimAPI.Services
{
    public interface ISearchLocationsService
    {
        Task<Pageable<Address>> GetPagedLocationsAsync(LocationSearchRequest searchRequest, Pagination pagination);
    }
}