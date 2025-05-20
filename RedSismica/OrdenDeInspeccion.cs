using System;

namespace RedSismica

public class OrdenDeInspeccion
{
    public DateTime FechaHoraCierre { get; set; }
    public DateTime FechaHoraFinalizacion { get; set; }
    public DateTime FechaHoraInicio { get; set; }
    public int NumeroOrden { get; set; }
    public string ObservacionCierre { get; set; }
    public Estado Estado { get; set; }
    public EstacionSismologica Estacion { get; set; }

    public bool EsDeEmpleado(Empleado empleado)
    {
        return true; // O una verificación si tenés listas asignadas
    }

    public bool EsCompletamenteRealizada()
    {
        return Estado != null && Estado.EsCompletamenteRealizada();
    }

    public string GetInfoOrdenInspeccion()
    {
        return $"Orden #{NumeroOrden} - Finaliza: {FechaHoraFinalizacion.ToShortDateString()} - " +
               $"Estación: {Estacion?.NombreEstacion} - Sismógrafo: {Estacion?.BuscarIdSismografo()}";
    }

    public void RegistrarCierreOrden(string observacion, DateTime fechaHora)
    {
        ObservacionCierre = observacion;
        FechaHoraCierre = fechaHora;
        Estado = new Estado { NombreEstado = "Cerrada", Ambito = "Inspección" };
    }

    public void PonerSismografoFueraDeServicio(List<MotivoFueraServicio> motivos, DateTime fechaHora)
    {
        Estacion?.PonerSismografoFueraDeServicio(motivos, fechaHora);
    }
}


