using GeekShopping.CuponAPI.Data.ValueObjects;

namespace GeekShopping.CuponAPI.Repository
{
    public interface ICuponRepository
    {
        Task <CuponVO> GetCuponByCuponCode(string cuponCode);
    }
}
