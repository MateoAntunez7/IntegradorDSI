// Usuario.cs
namespace RedSismica
{
    public class Usuario
    {
        public string NombreUsuario { get; private set; } // Atributo del diagrama de clases
        private string Contraseña { get; set; }          // Atributo del diagrama de clases (¡En una app real, usar HASH!)
        public Empleado EmpleadoAsociado { get; private set; } // Asociación con Empleado del diagrama

        // Constructor
        public Usuario(string nombreUsuario, string contraseña, Empleado empleado)
        {
            NombreUsuario = nombreUsuario;
            Contraseña = contraseña; // NUNCA almacenar contraseñas en texto plano en producción.
            EmpleadoAsociado = empleado;
        }

        // Método getEmpleado() del diagrama de clases [cite: 1]
        public Empleado GetEmpleado()
        {
            return EmpleadoAsociado;
        }

        // Método getEmail() del diagrama de clases [cite: 1] (delegado al empleado)
        public string GetEmail()
        {
            return EmpleadoAsociado?.GetEmail();
        }

        // Método sosResponsableDeReparacion() del diagrama de clases [cite: 1] (delegado al empleado)
        public bool SosResponsableDeReparacion()
        {
            return EmpleadoAsociado?.SosResponsableDeReparacion() ?? false;
        }

        // Método para simular la autenticación
        public bool Autenticar(string pass)
        {
            return Contraseña == pass; // Simplificado para el ejemplo
        }

        public override string ToString()
        {
            return NombreUsuario;
        }
    }
}