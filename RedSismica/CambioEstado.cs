// CambioEstado.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedSismica
{
    public class CambioEstado
    {
        public DateTime FechaHoraInicio { get; private set; } // Atributo 'fechaHoraInicio' del diagrama [cite: 1]
        public DateTime? FechaHoraFin { get; private set; }  // Atributo 'fechaHoraFin' del diagrama [cite: 1]
        public Estado EstadoAsociado { get; private set; }   // Asociación con Estado del diagrama [cite: 1]

        // Un CambioEstado puede tener múltiples MotivoFueraServicio asociados.
        // El diagrama muestra CambioEstado (0..*) -- (1) MotivoFueraServicio[cite: 1].
        // Interpretaremos esto como que un CambioEstado (específicamente a "Fuera de Servicio")
        // puede estar justificado por varios motivos detallados.
        public List<MotivoFueraServicio> Motivos { get; private set; }

        // Podríamos añadir el empleado que registra el cambio si es relevante para el historial.
        // El CU menciona "el RI logueado responsable del cierre" para el sismógrafo.
        public Empleado ResponsableDelCambio { get; private set; }

        private CambioEstado(DateTime fechaHoraInicio, Estado estadoAsociado, Empleado responsable = null)
        {
            FechaHoraInicio = fechaHoraInicio;
            EstadoAsociado = estadoAsociado;
            ResponsableDelCambio = responsable;
            Motivos = new List<MotivoFueraServicio>();
        }

        // Método crear() del diagrama [cite: 1]
        public static CambioEstado Crear(DateTime fechaHoraInicio, Estado estadoAsociado, Empleado responsable = null)
        {
            return new CambioEstado(fechaHoraInicio, estadoAsociado, responsable);
        }

        // Método setMotivo() del diagrama [cite: 1] (interpretado como agregar un motivo)
        public void AgregarMotivo(MotivoFueraServicio motivo)
        {
            if (motivo != null)
            {
                Motivos.Add(motivo);
            }
        }

        public void AgregarMotivos(List<MotivoFueraServicio> motivosNuevos)
        {
            if (motivosNuevos != null)
            {
                Motivos.AddRange(motivosNuevos.Where(m => m != null));
            }
        }

        // Método esActual() del diagrama [cite: 1]
        public bool EsActual()
        {
            return FechaHoraFin == null;
        }

        // Método finalizar() del diagrama [cite: 1]
        public void Finalizar(DateTime fechaHoraFin)
        {
            if (FechaHoraFin == null || fechaHoraFin > FechaHoraInicio)
            {
                FechaHoraFin = fechaHoraFin;
            }
            // Considerar manejo de error si fechaHoraFin es anterior a fechaHoraInicio
        }
    }
}