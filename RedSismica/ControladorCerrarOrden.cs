using System;
namespace RedSismica
public class ControladorCerrarOrden
{
    private Usuario responsableLogueado;
    private List<OrdenDeInspeccion> ordenes;
    private OrdenDeInspeccion ordenSeleccionada;
    private List<MotivoTipo> motivos;
    private List<MotivoFueraServicio> motivoSeleccionado;
    private string observacionIngresada;

    public ControladorCerrarOrden(Usuario usuario)
    {
        responsableLogueado = usuario;
        ordenes = new List<OrdenDeInspeccion>();
        motivos = MotivoTipo.BuscarMotivoTipo();
        motivoSeleccionado = new List<MotivoFueraServicio>();
    }

    public void RegistrarOpcionCerrarOrden()
    {
        // Cargar órdenes del empleado logueado
        ordenes = responsableLogueado.GetEmpleado().OrdenesAsignadas
            .Where(o => o.EsCompletamenteRealizada() && !o.Estado.EsEstadoCerrado())
            .OrderByDescending(o => o.FechaHoraFinalizacion)
            .ToList();
    }

    public List<OrdenDeInspeccion> ObtenerOrdenes()
    {
        return ordenes;
    }

    public void SeleccionarOrden(OrdenDeInspeccion orden)
    {
        ordenSeleccionada = orden;
    }

    public void IngresarObservacion(string observacion)
    {
        observacionIngresada = observacion;
    }

    public List<MotivoTipo> BuscarMotivos()
    {
        return motivos;
    }

    public void TomarMotivoSeleccionado(MotivoTipo tipo, string comentario)
    {
        if (!string.IsNullOrWhiteSpace(comentario))
        {
            motivoSeleccionado.Add(new MotivoFueraServicio(tipo, comentario));
        }
    }

    public bool ValidarObservacion()
    {
        return !string.IsNullOrWhiteSpace(observacionIngresada);
    }

    public bool ValidarMotivoSeleccionado()
    {
        return motivoSeleccionado.Any() && motivoSeleccionado.All(m => !string.IsNullOrWhiteSpace(m.Comentario));
    }

    public void CerrarOrden()
    {
        if (ordenSeleccionada == null) return;

        DateTime ahora = GetFechaHoraActual();
        ordenSeleccionada.RegistrarCierreOrden(observacionIngresada, ahora);
        ordenSeleccionada.PonerSismografoFueraDeServicio(motivoSeleccionado, ahora);

        NotificarViaMail();
        ActualizarPantalla();
    }

    public DateTime GetFechaHoraActual()
    {
        return DateTime.Now;
    }

    public void NotificarViaMail()
    {
        Console.WriteLine("📧 [NOTIFICACIÓN POR MAIL]");
        Console.WriteLine($"Orden cerrada por: {responsableLogueado.NombreUsuario}");
        Console.WriteLine($"Sismógrafo ID: {ordenSeleccionada.Estacion.Sismografo.IdentificadorSismografo}");
        Console.WriteLine($"Estado: Fuera de servicio");
        Console.WriteLine("Motivos:");
        foreach (var m in motivoSeleccionado)
        {
            Console.WriteLine($"- {m.Tipo.Descripcion}: {m.Comentario}");
        }
    }

    public void ActualizarPantalla()
    {
        Console.WriteLine("📺 [MONITOR CCRS ACTUALIZADO]");
        Console.WriteLine($"Sismógrafo fuera de servicio: {ordenSeleccionada.Estacion.Sismografo.IdentificadorSismografo}");
    }

    public void FinCU()
    {
        Console.WriteLine("✅ Cierre de orden completado correctamente.");
    }
}

