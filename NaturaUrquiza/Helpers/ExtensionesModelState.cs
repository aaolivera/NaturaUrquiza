using System.Web.Mvc;
using Dominio.Comandos;

namespace NaturaUrquiza.Helpers
{
    public static class ExtensionesModelState
    {
        public static void AgregarErrores(this ModelStateDictionary estado, Resultado resultado)
        {
            foreach (var error in resultado.Errores)
            {
                estado.AddModelError(error.Key, error.Value);
            }
        }
    }
}