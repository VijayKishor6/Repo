using CRUD.Domain.Models;
using CRUD.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimateController : ControllerBase
    {
        private readonly IEstimatesRepository _estimatesRepository;
        public EstimateController(IEstimatesRepository estimatesRepository)
        {
            _estimatesRepository = estimatesRepository;
        }
        // create estimate
        [HttpPost]
        public IActionResult CreateEstimates(Estimates model)
        {
            var result = _estimatesRepository.EstimateAdd(model);
            return Ok(result);
        }
    }
}
