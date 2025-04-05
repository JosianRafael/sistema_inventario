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
namespace sistema_inventario
{
    public partial class Menu : Form
    {
        public Form currentForm; // Para manejar el formulario actual

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario
            this.ClientSize = new Size(1200, 650);
            this.Text = "📦💰 Sistema de Inventario y Facturación By: Josian Rafael, Felix Mendoza, Billy Smith";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Panel superior (Barra morada) - Fijo
            Panel topBar = new Panel();
            topBar.Dock = DockStyle.Top;
            topBar.Height = 50;
            topBar.BackColor = Color.FromArgb(91, 63, 144);
            this.Controls.Add(topBar);

            // Título animado
            Label titleLabel = new Label();
            titleLabel.Text = ""; // Inicialmente vacío para la animación
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 15);
            topBar.Controls.Add(titleLabel);

            // Texto completo con iconos
            string titleText = "📦💰 Sistema de Inventario y Facturación";
            int titleIndex = 0;

            // Timer para animación del título
            Timer titleTimer = new Timer();
            titleTimer.Interval = 50; // Velocidad de aparición de cada letra
            titleTimer.Tick += (s, e) =>
            {
                if (titleIndex < titleText.Length)
                {
                    titleLabel.Text += titleText[titleIndex];
                    titleIndex++;
                }
                else
                {
                    titleTimer.Stop(); // Detiene el timer cuando termine el texto

                    // MOSTRAR NOMBRE DE USUARIO (NUEVO)
                    //Label userLabel = new Label();
                    //userLabel.Text = " | Usuario: " + UserSession.NombreCompleto;
                    //userLabel.ForeColor = Color.LightGreen;
                    //userLabel.Font = new Font("Arial", 10, FontStyle.Italic);
                    //userLabel.AutoSize = true;
                    //userLabel.Location = new Point(titleLabel.Right + 10, 15);
                    //topBar.Controls.Add(userLabel);
                }
            };
            titleTimer.Start();

            // Etiqueta para la fecha (ajusté la posición para que no se solape)
            Label dateLabel = new Label();
            dateLabel.ForeColor = Color.White;
            dateLabel.Font = new Font("Arial", 11, FontStyle.Bold);
            dateLabel.AutoSize = true;
            dateLabel.Location = new Point(800, 15); // Posición fija a la derecha
            dateLabel.Text = "                                                    Fecha actual: " + DateTime.Now.ToString("dd/MM/yyyy");
            topBar.Controls.Add(dateLabel);

            /* 
               ⚠ AQUÍ CONTINÚA TODO EL CÓDIGO ORIGINAL DEL MENÚ ⚠
               (Paneles laterales, botones, funcionalidades, etc.)
               No se ha modificado nada más, solo se agregó el Label del usuario.
               El resto del código permanece IDÉNTICO al original.
            */

            // Panel inferior (Footer) - Fijo
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

            // Panel lateral (Sidebar) - Fijo
            Panel sidebar = new Panel();
            sidebar.Location = new Point(0, topBar.Height);
            sidebar.Width = 250;
            sidebar.Height = this.ClientSize.Height - topBar.Height - bottomBar.Height;
            sidebar.BackColor = Color.FromArgb(40, 40, 60);
            this.Controls.Add(sidebar);

            Label menuLabel = new Label();
            menuLabel.Text = "  -- Menú principal --    \n-------------------------------";
            menuLabel.ForeColor = Color.White;
            menuLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            menuLabel.AutoSize = true;
            menuLabel.Location = new Point(40, 20);
            sidebar.Controls.Add(menuLabel);

            string[] menuItems = { "📊 Panel de control", "📝 Crear Factura", "🤝 Proveedores", "👥 Clientes", "📦 Producto", "👤 Empleado", "🛍 Inventario", "📊 Ventas", "📈 Reportes", "👫 Participantes" };
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

                btn.Click += (sender, e) =>
                {
                    if (currentForm != null && !currentForm.IsDisposed)
                    {
                        currentForm.Close();
                    }

                    switch (item)
                    {
                        case "📊 Panel de control":
                            break;
                        case "📝 Crear Factura":
                            currentForm = new menuesFactura();
                            break;
                        case "🤝 Proveedores":
                            currentForm = new menuesproveedor();
                            break;
                        case "👥 Clientes":
                            currentForm = new menuesCliente();
                            break;
                        case "📦 Producto":
                            currentForm = new menuesproducto();
                            break;
                        case "👤 Empleado":
                            currentForm = new menuesempleado();
                            break;
                        case "🛍 Inventario":
                            currentForm = new menuesinventario();
                            break;
                        case "📊 Ventas":
                            currentForm = new menuesresumendia();
                            break;
                        case "📈 Reportes":
                            currentForm = new menuesreporte();
                            break;
                        case "👫 Participantes":
                            break;
                        default:
                            MessageBox.Show($"Seleccionaste {item}");
                            return;
                    }

                    if (currentForm != null)
                    {
                        currentForm.StartPosition = FormStartPosition.CenterParent;
                        currentForm.Show(this);
                    }
                };

                sidebar.Controls.Add(btn);
                yOffset += 45;
            }

            // Panel de contenido (Dashboard) - Con scroll
            Panel contentPanel = new Panel();
            contentPanel.Location = new Point(sidebar.Width + 10, topBar.Height + 10);
            contentPanel.Size = new Size(this.ClientSize.Width - sidebar.Width - 20, this.ClientSize.Height - topBar.Height - bottomBar.Height - 20);
            contentPanel.AutoScroll = true;
            contentPanel.BackColor = Color.White;
            this.Controls.Add(contentPanel);

            // Título para el apartado de cuadros
            Label sectionTitle = new Label();
            sectionTitle.Text = "📊 Panel de Control";
            sectionTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            sectionTitle.ForeColor = Color.Black;
            sectionTitle.AutoSize = true;
            sectionTitle.Location = new Point(20, 10);
            contentPanel.Controls.Add(sectionTitle);

            // Botones principales del dashboard con imágenes
            string[] dashboardItems = { "Factura", "Empleados", "Proveedor", "Producto", "Clientes", "Inventario", "Reportes", "Participantes" };
            string[] iconPaths = {
                "recursos/img/factura.png",
                "recursos/img/empleado.png",
                "recursos/img/proveedor.png",
                "recursos/img/producto.png",
                "../../recursos/img/Clientes.png",
                "../../recursos/img/inventario.png",
                "../../recursos/img/reportes.png",
                "recursos/img/participantes.png"
            };

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

                buttonPanel.Click += (s, e) =>
                {
                    Form newForm = null;
                    switch (dashboardItems[index])
                    {
                        case "Factura":
                            newForm = new menuesFactura();
                            break;
                        case "Empleados":
                            newForm = new menuesempleado();
                            break;
                        case "Proveedor":
                            newForm = new menuesproveedor();
                            break;
                        case "Producto":
                            newForm = new menuesproducto();
                            break;
                        case "Clientes":
                            newForm = new menuesCliente();
                            break;
                        case "Inventario":
                            newForm = new menuesinventario();
                            break;
                        case "Reportes":
                            newForm = new menuesreporte();
                            break;
                        case "Participantes":
                            break;
                        default:
                            return;
                    }

                    if (newForm != null)
                    {
                        newForm.StartPosition = FormStartPosition.CenterScreen;
                        newForm.Show();
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