namespace NRFM_Auditoria
{
    partial class PoliticaDias
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
            label1 = new Label();
            archivoNombre = new TextBox();
            cargarArchivo = new Button();
            menuStrip1 = new MenuStrip();
            contarTotalesToolStripMenuItem = new ToolStripMenuItem();
            separarResponsabesToolStripMenuItem = new ToolStripMenuItem();
            limiteUA = new TextBox();
            label2 = new Label();
            aplicarFiltro = new Button();
            barraProgresoBajas = new ProgressBar();
            labelProgreso = new Label();
            protegerArchivosToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(162, 28);
            label1.Name = "label1";
            label1.Size = new Size(460, 41);
            label1.TabIndex = 0;
            label1.Text = "Marcar Cuentas por Inactividad";
            // 
            // archivoNombre
            // 
            archivoNombre.Location = new Point(53, 95);
            archivoNombre.Name = "archivoNombre";
            archivoNombre.Size = new Size(524, 27);
            archivoNombre.TabIndex = 1;
            // 
            // cargarArchivo
            // 
            cargarArchivo.Location = new Point(596, 95);
            cargarArchivo.Name = "cargarArchivo";
            cargarArchivo.Size = new Size(152, 29);
            cargarArchivo.TabIndex = 2;
            cargarArchivo.Text = "Cargar Archivo";
            cargarArchivo.UseVisualStyleBackColor = true;
            cargarArchivo.Click += cargarArchivo_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { contarTotalesToolStripMenuItem, separarResponsabesToolStripMenuItem, protegerArchivosToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // contarTotalesToolStripMenuItem
            // 
            contarTotalesToolStripMenuItem.Name = "contarTotalesToolStripMenuItem";
            contarTotalesToolStripMenuItem.Size = new Size(118, 24);
            contarTotalesToolStripMenuItem.Text = "Contar Totales";
            contarTotalesToolStripMenuItem.Click += contarTotalesToolStripMenuItem_Click;
            // 
            // separarResponsabesToolStripMenuItem
            // 
            separarResponsabesToolStripMenuItem.Name = "separarResponsabesToolStripMenuItem";
            separarResponsabesToolStripMenuItem.Size = new Size(168, 24);
            separarResponsabesToolStripMenuItem.Text = "Separar Responsables";
            separarResponsabesToolStripMenuItem.Click += separarResponsabesToolStripMenuItem_Click;
            // 
            // limiteUA
            // 
            limiteUA.Location = new Point(311, 159);
            limiteUA.Name = "limiteUA";
            limiteUA.Size = new Size(125, 27);
            limiteUA.TabIndex = 4;
            limiteUA.Text = "60";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(53, 155);
            label2.Name = "label2";
            label2.Size = new Size(252, 31);
            label2.TabIndex = 5;
            label2.Text = "Limite de Acceso (días)";
            // 
            // aplicarFiltro
            // 
            aplicarFiltro.Location = new Point(470, 159);
            aplicarFiltro.Name = "aplicarFiltro";
            aplicarFiltro.Size = new Size(107, 31);
            aplicarFiltro.TabIndex = 6;
            aplicarFiltro.Text = "Aplicar";
            aplicarFiltro.UseVisualStyleBackColor = true;
            aplicarFiltro.Click += aplicarFiltro_Click;
            // 
            // barraProgresoBajas
            // 
            barraProgresoBajas.Location = new Point(81, 297);
            barraProgresoBajas.Name = "barraProgresoBajas";
            barraProgresoBajas.Size = new Size(646, 13);
            barraProgresoBajas.TabIndex = 7;
            barraProgresoBajas.Visible = false;
            // 
            // labelProgreso
            // 
            labelProgreso.AutoSize = true;
            labelProgreso.Location = new Point(65, 265);
            labelProgreso.Name = "labelProgreso";
            labelProgreso.Size = new Size(68, 20);
            labelProgreso.TabIndex = 8;
            labelProgreso.Text = "Progreso";
            labelProgreso.Visible = false;
            // 
            // protegerArchivosToolStripMenuItem
            // 
            protegerArchivosToolStripMenuItem.Name = "protegerArchivosToolStripMenuItem";
            protegerArchivosToolStripMenuItem.Size = new Size(140, 24);
            protegerArchivosToolStripMenuItem.Text = "Proteger Archivos";
            protegerArchivosToolStripMenuItem.Click += protegerArchivosToolStripMenuItem_Click;
            // 
            // PoliticaDias
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelProgreso);
            Controls.Add(barraProgresoBajas);
            Controls.Add(aplicarFiltro);
            Controls.Add(label2);
            Controls.Add(limiteUA);
            Controls.Add(cargarArchivo);
            Controls.Add(archivoNombre);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "PoliticaDias";
            Text = "PoliticaDias";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox archivoNombre;
        private Button cargarArchivo;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem contarTotalesToolStripMenuItem;
        private ToolStripMenuItem separarResponsabesToolStripMenuItem;
        private TextBox limiteUA;
        private Label label2;
        private Button aplicarFiltro;
        private ProgressBar barraProgresoBajas;
        private Label labelProgreso;
        private ToolStripMenuItem protegerArchivosToolStripMenuItem;
    }
}