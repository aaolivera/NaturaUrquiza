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
        ListaPaginada<ProductoDto> ListarProductos(string filtro, int lineaId, Paginacion paginacion);

        [OperationContract]
        ProductoDto ObtenerProducto(int id);

        [OperationContract]
        List<LineaDto> ListarLineas();

        [OperationContract]
        List<TipoLineaDto> ListarTiposDeLineas();

        [OperationContract]
        List<TipoProductoDto> ListarTipos();
    }
}
