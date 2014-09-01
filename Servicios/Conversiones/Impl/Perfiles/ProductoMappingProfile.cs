using AutoMapper;
using Dominio.Dto;
using Dominio.Entidades;

namespace Servicios.Conversiones.Impl.Perfiles
{
    public class ProductoMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ProductoMappingProfile"; }
        }
        protected override void Configure()
        {
            Mapper.CreateMap<Producto, ProductoDto>();
            Mapper.CreateMap<ProductoDto, Producto>();
        }
    }
}