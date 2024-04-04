using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using System.Diagnostics;

namespace NRFM_Auditoria
{
    public partial class Form1 : Form
    {

        // filtro utilizado para solo mostrar acrhivos de excel
        public const string FILTROS_ARCHIVOS_EXCEL = "Archivos Excel|*.xlsx;*.xlsm;*.csv;";
        public const string NOMBRE_COL_RESPONSABLE = "Responsable";
        public const string RUTA_ARCHIVOS_RESPONSABLES = "Extracciones";

        public const int MAX_FILA_CABECERA = 1000;
        public Form1()
        {
            InitializeComponent();
        }

        public string obtenerArchivoSeleccionado()
        {
            /*
                Obtiene la ruta del archivo seleccionada en el explorador de archivos desplegado, si no se selecciona
                un archivo retorna null
            */
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = FILTROS_ARCHIVOS_EXCEL;


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }// si el archivo esta ok

            return null;
        }

        public int encontrarCabecera(IXLWorksheet hoja)
        {
            /*
                Busca en el archivo la primera celda de color negro que se encuentre en la primera columna para determinar que 
                es el fin de la cabecera, o en su defecto el encabezado de los datos. Si no encuentra una cabecera en el numero maximo
                de filas, entonces regresa -1.
            */
            int index = 1;

            // hay que buscar el fin de cabecera
            while (!hoja.Cell("A" + index.ToString()).Style.Fill.BackgroundColor.Equals(XLColor.FromTheme(XLThemeColor.Text1)) && MAX_FILA_CABECERA > index)
            {
                index++;
            }//fin while encontrar cabecera

            //si no se encuentra la cabecera en el limite, se omite la hoja
            if (MAX_FILA_CABECERA <= index)
            {
                return -1;
            }// fin maximo de columnas cabecera
            return index;
        }

        public char encontrarColResponsable(IXLWorksheet hoja,int filaEncabezado)
        {
            /*
                Inidcado la fila donde supuestamente se encuentra el encabezado de los datos (los titulos), recorre las columnas
                hasta encontrar la celda cuyo valor sea igual a NOMBRE_COL_RESPONSABLE, devolviendo la letra de dicha columna. En 
                caso de no encontrarla retorna "-".
            */
            // buscamos en donde se encuentra la columna de responsables
            char colResponsable = 'A';

            while (!hoja.Cell(colResponsable + filaEncabezado.ToString()).IsEmpty())
            {
                //Debug.WriteLine(hoja.Cell(colResponsable + index.ToString()).Value.ToString());
                if (hoja.Cell(colResponsable + filaEncabezado.ToString()).Value.ToString() == NOMBRE_COL_RESPONSABLE)
                {
                    return colResponsable;
                }//fin if celda con valor Responsable
                colResponsable++;
            }//fin while encontrar col responsable
            return '-';
        }

        public int finColumnasArchivo(IXLWorksheet hoja,int filaEncabezado)
        {
            /*
                Retorna el numero de columnas que tiene el archivo 
            */
            char columna = 'A';

            while (!hoja.Cell(columna + filaEncabezado.ToString()).Style.Fill.BackgroundColor.Equals(XLColor.FromTheme(XLThemeColor.Text1)))
            {
                columna++;
            }//fin while encontrar col responsable
            return columna;
        }
        public bool archivoTieneHojas(XLWorkbook archivo)
        {
            /*
                Recorre las hojas del archivo, a la primera retorna true.
            */
            foreach (IXLWorksheet hoja in archivo.Worksheets)
            {
                return true;
            }// fin for recorre las hojas del archivo nuevo
            return false;
        }

        public int encontrarFinDatos(IXLWorksheet hoja,int FinCabecera)
        {
            /*
                Retorna el numero de la fila donde terminan los datos.
            */

            int index = FinCabecera;
            while(!hoja.Cell("A" + index.ToString()).IsEmpty())
            {
                index++;
            }
            return index;
        }

        public string estandarizarNombre(string nombre)
        {
            /*
                Elimina los espacios de mas en el nombre del responsable. Separa los nombres por espacio
                y los reagrupa en una nueva cadena.
            */
            string[] Nombres = nombre.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            string resultado = string.Join(" ", Nombres);
            return resultado.ToUpper();
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
            string ruta_archivo = obtenerArchivoSeleccionado();

            if(ruta_archivo != null)
            {
                // cargamos el archivo seleccionado a un  objeto de closedXML
                var archivo = new XLWorkbook(ruta_archivo);

                // declaramos un diccionario para guardar los libros de cada responsable
                Dictionary<string,XLWorkbook> xl_responsable = new Dictionary<string, XLWorkbook>();

                // limpiamos la lista de los procesos terminados
                listaProceso.Items.Clear();

                foreach (IXLWorksheet hoja in archivo.Worksheets)
                {
                    int Fin_Cabecera = encontrarCabecera(hoja);
                    if(Fin_Cabecera == -1)
                    {
                        MessageBox.Show("No se ha encontrado la cabecera en " + hoja.Name);
                        continue; // si no se encontro cabecera que pase a la siguiente hoja
                    }// fin if no se encuentra la cabecera 
                    else
                    {
                        // buscamos la columna de responsable
                        char colResponsable = encontrarColResponsable(hoja, Fin_Cabecera);
                        if(colResponsable == '-')
                        {
                            MessageBox.Show("No se ha encontrado la columna " + NOMBRE_COL_RESPONSABLE + " en " + hoja.Name);
                            continue; // si no se encuentra la columna que pase a la siguiente hoja
                        }//fin if no encuentra la columna Responsable

                        // comenzamos a leer la informacion
                        int index = Fin_Cabecera + 1; // index sera quien recorra las filas del archivo
                        while(!hoja.Cell("A" + index.ToString()).IsEmpty())
                        {
                            string nombre_responsable = hoja.Cell(colResponsable + index.ToString()).Value.ToString();
                            // se escribe el nombre del responsable en maysuculas y con un solo espacio para separar los nombres
                            nombre_responsable = estandarizarNombre(nombre_responsable);

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

                            }// fin if no existe la hoja de la aplicacion actual

                            // lo utilizamos para iterar los campos de una fila
                            char colActual = 'A';

                            // revisa como vas a insertar los nombres de las columnas

                            // obtenemos el fin de los datos de la hoja del responsable
                            int finDatos = encontrarFinDatos(xl_responsable[nombre_responsable].Worksheet(hoja.Name), Fin_Cabecera);
                            while (colActual <= colResponsable)
                            {
                                // obtenemps el valor de la celda en el archivo de certificacion 
                                string valor = hoja.Cell(colActual + index.ToString()).Value.ToString();
  
                                //colocamos el valor al final de los datos
                                xl_responsable[nombre_responsable].Worksheet(hoja.Name).Cell(colActual + finDatos.ToString()).Value = valor;

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
                if(!Directory.Exists(carpeta_mensual))
                {
                    Directory.CreateDirectory(carpeta_mensual);
                }// si no existe la carpeta mensual

                // comenzar a guardar los excel de responsables en la ruta carpeta
                foreach (KeyValuePair<string, XLWorkbook>responsable in xl_responsable)
                {
                    // ajustamos las columnas al texto en cada hoja
                    foreach(IXLWorksheet aplicativo in responsable.Value.Worksheets)
                    {
                        aplicativo.Columns().AdjustToContents();
                    }// fin foreach ajustar columnas

                    responsable.Value.SaveAs(carpeta_mensual + "/" + responsable.Key + ".xlsx");
                }// fin for guardar archivos responsable

                    MessageBox.Show("Proceso Terminado");
            }// fin if ruta de archivo seleccionada

        }// fin cargar archivo
    }
}
