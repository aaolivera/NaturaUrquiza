using AutoMapper;
using Dominio.Dto;
using Dominio.Entidades;

namespace Servicios.Conversiones.Impl.Perfiles
{
    public class TipoMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "TipoMappingProfile"; }
        }
        protected override void Configure()
        {
            Mapper.CreateMap<Tipo, TipoDto>();
            Mapper.CreateMap<TipoDto, Tipo>();
        }
    }
}