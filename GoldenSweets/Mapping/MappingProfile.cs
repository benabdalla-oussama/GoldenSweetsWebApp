using AutoMapper;
using GoldenSweets.Core.Dto;
using GoldenSweets.Core.Models;

namespace GoldenSweets.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderDto, Order>();
            CreateMap<CakeDto, Cake>();

            CreateMap<Cake, CakeDto>();
        }
    }
}
