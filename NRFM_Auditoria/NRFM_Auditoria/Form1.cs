using ClosedXML.Excel;
using System.Diagnostics;

namespace NRFM_Auditoria
{
    public partial class Form1 : Form
    {

        // filtro utilizado para solo mostrar acrhivos de excel
        public const string FILTROS_ARCHIVOS_EXCEL = "Archivos Excel|*.xlsx;*.xlsm;*.csv;";
        public const string NOMBRE_COL_RESPONSABLE = "Responsable";

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
        private void cargarArchivo_Click(object sender, EventArgs e)
        {

            string ruta_archivo = obtenerArchivoSeleccionado();

            if(ruta_archivo != null)
            {
                // cargamos el archivo seleccionado a un  objeto de closedXML
                var archivo = new XLWorkbook(ruta_archivo);

                foreach(IXLWorksheet hoja in archivo.Worksheets)
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

                        Debug.WriteLine(hoja.Name + " " + colResponsable);

                        int index = Fin_Cabecera + 1; // a partir de esta fila son todos los datos


                    }// fin else se encuentra la cabecera
                }// fin for each para recorrer hojas

                MessageBox.Show("Proceso Terminado");
            }// fin if ruta de archivo seleccionada
            

        }
    }
}
