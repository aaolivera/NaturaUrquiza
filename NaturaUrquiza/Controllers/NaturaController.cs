using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;
using Dominio.Consultas;
using Dominio.Dto;
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

        public ActionResult Index(string filtro = "", int pagina = 1, string ordenarPor = "Prioridad", DirOrden dirOrden = DirOrden.Asc)
        {
            ListQuery(filtro, pagina, ordenarPor, dirOrden);
            return View();
        }

        [AjaxOnly]
        [ActionName("Index")]
        public ActionResult Listar(string filtro = "", int pagina = 1, string ordenarPor = "Prioridad", DirOrden dirOrden = DirOrden.Asc)
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

        [HttpPost]
        public ActionResult Comprar(CompraDto dto)
        {
            if (ModelState.IsValid)
            {
                EnviarMailVendedor(dto);
                EnviarMailComprador(dto);
                return new AjaxEditSuccessResult();
            }
            return View("Comprar");
        }

        private void EnviarMailVendedor(CompraDto dto)
        {
            var js = new DataContractJsonSerializer(typeof(ProductoDto[]));
            var ms = new MemoryStream(Encoding.Unicode.GetBytes(dto.Productos));
            var productos = js.ReadObject(ms) as ProductoDto[];

            var fromAddress = new MailAddress("romina.c.lozano@gmail.com", "Romina");
            var to = new MailAddress("aa.olivera09@gmail.com", "ale");
            const string fromPassword = "alejandro7";
            var subject = "Compra " + dto.Nombre + " " + DateTime.Now.ToShortDateString();
            var body = String.Format(
@"<div><b>Nombre:</b>   {0}</div>
<div><b>Telefono:</b>   {1}</div>
<div><b>Mail:</b>   {2}</div>
<div><b>Productos:</b></div> 
<div style='padding-left:5em'>{3}</div>
<div><b>Total: </b>${4}</div>", dto.Nombre, dto.Telefono, dto.Mail, productos.Select(x => "<div><b>" + x.Codigo + "-" + x.Nombre + "</b></div><div style='padding-left:2em'>" + x.PrecioVisible + "</div>").Aggregate((current, next) => current + next), productos.Sum(x => x.PrecioPromocional.HasValue ? x.PrecioPromocional : x.Precio));

            EnviarMAil(fromAddress, fromPassword, to, subject, body);
        }

        private void EnviarMailComprador(CompraDto dto)
        {
            var js = new DataContractJsonSerializer(typeof(ProductoDto[]));
            var ms = new MemoryStream(Encoding.Unicode.GetBytes(dto.Productos));
            var productos = js.ReadObject(ms) as ProductoDto[];

            var fromAddress = new MailAddress("romina.c.lozano@gmail.com", "Romina");
            var to = new MailAddress(dto.Mail, dto.Nombre);
            const string fromPassword = "alejandro7";
            var subject = "Confirmación de compra " + DateTime.Now.ToShortDateString();
            var body = String.Format(
@"<div><b>Estimada/o {0},</b></div>
<div><b>Has seleccionado los productos:</b></div> 
<div style='padding-left:5em'>{1}</div>
<div style='padding-left:5em'><b>Total: </b>${2}</div>
<br/><br/>
<div>En breve nos pondremos en contacto con usted para confirmar la venta.
Cualquier duda y/o consulta, no dude en comunicarse conmigo al telefono: 15-XXXX-XXXX.
Desde ya, muchas gracias por elegirnos.</div>", dto.Nombre, productos.Select(x => "<div><b>" + x.Nombre + "</b></div><div style='padding-left:2em'>" + x.PrecioVisible + "</div>").Aggregate((current, next) => current + next), productos.Sum(x => x.PrecioPromocional.HasValue ? x.PrecioPromocional : x.Precio));

            EnviarMAil(fromAddress, fromPassword, to, subject, body);
        }

        private static void EnviarMAil(MailAddress fromAddress, string fromPassword, MailAddress to, string subject, string body)
        {
            var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
            using (var message = new MailMessage(fromAddress, to)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
            {
                smtp.Send(message);
            }
        }
    }
}
