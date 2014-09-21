using System.ComponentModel.DataAnnotations;
using Dominio.Enum;
using Dominio.Recursos;

namespace Dominio.Dto
{
    public sealed class ProductoDto
    {
        public int Id { get; set; }
        //[Display(ResourceType = typeof(Textos), Name = "Usuario_NombreUsuario")]
        //[Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        //[StringLength(40, ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_ExcedeLargoMaximo")]

        [Display(ResourceType = typeof(Textos), Name = "Nombre")]
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        public string Nombre { get; set; }

        [Display(ResourceType = typeof(Textos), Name = "Descripcion")]
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        public string Descripcion { get; set; }

        [Display(ResourceType = typeof(Textos), Name = "DescripcionCorta")]
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        public string DescripcionCorta { get; set; }

        [Display(ResourceType = typeof(Textos), Name = "Precio")]
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        [RegularExpression(@"^[0-9]*(?:\,[0-9]*)?$", ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_SoloNumerico")]
        public decimal Precio { get; set; }

        [Display(ResourceType = typeof(Textos), Name = "PrecioPromocional")]
        [RegularExpression(@"^[0-9]*(?:\,[0-9]*)?$", ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_SoloNumerico")]
        public decimal? PrecioPromocional { get; set; }

        [Display(ResourceType = typeof(Textos), Name = "Puntos")]
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        public int Puntos { get; set; }

        [Display(ResourceType = typeof(Textos), Name = "Categoria")]
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        public Categoria Categoria { get; set; }

        [Display(ResourceType = typeof(Textos), Name = "Foto")]
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        public string FotoPath { get; set; }
    }
}
