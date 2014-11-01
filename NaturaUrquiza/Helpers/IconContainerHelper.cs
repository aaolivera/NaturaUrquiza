using System.IO;
using System.Web;
using System.Web.Mvc;
using Dominio.Consultas;
using Dominio.Dto;

namespace NaturaUrquiza.Helpers
{
    public static class IconContainerHelper
    {
        public static MvcHtmlString IconContainer(this HtmlHelper html, ListaPaginada<ProductoDto> items)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var celdas = 0;
            var htmlstring = "<div class='content-wrapper' id='icongrid'>";
            foreach (var entidad in items)
            {
                if (celdas == 0)
                {
                    htmlstring += "<div class='row'>";
                }

                htmlstring += @"<div class='col-lg-4'>
                                  <p class='text-center'><a class='ajax-ver-imagen-link' href='" + Path.Combine("images/profile", entidad.FotoPath ?? "") + "'><img class='img-circle' src='" + Path.Combine("images/profile", entidad.FotoPath ?? "") + @"' alt='Imagen genérica para marcar el lugar' style='width: 140px; height: 140px;'></a></p>
                                  <h3><p class='text-center'><os-p>" + entidad.Nombre + @"</os-p></h3></p>"
                                  + (entidad.PrecioPromocional.HasValue ? "<h6 class='text-center'><s><os-p>$" + entidad.Precio + @"</os-p></s></h6>" + "<p class='text-center'><os-p>$" + entidad.PrecioPromocional + @"</os-p></p>" : "<p class='text-center'><os-p>$" + entidad.Precio + @"</os-p></p>") + @"</p>
                                  <p class='text-center'><a class='btn btn-default btn-xs ajax-ver-link' href='" + urlHelper.Action("Ver", "Natura", new { id = entidad.Id }) + "' role='button'><os-p>detalles</os-p></a><a class='btn btn-default btn-xs' style='padding-bottom: 1px;' data-bind=\"click: agregarProducto.bind($data, '" + entidad.Id + "', '" + entidad.Nombre + "', '" + entidad.Precio + "', '" + entidad.PrecioPromocional + "', '" + Path.Combine("images/profile/", entidad.FotoPath ?? "") + "', '" + entidad.Codigo + "')\" role='button' href='#'><i class='glyphicon glyphicon-shopping-cart'></i><span class='jewelCount hide' id='" + entidad.Id + "-jewel'></p></a></div>";

                if (celdas == 2)
                {
                    htmlstring += "</div><br/>";
                    celdas = 0;
                }
                else
                {
                    celdas++;   
                }
            }
            htmlstring += "</div>";

            if (items.CantidadDePaginas > 0)
            {
                htmlstring += "<div class='content-wrapper text-center' id='icongridFoot'>";
                htmlstring += "<ul class='pagination'><li><a href='" + urlHelper.Action("Index", "Natura", new { pagina = (1) }) + "'>&laquo;</a></li>";
                for (var i = 0; i <= items.CantidadDePaginas; ++i)
                {
                    htmlstring += "<li><a href='"+ urlHelper.Action("Index","Natura",new { pagina = (i + 1)})+"'>" + (i + 1) + "</a></li>";
                }
                htmlstring += "<li><a href='" + urlHelper.Action("Index", "Natura", new { pagina = (items.CantidadDePaginas + 1)}) + "'>&raquo;</a></li></ul>";
                htmlstring += "</div>";
            }
            return new MvcHtmlString(htmlstring);
        }
    }
}
