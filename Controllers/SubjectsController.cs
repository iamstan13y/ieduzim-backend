﻿using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;
using IEduZimAPI.Services;
using System.Collections.Generic;
using IEduZimAPI.Models.Repository;
using System.Threading.Tasks;
using IEduZimAPI.Models.Local;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SubjectsController : ControllerBase //: BaseController<Subject, Models.Local.Subjects>
    {
        private readonly SubjectsService service;
        private readonly ISubjectRepository _subjectRepository;
        public SubjectsController(ISubjectRepository subjectRepository)
        {
            service = new SubjectsService();
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        [Route("paged/by-level/{levelId}")]
        public virtual Pagination<Paginator<Subject>> GetPaged([FromQuery] PageRequest request, int levelId) =>
           PagedExecution<Paginator<Subject>>.Execute(() => service.GetPagedByLevel(request, levelId));

        [HttpGet]
        public Result<IEnumerable<Subject>> Get() => _subjectRepository.GetAllSubjectsAsync().Result;

        [HttpGet("paged")]
        public Pagination<Paginator<Subject>> GetPaged([FromQuery] PageRequest request) => 
            Pagination<Paginator<Subject>>.FromObject(_subjectRepository.GetAllSubjectsPagedAsync(request).Result);
        
        [HttpGet("paged/search")]
        public async Task<IActionResult> Get([FromQuery] SearchSubjectRequest subjectRequest, [FromQuery] Pagination pagination) =>
            Ok(await _subjectRepository.GetPageByCriteriaAsync(subjectRequest, pagination));

        [HttpPost]
        public async Task<IActionResult> Post(SubjectRequest request)
        {
            var result = await _subjectRepository.AddAsync(new Subject
            {
                Active = true,
                CurrencyId = request.CurrencyId,
                LevelId = request.LevelId,
                Name = request.Name,
                Price = request.Price
            });

            if (!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }
    }
}