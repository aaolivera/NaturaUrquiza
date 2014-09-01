using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dominio.Recursos;

namespace Dominio.Dto
{
    public sealed class UsuarioDto
    {
        public int Id { get; set; }
        //[Display(ResourceType = typeof(Textos), Name = "Usuario_NombreUsuario")]
        //[Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        //[StringLength(40, ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_ExcedeLargoMaximo")]
        public string NombreUsuario { get; set; }
        //[Display(ResourceType = typeof(Textos), Name = "Chofer_Apellido")]
        //[Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        //[StringLength(40, ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_ExcedeLargoMaximo")]
        public string Apellido { get; set; }
        //[Display(ResourceType = typeof(Textos), Name = "Chofer_Nombre")]
        //[Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_Requerido")]
        //[StringLength(40, ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_ExcedeLargoMaximo")]
        public string Nombre { get; set; }
        //[StringLength(40, ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "Error_ExcedeLargoMaximo")]
        public string Email { get; set; }
    }
}
