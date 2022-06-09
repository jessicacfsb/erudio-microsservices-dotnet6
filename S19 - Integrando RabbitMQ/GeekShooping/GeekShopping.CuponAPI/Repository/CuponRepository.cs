using AutoMapper;
using GeekShopping.CuponAPI.Data.ValueObjects;
using GeekShopping.CuponAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CuponAPI.Repository
{
    public class CuponRepository : ICuponRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        //ctor tab tab cria um construtor
        public CuponRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CuponVO> GetCuponByCuponCode(string cuponCode)
        {
            var cupon = await _context.Cupons.FirstOrDefaultAsync(c => c.CuponCode == cuponCode);
            return _mapper.Map<CuponVO>(cupon);
        }
    }
}
