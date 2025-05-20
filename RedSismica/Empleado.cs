using System;
namespace RedSismica

public class Empleado
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Mail { get; set; }
    public string Telefono { get; set; }
    public Rol Rol { get; set; }

    public bool EsResponsableDeReparacion()
    {
        return Rol?.EsResponsableDeReparacion() ?? false;
    }

    public string GetMail()
    {
        return Mail;
    }
}

