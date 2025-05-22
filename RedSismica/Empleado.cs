// Empleado.cs
using System.Collections.Generic;
using System.Linq; // Necesario para .Any()

namespace RedSismica
{
    public class Empleado
    {
        public string Apellido { get; private set; } // Atributo del diagrama de clases
        public string Mail { get; private set; }     // Atributo del diagrama de clases
        public string Nombre { get; private set; }   // Atributo del diagrama de clases
        public string Telefono { get; set; } // Atributo del diagrama de clases (puede ser opcional)

        public List<Rol> Roles { get; private set; }

        // Constructor
        public Empleado(string nombre, string apellido, string mail)
        {
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            Roles = new List<Rol>();
        }

        public void AgregarRol(Rol rol)
        {
            if (rol != null && !Roles.Contains(rol))
            {
                Roles.Add(rol);
            }
        }

        // Método getEmpleado() del diagrama (aunque aquí tiene más sentido como propiedad o método simple)
        public string GetNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }

        public string GetEmail() // Equivalente a getEmail() en Usuario/Empleado del diagrama
        {
            return Mail;
        }

        // Método sosResponsableDeReparacion() del diagrama de clases [cite: 1]
        // Necesario para saber a quién notificar según el CU
        public bool SosResponsableDeReparacion()
        {
            return Roles.Any(r => r.EsResponsableDeReparacion());
        }

        // Para identificar al Actor Principal del CU
        public bool EsResponsableDeInspecciones()
        {
            return Roles.Any(r => r.EsResponsableDeInspecciones());
        }

        public override string ToString()
        {
            return GetNombreCompleto();
        }
    }
}