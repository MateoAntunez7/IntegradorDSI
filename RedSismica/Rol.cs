using System;

namespace RedSismica

public class Rol
{
    public string Nombre { get; set; }
    public string DescripcionRol { get; set; }

    public bool EsResponsableDeReparacion()
    {
        return Nombre == "Responsable de Reparación";
    }
}
