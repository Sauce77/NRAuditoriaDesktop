using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NRFM_Auditoria
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void separarResponsablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 separar_responsable = new Form1();
            separar_responsable.Show();
            this.Close();
        }

        private void cargarArchivo_Click(object sender, EventArgs e)
        {

        }
    }
}
