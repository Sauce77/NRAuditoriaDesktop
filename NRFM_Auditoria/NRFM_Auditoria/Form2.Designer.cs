namespace NRFM_Auditoria
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            separarResponsablesToolStripMenuItem = new ToolStripMenuItem();
            marcarInactividadToolStripMenuItem = new ToolStripMenuItem();
            cargarArchivo = new Button();
            label1 = new Label();
            archivoActual = new TextBox();
            label2 = new Label();
            generarTotales = new Button();
            barraProgresoTotales = new ProgressBar();
            labelProgreso = new Label();
            protegerArchivoToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { separarResponsablesToolStripMenuItem, marcarInactividadToolStripMenuItem, protegerArchivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // separarResponsablesToolStripMenuItem
            // 
            separarResponsablesToolStripMenuItem.Name = "separarResponsablesToolStripMenuItem";
            separarResponsablesToolStripMenuItem.Size = new Size(168, 24);
            separarResponsablesToolStripMenuItem.Text = "Separar Responsables";
            separarResponsablesToolStripMenuItem.Click += separarResponsablesToolStripMenuItem_Click;
            // 
            // marcarInactividadToolStripMenuItem
            // 
            marcarInactividadToolStripMenuItem.Name = "marcarInactividadToolStripMenuItem";
            marcarInactividadToolStripMenuItem.Size = new Size(146, 24);
            marcarInactividadToolStripMenuItem.Text = "Marcar Inactividad";
            marcarInactividadToolStripMenuItem.Click += marcarInactividadToolStripMenuItem_Click;
            // 
            // cargarArchivo
            // 
            cargarArchivo.Location = new Point(625, 134);
            cargarArchivo.Name = "cargarArchivo";
            cargarArchivo.Size = new Size(142, 27);
            cargarArchivo.TabIndex = 1;
            cargarArchivo.Text = "Cargar Archivo";
            cargarArchivo.UseVisualStyleBackColor = true;
            cargarArchivo.Click += cargarArchivo_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(263, 47);
            label1.Name = "label1";
            label1.Size = new Size(236, 41);
            label1.TabIndex = 2;
            label1.Text = "Generar Totales";
            label1.Click += label1_Click;
            // 
            // archivoActual
            // 
            archivoActual.Location = new Point(41, 134);
            archivoActual.Name = "archivoActual";
            archivoActual.Size = new Size(569, 27);
            archivoActual.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(41, 108);
            label2.Name = "label2";
            label2.Size = new Size(94, 23);
            label2.TabIndex = 4;
            label2.Text = "Mes Actual";
            // 
            // generarTotales
            // 
            generarTotales.Location = new Point(41, 193);
            generarTotales.Name = "generarTotales";
            generarTotales.Size = new Size(136, 29);
            generarTotales.TabIndex = 8;
            generarTotales.Text = "Generar Totales";
            generarTotales.UseVisualStyleBackColor = true;
            generarTotales.Click += generarTotales_Click;
            // 
            // barraProgresoTotales
            // 
            barraProgresoTotales.Location = new Point(71, 304);
            barraProgresoTotales.Name = "barraProgresoTotales";
            barraProgresoTotales.Size = new Size(643, 10);
            barraProgresoTotales.TabIndex = 9;
            barraProgresoTotales.Visible = false;
            // 
            // labelProgreso
            // 
            labelProgreso.AutoSize = true;
            labelProgreso.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelProgreso.Location = new Point(57, 278);
            labelProgreso.Name = "labelProgreso";
            labelProgreso.Size = new Size(68, 20);
            labelProgreso.TabIndex = 10;
            labelProgreso.Text = "Progreso";
            labelProgreso.Visible = false;
            // 
            // protegerArchivoToolStripMenuItem
            // 
            protegerArchivoToolStripMenuItem.Name = "protegerArchivoToolStripMenuItem";
            protegerArchivoToolStripMenuItem.Size = new Size(134, 24);
            protegerArchivoToolStripMenuItem.Text = "Proteger Archivo";
            protegerArchivoToolStripMenuItem.Click += protegerArchivoToolStripMenuItem_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelProgreso);
            Controls.Add(barraProgresoTotales);
            Controls.Add(generarTotales);
            Controls.Add(label2);
            Controls.Add(archivoActual);
            Controls.Add(label1);
            Controls.Add(cargarArchivo);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem separarResponsablesToolStripMenuItem;
        private Button cargarArchivo;
        private Label label1;
        private ToolStripMenuItem marcarInactividadToolStripMenuItem;
        private TextBox archivoActual;
        private Label label2;
        private Button generarTotales;
        private ProgressBar barraProgresoTotales;
        private Label labelProgreso;
        private ToolStripMenuItem protegerArchivoToolStripMenuItem;
    }
}