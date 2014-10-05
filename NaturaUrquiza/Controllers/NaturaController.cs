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
            var subject = "Compra " + dto.Nombre + " " + DateTime.Now.Date;
            var body = String.Format(
@"Nombre:   {0}
Mail:   {1}
Telefono:   {2}
Productos: 
    {3}", dto.Nombre, dto.Mail, dto.Telefono, productos.Select(x => x.Nombre + "      " + x.PrecioVisible).Aggregate((current, next) => current + @"
    " + next));

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
            var subject = "Compra " + dto.Nombre + " " + DateTime.Now.Date;
            var body = String.Format(
@"Nombre:   {0}
Mail:   {1}
Telefono:   {2}
Productos: 
    {3}", dto.Nombre, dto.Mail, dto.Telefono, productos.Select(x => x.Nombre + "      " + x.PrecioVisible).Aggregate((current, next) => current + @"
    " + next));

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
                    Body = body
                })
            {
                smtp.Send(message);
            }
        }
    }
}
