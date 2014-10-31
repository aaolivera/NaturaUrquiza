using System.ComponentModel.DataAnnotations;
using Dominio.Enum;

namespace Dominio.Entidades
{
    public class Producto : IIdentificable
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string DescripcionCorta { get; set; }
        public virtual decimal Precio { get; set; }
        public virtual decimal? PrecioPromocional { get; set; }
        public virtual int Puntos { get; set; }
        public virtual Tipo Tipo { get; set; }
        public virtual Linea Linea { get; set; }
        public virtual int Prioridad { get; set; }
        public virtual string FotoPath { get; set; }
        public virtual string Codigo { get; set; }
    }
}
