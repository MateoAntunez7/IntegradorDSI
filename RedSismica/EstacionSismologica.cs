// EstacionSismologica.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedSismica
{
    public class EstacionSismologica
    {
        public string CodigoEstacion { get; private set; } // Atributo del diagrama [cite: 1]
        public string NombreEstacion { get; private set; } // Atributo del diagrama [cite: 1]
        public string DocumentoCertificacionAdquisicion { get; set; } // Atributo del diagrama [cite: 1]
        public DateTime? FechaSolicitudCertificacion { get; set; }   // Atributo del diagrama [cite: 1]
        public double Latitud { get; set; }                           // Atributo del diagrama [cite: 1]
        public double Longitud { get; set; }                          // Atributo del diagrama [cite: 1]
        public string NroCertificacionAdquisicion { get; set; }       // Atributo del diagrama [cite: 1]

        public List<Sismografo> Sismografos { get; private set; } // Asociación con Sismografo del diagrama [cite: 1]

        public EstacionSismologica(string codigo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El código de la estación no puede estar vacío.", nameof(codigo));

            CodigoEstacion = codigo;
            NombreEstacion = nombre;
            Sismografos = new List<Sismografo>();
        }

        public void AgregarSismografo(Sismografo sismografo)
        {
            if (sismografo != null && !Sismografos.Contains(sismografo))
            {
                sismografo.Estacion = this; // Establece la relación bidireccional
                Sismografos.Add(sismografo);
            }
        }

        // Método getNombre() del diagrama [cite: 1]
        public string GetNombre()
        {
            return NombreEstacion;
        }

        // Método buscarIdSismografo() del diagrama [cite: 1] (interpretado como buscar sismógrafo por ID)
        public Sismografo BuscarSismografoPorId(string idSismografo)
        {
            return Sismografos.FirstOrDefault(s => s.IdentificadorSismografo.Equals(idSismografo, StringComparison.OrdinalIgnoreCase));
        }

        // Método ponerSismografoFueraDeServicio() del diagrama [cite: 1] (en EstacionSismologica)
        // Delega la acción al sismógrafo específico.
        public void PonerSismografoDeEstacionFueraDeServicio(string idSismografo, DateTime fechaHora, List<MotivoFueraServicio> motivos, Empleado responsableCierre, Estado estadoFueraServicio)
        {
            var sismografo = BuscarSismografoPorId(idSismografo);
            if (sismografo != null)
            {
                // Asegurarse que el estado pasado sea realmente "Fuera de Servicio"
                if (estadoFueraServicio != null && estadoFueraServicio.EsFueraDeServicio())
                {
                    sismografo.PonerFueraDeServicio(fechaHora, motivos, responsableCierre, estadoFueraServicio);
                }
                else
                {
                    Console.WriteLine($"Error: El estado proporcionado para el sismógrafo {idSismografo} no es 'Fuera de Servicio'.");
                }
            }
            else
            {
                Console.WriteLine($"Error: No se encontró el sismógrafo con ID {idSismografo} en la estación {NombreEstacion}.");
            }
        }

        public override string ToString()
        {
            return $"{NombreEstacion} ({CodigoEstacion})";
        }
    }
}