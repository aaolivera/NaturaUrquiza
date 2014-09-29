using System.ComponentModel.DataAnnotations;
using Dominio.Enum;
using Dominio.Recursos;

namespace Dominio.Dto
{
    public sealed class TipoDto
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Textos), Name = "Nombre")]
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        public string Nombre { get; set; }
    }
}
