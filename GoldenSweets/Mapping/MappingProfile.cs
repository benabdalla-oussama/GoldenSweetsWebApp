using AutoMapper;
using GoldenSweets.Core.Dto;
using GoldenSweets.Core.Models;
using GoldenSweets.Core.ViewModel;

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
