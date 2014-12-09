using System.Web.Mvc;
using Dominio.Recursos;
using Servicios;

namespace NaturaUrquiza.Controllers
{
    public class MenuController : Controller
    {
        private IServicioRepositorio _servicio;
        public MenuController(IServicioRepositorio servcio)
        {
            _servicio = servcio;
        }

        public ActionResult Menu()
        {
            ViewBag.Title = Textos.Menu_Titulo;
            ViewBag.Message = Textos.Menu_Mensaje;
            ViewBag.TiposDeLineas = _servicio.ListarTiposDeLineas();
            return PartialView("_Menu");
        }

    }
}
