using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NRFM_Auditoria
{
    public partial class ProtegerArchivos : Form
    {
        public ProtegerArchivos()
        {
            InitializeComponent();
        }

        private void botonProteger_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // boton cargar archivo;
            archivosSeleccionados.Text = FuncionesAuditoria.obtenerMultiplesArchivoSeleccionado();
        }
    }
}
