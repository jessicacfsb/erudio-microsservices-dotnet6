using Geekshopping.CartAPI.Data.ValueObjects;
using Geekshopping.CartAPI.Messages;
using Geekshopping.CartAPI.RabbitMQSender;
using Geekshopping.CartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Geekshopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;
        private ICuponRepository _cuponRepository;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository cartRepository, 
            ICuponRepository cuponRepository, 
            IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _cuponRepository = cuponRepository ?? throw new ArgumentNullException(nameof(cuponRepository));
            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            var cart = await _cartRepository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            var status = await _cartRepository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

        [HttpPost("apply-cupon")]
        public async Task<ActionResult<CartVO>> ApplyCupon(CartVO vo)
        {
            var status = await _cartRepository.ApplyCupon(vo.CartHeader.UserId, vo.CartHeader.CuponCode);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpDelete("remove-cupon/{userId}")]
        public async Task<ActionResult<CartVO>> ApplyCupon(string userId)
        {
            var status = await _cartRepository.RemoveCupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {
            string token = Request.Headers["Authorization"];
            if (vo?.UserId == null) return BadRequest();
            var cart = await _cartRepository.FindCartByUserId(vo.UserId);
            if (cart == null) return NotFound();
            if (!string.IsNullOrEmpty(vo.CuponCode))
            {
                CuponVO cupon = await _cuponRepository.GetCupon(vo.CuponCode, token);
                if (vo.DiscountAmount != cupon.DiscountAmount)
                {
                    return StatusCode(412); //412 refere a mudança de condições entre a 1 requisção até o pagamento (se o valor do desconto foi alterado)
                }
            }
            vo.CartDetails = cart.CartDetails;
            vo.DateTime = DateTime.Now;

            // RabbitMQ logic comes here!!!
            _rabbitMQMessageSender.SendMessage(vo, "checkoutqueue");

            return Ok(vo);
        }
    }
}
