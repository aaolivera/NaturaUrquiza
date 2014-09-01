using Dominio;
using Dominio.Comandos;
using Repositorio;
using Servicios.Conversiones;

namespace Servicios.Procesamiento
{
    public abstract class ProcesadorCrear<TComando, TEntidad> : ProcesadorComando<TComando> where TComando : Comando where TEntidad : class, IIdentificable
    {
        protected ProcesadorCrear(IRepositorio repositorio, IConversor conversor) : base(repositorio, conversor)
        {
        }

        public override Resultado Ejecutar(TComando comando)
        {
            var resultado = new ResultadoCrear();
            Validar(comando, resultado);
            if (!resultado.HayErrores)
            {
                var entidad = CrearEntidad(comando);
                Repositorio.Agregar(entidad);
                Repositorio.GuardarCambios();
                resultado.Id = entidad.Id; //(int)entidad.GetType().GetProperty("Id").GetValue(entidad, null);
            }

            return resultado;
        }

        protected abstract TEntidad CrearEntidad(TComando comando);

        protected abstract void Validar(TComando comando, Resultado resultado);
    }
}
