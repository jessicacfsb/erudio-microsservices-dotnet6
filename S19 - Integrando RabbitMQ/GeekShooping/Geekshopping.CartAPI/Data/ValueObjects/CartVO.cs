using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Geekshopping.CartAPI.Data.ValueObjects {
    public class CartVO {
        public CartHeaderVO CartHeader { get; set; }
        [ValidateNever]
        public IEnumerable<CartDetailVO> CartDetails { get; set; }
    }
}
