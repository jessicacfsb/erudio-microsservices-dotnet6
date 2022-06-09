using GeekShopping.CuponAPI.Data.ValueObjects;
using GeekShopping.CuponAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CuponAPI.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private ICuponRepository _repository;

        public CuponController(ICuponRepository repository)
        {
            _repository = repository ?? throw new
                ArgumentNullException(nameof(repository));
        }

        [HttpGet("{cuponCode}")]
        [Authorize]
        public async Task<ActionResult<CuponVO>> GetCuponByCuponCode(string cuponCode)
        {
            var cupon = await _repository.GetCuponByCuponCode(cuponCode);
            if (cupon == null) return NotFound();
            return Ok(cupon);
        }
    }
}
