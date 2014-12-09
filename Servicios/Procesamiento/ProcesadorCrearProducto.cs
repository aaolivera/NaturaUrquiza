using Dominio.Comandos;
using Dominio.Dto;
using Dominio.Entidades;
using Repositorio;
using Servicios.Conversiones;

namespace Servicios.Procesamiento
{
    public class ProcesadorCrearProducto : ProcesadorCrear<CrearProducto, Producto>
    {
        public ProcesadorCrearProducto(IRepositorio repositorio, IConversor conversor) : base(repositorio, conversor)
        {
        }

        protected override Producto CrearEntidad(CrearProducto comando)
        {
            var entidad = Conversor.Convertir<ProductoDto, Producto>(comando.Dto);
            if (comando.Dto.LineaId != 0)
            {
                entidad.Linea = Repositorio.Obtener<Linea>(comando.Dto.LineaId);
            }
            if (comando.Dto.TipoProductoId != 0)
            {
                entidad.TipoProducto = Repositorio.Obtener<TipoProducto>(comando.Dto.TipoProductoId);
            }
            return entidad;
        }

        protected override void Validar(CrearProducto comando, Resultado resultado)
        {
            
        }

    }
}