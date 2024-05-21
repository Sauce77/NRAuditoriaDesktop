using ClosedXML.Excel;
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
        public string[] CAMPOS_RESPONSABLE = ["Total Usuarios", "Enviado Responsable", "Respuesta Responsable", "Baja Automatica", "Baja Responsable", "Conservar Acceso Responsable"];

        public const int FILAS_ENTRE_RESPONSABLES = 1;

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
            // obtenemos el mes y anio para guardar el documento
            int mes = DateTime.Now.Month;
            int anio = DateTime.Now.Year;

            string fileActual = FuncionesAuditoria.obtenerArchivoSeleccionado();
            if (fileActual != null)
            {
                // declaramos un excel para insertar los totales
                var archivoNuevo = new XLWorkbook();

                // Empezamos a leer el excel actual
                var libroActual = new XLWorkbook(fileActual);

                //iteramos en cada hoja del libro de excel
                foreach (IXLWorksheet hoja in libroActual.Worksheets)
                {
                    int index = FuncionesAuditoria.encontrarCabecera(hoja);

                    if (index < 0)
                    {
                        continue;
                    }// si no se encuentra cabecera que se omita la hoja
                    // guardamos el valor como fin de cabecera
                    int FIN_CABECERA = index;

                    // buscamos en donde se encuentra la columna de responsables
                    char colResponsable = FuncionesAuditoria.encontrarColResponsable(hoja, FIN_CABECERA);

                    // en caso de no encontrar la columna de responsables, que pase a la siguiente hoja
                    if (colResponsable == '-')
                    {
                        MessageBox.Show("No se ha encontrado la columna de responsable en " + hoja.Name);
                        continue; // si no se encuentra la columna que pase a la siguiente hoja
                    }//fin if no encuentra la columna Responsable

                    index++; // sumamos uno para que omita el nombre de las columnas
                    /*
                        Mientras la celda de la primera columna no este vacia entonces que siga
                        iterando
                    */

                    // declaramos un diccionario para guardar las cuentas de cada responsable
                    SortedDictionary<string, int[]> conteo = new SortedDictionary<string, int[]>();
                    int total_aplicacion = 0;

                    while (!hoja.Cell(colResponsable + index.ToString()).IsEmpty())
                    {
                        total_aplicacion++; // se aumenta en uno el registro de usuarios.
                                            // obtenemos el nombre del responsable
                        string responsable = hoja.Cell(colResponsable + index.ToString()).Value.ToString();
                        // comprobamos si el responsable ya esta en el diccionario
                        if (!conteo.ContainsKey(responsable))
                        {
                            conteo[responsable] = [0, 0, 0];
                        }// si no esta en el diccionario

                        // se incrementa el registro del responsable
                        conteo[responsable][0]++;

                        // mejor intentalo por fecha
                        if (hoja.Cell(colResponsable + index.ToString()).Style.Fill.BackgroundColor.Equals(XLColor.FromIndex(64)))
                        {
                            conteo[responsable][2]++; //conserva acceso
                        }// si la celda no tiene color
                        else
                        {
                            conteo[responsable][1]++; //baja automatica
                        }// si la celda tiene color

                        index++;// avanza a la siguente fila

                    }// fin mientras no este vacia


                    // agregamos una hoja para la aplicacion
                    var hojaAplicacion = archivoNuevo.Worksheets.Add(hoja.Name);

                    // anadimos titulos a las columnas

                    // titulo Responsables
                    hojaAplicacion.Cell("A" + (FIN_CABECERA - 1).ToString()).Value = FuncionesAuditoria.NOMBRE_COL_RESPONSABLE;

                    // titulo mes pasado
                    hojaAplicacion.Cell("C" + (FIN_CABECERA - 1).ToString()).Value = ((mes - 1).ToString() + "-" + anio.ToString());

                    // titulo mes actual
                    hojaAplicacion.Cell("D" + (FIN_CABECERA - 1).ToString()).Value = (mes.ToString() + "-" + anio.ToString());

                    // utilizamos un index para saber en que fila nos encontramos
                    int fila = FIN_CABECERA;

                    // prueba para saber el conteo de cada responsable
                    foreach (KeyValuePair<string, int[]> conteo_responsable in conteo)
                    {

                        // escribimos el nombre del responsable
                        hojaAplicacion.Cell("A" + fila.ToString()).Value = conteo_responsable.Key;

                        for (int i = 0; i < CAMPOS_RESPONSABLE.Length; i++)
                        {
                            //escribimos el campo i
                            hojaAplicacion.Cell("B" + (fila + i).ToString()).Value = CAMPOS_RESPONSABLE[i];

                            // agregamos margenes a las celdas con numeros
                            hojaAplicacion.Cell("C" + (fila + i).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            hojaAplicacion.Cell("D" + (fila + i).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            // estilo columnas numeros
                            hojaAplicacion.Cell("C" + (fila + i).ToString()).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.8);
                            hojaAplicacion.Cell("D" + (fila + i).ToString()).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.9);

                            switch (i)
                            {
                                case 0:
                                    hojaAplicacion.Cell("D" + (fila + i).ToString()).Value = conteo_responsable.Value[0];
                                    break; // total enviados
                                case 1:
                                    hojaAplicacion.Cell("D" + (fila + i).ToString()).Value = conteo_responsable.Value[0] - conteo_responsable.Value[1];
                                    break; // total enviados a responsable
                                case 3:
                                    hojaAplicacion.Cell("D" + (fila + i).ToString()).Value = conteo_responsable.Value[1];
                                    break;
                                case 5:
                                    hojaAplicacion.Cell("D" + (fila + i).ToString()).Value = conteo_responsable.Value[2];
                                    break;
                            }//fin switch comprobar si insertar cuenta


                        }//fin campos por responsable

                        fila += CAMPOS_RESPONSABLE.Length + FILAS_ENTRE_RESPONSABLES;
                    }//fin foreach diccionario

                    // agregamos el total de usuarios de la aplicacion
                    hojaAplicacion.Cell("A2").Value = "Total Usuarios";
                    hojaAplicacion.Cell("B2").Value = total_aplicacion;

                    // estilo del total usuarios
                    hojaAplicacion.Cell("A2").Style.Font.Bold = true;
                    hojaAplicacion.Range("A2", "B2").Style.Font.FontSize = 14;

                    // estilo de los titulos de las columnas
                    hojaAplicacion.Range("A" + (FIN_CABECERA - 1).ToString(), "D" + (FIN_CABECERA - 1).ToString()).Style.Font.Bold = true;
                    hojaAplicacion.Range("A" + (FIN_CABECERA - 1).ToString(), "D" + (FIN_CABECERA - 1).ToString()).Style.Font.FontSize = 14;
                    hojaAplicacion.Range("A" + (FIN_CABECERA - 1).ToString(), "D" + (FIN_CABECERA - 1).ToString()).Style.Font.FontColor = XLColor.White;
                    hojaAplicacion.Range("A" + (FIN_CABECERA - 1).ToString(), "D" + (FIN_CABECERA - 1).ToString()).Style.Fill.BackgroundColor = XLColor.Black;
                    hojaAplicacion.Range("A" + (FIN_CABECERA - 1).ToString(), "D" + (FIN_CABECERA - 1).ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    // ajustar el ancho de las columnas
                    hojaAplicacion.Columns().AdjustToContents();

                }// fin foreach hojas del libro


                // variable para saber si el archivo tiene hojas
                bool tiene_hojas = false;
                foreach (IXLWorksheet hoja in archivoNuevo.Worksheets)
                {
                    tiene_hojas = true;
                    break;
                }// fin for recorre las hojas del archivo nuevo

                if (tiene_hojas)
                {
                    // alerta de que se ha generado el nuevo archivo
                    MessageBox.Show("Archivo de totales generado.\nSeleccione donde guardar el archivo.");

                    // guardar el archivo con un FileDialog

                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Title = "Guardar archivo de totales";
                    sfd.Filter = FuncionesAuditoria.FILTROS_ARCHIVOS_EXCEL;

                    // formato del nombre del archivo
                    string nombre_nuevo_archivo = "Totales-" + mes.ToString() + "-" + anio.ToString() + ".xlsx";

                    sfd.FileName = nombre_nuevo_archivo;

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // guardar el nuevo archivo excel
                        archivoNuevo.SaveAs(sfd.FileName);
                        MessageBox.Show("Archivo Guardado");
                    }// si la ruta es correcta
                }// fin si tiene hojas
                else
                {
                    MessageBox.Show("No se puede generar el archivo");
                }// fin no tiene hojas


            }//fin archivo seleccionado
        }// fin cargar archivo

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void marcarInactividadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form politica_dias = new PoliticaDias();
            politica_dias.Show();
            this.Close();
        }
    }
}
