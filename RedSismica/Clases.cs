using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSismica
{
    class Sismografo
    {
        // Propiedades 
        public string FechaAdquisicion { get; set; }
        public int IndentificadorSismografo { get; set; }
        public int NroSerie { get; set; }

        public EstacionSismologica EstacionSismologica { get; set; } // Relación con EstacionSismologica

        // Constructor
        public Sismografo(string fechaAdquisicion, int indentificadorSismografo, int nroSerie, EstacionSismologica estacionSismologica )

        {
            FechaAdquisicion = fechaAdquisicion;
            IndentificadorSismografo = indentificadorSismografo;
            NroSerie = nroSerie;
            EstacionSismologica = estacionSismologica;
        }
    }




    class EstacionSismologica
    {
        // Propiedades 
        public string CodigoEstacion { get; set; }
        public string DocumentoCertificacionAdquisicion { get; set; }
        public string FechaSolicitudCertificacion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Nombre { get; set; }
        public string NroCertificacionAdquisicion { get; set; }

        // Constructor
        public EstacionSismologica(string codigoEstacion, string documentoCertificacionAdquisicion,
                                   string fechaSolicitudCertificacion, double latitud, double longitud,
                                   string nombre, string nroCertificacionAdquisicion)
        {
            CodigoEstacion = codigoEstacion;
            DocumentoCertificacionAdquisicion = documentoCertificacionAdquisicion;
            FechaSolicitudCertificacion = fechaSolicitudCertificacion;
            Latitud = latitud;
            Longitud = longitud;
            Nombre = nombre;
            NroCertificacionAdquisicion = nroCertificacionAdquisicion;
        }
    }

    class CambioEstado
    {
        // Propiedades 
        public string FechaHoraInicio { get; set; }
        public string FechaHoraFin { get; set; }
        public MotivoFueraServicio MotivoFueraServicio { get; set; } // Relación con MotivoFueraServicio
        public Estado Estado { get; set; } // Relación con Estado   
        // Constructor
        public CambioEstado(string fechaHoraInicio, string fechaHoraFin, 
                            MotivoFueraServicio motivoFueraServicio, Estado estado)
        {
            FechaHoraInicio = fechaHoraInicio;
            FechaHoraFin = fechaHoraFin;
            MotivoFueraServicio = motivoFueraServicio;
            Estado = estado;
        }


    }


    class MotivoFueraServicio
    {   // Propiedades 
        public string Comentario { get; set; }
        public MotivoTipo MotivoTipo { get; set; } // Relacion con MotivoTipo
        // Constructor
        public MotivoFueraServicio(string comentario, MotivoTipo motivoTipo)
        {
            Comentario = comentario;
            MotivoTipo = motivoTipo;
        }
    }

    class MotivoTipo
    {
        // Propiedades 
        public string Descripcion { get; set; }
        // Constructor
        public MotivoTipo(string descripcion)
        {
            Descripcion = descripcion;
        }
    }


    class Estado
    {
        // Propiedades 
        public string Ambito { get; set; }
        public string NombreEstado { get; set; }
        // Constructor
        public Estado(string ambito, string nombreEstado)
        {
            Ambito = ambito;
            NombreEstado = nombreEstado;
        }
    }


    class OrdenDeInspeccion
    {
        // Propiedades 
        public string FechaHoraCierre { get; set; }
        public string FechaHoraFinalizacion { get; set; }
        public string FechaHoraInicio { get; set; }
        public int NumeroOrden { get; set; }
        public string ObservacionCierre { get; set; }
        public EstacionSismologica EstacionSismologica { get; set; } // Relación con EstacionSismologica
        // Constructor
        public OrdenDeInspeccion(string fechaHoraCierre, string fechaHoraFinalizacion , 
                                  string fechaHoraInicio, int numeroOrden, string observacionCierre, 
                                  EstacionSismologica estacionSismologica)
        {
            FechaHoraCierre = fechaHoraCierre;
            FechaHoraFinalizacion = fechaHoraFinalizacion;
            FechaHoraInicio = fechaHoraInicio;
            NumeroOrden = numeroOrden;
            ObservacionCierre = observacionCierre;
            EstacionSismologica = estacionSismologica;
        }
    }



    class Empleado
    {
        // Propiedades 
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public Rol Rol { get; set; } // Relación con Rol
        // Constructor
        public Empleado(string apellido, string mail, string nombre, string telefono, Rol rol)
        {
            Apellido = apellido;
            Mail = mail;
            Nombre = nombre;
            Telefono = telefono;
            Rol = rol;
        }
    }

    class Rol
    {
        // Propiedades 
        public string DescripcionRol { get; set; }
        public string Nombre { get; set; }
        // Constructor
        public Rol(string descripcionRol, string nombre)
        {
            DescripcionRol = descripcionRol;
            Nombre = nombre;
        }
    }



    class Usuario
    {         
        // Propiedades 
        public string Contraseña { get; set; }
        public string NombreUsuario { get; set; }
        public Empleado Empleado { get; set; } // Relación con Empleado 
        // Constructor
        public Usuario(string contraseña, string nombreUsuario, Empleado empleado)
        {
            Contraseña = contraseña;
            NombreUsuario = nombreUsuario;
            Empleado = empleado;
        }
    }


    class Sesion
    {
        public string FechaHoraInicio { get; set; }
        public string FechaHoraFin { get; set; }
        public Usuario Usuario { get; set; }

        public Sesion(string fechaHoraInicio, string fechaHoraFin, Usuario usuario)
        {
            FechaHoraInicio = fechaHoraInicio;
            FechaHoraFin = fechaHoraFin;
            Usuario = usuario;
        }
    }
}

