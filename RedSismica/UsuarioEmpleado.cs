using System;

public class Usuario
{
    public string NombreUsuario { get; set; }
    public string Contrasenia { get; set; }
    public Empleado Empleado { get; set; }

    public Empleado GetEmpleado() => Empleado;

    public string GetMail() => Empleado?.Mail;

    public bool EsResponsableDeReparacion()
    {
        return Empleado?.Rol?.EsResponsableDeReparacion() ?? false;
    }
}

public class Empleado
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Mail { get; set; }
    public string Telefono { get; set; }
    public Rol Rol { get; set; }

    public List<OrdenDeInspeccion> OrdenesAsignadas { get; set; } = new();
}

