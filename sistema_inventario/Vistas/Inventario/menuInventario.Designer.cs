using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using sistema_inventario;

namespace sistema_inventario.Vistas.Inventario
{
    partial class menuInventario
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
            // Botones principales del dashboard con imágenes
            Dictionary<string, Form> dashboardItems = new Dictionary<string, Form>()
            {
                { "Agregar producto", null },  // Asegúrate de que cada botón tenga un formulario específico
                {"Consultar inventario", null }, // Asegúrate de que cada botón tenga un formulario específico
                {"Ver movimientos de inventario", null } // Asegúrate de que cada botón tenga un formulario específico
            };

            string[] iconPaths = { "recursos/img/factura.png", "recursos/img/empleado.png", "recursos/img/proveedor.png" };
            int x = 20, y = 20;
            int i = 0;

            foreach (KeyValuePair<string, Form> item in dashboardItems)
            {
                Panel buttonPanel = new Panel();
                buttonPanel.Size = new Size(200, 100);
                buttonPanel.Location = new Point(x, y);
                buttonPanel.BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(buttonPanel); // Usamos el contentPanel desde el formulario padre

                PictureBox icon = new PictureBox();
                icon.Size = new Size(50, 50);
                icon.Location = new Point(75, 10);
                try
                {
                    icon.Image = Image.FromFile(iconPaths[i]);
                    icon.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch { }
                buttonPanel.Controls.Add(icon);

                Label btnLabel = new Label();
                btnLabel.Text = item.Key;
                btnLabel.Font = new Font("Arial", 10, FontStyle.Bold);
                btnLabel.AutoSize = false;
                btnLabel.TextAlign = ContentAlignment.MiddleCenter;
                btnLabel.Dock = DockStyle.Bottom;
                buttonPanel.Controls.Add(btnLabel);
                buttonPanel.Click += (sender, e) => CargarForma(item.Value);

                x += 220;
                if ((i + 1) % 4 == 0)
                {
                    x = 20;
                    y += 120;
                }

                i++;
            }
        }
        #endregion
    }
}