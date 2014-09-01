using Dominio.Comandos;
using Repositorio;
using Servicios.Conversiones;

namespace Servicios.Procesamiento
{
    public abstract class ProcesadorModificar<TComando> : ProcesadorComando<TComando>
        where TComando : Comando
    {
        protected ProcesadorModificar(IRepositorio repositorio, IConversor conversor)
            : base(repositorio, conversor)
        {
        }

        public override Resultado Ejecutar(TComando comando)
        {
            var resultado = new Resultado();
            Validar(comando, resultado);
            if (!resultado.HayErrores)
            {
                ModificarEntidad(comando);
                Repositorio.GuardarCambios();
            }

            return resultado;
        }

        protected abstract void ModificarEntidad(TComando comando);

        protected abstract void Validar(TComando comando, Resultado resultado);
    }

}
