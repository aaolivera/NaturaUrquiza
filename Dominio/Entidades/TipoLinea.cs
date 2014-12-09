using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    public class TipoLinea : IIdentificable
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string Nombre { get; set; }
        [InverseProperty("TipoLinea")]
        public virtual ICollection<Linea> Lineas { get; set; }
    }
}
