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
            Mapper.CreateMap<Producto, ProductoDto>()
                .ForMember(x => x.Linea,y =>y.MapFrom(x => x.Linea.Nombre))
                .ForMember(x => x.LineaId, y => y.MapFrom(x => x.Linea.Id))
                .ForMember(x => x.TipoProducto, y => y.MapFrom(x => x.TipoProducto.Nombre))
                .ForMember(x => x.TipoProductoId, y => y.MapFrom(x => x.TipoProducto.Id));
            Mapper.CreateMap<ProductoDto, Producto>();
        }
    }
}