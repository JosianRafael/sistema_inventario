using System;
using System.Drawing;
using System.Windows.Forms;
using sistema_inventario.vistaCliente;
using sistema_inventario.vistaFactura;
using sistema_inventario.vistaProducto;
using sistema_inventario.vistaProveedor;
using sistema_inventario.vistaEmpleado;
using sistema_inventario.vistaInventario;
using sistema_inventario.vistaResumendia;
using sistema_inventario.vistaReporte;
using sistema_inventario.FCliente;
using sistema_inventario.Factualizarcliente;
using sistema_inventario.Fconsultarcliente;

namespace sistema_inventario.vistaCliente
{
    public partial class menuesCliente : Form
    {
        private Form currentForm; // Para manejar el formulario actual
        public void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario (MANTENIDO EXACTAMENTE IGUAL)
            this.ClientSize = new Size(1200, 700);
            this.Text = "📦💰 Sistema de Inventario y Facturación By: Josian Rafael, Felix Mendoza, Billy Smith";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Panel superior (Barra morada) - Fijo (MANTENIDO EXACTO)
            Panel topBar = new Panel();
            topBar.Dock = DockStyle.Top;
            topBar.Height = 50;
            topBar.BackColor = Color.FromArgb(91, 63, 144);
            this.Controls.Add(topBar);

            // Título (MANTENIDO EXACTO)
            Label titleLabel = new Label();
            titleLabel.Text = "📦💰 Sistema de Inventario y Facturación";
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 15);
            topBar.Controls.Add(titleLabel);

            // Etiqueta para la fecha (MANTENIDO EXACTO)
            Label dateLabel = new Label();
            dateLabel.ForeColor = Color.White;
            dateLabel.Font = new Font("Arial", 11, FontStyle.Bold);
            dateLabel.AutoSize = true;
            dateLabel.Location = new Point(titleLabel.Right + 10, 15);
            dateLabel.Text = "                                                                                                                                               Fecha actual: " + DateTime.Now.ToString("dd/MM/yyyy");
            topBar.Controls.Add(dateLabel);

            // Panel inferior (Footer) - Fijo (MANTENIDO EXACTO)
            Panel bottomBar = new Panel();
            bottomBar.Dock = DockStyle.Bottom;
            bottomBar.Height = 50;
            bottomBar.BackColor = Color.FromArgb(91, 63, 144);
            this.Controls.Add(bottomBar);

            Label footerLabel = new Label();
            footerLabel.Text = $"© {DateTime.Now.Year} Creado por: Josian Rafael, Felix Mendoza, Billy Smith";
            footerLabel.ForeColor = Color.White;
            footerLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            footerLabel.AutoSize = true;
            footerLabel.Location = new Point((bottomBar.Width - footerLabel.Width) / 2, 15);
            bottomBar.Controls.Add(footerLabel);

            // Panel lateral (Sidebar) - Fijo (MANTENIDO EXACTO)
            Panel sidebar = new Panel();
            sidebar.Location = new Point(0, topBar.Height);
            sidebar.Width = 250;
            sidebar.Height = this.ClientSize.Height - topBar.Height - bottomBar.Height;
            sidebar.BackColor = Color.FromArgb(40, 40, 60);
            this.Controls.Add(sidebar);

            Label userLabel = new Label();
            userLabel.Text = "  -- Menú principal --    \n-------------------------------";
            userLabel.ForeColor = Color.White;
            userLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            userLabel.AutoSize = true;
            userLabel.Location = new Point(40, 20);
            sidebar.Controls.Add(userLabel);

            string[] menuItems = { "📊 Panel de control", "📝 Crear Factura", "🤝 Proveedores", "👥 Clientes", "📦 Producto", "👤 Empleado", "🛍️ Inventario", "📊 Ventas", "📈 Reportes", "👫 Participantes" };
            int yOffset = 60;
            foreach (string item in menuItems)
            {
                Button btn = new Button();
                btn.Text = item;
                btn.Size = new Size(200, 40);
                btn.Location = new Point(25, yOffset);
                btn.BackColor = Color.FromArgb(60, 60, 80);
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Font = new Font("Arial", 10, FontStyle.Bold);
                btn.TextAlign = ContentAlignment.MiddleLeft;

                // MANEJADOR DE EVENTOS MODIFICADO (SOLO ESTA PARTE)
                btn.Click += (sender, e) =>
                {
                    if (currentForm != null && !currentForm.IsDisposed)
                    {
                        currentForm.Close();
                    }

                    switch (item)
                    {
                        case "📊 Panel de control":
                            currentForm = new Menu();
                            break;
                        case "📝 Crear Factura":
                            currentForm = new menuesFactura();
                            break;
                        case "🤝 Proveedores":
                            currentForm = new menuesproveedor();
                            break;
                        case "👥 Clientes":
                            // currentForm = new menuesCliente();
                            break;
                        case "📦 Producto":
                            currentForm = new menuesproducto();
                            break;
                        case "👤 Empleado":
                            currentForm = new menuesempleado();
                            break;
                        case "🛍️ Inventario":
                            currentForm = new menuesinventario();
                            break;
                        case "📊 Ventas":
                            currentForm = new menuesresumendia();
                            break;
                        case "📈 Reportes":
                            currentForm = new menuesreporte();
                            break;
                        case "👫 Participantes":
                            // currentForm = new menuesParticipantes();
                            break;
                    }

                    if (currentForm != null)
                    {
                        this.Hide(); // Oculta el formulario actual
                        currentForm.StartPosition = FormStartPosition.CenterParent;
                        currentForm.Show(this);
                        currentForm.FormClosed += (s, args) => this.Close(); // Cierra al salir
                    }
                };
                sidebar.Controls.Add(btn);
                yOffset += 45;
            }

            // Panel de contenido (Dashboard) - Con scroll (MANTENIDO EXACTO)
            Panel contentPanel = new Panel();
            contentPanel.Location = new Point(sidebar.Width + 10, topBar.Height + 10);
            contentPanel.Size = new Size(this.ClientSize.Width - sidebar.Width - 20, this.ClientSize.Height - topBar.Height - bottomBar.Height - 20);
            contentPanel.AutoScroll = true;
            contentPanel.BackColor = Color.White;
            this.Controls.Add(contentPanel);

            // Título para el apartado de cuadros (MANTENIDO EXACTO)
            Label sectionTitle = new Label();
            sectionTitle.Text = "👤 Gestión de Clientes";
            sectionTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            sectionTitle.ForeColor = Color.Black;
            sectionTitle.AutoSize = true;
            sectionTitle.Location = new Point(20, 10);
            contentPanel.Controls.Add(sectionTitle);

            // Botones principales del dashboard con imágenes (MANTENIDO EXACTO)
            string[] dashboardItems = { "Crear Cliente", "Modificar Cliente", "Ver lista de clientes", "Volver Panel Principal", "Modulos", "Modulos", "Modulos" };
            string[] iconPaths = { "../../recursos/img/crearcliente.png", "../../recursos/img/modificarcliente.png", "../../recursos/img/verclientes.png", "../../recursos/img/volveratras.png", "../../recursos/img/comingson.png", "../../recursos/img/comingson.png", "../../recursos/img/comingson.png" };

            int x = 20, y = 60;
            for (int i = 0; i < dashboardItems.Length; i++)
            {
                int index = i;

                Panel buttonPanel = new Panel();
                buttonPanel.Size = new Size(200, 100);
                buttonPanel.Location = new Point(x, y);
                buttonPanel.BorderStyle = BorderStyle.FixedSingle;
                contentPanel.Controls.Add(buttonPanel);

                PictureBox icon = new PictureBox();
                icon.Size = new Size(50, 50);
                icon.Location = new Point(75, 10);

                if (index < iconPaths.Length)
                {
                    try
                    {
                        icon.Image = Image.FromFile(iconPaths[index]);
                        icon.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar la imagen: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                buttonPanel.Controls.Add(icon);

                Label label = new Label();
                label.Text = dashboardItems[index];
                label.Font = new Font("Arial", 10, FontStyle.Bold);
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Location = new Point(0, 60);
                label.Size = new Size(200, 30);
                buttonPanel.Controls.Add(label);

                // MANEJADOR DE EVENTOS MODIFICADO (SOLO ESTA PARTE)
                buttonPanel.Click += (s, e) =>
                {
                    Form newForm = null;
                    switch (dashboardItems[index])
                    {
                        case "Crear Cliente":
                            newForm = new formulariocliente();
                            break;
                        case "Modificar Cliente":
                            newForm = new formularioActualizarCliente();
                            break;
                        case "Ver lista de clientes":
                            newForm = new formularioConsultarCliente();
                            break;
                        case "Volver Panel Principal":
                            this.Hide();
                            new Menu().Show();
                            new Menu().FormClosed += (sender, args) => this.Close();
                            return;
                        case "Modulos":
                            // newForm = new ModulosForm();
                            break;
                    }

                    if (newForm != null)
                    {
                        this.Hide();
                        newForm.StartPosition = FormStartPosition.CenterScreen;
                        newForm.Show();
                        newForm.FormClosed += (sender, args) => this.Close();
                    }
                };

                x += 220;
                if (x > contentPanel.Width - 200)
                {
                    x = 20;
                    y += 120;
                }
            }

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}