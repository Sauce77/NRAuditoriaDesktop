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
    public partial class PoliticaDias : Form
    {
        public PoliticaDias()
        {
            InitializeComponent();
        }

        private void cargarArchivo_Click(object sender, EventArgs e)
        {
            archivoNombre.Text = string.Empty;
            // se selcciona el archivo a leer
            string ruta_archivo = FuncionesAuditoria.obtenerArchivoSeleccionado();
            if (ruta_archivo != null)
            {
                archivoNombre.Text = ruta_archivo;
            }//si el archivo fue seleccionado
        }

        private void contarTotalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form totales = new Form2();
            totales.Show();
            this.Close();
        }

        private void separarResponsabesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form separarResponsables = new Form1();
            separarResponsables.Show();
            this.Close();
        }

        private bool esNumero(string texto)
        {
            try
            {
                int numero = int.Parse(limiteUA.Text);
                // Es un número válido
                return true;
            }// probamos que sea un numero
            catch (FormatException)
            {
                // No es un número válido
                MessageBox.Show("Cantidad invalida de dias");
                return false;
            }
        }

        private void aplicarFiltro_Click(object sender, EventArgs e)
        {
            string archivo = archivoNombre.Text;
            int numeroDias;

            if(archivo == String.Empty)
            {
                MessageBox.Show("No se ha seleccionado un archivo");
            }// si no se ha seleccionado archivo
            else if(esNumero(limiteUA.Text))
            {
                numeroDias = int.Parse((string)limiteUA.Text);
                if (numeroDias < 0)
                {
                    limiteUA.Text = string.Empty;
                    MessageBox.Show("Cantidad invalida de dias");
                }// si el numero de dias es negativo
                else
                {
                   Debug.WriteLine(numeroDias);
                }// la cantidad de dias es correcta
            }// el campo contiene un numero
            else
            {
                limiteUA.Text = string.Empty;
            }// no se ingreso un numero

        }
    }
}
