// Sismografo.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedSismica
{
    public class Sismografo
    {
        public string IdentificadorSismografo { get; private set; } // Atributo del diagrama [cite: 1]
        public string NroSerie { get; private set; }                 // Atributo del diagrama [cite: 1]
        public DateTime FechaAdquisicion { get; private set; }       // Atributo del diagrama [cite: 1]

        // Asociación con EstacionSismologica (se establece cuando se añade a una estación)
        public EstacionSismologica Estacion { get; internal set; }

        public Estado EstadoActualSismografo { get; private set; }
        public List<CambioEstado> HistorialCambiosEstado { get; private set; }

        public Sismografo(string identificador, string nroSerie, DateTime fechaAdquisicion, Estado estadoInicial)
        {
            if (string.IsNullOrWhiteSpace(identificador))
                throw new ArgumentException("El identificador del sismógrafo no puede estar vacío.", nameof(identificador));
            if (estadoInicial == null)
                throw new ArgumentNullException(nameof(estadoInicial), "El estado inicial no puede ser nulo.");

            IdentificadorSismografo = identificador;
            NroSerie = nroSerie;
            FechaAdquisicion = fechaAdquisicion;
            HistorialCambiosEstado = new List<CambioEstado>();

            // Establecer el estado inicial
            EstadoActualSismografo = estadoInicial;
            var cambioInicial = CambioEstado.Crear(FechaAdquisicion, estadoInicial); // O DateTime.Now si es más apropiado
            HistorialCambiosEstado.Add(cambioInicial);
        }

        // Método getIdSismografo() del diagrama [cite: 1]
        public string GetIdSismografo()
        {
            return IdentificadorSismografo;
        }

        // Método ponerSismografoFueraDeServicio() del diagrama [cite: 1] y del CU
        public void PonerFueraDeServicio(DateTime fechaHora, List<MotivoFueraServicio> motivos, Empleado responsableCierre, Estado nuevoEstadoFueraServicio)
        {
            if (nuevoEstadoFueraServicio == null || !nuevoEstadoFueraServicio.EsFueraDeServicio())
            {
                // Podría lanzar una excepción o manejarlo de otra forma si el estado no es el esperado.
                Console.WriteLine("Error: El nuevo estado para poner fuera de servicio no es válido.");
                return;
            }
            if (responsableCierre == null)
            {
                Console.WriteLine("Advertencia: No se especificó un responsable para el cambio de estado a Fuera de Servicio.");
            }


            // Finaliza el estado actual en el historial
            var estadoActualPrevio = HistorialCambiosEstado.FirstOrDefault(ce => ce.EsActual());
            estadoActualPrevio?.Finalizar(fechaHora);

            // Crea el nuevo cambio de estado
            var nuevoCambio = CambioEstado.Crear(fechaHora, nuevoEstadoFueraServicio, responsableCierre);
            nuevoCambio.AgregarMotivos(motivos); // Asocia los motivos específicos

            HistorialCambiosEstado.Add(nuevoCambio);
            EstadoActualSismografo = nuevoEstadoFueraServicio;

            Console.WriteLine($"Sismógrafo {IdentificadorSismografo} puesto '{EstadoActualSismografo.NombreEstado}' el {fechaHora} por {responsableCierre?.GetNombreCompleto() ?? "Sistema"}.");
        }

        public override string ToString()
        {
            return $"{IdentificadorSismografo} (Serie: {NroSerie}) - {EstadoActualSismografo.NombreEstado}";
        }
    }
}