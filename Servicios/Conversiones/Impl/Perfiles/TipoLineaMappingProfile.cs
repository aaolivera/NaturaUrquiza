using AutoMapper;
using Dominio.Dto;
using Dominio.Entidades;

namespace Servicios.Conversiones.Impl.Perfiles
{
    public class TipoLineaMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "TipoLineaMappingProfile"; }
        }
        protected override void Configure()
        {
            Mapper.CreateMap<TipoLinea, TipoLineaDto>();
            Mapper.CreateMap<TipoLineaDto, TipoLinea>();
        }
    }
}