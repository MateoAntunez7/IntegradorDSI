using System;

namespace RedSismica

public class EstacionSismologica
{
    public string CodigoEstacion { get; set; }
    public string DocumentoCertificacionAdquisicion { get; set; }
    public DateTime FechaSolicitudCertificacion { get; set; }
    public double Latitud { get; set; }
    public double Longitud { get; set; }
    public string NombreEstacion { get; set; }
    public string NroCertificacionAdquisicion { get; set; }
    public Sismografo Sismografo { get; set; }

    public void PonerSismografoFueraDeServicio(List<MotivoFueraServicio> motivos, DateTime fecha)
    {
        Sismografo?.SismografoFueraDeServicio();
        // Acá podrías guardar los motivos si fuera necesario
    }

    public string GetNombre()
    {
        return NombreEstacion;
    }

    public string BuscarIdSismografo()
    {
        return Sismografo?.IdentificadorSismografo;
    }
}

