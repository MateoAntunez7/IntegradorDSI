using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RedSismica;

namespace RedSismica
{
    public partial class FormInicio : Form
    {
        
        private Usuario usuarioLogueado;
        private ControladorCerrarOrden controlador;

        public FormInicio()
        {
            InitializeComponent();

            
            usuarioLogueado = SimuladorDatos.ObtenerUsuario(); 
            controlador = new ControladorCerrarOrden(usuarioLogueado);
        }

        private void FormInicio_Load(object sender, EventArgs e)
        {
            
        }

        private void btnCerrarOrden_Click(object sender, EventArgs e)
        {
            controlador.RegistrarOpcionCerrarOrden();
            var formSeleccion = new FormSeleccionOrden(controlador);
            formSeleccion.ShowDialog();
        }
    }
}
