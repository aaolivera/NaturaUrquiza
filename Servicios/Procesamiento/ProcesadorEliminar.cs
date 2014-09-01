using System;
using Dominio.Comandos;
using Dominio.Recursos;
using Repositorio;
using Servicios.Conversiones;

namespace Servicios.Procesamiento
{
    public abstract class ProcesadorEliminar<TComando, TEntidad> : ProcesadorComando<TComando> where TComando : Comando where TEntidad : class
    {
        protected ProcesadorEliminar(IRepositorio repositorio, IConversor conversor)
            : base(repositorio, conversor)
        {
        }

        public sealed override Resultado Ejecutar(TComando comando)
        {
            var resultado = new Resultado();
            Validar(comando, resultado);
            if (!resultado.HayErrores)
            {
                Repositorio.Remover<TEntidad>(IdEntidad(comando));
                try
                {
                    Repositorio.GuardarCambios();
                }
                catch (EntidadReferenciadaException)
                {
                    resultado.Error("", Textos.Error_EliminarReferenciado);
                }
                catch (Exception e)
                {
                    resultado.Error("", Textos.Error_ActualizarGenerico);
                }
            }

            return resultado;
        }

        protected abstract int IdEntidad(TComando comando);

        protected abstract void Validar(TComando comando, Resultado resultado);
    }
}
