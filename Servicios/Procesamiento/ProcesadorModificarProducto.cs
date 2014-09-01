using Dominio.Comandos;
using Dominio.Entidades;
using Repositorio;
using Servicios.Conversiones;

namespace Servicios.Procesamiento
{
    public class ProcesadorModificarProducto : ProcesadorModificar<ModificarProducto>
    {
        public ProcesadorModificarProducto(IRepositorio repositorio, IConversor conversor)
            : base(repositorio, conversor)
        {
        }

        protected override void ModificarEntidad(ModificarProducto comando)
        {
            var producto = Repositorio.Obtener<Producto>(comando.Dto.Id);
            Conversor.Convertir(comando.Dto, producto);
        }

        protected override void Validar(ModificarProducto comando, Resultado resultado)
        {

        }
    }
}
