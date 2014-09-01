using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Comandos;
using Ninject;
using Servicios.Procesamiento;

namespace Servicios.Impl
{
    public class ServicioComandos : IServicioComandos
    {
        private readonly IKernel kernel;
        private IDictionary<Type, Type> procesadores;

        public ServicioComandos(IKernel kernel)
        {
            this.kernel = kernel;
            RegistrarProcesadores();
        }

        public Resultado Ejecutar(Comando comando)
        {
            var tipoProcesador = procesadores[comando.GetType()];
            var procesador = (IProcesadorComando)kernel.Get(tipoProcesador);
            return procesador.Ejecutar(comando);
            //return new Resultado();
        }

        private void RegistrarProcesadores()
        {
            procesadores = new Dictionary<Type, Type>();

            var comandos = Comando.TiposDeComandos().Where(t => !t.IsAbstract);

            foreach (var comando in comandos)
            {
                var procesador = ObtenerProcesador(comando);
                procesadores.Add(comando, procesador);
            }
        }

        private Type ObtenerProcesador(Type comando)
        {
            try
            {
                return typeof(IProcesadorComando<>)
                    .Assembly
                    .GetExportedTypes()
                    .Single(
                        x => !x.IsAbstract && x.GetInterfaces().Any(i => i.IsGenericType
                                                        && i.GetGenericTypeDefinition() == typeof(IProcesadorComando<>)
                                                        && i.GetGenericArguments().Single() == comando));
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }
        
    }
}
