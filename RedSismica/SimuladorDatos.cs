namespace RedSismica
{
    public static class SimuladorDatos
    {
        public static Usuario ObtenerUsuario()
        {
            var rol = new Rol
            {
                Nombre = "Responsable de Reparación",
                DescripcionRol = "Rol que permite recibir notificaciones"
            };

            var empleado = new Empleado
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                Mail = "juan@example.com",
                Telefono = "3512345678",
                Rol = rol,
                OrdenesAsignadas = new List<OrdenDeInspeccion>() // por ahora vacío
            };

            var usuario = new Usuario
            {
                NombreUsuario = "ri123",
                Contrasenia = "1234",
                Empleado = empleado
            };

            return usuario;
        }
    }
}
