// ControladorCerrarOrden.cs
using System;
using System.Collections.Generic;
using System.Linq;
// System.Windows.Forms; // No es necesario aquí si las Forms de notificación se llaman externamente o son auto-contenidas.
// Lo mantenemos si el controlador debe instanciar Forms. Sí, las instanciará.
using System.Windows.Forms;


namespace RedSismica // Asegúrate que el namespace sea el correcto
{
    public class ControladorCerrarOrden
    {
        // Atributos del diagrama de clases
        public OrdenDeInspeccion ordenSeleccionada { get; private set; }
        public string observacionIngresada { get; private set; } // El diagrama usa este nombre
        private Empleado responsableLogueado; // Diagrama: responsablelogueado
        private List<OrdenDeInspeccion> _ordenesDelRI; // Para las órdenes del RI a mostrar; el diagrama lo llama "ordenes"
        public MotivoFueraServicio motivoSeleccionadoParaSismografo { get; private set; } // Diagrama: motivoSeleccionado. Usaremos una lista interna.
        private List<MotivoTipo> _tiposDeMotivoDisponibles; // Para los tipos de motivo a mostrar; el diagrama lo llama "motivos"

        // Referencias a datos en memoria
        private readonly List<OrdenDeInspeccion> _todasLasOrdenesDelSistema;
        private readonly List<MotivoTipo> _catalogoTiposDeMotivo;
        private readonly List<Estado> _catalogoEstados;
        private readonly List<Empleado> _todosLosEmpleadosDelSistema; // Para notificar a responsables de reparación

        private Sesion _sesionActual;

        // Estado interno para el flujo del CU
        private List<MotivoFueraServicio> _motivosAcumuladosParaSismografo; // Lista interna para los motivos seleccionados con sus comentarios

        public string MensajeErrorParaUI { get; private set; }

        public ControladorCerrarOrden(
            List<OrdenDeInspeccion> todasLasOrdenes,
            List<MotivoTipo> catalogoTiposMotivo,
            List<Estado> catalogoEstados,
            List<Empleado> todosLosEmpleados, // Necesario para buscar responsables de reparación
            Sesion sesionActual)
        {
            _todasLasOrdenesDelSistema = todasLasOrdenes;
            _catalogoTiposDeMotivo = catalogoTiposMotivo;
            _catalogoEstados = catalogoEstados;
            _todosLosEmpleadosDelSistema = todosLosEmpleados;
            _sesionActual = sesionActual;

            _motivosAcumuladosParaSismografo = new List<MotivoFueraServicio>();
            this._ordenesDelRI = new List<OrdenDeInspeccion>();
            this._tiposDeMotivoDisponibles = new List<MotivoTipo>();
        }

        // Corresponde a: tomarOpcionSeleccionada(), buscarUsuario(), buscarOrdenInspeccion(), ordenarPorFecha()
        public List<OrdenDeInspeccion> IniciarProcesoYObtenerOrdenesParaCierre()
        {
            MensajeErrorParaUI = null;
            responsableLogueado = _sesionActual.GetUsuario()?.GetEmpleado();

            if (responsableLogueado == null || !responsableLogueado.EsResponsableDeInspecciones())
            {
                MensajeErrorParaUI = "Usuario no logueado o no es Responsable de Inspecciones.";
                this._ordenesDelRI.Clear();
                return this._ordenesDelRI;
            }

            this._ordenesDelRI = _todasLasOrdenesDelSistema
                .Where(o => o.EsDeEmpleado(responsableLogueado) &&
                            o.EsCompletamenteRealizada() && // CU: "estado completamente realizadas"
                            o.EstadoActualOrden.NombreEstado != "Cerrada")
                .OrderBy(o => o.FechaHoraFinalizacion) // CU: "ordenadas por fecha de finalización"
                .ToList();

            if (!this._ordenesDelRI.Any())
            {
                // CU Flujo Alternativo A1: "El RI no tiene órdenes de inspección realizadas."
                MensajeErrorParaUI = "No tiene órdenes de inspección completadas pendientes de cierre.";
            }
            return this._ordenesDelRI;
        }

        // Corresponde a: tomarOrdenSeleccionada(OrdenDeInspeccion)
        public void SetearOrdenSeleccionada(OrdenDeInspeccion orden)
        {
            this.ordenSeleccionada = orden;
            this.observacionIngresada = string.Empty; // Resetear para nueva selección
            this._motivosAcumuladosParaSismografo.Clear(); // Resetear para nueva selección
            this.motivoSeleccionadoParaSismografo = null;
        }

        // Corresponde a: tomarObservacionIngresada(string)
        public void SetearObservacionIngresada(string observacion)
        {
            this.observacionIngresada = observacion;
        }

        // Corresponde a: buscarMotivos(), cargarTiposMotivos()
        public List<MotivoTipo> ObtenerTiposDeMotivoDisponibles()
        {
            this._tiposDeMotivoDisponibles = _catalogoTiposDeMotivo.ToList();
            return this._tiposDeMotivoDisponibles;
        }

        // Corresponde a: tomarMotivoSeleccionado(MotivoTipo), tomarComentarioIngresado(string)
        // Se llamará por cada motivo que el usuario agregue.
        public void AcumularMotivoParaSismografo(MotivoTipo tipo, string comentario)
        {
            if (tipo == null) return;
            var nuevoMotivo = new MotivoFueraServicio(tipo, comentario);
            _motivosAcumuladosParaSismografo.Add(nuevoMotivo);
            this.motivoSeleccionadoParaSismografo = nuevoMotivo; // El diagrama menciona uno, guardamos el último.
        }

        public void LimpiarMotivosAcumulados()
        {
            _motivosAcumuladosParaSismografo.Clear();
            this.motivoSeleccionadoParaSismografo = null;
        }

        // Corresponde a: tomarConfirmacionRegistrada(), validarObservacion(), validarMotivoSeleccionado(),
        // buscarEstados(), buscarEstadoCerrado(), getFechaHoraActual(), cerrarOrden(), ponerFueraServicio(),
        // notificarViaMail(), actualizarPantalla(), fin C.U()
        public bool ConfirmarCierreTotal()
        {
            MensajeErrorParaUI = null;

            // 1. Validaciones (validarObservacion, validarMotivoSeleccionado)
            // CU Paso 10: "Valida que exista una observación de cierre de orden y al menos un motivo seleccionado..."
            if (this.ordenSeleccionada == null)
            {
                MensajeErrorParaUI = "No se ha seleccionado ninguna orden para cerrar.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.observacionIngresada))
            {
                // CU Flujo Alternativo A3: "...y hay datos faltantes"
                MensajeErrorParaUI = "La observación de cierre es obligatoria.";
                return false;
            }
            if (!_motivosAcumuladosParaSismografo.Any())
            {
                // CU Flujo Alternativo A3: "...y hay datos faltantes"
                MensajeErrorParaUI = "Debe agregar al menos un motivo para poner el sismógrafo fuera de servicio.";
                return false;
            }

            // 2. Obtener Fecha/Hora (getFechaHoraActual)
            DateTime fechaHoraActual = DateTime.Now;

            // 3. Actualizar Orden (buscarEstadoCerrado, cerrarOrden)
            // CU Paso 11: "actualiza la orden de inspección a cerrada y registra la fecha y hora del sistema como fecha de cierre."
            Estado estadoOrdenCerrada = _catalogoEstados.FirstOrDefault(e => e.EsEstadoCerrado());
            if (estadoOrdenCerrada == null)
            {
                MensajeErrorParaUI = "Error de configuración: Estado 'Cerrada' no encontrado.";
                return false;
            }
            this.ordenSeleccionada.RegistrarCierreOrden(this.observacionIngresada, fechaHoraActual, estadoOrdenCerrada);

            // 4. Actualizar Sismógrafo (ponerFueraServicio)
            // CU Paso 12: "actualiza al sismógrafo de la ES como fuera de servicio, asociando al nuevo estado los motivos seleccionados..."
            Sismografo sismografoAfectado = this.ordenSeleccionada.SismografoInspeccionado;
            Estado estadoSismoFueraServicio = _catalogoEstados.FirstOrDefault(e => e.EsFueraDeServicio());
            if (estadoSismoFueraServicio == null)
            {
                MensajeErrorParaUI = "Error de configuración: Estado 'Fuera de Servicio' no encontrado.";
                return false;
            }
            sismografoAfectado.PonerFueraDeServicio(fechaHoraActual, _motivosAcumuladosParaSismografo, this.responsableLogueado, estadoSismoFueraServicio);

            // 5. Notificaciones (notificarViaMail, actualizarPantalla para CCRS)
            // CU Paso 13: "Envía una notificación por defecto a los mails de los empleados responsables de reparaciones y la publica en los monitores del CCRS"
            EjecutarNotificacionViaMail(sismografoAfectado, estadoSismoFueraServicio, fechaHoraActual);
            EjecutarActualizacionPantallaCCRS(sismografoAfectado, estadoSismoFueraServicio, fechaHoraActual);

            FinalizarCasoDeUso(); // fin C.U()
            return true;
        }

        // Método privado para notificarViaMail()
        private void EjecutarNotificacionViaMail(Sismografo sismografo, Estado nuevoEstado, DateTime fechaHoraEvento)
        {
            // CU Observación 2: "La notificación debe incluir la identificación del sismógrafo, el nombre del estado Fuera de Servicio, la fecha y hora de registro del nuevo estado, y los motivos y comentarios asociados al cambio."
            string asunto = $"Alerta Sistema Sísmico: Sismógrafo {sismografo.IdentificadorSismografo} ahora {nuevoEstado.NombreEstado}";
            StringBuilder cuerpoMail = new StringBuilder();
            cuerpoMail.AppendLine($"El sismógrafo {sismografo.IdentificadorSismografo} (Estación: {sismografo.Estacion.GetNombre()}) ha cambiado su estado a '{nuevoEstado.NombreEstado}'.");
            cuerpoMail.AppendLine($"Fecha y Hora del cambio: {fechaHoraEvento:G}");
            cuerpoMail.AppendLine($"Registrado por: {responsableLogueado.GetNombreCompleto()}");
            cuerpoMail.AppendLine("Detalles de la Orden de Inspección:");
            cuerpoMail.AppendLine($"- Número: {ordenSeleccionada.NumeroOrden}");
            cuerpoMail.AppendLine($"- Observación de Cierre: {observacionIngresada}");
            cuerpoMail.AppendLine("Motivos para 'Fuera de Servicio':");
            if (_motivosAcumuladosParaSismografo.Any())
            {
                foreach (var motivo in _motivosAcumuladosParaSismografo)
                {
                    cuerpoMail.AppendLine($"- Tipo: {motivo.Tipo.Descripcion}, Comentario: {motivo.Comentario}");
                }
            }
            else
            {
                cuerpoMail.AppendLine("(No se especificaron motivos detallados)");
            }

            // Buscar responsables de reparación
            var responsables = _todosLosEmpleadosDelSistema.Where(e => e.SosResponsableDeReparacion()).ToList();
            if (responsables.Any())
            {
                foreach (var responsable in responsables)
                {
                    Console.WriteLine($"SIMULANDO ENVIO DE MAIL A: {responsable.GetEmail()}"); // Log para consola
                    using (FormPantallaMail formMail = new FormPantallaMail(responsable.GetEmail(), asunto, cuerpoMail.ToString()))
                    {
                        formMail.ShowDialog(); // Mostrar la Form de notificación
                    }
                }
            }
            else
            {
                using (FormPantallaMail formMail = new FormPantallaMail("No hay responsables de reparación configurados", asunto, cuerpoMail.ToString()))
                {
                    formMail.ShowDialog();
                }
                Console.WriteLine("ADVERTENCIA: No hay responsables de reparación para notificar por mail.");
            }
        }

        // Método privado para actualizarPantalla() (CCRS)
        private void EjecutarActualizacionPantallaCCRS(Sismografo sismografo, Estado nuevoEstado, DateTime fechaHoraEvento)
        {
            // CU Observación 2
            Console.WriteLine($"SIMULANDO ACTUALIZACION MONITOR CCRS para sismógrafo: {sismografo.IdentificadorSismografo}"); // Log para consola
            using (FormPantallaCCRS formCCRS = new FormPantallaCCRS(
                sismografo.IdentificadorSismografo,
                nuevoEstado.NombreEstado,
                fechaHoraEvento,
                _motivosAcumuladosParaSismografo))
            {
                formCCRS.ShowDialog(); // Mostrar la Form de notificación
            }
        }

        // Corresponde a: fin C.U()
        public void FinalizarCasoDeUso()
        {
            this.ordenSeleccionada = null;
            this.observacionIngresada = string.Empty;
            this._motivosAcumuladosParaSismografo.Clear();
            this.motivoSeleccionadoParaSismografo = null;
            this._ordenesDelRI.Clear();
            this._tiposDeMotivoDisponibles.Clear();
            // El responsableLogueado persiste mientras la sesión esté activa.
            Console.WriteLine("Controlador: Estado interno del Caso de Uso 'Cerrar Orden de Inspección' reseteado.");
        }
    }
}