using System.Runtime.Serialization;

namespace Dominio.Comandos
{
    [DataContract]
    public class ResultadoCrear : Resultado
    {
        [DataMember]
        public int Id { get; set; }
    }
}
