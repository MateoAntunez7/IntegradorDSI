// MotivoFueraServicio.cs
namespace RedSismica
{
    public class MotivoFueraServicio
    {
        public MotivoTipo Tipo { get; private set; }     // Asociación con MotivoTipo del diagrama
        public string Comentario { get; private set; } // Atributo 'comentario' del diagrama

        // Constructor (equivalente al new() del diagrama)
        public MotivoFueraServicio(MotivoTipo tipo, string comentario)
        {
            Tipo = tipo;
            Comentario = comentario;
        }

        public override string ToString()
        {
            return $"Tipo: {Tipo.Descripcion}, Comentario: {Comentario}";
        }
    }
}