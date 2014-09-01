using System.Data.Entity;
using Ninject.Modules;
using Ninject.Web.Common;
using Repositorio;
using Servicios;
using Servicios.Conversiones;
using Servicios.Conversiones.Impl;
using Servicios.Impl;

namespace Dependencias
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            /*
             * Para la comunicación con la capa de servicios se pueden usar dos formas:
             *  - Directa: referencia directa a las dlls de servicio
             *  - WCF:  se accede a la capa de servicios a traves de servicios WCF
             * Para cambiar de metodo ce comunicacion, basta con intercambiar las configuraciones de aca abajo.
            */

            // Comunicacion Directa 
            Bind<DbContext>().To<DbContexto>().InRequestScope();
            Bind<IRepositorio>().To<RepositorioEF>().InRequestScope();

            Bind<IConversor>().To<ConversorAutoMapper>().InSingletonScope();
            Bind<IServicioRepositorio>().To<ServicioRepositorio>().InRequestScope();
            Bind<IServicioComandos>().To<ServicioComandos>().InSingletonScope();
            // Fin comunicacion directa

        }
    }
}
