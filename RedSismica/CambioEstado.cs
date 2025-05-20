using System;
namespace RedSismica

public class CambioEstado
{
    public DateTime FechaHoraInicio { get; set; }
    public DateTime? FechaHoraFin { get; set; }
    public MotivoFueraServicio Motivo { get; set; }

    public bool EsActual()
    {
        return !FechaHoraFin.HasValue;
    }

    public void SetMotivo(MotivoFueraServicio motivo)
    {
        Motivo = motivo;
    }

    public void SetFechaHora(DateTime fecha)
    {
        FechaHoraInicio = fecha;
    }

    public void Finalizar()
    {
        FechaHoraFin = DateTime.Now;
    }

    public static CambioEstado Crear(MotivoFueraServicio motivo)
    {
        return new CambioEstado
        {
            Motivo = motivo,
            FechaHoraInicio = DateTime.Now
        };
    }
}

