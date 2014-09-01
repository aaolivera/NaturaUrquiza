using Dominio.Dto;

namespace Dominio.Comandos
{
    public class CrearProducto : Comando
    {
        public ProductoDto Dto { get; set; }
    }
}
