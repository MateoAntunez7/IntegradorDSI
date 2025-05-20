using System;

namespace RedSismica

public class Estado
{
    public string Ambito { get; set; }
    public string NombreEstado { get; set; }

    public bool EsCompletamenteRealizada()
    {
        return NombreEstado == "Completamente Realizada";
    }

    public bool EsEstadoCerrado()
    {
        return NombreEstado == "Cerrado";
    }
}
