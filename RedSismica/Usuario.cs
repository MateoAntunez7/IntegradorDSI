using System;
namespace RedSismica

public class Usuario
{
    public string NombreUsuario { get; set; }
    public string Contrasenia { get; set; }
    public Empleado Empleado { get; set; }

    public Empleado GetEmpleado()
    {
        return Empleado;
    }

    public bool EsResponsableDeReparacion()
    {
        return Empleado.EsResponsableDeReparacion();
    }

    public string GetMail()
    {
        return Empleado.GetMail();
    }
}

