using Geekshopping.CartAPI.Data.ValueObjects;

namespace Geekshopping.CartAPI.Repository
{
    public interface ICuponRepository
    {
        Task <CuponVO> GetCupon(string cuponCode, string token);
    }
}
