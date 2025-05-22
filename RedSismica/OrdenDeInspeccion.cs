// OrdenDeInspeccion.cs
using System;

namespace RedSismica // Asegúrate que el namespace sea el correcto
{
    public class OrdenDeInspeccion
    {
        public string NumeroOrden { get; private set; }                // Atributo del diagrama de clases
        public DateTime FechaHoraInicio { get; private set; }          // Atributo del diagrama de clases
        public DateTime? FechaHoraFinalizacion { get; private set; }  // Atributo del diagrama de clases y CU
        public DateTime? FechaHoraCierre { get; private set; }        // Atributo del diagrama de clases
        public string ObservacionCierre { get; private set; }          // Atributo del diagrama de clases

        public Estado EstadoActualOrden { get; private set; }          // Asociación con Estado del diagrama
        public Empleado ResponsableInspeccion { get; private set; }    // Asociación con Empleado (RI) del diagrama

        // Suposición crucial basada en el CU: una orden de inspección está ligada a un sismógrafo.
        // El CU requiere mostrar info del sismógrafo y actualizarlo.
        public Sismografo SismografoInspeccionado { get; private set; }

        // Constructor
        public OrdenDeInspeccion(string numeroOrden, DateTime fechaHoraInicio, Empleado responsable, Sismografo sismografo, Estado estadoInicial)
        {
            if (string.IsNullOrWhiteSpace(numeroOrden))
                throw new ArgumentException("El número de orden no puede estar vacío.", nameof(numeroOrden));
            if (responsable == null)
                throw new ArgumentNullException(nameof(responsable), "El responsable de la inspección no puede ser nulo.");
            if (sismografo == null)
                throw new ArgumentNullException(nameof(sismografo), "El sismógrafo inspeccionado no puede ser nulo.");
            if (estadoInicial == null)
                throw new ArgumentNullException(nameof(estadoInicial), "El estado inicial de la orden no puede ser nulo.");

            NumeroOrden = numeroOrden;
            FechaHoraInicio = fechaHoraInicio;
            ResponsableInspeccion = responsable;
            SismografoInspeccionado = sismografo;
            EstadoActualOrden = estadoInicial;
        }

        // Método para simular la finalización de la inspección por parte del RI,
        // dejándola lista para el cierre según el CU.
        public void MarcarComoCompletamenteRealizada(DateTime fechaFinalizacion, Estado estadoCompletada)
        {
            if (estadoCompletada == null || !estadoCompletada.EsCompletamenteRealizada())
            {
                // Podríamos lanzar una excepción o manejarlo si el estado no es "Completamente Realizada"
                Console.WriteLine("Error: El estado para marcar la orden como completada no es válido.");
                return;
            }
            FechaHoraFinalizacion = fechaFinalizacion; // El CU menciona "ordenadas por fecha de finalización"
            EstadoActualOrden = estadoCompletada;
        }

        // Método esCompletamenteRealizada() del diagrama de clases y CU
        // El CU define cuándo una orden está "completamente realizada" (Observación 1).
        // Para esta simulación, nos basamos en el estado.
        public bool EsCompletamenteRealizada()
        {
            return EstadoActualOrden.EsCompletamenteRealizada();
        }

        // Método registrarCierreOrden() del diagrama de clases y CU
        // "actualiza la orden de inspección a cerrada y registra la fecha y hora del sistema como fecha de cierre"
        public void RegistrarCierreOrden(string observacion, DateTime fechaCierreSistema, Estado estadoCerrado)
        {
            if (estadoCerrado == null || !estadoCerrado.EsEstadoCerrado())
            {
                // Podríamos lanzar una excepción
                Console.WriteLine("Error: El estado para registrar el cierre de la orden no es 'Cerrada'.");
                return;
            }
            if (string.IsNullOrWhiteSpace(observacion) && EstadoActualOrden.NombreEstado != "Cerrada") // No requerir observación si ya estaba cerrada y se re-procesa
            {
                // El CU valida que exista una observación de cierre
                Console.WriteLine("Advertencia: La observación de cierre está vacía.");
                // No impedimos el cierre, pero el controlador debería validar esto antes.
            }

            ObservacionCierre = observacion;
            FechaHoraCierre = fechaCierreSistema;
            EstadoActualOrden = estadoCerrado;
            Console.WriteLine($"Orden {NumeroOrden} registrada como '{EstadoActualOrden.NombreEstado}' el {FechaHoraCierre} con observación: '{ObservacionCierre}'");
        }

        // Método esDeEmpleado() del diagrama de clases
        public bool EsDeEmpleado(Empleado empleado)
        {
            return ResponsableInspeccion == empleado;
        }

        // Método getInfoOrdenInspeccion() del diagrama de clases
        // El CU requiere mostrar esta información
        public string GetInfoOrdenInspeccion()
        {
            return $"Orden: {NumeroOrden}, Finalización: {FechaHoraFinalizacion?.ToString("g") ?? "N/A"}, " +
                   $"Estación: {SismografoInspeccionado.Estacion.GetNombre()}, " +
                   $"Sismógrafo: {SismografoInspeccionado.GetIdSismografo()}";
        }

        // El método ponerSismografoFueraDeServicio() que aparece en OrdenDeInspeccion en el diagrama de clases
        // es una acción que el *Controlador* realizará sobre el sismógrafo asociado a esta orden.
        // La orden en sí no "pone" fuera de servicio al sismógrafo; es un resultado del proceso de cierre de la orden.

        public override string ToString()
        {
            // Para visualización en ListBox u otros controles.
            // El CU pide visualizar Nro Orden, Fecha Finalización, Nombre Estación, ID Sismógrafo.
            return $"Orden: {NumeroOrden} (Est: {SismografoInspeccionado.Estacion.GetNombre()}, Sismo: {SismografoInspeccionado.IdentificadorSismografo}, Fin: {FechaHoraFinalizacion?.ToString("dd/MM/yy HH:mm") ?? "Pend."})";
        }
    }
}