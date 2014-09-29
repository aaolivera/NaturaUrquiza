using System.ComponentModel.DataAnnotations;
using Dominio.Recursos;

namespace Dominio.Dto
{
    public sealed class CompraDto
    {
        [Display(ResourceType = typeof(Textos), Name = "Mail")]
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        public string Mail { get; set; }

        [Display(ResourceType = typeof(Textos), Name = "Telefono")]
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        public string Telefono { get; set; }

        public string Productos { get; set; }
    }
}
