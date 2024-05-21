using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using System.Diagnostics;

namespace NRFM_Auditoria
{
    public partial class Form1 : Form
    {

        // filtro utilizado para solo mostrar acrhivos de excel
        public const string RUTA_ARCHIVOS_RESPONSABLES = "Extracciones";

        public Form1()
        {
            InitializeComponent();
        }

        private void cargarArchivo_Click(object sender, EventArgs e)
        {
            // obtenemos la anio y mes del sistema
            int sistema_month = DateTime.Now.Month;
            int sistema_year = DateTime.Now.Year;

            // se establece la ruta para las extracciones
            string ruta_carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RUTA_ARCHIVOS_RESPONSABLES);

            // en caso de que no exista folder de extraccion, crearlo
            if (!Directory.Exists(ruta_carpeta))
            {
                Directory.CreateDirectory(ruta_carpeta);
            }// fi no existe carpeta Extraccion 

            // se selcciona el archivo a leer
            string ruta_archivo = FuncionesAuditoria.obtenerArchivoSeleccionado();

            if (ruta_archivo != null)
            {
                // cargamos el archivo seleccionado a un  objeto de closedXML
                var archivo = new XLWorkbook(ruta_archivo);

                // declaramos un diccionario para guardar los libros de cada responsable
                Dictionary<string, XLWorkbook> xl_responsable = new Dictionary<string, XLWorkbook>();

                // limpiamos la lista de los procesos terminados
                listaProceso.Items.Clear();

                foreach (IXLWorksheet hoja in archivo.Worksheets)
                {
                    int Fin_Cabecera = FuncionesAuditoria.encontrarCabecera(hoja);
                    if (Fin_Cabecera == -1)
                    {
                        continue; // si no se encontro cabecera que pase a la siguiente hoja
                    }// fin if no se encuentra la cabecera 
                    else
                    {
                        // buscamos la columna de responsable
                        char colResponsable = FuncionesAuditoria.encontrarColResponsable(hoja, Fin_Cabecera);
                        if (colResponsable == '-')
                        {
                            MessageBox.Show("No se ha encontrado la columna de responsable en " + hoja.Name);
                            continue; // si no se encuentra la columna que pase a la siguiente hoja
                        }//fin if no encuentra la columna Responsable

                        // comenzamos a leer la informacion
                        int index = Fin_Cabecera + 1; // index sera quien recorra las filas del archivo
                        while (!hoja.Cell("A" + index.ToString()).IsEmpty())
                        {
                            // verificamos que la celda en cuestion no color
                            while (hoja.Cell("A" + index.ToString()).Style.Fill.BackgroundColor != XLColor.FromIndex(64))
                            {
                                index++; // si la celda tiene color que la ignore
                            }// fin si la celda tiene color

                            string nombre_responsable = hoja.Cell(colResponsable + index.ToString()).Value.ToString();
                            // se escribe el nombre del responsable en maysuculas y con un solo espacio para separar los nombres
                            nombre_responsable = FuncionesAuditoria.estandarizarNombre(nombre_responsable);

                            if (!xl_responsable.ContainsKey(nombre_responsable))
                            {
                                xl_responsable[nombre_responsable] = new XLWorkbook();
                            }// fin if no existe workbook de responsable

                            if (!xl_responsable[nombre_responsable].Worksheets.Contains(hoja.Name))
                            {
                                xl_responsable[nombre_responsable].Worksheets.Add(hoja.Name);
                                /*
                                    Genera el formato de cada cabecera de las hojas de excel 
                                */

                                string[] Textos = {
                                    "NR Finance Mexico",
                                    hoja.Name,
                                    "Certificacion de usuarios " + sistema_year.ToString(),
                                    "Reporte de usuarios"
                                };

                                int fila = 1; char columna = 'D';
                                foreach (string texto in Textos)
                                {
                                    xl_responsable[nombre_responsable].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Value = texto;
                                    // que este en negritas
                                    xl_responsable[nombre_responsable].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.Bold = true;
                                    // definir el tamano
                                    xl_responsable[nombre_responsable].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.FontSize = 16;
                                    // centrar el texto
                                    xl_responsable[nombre_responsable].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                                    fila++;
                                }// fin foreach escribir texto

                                /*
                                    Agregar el nombre de las columnas 
                                */
                                columna = 'A';
                                while (!hoja.Cell(columna + Fin_Cabecera.ToString()).IsEmpty())
                                {
                                    // obtenemos el nombre de la columna del documento original
                                    string nombre_col = hoja.Cell(columna + Fin_Cabecera.ToString()).Value.ToString();
                                    // se lo asignamos en la hoja actual
                                    xl_responsable[nombre_responsable].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Value = nombre_col;
                                    // damos formato a la casilla

                                    // fondo de la celda negro
                                    xl_responsable[nombre_responsable].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Fill.SetBackgroundColor(XLColor.FromTheme(XLThemeColor.Text1));
                                    // fuente color blanco
                                    xl_responsable[nombre_responsable].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.FontColor = XLColor.White;
                                    // en negritas
                                    xl_responsable[nombre_responsable].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.Bold = true;

                                    columna++;
                                }//fin while fin cabecera no este vacio
                                // agregamos los filtros de las columnas
                                xl_responsable[nombre_responsable].Worksheet(hoja.Name).Range('A' + fila.ToString(), columna + fila.ToString()).SetAutoFilter(true);

                            }// fin if no existe la hoja de la aplicacion actual

                            // lo utilizamos para iterar los campos de una fila
                            char colActual = 'A';

                            // buscamos la cabecera en la hoja actual 
                            int Fin_Cabecera_HR = FuncionesAuditoria.encontrarCabecera(xl_responsable[nombre_responsable].Worksheet(hoja.Name));
                            // obtenemos el fin de los datos de la hoja del responsable
                            int finDatos = FuncionesAuditoria.encontrarFinDatos(xl_responsable[nombre_responsable].Worksheet(hoja.Name), Fin_Cabecera_HR);
                            while (!hoja.Cell(colActual + Fin_Cabecera.ToString()).IsEmpty())
                            {
                                // obtenemps el valor de la celda en el archivo de certificacion 
                                var celdaOrigen = hoja.Cell(colActual + index.ToString()).Value;
                                //colocamos el valor al final de los datos
                                xl_responsable[nombre_responsable].Worksheet(hoja.Name).Cell(colActual + finDatos.ToString()).Value = celdaOrigen;

                                // agregamos bordes a la celda
                                xl_responsable[nombre_responsable].Worksheet(hoja.Name).Cell(colActual + finDatos.ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                colActual++;

                            }// fin while recorre una fila


                            index++;// pasa a la siguiente fila
                        }// fin mientras no llegue a celda 'A' vacia

                        listaProceso.Items.Add(hoja.Name + " - Lista.");
                    }// fin else se encuentra la cabecera
                }// fin for each para recorrer hojas

                // creamos una carpeta especifica para este mes y anio
                string carpeta_mensual = ruta_carpeta + "/" + sistema_month.ToString() + "-" + sistema_year.ToString();
                if (!Directory.Exists(carpeta_mensual))
                {
                    Directory.CreateDirectory(carpeta_mensual);
                }// si no existe la carpeta mensual

                // comenzar a guardar los excel de responsables en la ruta carpeta
                foreach (KeyValuePair<string, XLWorkbook> responsable in xl_responsable)
                {
                    // ajustamos las columnas al texto en cada hoja
                    foreach (IXLWorksheet aplicativo in responsable.Value.Worksheets)
                    {
                        aplicativo.Columns().AdjustToContents();
                    }// fin foreach ajustar columnas

                    responsable.Value.SaveAs(carpeta_mensual + "/" + responsable.Key + ".xlsx");
                }// fin for guardar archivos responsable

                MessageBox.Show("Proceso Terminado");
            }// fin if ruta de archivo seleccionada

        }// fin cargar archivo

        private void separarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void generarTotalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form generar_totales = new Form2();
            generar_totales.Show();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
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
