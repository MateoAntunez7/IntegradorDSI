using System;

namespace RedSismica

public class Sismografo
{
    public DateTime FechaAdquisicion { get; set; }
    public string IdentificadorSismografo { get; set; }
    public string NroSerie { get; set; }
    public bool FueraDeServicio { get; set; } = false;

    public string GetIdSismografo()
    {
        return IdentificadorSismografo;
    }

    public void SismografoFueraDeServicio()
    {
        FueraDeServicio = true;
    }
}