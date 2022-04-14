using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using IEduZimAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public ExchangeRatesController(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<ExchangeRate>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(ExchangeRateRequest request)
        {
            var result = await _exchangeRateRepository.AddAsync(new ExchangeRate
            {
                CurrencyId = request.CurrencyId,
                Rate = request.UsdRate
            });

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<ExchangeRate>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _exchangeRateRepository.GetAllAsync());
    }
}