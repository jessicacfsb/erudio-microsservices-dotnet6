using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository {
    public class ProductRepository : IProductRepository {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        //ctor tab tab cria um construtor
        public ProductRepository(MySQLContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll() {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id) {
            Product product = 
                await _context.Products.Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }
        public async Task<ProductVO> Create(ProductVO vo) {
            Product product = _mapper.Map<Product>(vo); //Converter para entidade
            _context.Products.Add(product);             //persiste
            await _context.SaveChangesAsync();          //Salvar o produto
            return _mapper.Map<ProductVO>(product);     //converte para vo e retorna para a entidade
        }

        public async Task<ProductVO> Update(ProductVO vo) {
            Product product = _mapper.Map<Product>(vo); //Converter para entidade
            _context.Products.Update(product);             //persiste
            await _context.SaveChangesAsync();          //Salvar o produto
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<bool> Delete(long id) {
            try {
                Product product =
                await _context.Products.Where(p => p.Id == id)
                .FirstOrDefaultAsync();
                if (product == null) return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

    }
}
