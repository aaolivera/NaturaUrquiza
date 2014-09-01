using System.Web.Mvc;
using Dominio.Recursos;
using Servicios;

namespace NaturaUrquiza.Controllers
{
    public class MenuController : Controller
    {
        public ActionResult Menu()
        {
            ViewBag.Title = Textos.Menu_Titulo;
            ViewBag.Message = Textos.Menu_Mensaje;
            return PartialView("_Menu");
        }

    }
}
