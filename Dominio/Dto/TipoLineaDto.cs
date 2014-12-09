using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dominio.Recursos;

namespace Dominio.Dto
{
    public sealed class TipoLineaDto
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Textos), Name = "Nombre")]
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        public string Nombre { get; set; }

        public ICollection<LineaDto> Lineas { get; set; }
    }
}
