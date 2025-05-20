using System;
namespace RedSismica

public class MotivoTipo
{
    public string Descripcion { get; set; }

    public static List<MotivoTipo> BuscarMotivoTipo()
    {
        return new List<MotivoTipo>
        {
            new MotivoTipo { Descripcion = "Fallo técnico" },
            new MotivoTipo { Descripcion = "Clima extremo" },
            new MotivoTipo { Descripcion = "Mantenimiento programado" }
        };
    }
}

