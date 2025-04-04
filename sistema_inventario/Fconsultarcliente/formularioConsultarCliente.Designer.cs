using System;
using System.Data;
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
using CapaNegocio;

namespace sistema_inventario.Fconsultarcliente
{
    public partial class formularioConsultarCliente : Form
    {
        private Form currentForm;
        private DataGridView dataGridViewClientes;
        private TextBox txtBusqueda;

        public formularioConsultarCliente()
        {
            InitializeComponent();
            CargarTodosClientes();
        }

        public void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario (igual que en el original)
            this.ClientSize = new Size(1200, 700);
            this.Text = "📦💰 Sistema de Inventario y Facturación By: Josian Rafael, Felix Mendoza, Billy Smith";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // Panel superior (Barra morada) - Fijo (igual que en el original)
            Panel topBar = new Panel();
            topBar.Dock = DockStyle.Top;
            topBar.Height = 50;
            topBar.BackColor = Color.FromArgb(91, 63, 144);
            this.Controls.Add(topBar);

            // Título (igual que en el original)
            Label titleLabel = new Label();
            titleLabel.Text = "📦💰 Sistema de Inventario y Facturación";
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 15);
            topBar.Controls.Add(titleLabel);

            // Etiqueta para la fecha (igual que en el original)
            Label dateLabel = new Label();
            dateLabel.ForeColor = Color.White;
            dateLabel.Font = new Font("Arial", 11, FontStyle.Bold);
            dateLabel.AutoSize = true;
            dateLabel.Location = new Point(titleLabel.Right + 10, 15);
            dateLabel.Text = "                                                                                                                                               Fecha actual: " + DateTime.Now.ToString("dd/MM/yyyy");
            topBar.Controls.Add(dateLabel);

            // Panel inferior (Footer) - Fijo (igual que en el original)
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

            // Panel lateral (Sidebar) - Fijo (igual que en el original)
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

                btn.Click += (sender, e) =>
                {
                    if (currentForm != null && !currentForm.IsDisposed)
                    {
                        currentForm.Close();
                    }

                    switch (item)
                    {
                        case "📊 Panel de control":
                            // currentForm = new menuesPanelControl();
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
                            // currentForm = new menuesParticipantes();
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

            
            Label sectionTitle = new Label();
            sectionTitle.Text = "🔍 Consultar Clientes";
            sectionTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            sectionTitle.ForeColor = Color.Black;
            sectionTitle.AutoSize = true;
            sectionTitle.Location = new Point(20, 10);
            contentPanel.Controls.Add(sectionTitle);

           
            Panel searchPanel = new Panel();
            searchPanel.Size = new Size(900, 80);
            searchPanel.Location = new Point(20, 50);
            searchPanel.BackColor = Color.White;
            contentPanel.Controls.Add(searchPanel);

            // Campo de búsqueda
            Label lblBusqueda = new Label();
            lblBusqueda.Text = "Buscar (ID, Nombre o Apellido):";
            lblBusqueda.Location = new Point(15, 15);
            lblBusqueda.AutoSize = true;
            lblBusqueda.Font = new Font("Arial", 10, FontStyle.Bold);
            searchPanel.Controls.Add(lblBusqueda);

            txtBusqueda = new TextBox();
            txtBusqueda.Location = new Point(240, 18);
            txtBusqueda.Size = new Size(370, 20);
            txtBusqueda.Font = new Font("Arial", 10);
            searchPanel.Controls.Add(txtBusqueda);

            // Botón de búsqueda
            Button btnBuscar = new Button();
            btnBuscar.Text = "Buscar";
            btnBuscar.Location = new Point(640, 15);
            btnBuscar.Size = new Size(100, 30);
            btnBuscar.BackColor = Color.FromArgb(91, 63, 144);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnBuscar.Click += BtnBuscar_Click;
            searchPanel.Controls.Add(btnBuscar);

            // Botón para mostrar todos
            Button btnMostrarTodos = new Button();
            btnMostrarTodos.Text = "Mostrar Todos";
            btnMostrarTodos.Location = new Point(750, 15);
            btnMostrarTodos.Size = new Size(120, 30);
            btnMostrarTodos.BackColor = Color.FromArgb(60, 60, 80);
            btnMostrarTodos.ForeColor = Color.White;
            btnMostrarTodos.FlatStyle = FlatStyle.Flat;
            btnMostrarTodos.Font = new Font("Arial", 10, FontStyle.Bold);
            btnMostrarTodos.Click += BtnMostrarTodos_Click;
            searchPanel.Controls.Add(btnMostrarTodos);

            // DataGridView para mostrar resultados (similar al original)
            dataGridViewClientes = new DataGridView();
            dataGridViewClientes.Location = new Point(20, 150);
            dataGridViewClientes.Size = new Size(900, 400);
            dataGridViewClientes.BackgroundColor = Color.White;
            dataGridViewClientes.BorderStyle = BorderStyle.None;
            dataGridViewClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(91, 63, 144);
            dataGridViewClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridViewClientes.EnableHeadersVisualStyles = false;
            dataGridViewClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            contentPanel.Controls.Add(dataGridViewClientes);

            this.ResumeLayout(false);
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            BuscarClientes(txtBusqueda.Text.Trim());
        }

        private void BtnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            CargarTodosClientes();
        }

        private void CargarTodosClientes()
        {
            try
            {
                DataTable dt = CNCliente.CN_Consultar_Cliente("");
                dataGridViewClientes.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarClientes(string parametroBusqueda)
        {
            try
            {
                DataTable dt = CNCliente.CN_Consultar_Cliente(parametroBusqueda);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron clientes con ese criterio de búsqueda.",
                                  "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dataGridViewClientes.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar clientes: " + ex.Message,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}