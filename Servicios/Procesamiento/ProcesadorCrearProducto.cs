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
            return Conversor.Convertir<ProductoDto, Producto>(comando.Dto);
        }

        protected override void Validar(CrearProducto comando, Resultado resultado)
        {
            
        }

    }
}