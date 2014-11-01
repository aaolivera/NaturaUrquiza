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
            if (comando.Dto.TipoId != 0)
            {
                entidad.Tipo = Repositorio.Obtener<Tipo>(comando.Dto.TipoId);
            }
            return entidad;
        }

        protected override void Validar(CrearProducto comando, Resultado resultado)
        {
            
        }

    }
}