// MotivoTipo.cs
namespace RedSismica
{
    public class MotivoTipo
    {
        public string Descripcion { get; set; } // Atributo del diagrama de clases

        // Constructor
        public MotivoTipo(string descripcion)
        {
            Descripcion = descripcion;
        }

        // El diagrama menciona buscarMotivoTipo(), que podría ser un método estático 
        // en una clase gestora o aquí si se pasa una lista. Por ahora, no lo implementamos
        // directamente aquí para mantener la clase simple.

        public override string ToString()
        {
            return Descripcion;
        }
    }
}