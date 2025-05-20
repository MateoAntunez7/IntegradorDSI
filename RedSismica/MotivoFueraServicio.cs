using System;

namespace RedSismica

public class MotivoFueraServicio
{
    public string Comentario { get; set; }
    public MotivoTipo Tipo { get; set; }

    public MotivoFueraServicio(MotivoTipo tipo, string comentario)
    {
        Tipo = tipo;
        Comentario = comentario;
    }
}

