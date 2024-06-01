using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
        private bool esFecha(string fecha)
        {
            DateTime enteredDate;

            if (DateTime.TryParse(fecha, out enteredDate))
            {
                return true;
            }// si se logra convertir a fecha

            return false;
        }

        private void aplicarFiltro_Click(object sender, EventArgs e)
        {
            string ruta_archivo = archivoNombre.Text;
            int numeroDias;

            labelProgreso.Visible = true;
            barraProgresoBajas.Visible = true;

            // valor barra de progreso
            barraProgresoBajas.Value = barraProgresoBajas.Minimum;

            if (ruta_archivo == String.Empty)
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
                    DateTime fechaActualSinHora = DateTime.Today;

                    // valor barra de progreso
                    barraProgresoBajas.Value = (barraProgresoBajas.Maximum * 2) / 5;

                    var archivo = new XLWorkbook(ruta_archivo);
                    foreach(IXLWorksheet hoja in archivo.Worksheets)
                    {
                        int finCabecera = FuncionesAuditoria.encontrarCabecera(hoja);
                        if(finCabecera < 0)
                        {
                            MessageBox.Show("No se encontro la cabecera en la hoja " + hoja.Name);
                            continue;
                        }// no se encontro la cabecera

                        char colUltimoAcceso = FuncionesAuditoria.encontrarColUltimoAcceso(hoja,finCabecera);
                        if (colUltimoAcceso == '-')
                        {
                            continue; 
                        }// No se encontro la columna de ultimo acceso

                        //en caso que si la encontrara
                        Debug.WriteLine(hoja.Name + " " + colUltimoAcceso);

                        int index = finCabecera + 1;// el inicio de los datos

                        // obtenemos los dias a los que aplicamos el filtro
                        int dias_filtro = int.Parse((string)limiteUA.Text);

                        // restamos a la fecha actual los dias del filtro
                        fechaActualSinHora.AddDays(-dias_filtro);

                        while (!hoja.Cell('A' + index.ToString()).IsEmpty())
                        {
                            if(!hoja.Cell(colUltimoAcceso + index.ToString()).IsEmpty())
                            {
                                string valor_fecha = hoja.Cell(colUltimoAcceso + index.ToString()).Value.ToString();
                                if (esFecha(valor_fecha))
                                {
                                    // recortamos para que solo quede el formato dd/MM/yyyy
                                    valor_fecha = valor_fecha.Substring(0, 10);

                                    DateTime fecha = DateTime.ParseExact(valor_fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                    // si la fecha es anterior a la actual despues de restar los dias del filtro
                                    // se colorea dicha celda
                                    int resultado = DateTime.Compare(fecha, fechaActualSinHora);
                                    if (resultado <= 0)
                                    {
                                        hoja.Row(index).Style.Fill.BackgroundColor = FuncionesAuditoria.COLOR_CELDAS_BAJA;
                                    }// si la fecha es anterior o igual
                                }// si se puede convertir a fecha
                                else if(valor_fecha == String.Empty || valor_fecha.ToLower() == "null")
                                {
                                    // obtenemos la columna de fecha de creacion
                                    char col_Creacion = FuncionesAuditoria.encontrarColFechaCreacion(hoja, finCabecera);

                                    // comprueba si existe la columna fecha de creacion

                                    string calor_fecha_creaicion = hoja.Cell(colUltimoAcceso + index.ToString()).Value.ToString();
                                }// fin for el es
                            }// si la celda tiene datos
                            index++; 
                        }// mientras tenga datos la primera columna

                    }// fin for hojas del archivo

                    // valor de barra de progreso
                    barraProgresoBajas.Value = barraProgresoBajas.Maximum;

                    //se han recorrido todas las hojas
                    MessageBox.Show("Proceso terminado");
                    archivo.SaveAs(ruta_archivo);

                }// la cantidad de dias es correcta
            }// el campo contiene un numero
            else
            {
                limiteUA.Text = string.Empty;


            }// no se ingreso un numero
            barraProgresoBajas.Visible = false;
            labelProgreso.Visible = false;
        }
    }
}
