using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Dominio.Comandos;
using Dominio.Consultas;
using Dominio.Dto;
using Microsoft.Web.Mvc;
using NaturaUrquiza.Helpers;
using Servicios;

namespace NaturaUrquiza.Controllers
{
    public class AdministracionController : Controller
    {
        private readonly IServicioRepositorio servicio;
        private readonly IServicioComandos servicioComandos;

        public AdministracionController(IServicioRepositorio servicio, IServicioComandos servicioComandos)
        {
            this.servicio = servicio;
            this.servicioComandos = servicioComandos;
        }

        public ActionResult Index(string filtro = "", int pagina = 1, string ordenarPor = "Id", DirOrden dirOrden = DirOrden.Asc)
        {
            ListQuery(filtro, pagina, ordenarPor, dirOrden);
            return View((object)filtro);
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
            var paginacion = new Paginacion(ordenarPor, dirOrden, pagina, 10);

            ViewBag.Items = servicio.ListarProductos(filtro,paginacion);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(ProductoDto model)
        {
            if (ModelState.IsValid)
            {
                
                var resultado = servicioComandos.Ejecutar(new CrearProducto { Dto = model });
                if (!resultado.HayErrores)
                {
                    return new AjaxEditSuccessResult();
                }
                ModelState.AgregarErrores(resultado);
            }

            return View(model);
        }

        public ActionResult Modificar(int id)
        {
            var choferAModificar = servicio.ObtenerProducto(id);

            return View(choferAModificar);
        }

        [HttpPost]
        public ActionResult Modificar(ProductoDto model)
        {
            if (ModelState.IsValid)
            {
                var resultado = servicioComandos.Ejecutar(new ModificarProducto { Dto = model });
                if (!resultado.HayErrores)
                {
                    return new AjaxEditSuccessResult();
                }
                ModelState.AgregarErrores(resultado);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            var resultado = servicioComandos.Ejecutar(new EliminarProducto { Id = id });
            return Content(!resultado.HayErrores ? "true" : resultado.Errores.Values.First());
        }
    }
}
