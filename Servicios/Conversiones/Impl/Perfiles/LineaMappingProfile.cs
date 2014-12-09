using AutoMapper;
using Dominio.Dto;
using Dominio.Entidades;

namespace Servicios.Conversiones.Impl.Perfiles
{
    public class LineaMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "LineaMappingProfile"; }
        }
        protected override void Configure()
        {
            Mapper.CreateMap<Linea, LineaDto>()
                .ForMember(x => x.TipoLinea, y => y.MapFrom(x => x.TipoLinea.Nombre))
                .ForMember(x => x.TipoLineaId, y => y.MapFrom(x => x.TipoLinea.Id));
            Mapper.CreateMap<LineaDto, Linea>();
        }
    }
}