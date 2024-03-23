using System.Diagnostics;

namespace NRFM_Auditoria
{
    public partial class Form1 : Form
    {

        // filtro utilizado para solo mostrar acrhivos de excel
        public const string FILTROS_ARCHIVOS_EXCEL = "Archivos Excel|*.xlsx;*.xlsm;*.csv;";
        public Form1()
        {
            InitializeComponent();
        }

        public string obtenerArchivoSeleccionado()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = FILTROS_ARCHIVOS_EXCEL;


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }// si el archivo esta ok

            return null;
        }
        private void cargarArchivo_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Hola soy el Anticristo666");
            string archivo = obtenerArchivoSeleccionado();
            Debug.WriteLine(archivo);
        }
    }
}
