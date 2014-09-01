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
    public class ServiciosWebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<DbContexto>().InRequestScope();
            Bind<IRepositorio>().To<RepositorioEF>().InRequestScope();

            Bind<IConversor>().To<ConversorAutoMapper>().InSingletonScope();

            Bind<IServicioRepositorio, ServicioRepositorio>().To<ServicioRepositorio>().InRequestScope();
            Bind<IServicioComandos, ServicioComandos>().To<ServicioComandos>().InSingletonScope();

        }
    }
}
