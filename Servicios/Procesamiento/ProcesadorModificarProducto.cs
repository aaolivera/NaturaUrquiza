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

            if (comando.Dto.LineaId != 0 && (producto.Linea == null || producto.Linea.Id != comando.Dto.LineaId))
            {
                producto.Linea = Repositorio.Obtener<Linea>(comando.Dto.LineaId);
            }
            if (comando.Dto.TipoId != 0 && (producto.Tipo == null || producto.Tipo.Id != comando.Dto.TipoId))
            {
                producto.Tipo = Repositorio.Obtener<Tipo>(comando.Dto.TipoId);
            }
        }

        protected override void Validar(ModificarProducto comando, Resultado resultado)
        {

        }
    }
}
