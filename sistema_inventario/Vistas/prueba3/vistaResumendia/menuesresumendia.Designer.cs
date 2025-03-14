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

namespace sistema_inventario.vistaResumendia
{
    public partial class menuesresumendia : Form
    {
        private Form currentForm; // Para manejar el formulario actual

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario
            this.ClientSize = new Size(1200, 700);
            this.Text = "📦💰 Sistema de Inventario y Facturación By: Josian Rafael, Felix Mendoza, Billy Smith";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // Centrando manualmente el formulario
            CenterForm();

            // Panel superior (Barra morada) - Fijo
            Panel topBar = new Panel();
            topBar.Dock = DockStyle.Top;
            topBar.Height = 50;
            topBar.BackColor = Color.FromArgb(91, 63, 144);
            this.Controls.Add(topBar);

            // Título
            Label titleLabel = new Label();
            titleLabel.Text = "📦💰 Sistema de Inventario y Facturación";
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 15);
            topBar.Controls.Add(titleLabel);

            // Etiqueta para la fecha
            Label dateLabel = new Label();
            dateLabel.ForeColor = Color.White;
            dateLabel.Font = new Font("Arial", 11, FontStyle.Bold);
            dateLabel.AutoSize = true;
            dateLabel.Location = new Point(titleLabel.Right + 10, 15); // A la derecha del título
            dateLabel.Text = "                                                                                                                                               Fecha actual: " + DateTime.Now.ToString("dd/MM/yyyy"); // Formato de fecha
            topBar.Controls.Add(dateLabel);

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

                // Manejar clics
                btn.Click += (sender, e) =>
                {
                    // Cerrar el formulario actual si existe
                    if (currentForm != null && !currentForm.IsDisposed)
                    {
                        currentForm.Close();
                    }

                    // Crear y mostrar un nuevo formulario basado en el botón presionado
                    switch (item)
                    {
                        case "📊 Panel de control":
                            //currentForm = new menuesPanelControl();
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
                            //currentForm = new menuesParticipantes();
                            break;
                        default:
                            MessageBox.Show($"Seleccionaste {item}");
                            return;
                    }

                    // Mostrar el formulario si se ha creado
                    if (currentForm != null)
                    {
                        currentForm.Show();
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
            sectionTitle.Text = "📝 Gestión Ventas";
            sectionTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            sectionTitle.ForeColor = Color.Black;
            sectionTitle.AutoSize = true;
            sectionTitle.Location = new Point(20, 10);
            contentPanel.Controls.Add(sectionTitle);

            // Espacio entre el título y los cuadros
            int spaceBetweenTitleAndBoxes = 20; // Espacio deseado en píxeles

            // Botones principales del dashboard con imágenes
            string[] dashboardItems = { "Resumen del dia" };
            string[] iconPaths = { "../../recursos/img/resumendia.png", "../../recursos/img/modificarproveedor.png", "../../recursos/img/verlistaproovedor.png", "../../recursos/img/insertarproveedorproducto.png", "../../recursos/img/modificarproveedorproducto.png", "../../recursos/img/consultarproveedorproducto.png", "recursos/img/reporte.png", "recursos/img/participantes.png", "icons/check.png", "icons/expense.png", "icons/cashcut.png", "icons/transfer.png" };

            int x = 20, y = 60;
            for (int i = 0; i < dashboardItems.Length; i++)
            {
                Panel buttonPanel = new Panel();
                buttonPanel.Size = new Size(200, 100);
                buttonPanel.Location = new Point(x, y);
                buttonPanel.BorderStyle = BorderStyle.FixedSingle;
                contentPanel.Controls.Add(buttonPanel);

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
                btnLabel.Text = dashboardItems[i];
                btnLabel.Font = new Font("Arial", 10, FontStyle.Bold);
                btnLabel.AutoSize = false;
                btnLabel.TextAlign = ContentAlignment.MiddleCenter;
                btnLabel.Dock = DockStyle.Bottom;
                buttonPanel.Controls.Add(btnLabel);

                buttonPanel.Click += (sender, e) => MessageBox.Show($"Seleccionaste {dashboardItems[i]}");

                x += 220;
                if ((i + 1) % 4 == 0)
                {
                    x = 20; // Reiniciar la posición X
                    y += 120; // Mover hacia abajo
                }
            }

            this.ResumeLayout(false);
        }

        private void CenterForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen; // Centra el formulario en la pantalla
        }

     
    }
}
