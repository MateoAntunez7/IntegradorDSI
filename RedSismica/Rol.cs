// Rol.cs
namespace RedSismica

public class Rol
{
    public string Nombre { get; private set; }
    public string DescripcionRol { get; private set; } // Atributo del diagrama de clases

    // Constructor
    public Rol(string nombre, string descripcion)
    {
        Nombre = nombre;
        DescripcionRol = descripcion;
    }

    // Método del diagrama de clases: esResponsableDeReparacion()
    // Este método es importante para identificar a quién notificar
    public bool EsResponsableDeReparacion()
    {
        // Asumimos un nombre de rol específico para esta función.
        return Nombre == "ResponsableDeReparacion";
    }

    public bool EsResponsableDeInspecciones()
    {
        // El Actor Principal del CU es "Responsable de Inspecciones"
        return Nombre == "ResponsableDeInspecciones";
    }


    public override string ToString()
    {
        return Nombre;
    }
}