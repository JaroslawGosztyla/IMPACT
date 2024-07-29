using Microsoft.AspNetCore.Mvc;
using Tenders.Domain.Models;
using Tenders.Services;

namespace Tenders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TendersController : ControllerBase
    {
        private readonly TenderService _tenderService;

        public TendersController(TenderService tenderService)
        {
            _tenderService = tenderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tender>>> GetTenders(int page = 1)
        {
            var tenders = await _tenderService.GetTenders(page);
            return Ok(tenders);
        }

        [HttpGet("by-price")]
        public async Task<ActionResult<IEnumerable<Tender>>> GetTendersByPrice(decimal price, int page = 1, int pageSize = 10)
        {
            var tenders = await _tenderService.GetTendersByPrice(price, page, pageSize);
            return Ok(tenders);
        }

        [HttpGet("ordered-by-price")]
        public async Task<ActionResult<IEnumerable<Tender>>> GetTendersOrderedByPrice(bool ascending = true, int page = 1, int pageSize = 10)
        {
            var tenders = await _tenderService.GetOrderedTendersByPrice(ascending, page, pageSize);
            return Ok(tenders);
        }

        [HttpGet("by-date")]
        public async Task<ActionResult<IEnumerable<Tender>>> GetTendersByDate(DateTime date, int page = 1, int pageSize = 10)
        {
            var tenders = await _tenderService.GetTendersByDate(date, page, pageSize);
            return Ok(tenders);
        }

        [HttpGet("ordered-by-date")]
        public async Task<ActionResult<IEnumerable<Tender>>> GetTendersOrderedByDate(bool ascending = true, int page = 1, int pageSize = 10)
        {
            var tenders = await _tenderService.GetOrderedTendersByDate(ascending, page, pageSize);
            return Ok(tenders);
        }

        [HttpGet("by-supplier/{supplierId}")]
        public async Task<ActionResult<IEnumerable<Tender>>> GetTendersBySupplier(int supplierId, int page = 1, int pageSize = 10)
        {
            var tenders = await _tenderService.GetTendersBySupplier(supplierId, page, pageSize);
            return Ok(tenders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tender>> GetTenderById(int id)
        {
            var tender = await _tenderService.GetTenderById(id);
            return tender == null ? NotFound() : Ok(tender);
        }
    }
}
