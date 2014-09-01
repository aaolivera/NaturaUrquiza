using System.Data.Entity;
using Dominio.Consultas;

namespace Repositorio
{
    public interface IConsultaPaginada<TEntidad>
    {
        ListaPaginada<TEntidad> Ejecutar(DbContext contexto);
    }
}
