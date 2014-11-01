using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Dominio.Entidades;

namespace Repositorio
{
 
    public class DbContexto : DbContext
    {
        public DbContexto():base("NaturaUrquizaDb")
        {
            // Eliminar siempre la base de datos
            //Database.SetInitializer(new DropCreateDatabaseAlways<DbContexto>());

            // Crear la base de datos si no existe.
            //Database.SetInitializer(new CreateDatabaseIfNotExists<DbContexto>());

            // Eliminar y Crear nuevamente la base de datos al 
            // detectar cambios en el modelo. 
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DbContexto>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // No usamos nombres de tablas en plural. Igualmente la pluralización fucniona solo con nombres en ingles.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();   

            //Se mapean todas las entidades bajo el namespace Dominio.Entidades      
            MapearAssemblyDe<Usuario>(modelBuilder, x => x.Namespace == typeof(Usuario).Namespace, excluir: null);
        }

        private void MapearAssemblyDe<TEntidad>(DbModelBuilder modelBuilder, Predicate<Type> incluir, Predicate<Type> excluir)
        {
            var tiposEntidades  = typeof (TEntidad).Assembly.GetTypes()
                .Where(x => incluir(x));
            if (excluir != null)
            {
                tiposEntidades = tiposEntidades.Where(x => !excluir(x));
            }

            var metodo = modelBuilder.GetType().GetMethod("Entity");
            foreach (var tipoEntidad in tiposEntidades)
            {
                metodo.MakeGenericMethod(tipoEntidad).Invoke(modelBuilder, null);
            }
        }
    }


}
