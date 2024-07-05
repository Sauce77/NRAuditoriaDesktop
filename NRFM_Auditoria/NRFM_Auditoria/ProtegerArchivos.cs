using ClosedXML.Excel;
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
        // se quedan los archivos cargados
        public static string[] archivos_cargados = { "" };
        public ProtegerArchivos()
        {
            InitializeComponent();
        }

        private void botonProteger_Click(object sender, EventArgs e)
        {

            if (Password.Text == String.Empty)
            {
                MessageBox.Show("Ingrese una contrasena antes");
            }// si no se ha ingresado una contrasena
            else
            {
                // obtenemos la contrasena ingresada
                string contrasena = Password.Text.ToString();

                // recorremos el arreglo de archivos cargados
                foreach (string archivo_actual in archivos_cargados)
                {
                    using (var workbook = new XLWorkbook(archivo_actual))
                    {
                        // recorremos las hojas de cada archivo
                        foreach (var hoja in workbook.Worksheets)
                        {
                            if (!hoja.IsPasswordProtected)
                            {
                                hoja.Protect(contrasena).AllowElement(XLSheetProtectionElements.AutoFilter).
                                                                AllowElement(XLSheetProtectionElements.Sort);
                            }// si no esta protegido

                        }// para cada hoja en el libro actual

                        // protegemos la integridad del libro
                        if (!workbook.IsPasswordProtected)
                        {
                            workbook.Protect(contrasena);
                        }// si los libros no estan protegidos


                        // Guarda los cambios
                        workbook.Save();
                    }
                }// fin for para cada archivo cargado

                MessageBox.Show("Archivos protegidos");
            }// si se ingreso una contrasena


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // boton cargar archivo;
            ArchivosCargados.Items.Clear();
            archivos_cargados = FuncionesAuditoria.obtenerMultiplesArchivoSeleccionado();

            foreach (string ruta_archivo in archivos_cargados)
            {
                ArchivosCargados.Items.Add(ruta_archivo);
            }// fin para cada archivo cargado
        }

        private void botonQuitarProteccion_Click(object sender, EventArgs e)
        {
            if (Password.Text == String.Empty)
            {
                MessageBox.Show("Ingrese una contrasena antes");
            }// si no se ha ingresado una contrasena
            else
            {
                // obtenemos la contrasena ingresada
                string contrasena = Password.Text.ToString();

                // recorremos el arreglo de archivos cargados
                foreach (string archivo_actual in archivos_cargados)
                {
                    using (var workbook = new XLWorkbook(archivo_actual))
                    {
                        // recorremos las hojas de cada archivo
                        foreach (var hoja in workbook.Worksheets)
                        {
                            if (hoja.IsPasswordProtected)
                            {
                                hoja.Unprotect(contrasena);
                            }// si esta protegido

                        }// para cada hoja en el libro actual

                        // protegemos la integridad del libro
                        if (workbook.IsPasswordProtected)
                        {
                            workbook.Unprotect(contrasena);
                        }// si los libros estan protegidos


                        // Guarda los cambios
                        workbook.Save();
                    }
                }// fin for para cada archivo cargado

                MessageBox.Show("Archivos desprotegidos");
            }// si se ingreso una contrasena
        }

        private void generarTotalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form generar_totales = new Form2();
            generar_totales.Show();
            this.Close();
        }

        private void separarResponsablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form separar = new Form1();
            separar.Show();
            this.Close();
        }

        private void marcarInactividadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form inactividad = new PoliticaDias();
            inactividad.Show();
            this.Close();
        }
    }
}
