using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using sistema_inventario.vistaCliente;
using sistema_inventario.vistaFactura;
using sistema_inventario.vistaProducto;
using sistema_inventario.vistaProveedor;
using sistema_inventario.vistaEmpleado;
using sistema_inventario.Inventario;
using sistema_inventario.vistaResumendia;
using sistema_inventario.vistaReporte;
using CapaNegocio;

namespace sistema_inventario.FCliente
{
    public partial class formulariocliente : Form
    {
        private Form currentForm; // Para manejar el formulario actual
        public DataGridView dataGridViewClientes;


        public void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario
            this.ClientSize = new Size(1200, 700); // Tamaño ajustado para que todo quepa
            this.Text = "📦💰 Sistema de Inventario y Facturación By: Josian Rafael, Felix Mendoza, Billy Smith";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

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
            dateLabel.Location = new Point(titleLabel.Right + 10, 15);
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

                    // Mostrar el formulario si se ha creado
                    if (currentForm != null)
                    {
                        // Centrar el formulario en la pantalla
                        currentForm.StartPosition = FormStartPosition.CenterParent; // Cambia la posición de inicio
                        currentForm.Show(this); // Pasa el formulario padre para centrarlo
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

            // Título para el apartado de gestión de clientes
            Label sectionTitle = new Label();
            sectionTitle.Text = "👤 Insertar de Clientes";
            sectionTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            sectionTitle.ForeColor = Color.Black;
            sectionTitle.AutoSize = true;
            sectionTitle.Location = new Point(20, 10);
            contentPanel.Controls.Add(sectionTitle);

            // Crear el formulario de inserción de clientes
            Panel formPanel = new Panel(); // Usar un Panel en lugar de un GroupBox
            formPanel.Size = new Size(800, 250); // Aumentar el tamaño
            formPanel.Location = new Point(20, 50);
            formPanel.BackColor = Color.White; // Fondo blanco
            contentPanel.Controls.Add(formPanel);

            // Campos del formulario (organizados en dos columnas)
            int xOffset = 20; // Espaciado horizontal
            int yOffsetForm = 20; // Espaciado vertical

            // Columna 1
            Label lblNombre = new Label();
            lblNombre.Text = "Nombre:";
            lblNombre.Location = new Point(xOffset, yOffsetForm);
            lblNombre.AutoSize = true;
            formPanel.Controls.Add(lblNombre);

            TextBox txtNombre = new TextBox();
            txtNombre.Location = new Point(xOffset + 100, yOffsetForm);
            txtNombre.Size = new Size(250, 30);
            formPanel.Controls.Add(txtNombre);

            Label lblApellido = new Label();
            lblApellido.Text = "Apellido:";
            lblApellido.Location = new Point(xOffset, yOffsetForm + 50);
            lblApellido.AutoSize = true;
            formPanel.Controls.Add(lblApellido);

            TextBox txtApellido = new TextBox();
            txtApellido.Location = new Point(xOffset + 100, yOffsetForm + 50);
            txtApellido.Size = new Size(250, 30);
            formPanel.Controls.Add(txtApellido);

            Label lblTelefono = new Label();
            lblTelefono.Text = "Teléfono:";
            lblTelefono.Location = new Point(xOffset, yOffsetForm + 100);
            lblTelefono.AutoSize = true;
            formPanel.Controls.Add(lblTelefono);

            TextBox txtTelefono = new TextBox();
            txtTelefono.Location = new Point(xOffset + 100, yOffsetForm + 100);
            txtTelefono.Size = new Size(250, 30);
            formPanel.Controls.Add(txtTelefono);

            // Columna 2
            int xOffsetCol2 = xOffset + 400; // Segunda columna

            Label lblDireccion = new Label();
            lblDireccion.Text = "Dirección:";
            lblDireccion.Location = new Point(xOffsetCol2, yOffsetForm);
            lblDireccion.AutoSize = true;
            formPanel.Controls.Add(lblDireccion);

            TextBox txtDireccion = new TextBox();
            txtDireccion.Location = new Point(xOffsetCol2 + 100, yOffsetForm);
            txtDireccion.Size = new Size(250, 30);
            formPanel.Controls.Add(txtDireccion);

            Label lblCorreo = new Label();
            lblCorreo.Text = "Correo (Opcional):";
            lblCorreo.Location = new Point(xOffsetCol2, yOffsetForm + 50);
            lblCorreo.AutoSize = true;
            formPanel.Controls.Add(lblCorreo);

            TextBox txtCorreo = new TextBox();
            txtCorreo.Location = new Point(xOffsetCol2 + 100, yOffsetForm + 50);
            txtCorreo.Size = new Size(250, 30);
            formPanel.Controls.Add(txtCorreo);

            // Botón para guardar
            Button btnGuardar = new Button();
            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new Point(xOffsetCol2 + 100, yOffsetForm + 120);
            btnGuardar.Size = new Size(100, 30);
            btnGuardar.BackColor = Color.FromArgb(91, 63, 144);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnGuardar.Click += (sender, e) =>
            {
                // Validar si todos los campos están vacíos
                if (string.IsNullOrEmpty(txtNombre.Text) && string.IsNullOrEmpty(txtApellido.Text) &&
                    string.IsNullOrEmpty(txtTelefono.Text) && string.IsNullOrEmpty(txtDireccion.Text) &&
                    string.IsNullOrEmpty(txtCorreo.Text))
                {
                    // Insertar cliente por defecto
                    string resultado = CNCliente.CN_Insertar_Cliente(
                        "Cliente de contado", // Nombre por defecto
                        "Contado",            // Apellido por defecto
                        null,                 // Teléfono (opcional)
                        null,                 // Dirección (opcional)
                        null                  // Correo (opcional)
                    );

                    // Mostrar el resultado de la operación
                    MessageBox.Show(resultado);
                }
                else
                {
                    // Validar que los campos obligatorios estén llenos
                    if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                        string.IsNullOrEmpty(txtTelefono.Text) || string.IsNullOrEmpty(txtDireccion.Text))
                    {
                        MessageBox.Show("Por favor, complete todos los campos obligatorios.");
                        return;
                    }

                    // Llamar al método de la capa de negocio para insertar el cliente
                    string resultado = CNCliente.CN_Insertar_Cliente(
                        txtNombre.Text,
                        txtApellido.Text,
                        txtTelefono.Text,
                        txtDireccion.Text,
                        txtCorreo.Text
                    );

                    // Mostrar el resultado de la operación
                    MessageBox.Show(resultado);
                }

                // Limpiar los campos después de guardar
                txtNombre.Clear();
                txtApellido.Clear();
                txtTelefono.Clear();
                txtDireccion.Clear();
                txtCorreo.Clear();

                // Actualizar el DataGridView con los datos más recientes
                ActualizarDataGridView();
            };
            formPanel.Controls.Add(btnGuardar);

            // Crear un nuevo DataGridView
            dataGridViewClientes = new DataGridView();
            dataGridViewClientes.Location = new Point(20, 320);
            dataGridViewClientes.Size = new Size(900, 200); // Reducir la altura del DataGridView
            dataGridViewClientes.BackgroundColor = Color.White;
            dataGridViewClientes.BorderStyle = BorderStyle.None;
            dataGridViewClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(91, 63, 144);
            dataGridViewClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridViewClientes.EnableHeadersVisualStyles = false;
            contentPanel.Controls.Add(dataGridViewClientes);

            // Cargar los datos iniciales en el DataGridView
            ActualizarDataGridView();

            this.ResumeLayout(false);
        }

        // Método para actualizar el DataGridView
        private void ActualizarDataGridView()
        {
            try
            {
                // Obtener los datos de la base de datos
                DataTable dt = CNCliente.CN_Consultar_Cliente("");

                // Asignar los datos al DataGridView
                dataGridViewClientes.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }
    }
}