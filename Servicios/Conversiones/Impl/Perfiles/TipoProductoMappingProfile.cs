using AutoMapper;
using Dominio.Dto;
using Dominio.Entidades;

namespace Servicios.Conversiones.Impl.Perfiles
{
    public class TipoProductoMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "TipoProductoMappingProfile"; }
        }
        protected override void Configure()
        {
            Mapper.CreateMap<TipoProducto, TipoProductoDto>();
            Mapper.CreateMap<TipoProductoDto, TipoProducto>();
        }
    }
}