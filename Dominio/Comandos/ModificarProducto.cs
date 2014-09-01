using Dominio.Dto;

namespace Dominio.Comandos
{
    public class ModificarProducto : Comando
    {
        public ProductoDto Dto { get; set; }
    }
}
