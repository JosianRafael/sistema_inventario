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

namespace sistema_inventario.FProducto
{
    public partial class formularioproducto : Form
    {
        private Form currentForm;
        public DataGridView dataGridViewProductos;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private TextBox txtCodigoBarras;
        private NumericUpDown numStock;
        private TextBox txtUbicacion;
        private DateTimePicker dtpVencimiento;
        private NumericUpDown numPrecio;
        private NumericUpDown numCosto;
        private ComboBox ComboboxProveedor;

        public formularioproducto()
        {
            InitializeComponent();
            ActualizarDataGridView();
            CargarProveedores();
        }

        public void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario
            this.ClientSize = new Size(1200, 850); // Aumenté la altura para más espacio
            this.Text = "📦💰 Sistema de Inventario y Facturación By: Josian Rafael, Felix Mendoza, Billy Smith";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // Panel superior (Barra morada)
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

            // Fecha actual
            Label dateLabel = new Label();
            dateLabel.ForeColor = Color.White;
            dateLabel.Font = new Font("Arial", 11, FontStyle.Bold);
            dateLabel.AutoSize = true;
            dateLabel.Location = new Point(titleLabel.Right + 10, 15);
            dateLabel.Text = "                                                                                                                                               Fecha actual: " + DateTime.Now.ToString("dd/MM/yyyy");
            topBar.Controls.Add(dateLabel);

            // Panel inferior (Footer)
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

            // Panel lateral (Sidebar)
            Panel sidebar = new Panel();
            sidebar.Location = new Point(0, topBar.Height);
            sidebar.Width = 250;
            sidebar.Height = this.ClientSize.Height - topBar.Height - bottomBar.Height;
            sidebar.BackColor = Color.FromArgb(40, 40, 60);
            this.Controls.Add(sidebar);

            // Menú principal
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
                            break;
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

            // Panel de contenido
            Panel contentPanel = new Panel();
            contentPanel.Location = new Point(sidebar.Width + 10, topBar.Height + 10);
            contentPanel.Size = new Size(this.ClientSize.Width - sidebar.Width - 20, this.ClientSize.Height - topBar.Height - bottomBar.Height - 20);
            contentPanel.AutoScroll = true;
            contentPanel.BackColor = Color.White;
            this.Controls.Add(contentPanel);

            // Título para gestión de productos
            Label sectionTitle = new Label();
            sectionTitle.Text = "📦 Insertar Productos";
            sectionTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            sectionTitle.ForeColor = Color.Black;
            sectionTitle.AutoSize = true;
            sectionTitle.Location = new Point(20, 10);
            contentPanel.Controls.Add(sectionTitle);

            // Formulario de inserción de productos
            Panel formPanel = new Panel();
            formPanel.Size = new Size(900, 320); // Aumenté la altura para los nuevos campos
            formPanel.Location = new Point(20, 50);
            formPanel.BackColor = Color.White;
            contentPanel.Controls.Add(formPanel);

            // Campos del formulario
            int xOffset = 20;
            int yOffsetForm = 20;

            // Columna 1
            Label lblNombre = new Label();
            lblNombre.Text = "Nombre:";
            lblNombre.Location = new Point(xOffset, yOffsetForm);
            lblNombre.AutoSize = true;
            formPanel.Controls.Add(lblNombre);

            txtNombre = new TextBox();
            txtNombre.Location = new Point(xOffset + 120, yOffsetForm);
            txtNombre.Size = new Size(250, 30);
            formPanel.Controls.Add(txtNombre);

            Label lblDescripcion = new Label();
            lblDescripcion.Text = "Descripción:";
            lblDescripcion.Location = new Point(xOffset, yOffsetForm + 40);
            lblDescripcion.AutoSize = true;
            formPanel.Controls.Add(lblDescripcion);

            txtDescripcion = new TextBox();
            txtDescripcion.Location = new Point(xOffset + 120, yOffsetForm + 40);
            txtDescripcion.Size = new Size(250, 60);
            txtDescripcion.Multiline = true;
            formPanel.Controls.Add(txtDescripcion);

            Label lblProveedor = new Label();
            lblProveedor.Text = "Seleccione un proveedor:";
            lblProveedor.Location = new Point(xOffset, yOffsetForm + 110);
            lblProveedor.AutoSize = true;
            formPanel.Controls.Add(lblProveedor);

            ComboboxProveedor = new ComboBox();
            ComboboxProveedor.Location = new Point(xOffset + 150, yOffsetForm + 110);
            ComboboxProveedor.Size = new Size(150, 30);
            formPanel.Controls.Add(ComboboxProveedor);

            // Columna 2
            int xOffsetCol2 = xOffset + 400;

            Label lblCodigoBarras = new Label();
            lblCodigoBarras.Text = "Código Barras:";
            lblCodigoBarras.Location = new Point(xOffsetCol2, yOffsetForm);
            lblCodigoBarras.AutoSize = true;
            formPanel.Controls.Add(lblCodigoBarras);

            txtCodigoBarras = new TextBox();
            txtCodigoBarras.Location = new Point(xOffsetCol2 + 120, yOffsetForm);
            txtCodigoBarras.Size = new Size(250, 30);
            formPanel.Controls.Add(txtCodigoBarras);

            Label lblStock = new Label();
            lblStock.Text = "Cantidad inicial:";
            lblStock.Location = new Point(xOffsetCol2, yOffsetForm + 40);
            lblStock.AutoSize = true;
            formPanel.Controls.Add(lblStock);

            numStock = new NumericUpDown();
            numStock.Location = new Point(xOffsetCol2 + 120, yOffsetForm + 40);
            numStock.Size = new Size(100, 30);
            numStock.Minimum = 0;
            formPanel.Controls.Add(numStock);

            Label lblUbicacion = new Label();
            lblUbicacion.Text = "Ubicación (ej: A03):";
            lblUbicacion.Location = new Point(xOffsetCol2, yOffsetForm + 80);
            lblUbicacion.AutoSize = true;
            formPanel.Controls.Add(lblUbicacion);

            txtUbicacion = new TextBox();
            txtUbicacion.Location = new Point(xOffsetCol2 + 120, yOffsetForm + 80);
            txtUbicacion.Size = new Size(100, 30);
            formPanel.Controls.Add(txtUbicacion);

            Label lblVencimiento = new Label();
            lblVencimiento.Text = "Fecha Vencimiento:";
            lblVencimiento.Location = new Point(xOffsetCol2, yOffsetForm + 120);
            lblVencimiento.AutoSize = true;
            formPanel.Controls.Add(lblVencimiento);

            dtpVencimiento = new DateTimePicker();
            dtpVencimiento.Location = new Point(xOffsetCol2 + 120, yOffsetForm + 120);
            dtpVencimiento.Size = new Size(150, 30);
            dtpVencimiento.Format = DateTimePickerFormat.Short;
            dtpVencimiento.Value = DateTime.Now.AddMonths(6);
            formPanel.Controls.Add(dtpVencimiento);

            // Nuevos campos para precio y costo
            Label lblPrecio = new Label();
            lblPrecio.Text = "Precio:";
            lblPrecio.Location = new Point(xOffsetCol2, yOffsetForm + 160);
            lblPrecio.AutoSize = true;
            formPanel.Controls.Add(lblPrecio);

            numPrecio = new NumericUpDown();
            numPrecio.Location = new Point(xOffsetCol2 + 120, yOffsetForm + 160);
            numPrecio.Size = new Size(150, 30);
            numPrecio.DecimalPlaces = 2;
            numPrecio.Minimum = 0;
            numPrecio.Maximum = 999999;
            numPrecio.Increment = 0.01m;
            formPanel.Controls.Add(numPrecio);

            Label lblCosto = new Label();
            lblCosto.Text = "Costo:";
            lblCosto.Location = new Point(xOffsetCol2, yOffsetForm + 200);
            lblCosto.AutoSize = true;
            formPanel.Controls.Add(lblCosto);

            numCosto = new NumericUpDown();
            numCosto.Location = new Point(xOffsetCol2 + 120, yOffsetForm + 200);
            numCosto.Size = new Size(150, 30);
            numCosto.DecimalPlaces = 2;
            numCosto.Minimum = 0;
            numCosto.Maximum = 999999;
            numCosto.Increment = 0.01m;
            formPanel.Controls.Add(numCosto);

            // Botón para guardar (actualizado con nuevos campos)
            Button btnGuardar = new Button();
            btnGuardar.Text = "Guardar Producto";
            btnGuardar.Location = new Point(xOffsetCol2 + 120, yOffsetForm + 240);
            btnGuardar.Size = new Size(150, 40);
            btnGuardar.BackColor = Color.FromArgb(91, 63, 144);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnGuardar.Click += (sender, e) =>
            {
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtUbicacion.Text))
                {
                    MessageBox.Show("Complete los campos obligatorios (Nombre y Ubicación)");
                    return;
                }

                string resultado = CNproducto.CN_Insertar_Producto(
                    txtNombre.Text,
                    Convert.ToInt32(ComboboxProveedor.SelectedValue),
                    txtUbicacion.Text,
                    dtpVencimiento.Checked ? dtpVencimiento.Value : DateTime.Now,
                    txtDescripcion.Text,
                    txtCodigoBarras.Text,
                    numPrecio.Value,
                    numCosto.Value,
                    (int)numStock.Value
                );

                //MessageBox.Show(resultado);
                LimpiarCampos();
                ActualizarDataGridView();
            };
            formPanel.Controls.Add(btnGuardar);

            // DataGridView
            dataGridViewProductos = new DataGridView();
            dataGridViewProductos.Location = new Point(20, 380); // Ajusté la posición por los nuevos campos
            dataGridViewProductos.Size = new Size(contentPanel.Width - 40, contentPanel.Height - 400);
            dataGridViewProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewProductos.BackgroundColor = Color.White;
            dataGridViewProductos.BorderStyle = BorderStyle.None;
            dataGridViewProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(91, 63, 144);
            dataGridViewProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridViewProductos.EnableHeadersVisualStyles = false;
            dataGridViewProductos.ReadOnly = true;
            dataGridViewProductos.AllowUserToAddRows = false;
            
            // Formato para columnas monetarias
            dataGridViewProductos.ColumnAdded += (sender, e) =>
            {
                if (e.Column.Name == "precio" || e.Column.Name == "costo")
                {
                    e.Column.DefaultCellStyle.Format = "C2";
                    e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            };
            
            contentPanel.Controls.Add(dataGridViewProductos);

            this.ResumeLayout(false);
        }

        private void ActualizarDataGridView()
        {
            try
            {
                DataTable dt = CNproducto.CN_Consultar_Producto("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGridViewProductos.DataSource = dt;
                    dataGridViewProductos.Visible = true;
                }
                else
                {
                    MessageBox.Show("No se encontraron productos en la base de datos.");
                    dataGridViewProductos.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
                dataGridViewProductos.Visible = false;
            }
        }

        private void CargarProveedores()
        {
            DataTable datos;
            try 
            {
                datos = CNProveedor.CN_Consultar_Proveedor("");
            }
            catch (Exception ex)
            {
                datos = null;
            }
               
            if (datos != null && datos.Rows.Count > 0)
            {
                ComboboxProveedor.DataSource = datos;
                ComboboxProveedor.DisplayMember = "nombre";
                ComboboxProveedor.ValueMember = "id_proveedor";
            }
            else
            {
                MessageBox.Show("No hay proveedores registrados", "ok");
            }

        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtCodigoBarras.Clear();
            numStock.Value = 0;
            txtUbicacion.Clear();
            dtpVencimiento.Value = DateTime.Now.AddMonths(6);
            numPrecio.Value = 0;
            numCosto.Value = 0;
            ComboboxProveedor.SelectedIndex = -1;
        }
    }
}