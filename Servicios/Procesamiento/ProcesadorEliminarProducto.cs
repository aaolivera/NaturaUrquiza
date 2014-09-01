using Dominio.Comandos;
using Dominio.Entidades;
using Repositorio;
using Servicios.Conversiones;

namespace Servicios.Procesamiento
{
    public class ProcesadorEliminarProducto : ProcesadorEliminar<EliminarProducto, Producto>
    {
        public ProcesadorEliminarProducto(IRepositorio repositorio, IConversor conversor)
            : base(repositorio, conversor)
        {
        }

        protected override int IdEntidad(EliminarProducto comando)
        {
            return comando.Id;
        }

        protected override void Validar(EliminarProducto comando, Resultado resultado)
        {
        }
    }
}
