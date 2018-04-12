namespace _LFP_Proyecto1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("raiz");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Consola = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Arbol = new System.Windows.Forms.TreeView();
            this.Iconos = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // Consola
            // 
            this.Consola.BackColor = System.Drawing.SystemColors.WindowText;
            this.Consola.ForeColor = System.Drawing.Color.Lime;
            this.Consola.Location = new System.Drawing.Point(12, 467);
            this.Consola.Name = "Consola";
            this.Consola.Size = new System.Drawing.Size(834, 317);
            this.Consola.TabIndex = 0;
            this.Consola.Text = "";
            this.Consola.TextChanged += new System.EventHandler(this.Consola_TextChanged);
            this.Consola.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Consola_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 451);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Consola:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Arbol
            // 
            this.Arbol.Location = new System.Drawing.Point(12, 12);
            this.Arbol.Name = "Arbol";
            treeNode1.Name = "raiz";
            treeNode1.Text = "raiz";
            this.Arbol.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.Arbol.Size = new System.Drawing.Size(834, 436);
            this.Arbol.TabIndex = 2;
            this.Arbol.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Arbol_AfterSelect);
            // 
            // Iconos
            // 
            this.Iconos.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Iconos.ImageStream")));
            this.Iconos.TransparentColor = System.Drawing.Color.Transparent;
            this.Iconos.Images.SetKeyName(0, "root.png");
            this.Iconos.Images.SetKeyName(1, "folder.png");
            this.Iconos.Images.SetKeyName(2, "open.png");
            this.Iconos.Images.SetKeyName(3, "doc.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 796);
            this.Controls.Add(this.Arbol);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Consola);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Consola;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView Arbol;
        private System.Windows.Forms.ImageList Iconos;
    }
}

