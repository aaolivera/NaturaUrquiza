using Dominio.Comandos;
using Repositorio;
using Servicios.Conversiones;

namespace Servicios.Procesamiento
{
    public abstract class ProcesadorComando<TComando> : IProcesadorComando<TComando> where TComando : Comando
    {
        protected IRepositorio Repositorio { get; private set; }
        protected IConversor Conversor { get; private set; }

        protected ProcesadorComando(IRepositorio repositorio, IConversor conversor)
        {
            Repositorio = repositorio;
            Conversor = conversor;
        }

        public Resultado Ejecutar(Comando comando)
        {
            return Ejecutar((TComando) comando);
        }

        public abstract Resultado Ejecutar(TComando comando);

    }
}
