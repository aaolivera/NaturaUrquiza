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
            Mapper.CreateMap<Linea, LineaDto>();
            Mapper.CreateMap<LineaDto, Linea>();
        }
    }
}