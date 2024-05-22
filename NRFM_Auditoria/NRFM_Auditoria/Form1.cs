using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using System.Diagnostics;

namespace NRFM_Auditoria
{
    public partial class Form1 : Form
    {

        // filtro utilizado para solo mostrar acrhivos de excel
        public const string RUTA_ARCHIVOS_RESPONSABLES = "Extracciones/Responsables";
        public const string RUTA_ARCHIVOS_BAJAS = "Extracciones/Bajas";

        public const string NOMBRE_CONCENTRADO_BAJAS = "Concentrado Bajas"; 

        public Form1()
        {
            InitializeComponent();
        }

        private void cargarArchivo_Click(object sender, EventArgs e)
        {
            //borramos el campo de texto de archivo
            archivoNombre.Text = "";
            // se selcciona el archivo a leer
            string ruta_archivo = FuncionesAuditoria.obtenerArchivoSeleccionado();

            archivoNombre.Text = ruta_archivo;

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

        private bool sePudoAbrirArchivo(string ruta)
        {
            /*
                Verifica que al ruta elegida, pueda ser abierta por ClosedXML
                retorna true en caso de ser posible, de lo contrario false
            */
            try
            {
                var archivo = new XLWorkbook(ruta);
                return true;
            }
            catch
            {
                MessageBox.Show("No se pudo abrir el archivo");
                return false;
            }
        }
        private void separarResp_Click(object sender, EventArgs e)
        {
            // se establece la ruta para las extracciones
            string ruta_carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RUTA_ARCHIVOS_RESPONSABLES);

            // en caso de que no exista folder de extraccion, crearlo
            if (!Directory.Exists(ruta_carpeta))
            {
                Directory.CreateDirectory(ruta_carpeta);
            }// fi no existe carpeta Extraccion 

            string ruta_archivo = archivoNombre.Text;
            if (ruta_archivo == String.Empty)
            {
                MessageBox.Show("Seleccione un archivo antes");
            }// fin if la ruta esta vacia
            else if (sePudoAbrirArchivo(ruta_archivo))
            {
                // obtenemos la anio y mes del sistema
                int sistema_month = DateTime.Now.Month;
                int sistema_year = DateTime.Now.Year;

                // cargamos el archivo seleccionado a un  objeto de closedXML
                var archivo = new XLWorkbook(ruta_archivo);

                // declaramos un diccionario para guardar los libros de cada responsable
                Dictionary<string, XLWorkbook> xl_responsable = new Dictionary<string, XLWorkbook>();

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
                            if (nombre_responsable != String.Empty)
                            {
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
                            }// fin if tiene nombre de responsable

                            index++;// pasa a la siguiente fila
                        }// fin mientras no llegue a celda 'A' vacia
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
            }// fin else if se pudo abrir el archivo

        }// fin boton separar responsables

        private void sepBajasApp_Click(object sender, EventArgs e)
        {
            // se establece la ruta para las extracciones
            string ruta_carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RUTA_ARCHIVOS_BAJAS);

            // en caso de que no exista folder de extraccion, crearlo
            if (!Directory.Exists(ruta_carpeta))
            {
                Directory.CreateDirectory(ruta_carpeta);
            }// fi no existe carpeta Extraccion 

            string ruta_archivo = archivoNombre.Text;
            if (ruta_archivo == String.Empty)
            {
                MessageBox.Show("Seleccione un archivo antes");
            }// fin if la ruta esta vacia

            else if (sePudoAbrirArchivo(ruta_archivo))
            {
                // obtenemos la anio y mes del sistema
                int sistema_month = DateTime.Now.Month;
                int sistema_year = DateTime.Now.Year;

                // cargamos el archivo seleccionado a un  objeto de closedXML
                var archivo = new XLWorkbook(ruta_archivo);

                // declaramos un diccionario para guardar los libros de cada responsable
                Dictionary<string, XLWorkbook> xl_bajas = new Dictionary<string, XLWorkbook>();

                // insertamos formato para concentrado de bajas



                // fin formato concentrado de bajas

                //creamos un archivo para concentrado de bajas
                xl_bajas[NOMBRE_CONCENTRADO_BAJAS] = new XLWorkbook();

                // iteramos en cada hoja del archivo
                foreach(IXLWorksheet hoja in archivo.Worksheets)
                {
                    int Fin_Cabecera = FuncionesAuditoria.encontrarCabecera(hoja);
                    if (Fin_Cabecera == -1)
                    {
                        continue; // si no se encontro cabecera que pase a la siguiente hoja
                    }// fin if no se encuentra la cabecera

                    int index = Fin_Cabecera + 1;

                    while (!hoja.Cell('A' + index.ToString()).IsEmpty())
                    {
                        if(hoja.Cell("A" + index.ToString()).Style.Fill.BackgroundColor != XLColor.FromIndex(64))
                        {
                            if (!xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheets.Contains(hoja.Name))
                            {
                                xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheets.Add(hoja.Name);
                                /*
                                    Genera el formato de cada cabecera de las hojas de excel 
                                */

                                string[] Textos = {
                                        "NR Finance Mexico",
                                        hoja.Name,
                                        "Certificacion de usuarios " + sistema_year.ToString(),
                                        "Bajas de usuarios"
                                    };

                                int fila = 1; char columna = 'D';
                                foreach (string texto in Textos)
                                {
                                    xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Value = texto;
                                    // que este en negritas
                                    xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.Bold = true;
                                    // definir el tamano
                                    xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.FontSize = 16;
                                    // centrar el texto
                                    xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

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
                                    xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Value = nombre_col;
                                    // damos formato a la casilla

                                    // fondo de la celda negro
                                    xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Fill.SetBackgroundColor(XLColor.FromTheme(XLThemeColor.Text1));
                                    // fuente color blanco
                                    xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.FontColor = XLColor.White;
                                    // en negritas
                                    xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.Bold = true;

                                    columna++;
                                }//fin while fin cabecera no este vacio
                                 // agregamos los filtros de las columnas
                                xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Range('A' + fila.ToString(), columna + fila.ToString()).SetAutoFilter(true);

                            }// fin if no existe hoja de concentrado de bajas del aplicativo actual

                            // Verficar si el libro existe, si tiene una hoja e insertar el formato de la cabecera
                            if (!xl_bajas.ContainsKey(hoja.Name)) {
                                xl_bajas[hoja.Name] = new XLWorkbook();
                            }// si no contiene hoja del aplicativo

                            if (!xl_bajas[hoja.Name].Worksheets.Contains(hoja.Name))
                            {
                                xl_bajas[hoja.Name].Worksheets.Add(hoja.Name);
                                /*
                                    Genera el formato de cada cabecera de las hojas de excel 
                                */

                                string[] Textos = {
                                        "NR Finance Mexico",
                                        hoja.Name,
                                        "Certificacion de usuarios " + sistema_year.ToString(),
                                        "Bajas de usuarios"
                                    };

                                int fila = 1; char columna = 'D';
                                foreach (string texto in Textos)
                                {
                                    xl_bajas[hoja.Name].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Value = texto;
                                    // que este en negritas
                                    xl_bajas[hoja.Name].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.Bold = true;
                                    // definir el tamano
                                    xl_bajas[hoja.Name].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.FontSize = 16;
                                    // centrar el texto
                                    xl_bajas[hoja.Name].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

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
                                    xl_bajas[hoja.Name].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Value = nombre_col;
                                    // damos formato a la casilla

                                    // fondo de la celda negro
                                    xl_bajas[hoja.Name].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Fill.SetBackgroundColor(XLColor.FromTheme(XLThemeColor.Text1));
                                    // fuente color blanco
                                    xl_bajas[hoja.Name].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.FontColor = XLColor.White;
                                    // en negritas
                                    xl_bajas[hoja.Name].Worksheet(hoja.Name).Cell(columna + fila.ToString()).Style.Font.Bold = true;

                                    columna++;
                                }//fin while fin cabecera no este vacio
                                 // agregamos los filtros de las columnas
                                xl_bajas[hoja.Name].Worksheet(hoja.Name).Range('A' + fila.ToString(), columna + fila.ToString()).SetAutoFilter(true);

                            }// fin if no existe la hoja de la aplicacion actual

                            char colActual = 'A';

                            // buscamos la cabecera en la hoja actual 
                            int Fin_Cabecera_HA = FuncionesAuditoria.encontrarCabecera(xl_bajas[hoja.Name].Worksheet(hoja.Name));
                            int Fin_Cabecera_CB = FuncionesAuditoria.encontrarCabecera(xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name));
                            // obtenemos el fin de los datos de la hoja del responsable
                            int finDatos_HA = FuncionesAuditoria.encontrarFinDatos(xl_bajas[hoja.Name].Worksheet(hoja.Name), Fin_Cabecera_HA);
                            int finDatos_CB = FuncionesAuditoria.encontrarFinDatos(xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name), Fin_Cabecera_CB);
                            while (!hoja.Cell(colActual + Fin_Cabecera.ToString()).IsEmpty())
                            {
                                // obtenemps el valor de la celda en el archivo de certificacion 
                                var celdaOrigen = hoja.Cell(colActual + index.ToString()).Value;
                                //colocamos el valor al final de los datos
                                xl_bajas[hoja.Name].Worksheet(hoja.Name).Cell(colActual + finDatos_HA.ToString()).Value = celdaOrigen;
                                xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Cell(colActual + finDatos_CB.ToString()).Value = celdaOrigen;

                                // agregamos bordes a la celda
                                xl_bajas[hoja.Name].Worksheet(hoja.Name).Cell(colActual + finDatos_HA.ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Cell(colActual + finDatos_CB.ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                colActual++;

                            }// fin while recorre una fila

                            // coloreamos las filas
                            xl_bajas[hoja.Name].Worksheet(hoja.Name).Row(finDatos_HA).Style.Fill.BackgroundColor = XLColor.Yellow;
                            xl_bajas[NOMBRE_CONCENTRADO_BAJAS].Worksheet(hoja.Name).Row(finDatos_CB).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }// si la celda tiene color
                        index++;
                    }// mientras la columna A no este vacia
                }// fin for hojas del archivo

                // creamos una carpeta especifica para este mes y anio
                string carpeta_mensual = ruta_carpeta + "/" + sistema_month.ToString() + "-" + sistema_year.ToString();
                if (!Directory.Exists(carpeta_mensual))
                {
                    Directory.CreateDirectory(carpeta_mensual);
                }// si no existe la carpeta mensual

                // comenzar a guardar los excel de responsables en la ruta carpeta
                foreach (KeyValuePair<string, XLWorkbook>aplicativo  in xl_bajas)
                {
                    // ajustamos las columnas al texto en cada hoja
                    foreach (IXLWorksheet hoja in aplicativo.Value.Worksheets)
                    {
                        hoja.Columns().AdjustToContents();
                    }// fin foreach ajustar columnas

                    aplicativo.Value.SaveAs(carpeta_mensual + "/" + aplicativo.Key + ".xlsx");
                }// fin for guardar archivos responsable

                MessageBox.Show("Proceso Terminado");
            }// if se pudo abrir el archivo
        }
    }
}
