namespace GeekShopping.CartAPI.Data.ValueObjects {
    public class CartViewModel {
        public CartHeaderViewModel CartHeader { get; set; }
        public IEnumerable<CartDetailViewModel> CartDetails { get; set; }
    }
}
