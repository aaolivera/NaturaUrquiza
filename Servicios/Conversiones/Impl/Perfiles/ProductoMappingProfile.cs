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
                .ForMember(x => x.Tipo, y => y.MapFrom(x => x.Tipo.Nombre))
                .ForMember(x => x.TipoId, y => y.MapFrom(x => x.Tipo.Id));
            Mapper.CreateMap<ProductoDto, Producto>();
        }
    }
}