using Dominio.Comandos;

namespace Servicios.Procesamiento
{
    public interface IProcesadorComando
    {
        Resultado Ejecutar(Comando comando);
    }

    public interface IProcesadorComando<in TComando> : IProcesadorComando
    {
        Resultado Ejecutar(TComando comando);
    }
    
}
