// Sesion.cs
using System;

namespace RedSismica
{
    public class Sesion
    {
        public Usuario UsuarioLogueado { get; private set; } // Asociación con Usuario del diagrama
        public DateTime FechaHoraInicio { get; private set; }  // Atributo del diagrama de clases
        public DateTime? FechaHoraFin { get; private set; }   // Atributo del diagrama de clases

        // Constructor
        public Sesion(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo para iniciar una sesión.");
            }
            UsuarioLogueado = usuario;
            FechaHoraInicio = DateTime.Now;
            FechaHoraFin = null; // Sesión activa
        }

        // Método getUsuario() del diagrama de clases [cite: 1]
        public Usuario GetUsuario()
        {
            return UsuarioLogueado;
        }

        public void CerrarSesion()
        {
            FechaHoraFin = DateTime.Now;
            // Opcionalmente, podrías desasignar UsuarioLogueado aquí si la lógica de tu app lo requiere.
            // UsuarioLogueado = null; 
            Console.WriteLine($"Sesión cerrada para {UsuarioLogueado.NombreUsuario} a las {FechaHoraFin}");
        }

        public bool EstaActiva()
        {
            return FechaHoraFin == null;
        }
    }
}