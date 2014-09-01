using System.Web.Mvc;
using Dominio.Consultas;
using Servicios;

namespace NaturaUrquiza.Controllers
{
    public class NaturaController : Controller
    {
        private IServicioRepositorio _servicio;
        public NaturaController(IServicioRepositorio servcio)
        {
            _servicio = servcio;
        }

        public ActionResult Index(string filtro = "", int pagina = 1, string ordenarPor = "Id", DirOrden dirOrden = DirOrden.Asc)
        {
            ListQuery(filtro, pagina, ordenarPor, dirOrden);
            return View();
        }

        private void ListQuery(string filtro, int pagina, string ordenarPor, DirOrden dirOrden)
        {
            var paginacion = new Paginacion(ordenarPor, dirOrden, pagina, 9);
            ViewBag.Productos = _servicio.ListarProductos(filtro, paginacion);
        }

        public ActionResult Ver(int id)
        {
            return View(_servicio.ObtenerProducto(id));
        }

    }
}
