using GeekShopping.CartAPI.Data.ValueObjects;

namespace Geekshopping.CartAPI.Repository {
    public interface ICartRepository {
        Task<CartVO> FindCartByUserId(string userID);
        Task<CartVO> SaveOrUpdateCart(CartVO cart);
        Task<bool> RemoveFromCart(long cartDetailsId);
        Task<bool> ApplyCupon(string userID, string cuponCode);
        Task<bool> RemoveCupon(string userID);
        Task<bool> ClearCart(string userID);
    }
}
