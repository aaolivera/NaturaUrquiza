using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class Usuario : IIdentificable
    {
        [Key]
        public virtual int Id { get; set; }
        [Required]
        public virtual string NombreUsuario { get; set; }
        [Required]
        public virtual string Apellido { get; set; }
        [Required]
        public virtual string Nombre { get; set; }
        public virtual string Email { get; set; }
    }
}
