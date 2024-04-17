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
            cargarArchivo = new Button();
            label1 = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { separarResponsablesToolStripMenuItem });
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
            // cargarArchivo
            // 
            cargarArchivo.Location = new Point(51, 111);
            cargarArchivo.Name = "cargarArchivo";
            cargarArchivo.Size = new Size(170, 42);
            cargarArchivo.TabIndex = 1;
            cargarArchivo.Text = "Cargar Archivo";
            cargarArchivo.UseVisualStyleBackColor = true;
            cargarArchivo.Click += cargarArchivo_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(243, 41);
            label1.Name = "label1";
            label1.Size = new Size(252, 46);
            label1.TabIndex = 2;
            label1.Text = "Generar Totales";
            label1.Click += label1_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}