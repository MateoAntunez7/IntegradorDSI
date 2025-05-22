// Estado.cs
namespace RedSismica

public class Estado
{
    public string NombreEstado { get; set; }

    // Constructor
    public Estado(string nombreEstado)
    {
        NombreEstado = nombreEstado;
    }

    // Métodos para facilitar la verificación del estado
    // Estos métodos se basan en los nombres de estado que esperamos usar.
    // El diagrama de clases menciona: esCompletamenteRealizada(), esEstadoCerrado(), buscarEstadoCerrado()
    // Aquí simplificamos con chequeos directos del NombreEstado.

    public bool EsEstadoCerrado()
    {
        return NombreEstado == "Cerrada"; // Para OrdenDeInspeccion
    }

    public bool EsCompletamenteRealizada()
    {
        // Este estado es crucial para que una orden pueda ser cerrada según el CU
        return NombreEstado == "Completamente Realizada"; // Para OrdenDeInspeccion
    }

    public bool EsOperativo()
    {
        return NombreEstado == "Operativo"; // Para Sismografo
    }

    public bool EsFueraDeServicio()
    {
        // Este es el estado al que se cambiará el sismógrafo en el CU
        return NombreEstado == "Fuera de Servicio"; // Para Sismografo
    }

    public override string ToString()
    {
        return NombreEstado;
    }
}