using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionsController(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<Subscription>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get() => Ok(await _subscriptionRepository.GetAllAsync());

        [HttpGet("get-by-id/{id}")]
        [ProducesResponseType(typeof(Result<Subscription>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<Subscription>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _subscriptionRepository.GetByIdAsync(id);
            if (result.Data == null) return NotFound(result);
            return Ok(result);

        }
        
        [HttpGet("get-by-studentId/{studentId}")]
        [ProducesResponseType(typeof(Result<IEnumerable<Subscription>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByStudentId(int studentId)
        {
            var result = await _subscriptionRepository.GetByStudentIdAsync(studentId);
            return Ok(result);

        }
    }
}