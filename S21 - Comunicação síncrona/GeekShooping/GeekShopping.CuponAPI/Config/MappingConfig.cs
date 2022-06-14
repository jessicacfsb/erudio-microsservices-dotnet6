using AutoMapper;
using GeekShopping.CuponAPI.Data.ValueObjects;
using GeekShopping.CuponAPI.Model;

namespace GeekShopping.CuponAPI.Config
{
    public class MappingConfig {
        public static MapperConfiguration RegisterMaps() {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<CuponVO, Cupon>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
