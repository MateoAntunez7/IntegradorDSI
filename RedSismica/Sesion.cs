using System;

namespace RedSismica

public class Sesion
{
    public DateTime FechaHoraInicio { get; set; }
    public DateTime FechaHoraFin { get; set; }
    public Usuario Usuario { get; set; }

    public Usuario GetUsuario() => Usuario;
}
