namespace NRFM_Auditoria
{
    partial class ProtegerArchivos
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
            archivosSeleccionados = new TextBox();
            botonProteger = new Button();
            Password = new MaskedTextBox();
            label1 = new Label();
            label2 = new Label();
            botonQuitarProteccion = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // archivosSeleccionados
            // 
            archivosSeleccionados.Location = new Point(57, 114);
            archivosSeleccionados.Name = "archivosSeleccionados";
            archivosSeleccionados.Size = new Size(554, 27);
            archivosSeleccionados.TabIndex = 0;
            // 
            // botonProteger
            // 
            botonProteger.Location = new Point(553, 199);
            botonProteger.Name = "botonProteger";
            botonProteger.Size = new Size(94, 29);
            botonProteger.TabIndex = 1;
            botonProteger.Text = "Proteger";
            botonProteger.UseVisualStyleBackColor = true;
            botonProteger.Click += botonProteger_Click;
            // 
            // Password
            // 
            Password.Location = new Point(57, 201);
            Password.Name = "Password";
            Password.Size = new Size(312, 27);
            Password.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(265, 49);
            label1.Name = "label1";
            label1.Size = new Size(252, 38);
            label1.TabIndex = 4;
            label1.Text = "Proteger Archivos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(78, 167);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 5;
            label2.Text = "Password:";
            // 
            // botonQuitarProteccion
            // 
            botonQuitarProteccion.Location = new Point(553, 256);
            botonQuitarProteccion.Name = "botonQuitarProteccion";
            botonQuitarProteccion.Size = new Size(145, 29);
            botonQuitarProteccion.TabIndex = 6;
            botonQuitarProteccion.Text = "Quitar Proteccion";
            botonQuitarProteccion.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(639, 112);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 7;
            button1.Text = "Cargar Archivo(s)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ProtegerArchivos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(botonQuitarProteccion);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Password);
            Controls.Add(botonProteger);
            Controls.Add(archivosSeleccionados);
            Name = "ProtegerArchivos";
            Text = "ProtegerArchivos";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox archivosSeleccionados;
        private Button botonProteger;
        private MaskedTextBox Password;
        private Label label1;
        private Label label2;
        private Button botonQuitarProteccion;
        private Button button1;
    }
}