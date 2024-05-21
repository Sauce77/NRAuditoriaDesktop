﻿using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRFM_Auditoria
{
    internal class FuncionesAuditoria
    {

        public const string FILTROS_ARCHIVOS_EXCEL = "Archivos Excel|*.xlsx;*.xlsm;*.csv;";

        public const string NOMBRE_COL_RESPONSABLE = "Responsable";

        public const string NOMBRE_COL_ULTIMO_ACCESO = "Ultimo Acceso";

        public const int MAX_FILA_CABECERA = 1000;

        /*
                COMIENZAN FUNCIONES PARA LA CERTIFICACION
        */
        public static string obtenerArchivoSeleccionado()
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
        }// fin obtenerArchivoSeleccionado

        public static int encontrarCabecera(IXLWorksheet hoja)
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
                MessageBox.Show("No se ha encontrado la cabecera en " + hoja.Name);
                return -1;
            }// fin maximo de columnas cabecera
            return index;
        }// fin encontrar cabecera


        public static char encontrarColResponsable(IXLWorksheet hoja, int filaEncabezado)
        {
            /*
                Indicando la fila donde supuestamente se encuentra el encabezado de los datos (los titulos), recorre las columnas
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
        }// fin encontrarColResponsable

        public char encontrarColUltimoAcceso(IXLWorksheet hoja, int filaEncabezado)
        {
            /*
                Indicando la fila donde supuestamente se encuentra el encabezado de los datos (los titulos), recorre las columnas
                hasta encontrar la celda cuyo valor sea igual a NOMBRE_COL_ULTIMO_ACCESO , devolviendo la letra de dicha columna. En 
                caso de no encontrarla retorna "-".
            */
            // buscamos en donde se encuentra la columna de responsables
            char colResponsable = 'A';

            while (!hoja.Cell(colResponsable + filaEncabezado.ToString()).IsEmpty())
            {
                //Debug.WriteLine(hoja.Cell(colResponsable + index.ToString()).Value.ToString());
                if (hoja.Cell(colResponsable + filaEncabezado.ToString()).Value.ToString() == NOMBRE_COL_ULTIMO_ACCESO)
                {
                    return colResponsable;
                }//fin if celda con valor Responsable
                colResponsable++;
            }//fin while encontrar col responsable
            return '-';
        }
        public static int finColumnasArchivo(IXLWorksheet hoja, int filaEncabezado)
        {
            /*
                Retorna la columna final del encabezado de los datos en el archivo
            */
            char columna = 'A';

            while (!hoja.Cell(columna + filaEncabezado.ToString()).Style.Fill.BackgroundColor.Equals(XLColor.FromTheme(XLThemeColor.Text1)))
            {
                columna++;
            }//fin while encontrar col responsable
            return columna;
        }// fin finColumnasArchivo
        public static bool archivoTieneHojas(XLWorkbook archivo)
        {
            /*
                Recorre las hojas del archivo, a la primera retorna true.
            */
            foreach (IXLWorksheet hoja in archivo.Worksheets)
            {
                return true;
            }// fin for recorre las hojas del archivo nuevo
            return false;
        }// fin archivoTieneHojas

        public static int encontrarFinDatos(IXLWorksheet hoja, int FinCabecera)
        {
            /*
                Retorna el numero de la fila donde terminan los datos.
            */

            int index = FinCabecera;
            while (!hoja.Cell("A" + index.ToString()).IsEmpty())
            {
                index++;
            }
            return index;
        }// fin encontrarFinDatos

        public static string estandarizarNombre(string nombre)
        {
            /*
                Elimina los espacios de mas en el nombre del responsable. Separa los nombres por espacio
                y los reagrupa en una nueva cadena.
            */
            string[] Nombres = nombre.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            string resultado = string.Join(" ", Nombres);
            return resultado.ToUpper();
        }// fin estandarizarNombres
    }
}
