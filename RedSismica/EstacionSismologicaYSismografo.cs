using System;

public class EstacionSismologica
{
    public string CodigoEstacion { get; set; }
    public string DocumentoCertificacionAdquisicion { get; set; }
    public DateTime FechaSolicitudCertificacion { get; set; }
    public string Nombre { get; set; }
    public string NroCertificacionAdquisicion { get; set; }
    public double Latitud { get; set; }
    public double Longitud { get; set; }

    public Sismografo Sismografo { get; set; }

    public void PonerSismografoFueraDeServicio(List<MotivoFueraServicio> motivos, DateTime fechaHora)
    {
        Sismografo?.SismografoFueraDeServicio(motivos, fechaHora);
    }

    public string BuscarIdSismografo() => Sismografo?.IdentificadorSismografo;
}

public class Sismografo
{
    public DateTime FechaAdquisicion { get; set; }
    public string IdentificadorSismografo { get; set; }
    public string NroSerie { get; set; }
    public bool FueraDeServicio { get; set; }

    public void SismografoFueraDeServicio(List<MotivoFueraServicio> motivos, DateTime fechaHora)
    {
        FueraDeServicio = true;
        // Aquí podrías guardar motivos y fecha si necesitás
    }

    public string GetIdSismografo() => IdentificadorSismografo;
}

