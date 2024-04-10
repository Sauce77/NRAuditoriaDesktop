namespace NRFM_Auditoria
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            generarTotalesToolStripMenuItem = new ToolStripMenuItem();
            cargarArchivo = new Button();
            listaProceso = new ListBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { generarTotalesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // generarTotalesToolStripMenuItem
            // 
            generarTotalesToolStripMenuItem.Name = "generarTotalesToolStripMenuItem";
            generarTotalesToolStripMenuItem.Size = new Size(126, 24);
            generarTotalesToolStripMenuItem.Text = "Generar Totales";
            generarTotalesToolStripMenuItem.Click += generarTotalesToolStripMenuItem_Click;
            // 
            // cargarArchivo
            // 
            cargarArchivo.Location = new Point(50, 39);
            cargarArchivo.Name = "cargarArchivo";
            cargarArchivo.Size = new Size(138, 49);
            cargarArchivo.TabIndex = 1;
            cargarArchivo.Text = "Cargar Archivo";
            cargarArchivo.UseVisualStyleBackColor = true;
            cargarArchivo.Click += cargarArchivo_Click;
            // 
            // listaProceso
            // 
            listaProceso.FormattingEnabled = true;
            listaProceso.Location = new Point(50, 108);
            listaProceso.Name = "listaProceso";
            listaProceso.Size = new Size(670, 284);
            listaProceso.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listaProceso);
            Controls.Add(cargarArchivo);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private Button cargarArchivo;
        private ListBox listaProceso;
        private ToolStripMenuItem generarTotalesToolStripMenuItem;
    }
}
