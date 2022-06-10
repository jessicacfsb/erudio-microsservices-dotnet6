using AutoMapper;
using Geekshopping.CartAPI.Model;
using Geekshopping.CartAPI.Data.ValueObjects;
using Geekshopping.CartAPI.Model;

namespace Geekshopping.CartAPI.Config {
    public class MappingConfig {
        public static MapperConfiguration RegisterMaps() {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<ProductVO, Product>().ReverseMap();
                config.CreateMap<CartHeaderVO, CartHeader>().ReverseMap();
                config.CreateMap<CartDetailVO, CartDetail>().ReverseMap();
                config.CreateMap<CartVO, Cart>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
