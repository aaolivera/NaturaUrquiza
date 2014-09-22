using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Dominio.Consultas;
using Microsoft.Web.Mvc;
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

        [AjaxOnly]
        [ActionName("Index")]
        public ActionResult Listar(string filtro = "", int pagina = 1, string ordenarPor = "Id", DirOrden dirOrden = DirOrden.Asc)
        {
            ListQuery(filtro, pagina, ordenarPor, dirOrden);
            return View("Listar", (object)filtro);
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

        [HttpPost]
        public ActionResult Upload()
        {
            // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index
            var file = Request.Files[0];

            // do something with the file in this space 
            var pic = Path.GetFileName(file.FileName.Replace(' ', '-'));
            var path = Path.Combine(Server.MapPath("~/images/profile"), pic);
            // file is uploaded
            file.SaveAs(path);

            // end of file doinge
            return Json(new {HttpStatusCode.OK});
        }

    }
}
