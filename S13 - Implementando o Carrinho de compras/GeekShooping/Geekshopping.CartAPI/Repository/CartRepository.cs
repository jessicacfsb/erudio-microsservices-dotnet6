using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace Geekshopping.CartAPI.Repository {
    public class CartRepository : ICartRepository {

        private readonly MySQLContext _context;
        private IMapper _mapper;

        //ctor tab tab cria um construtor
        public CartRepository(MySQLContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public Task<bool> ApplyCupon(string userId, string cuponCode) {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId) {
            var cartHeader = await _context.CartHeaders
                        .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cartHeader != null) {
                _context.CartDetails
                    .RemoveRange(
                    _context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));
                _context.CartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartVO> FindCartByUserId(string userId) {
            Cart cart = new() {
                CartHeader = await _context.CartHeaders
                   .FirstOrDefaultAsync(c => c.UserId == userId),
            };
            cart.CartDetails = _context.CartDetails
                .Where(c => c.CartHeaderId == cart.CartHeader.Id)
                    .Include(c => c.Product);
            return _mapper.Map<CartVO>(cart);
        }

        public Task<bool> RemoveCupon(string userID) {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId) {
            try {
                CartDetail cartDetail = await _context.CartDetails
                    .FirstOrDefaultAsync(c => c.Id == cartDetailsId);

                int total = _context.CartDetails
                    .Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();

                _context.CartDetails.Remove(cartDetail);

                if (total == 1) {
                    var cartHeaderToRemove = await _context.CartHeaders
                        .FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO vo) {
            Cart cart = _mapper.Map<Cart>(vo);
            //verificar se o produto já está salvo na base e salvá-lo caso não exista
            var product = await _context.Products.FirstOrDefaultAsync(
                p => p.Id == vo.CartDetails.FirstOrDefault().ProductId);

            if (product == null) {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            //verificar se o cartHeader é null
            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                c => c.UserId == cart.CartHeader.UserId);

            if (cartHeader == null) {
                //Criar o CartHeader
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());  //adicionar o cart details ao contexto
                await _context.SaveChangesAsync();
            }
            else {
                //Se CartHeader for not null
                //checar se o CartDetails tem o mesmo product
                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    p => p.ProductId == vo.CartDetails.FirstOrDefault().ProductId &&
                    p.CartHeaderId == cartHeader.Id);

                if (cartDetail == null) {
                    //Criar CartDetails
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else {
                    //Fazer o Update de product count e CartDetails
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartVO>(cart);
        }
    }
}
