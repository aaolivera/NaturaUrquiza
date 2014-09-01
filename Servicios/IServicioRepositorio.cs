using System.Collections.Generic;
using System.ServiceModel;
using Dominio.Consultas;
using Dominio.Dto;

namespace Servicios
{
    [ServiceContract(Namespace = "http://UnCuentoParaTodos.com.ar")]
    public interface IServicioRepositorio
    {
        [OperationContract]
        string Saludar();

        [OperationContract]
        List<UsuarioDto> ListarUsuarios();

        [OperationContract]
        ListaPaginada<ProductoDto> ListarProductos(string filtro, Paginacion paginacion);

        [OperationContract]
        ProductoDto ObtenerProducto(int id);
    }
}
