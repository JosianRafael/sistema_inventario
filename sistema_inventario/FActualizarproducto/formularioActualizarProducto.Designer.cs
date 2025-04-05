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

namespace sistema_inventario.FActualizarproducto
{
    public partial class formularioActualizarProducto : Form
    {
        private Form currentForm;
        private DataGridView dataGridViewProductos;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private TextBox txtCodigoBarras;
        private NumericUpDown numStock;
        private TextBox txtUbicacion;
        private DateTimePicker dtpVencimiento;
        private NumericUpDown numPrecio;
        private NumericUpDown numCosto;
        private ComboBox proveedorCombobox;
        private int idProductoSeleccionado;
        private int idProductoProveedor;
        private static DataTable datos;

        public formularioActualizarProducto()
        {
            InitializeComponent();
            ActualizarDataGridView();
            CargarProveedores();
        }

        public void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario
            this.ClientSize = new Size(1200, 900);
            this.Text = "📦💰 Actualizar Producto - Sistema de Inventario";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // 1. PANEL SUPERIOR (BARRA MORADA)
            Panel topBar = new Panel();
            topBar.Dock = DockStyle.Top;
            topBar.Height = 50;
            topBar.BackColor = Color.FromArgb(91, 63, 144);
            this.Controls.Add(topBar);

            Label titleLabel = new Label();
            titleLabel.Text = "📦💰 Sistema de Inventario y Facturación";
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 15);
            topBar.Controls.Add(titleLabel);

            Label dateLabel = new Label();
            dateLabel.ForeColor = Color.White;
            dateLabel.Font = new Font("Arial", 11, FontStyle.Bold);
            dateLabel.AutoSize = true;
            dateLabel.Location = new Point(titleLabel.Right + 10, 15);
            dateLabel.Text = "                                                                                                                                               Fecha actual: " + DateTime.Now.ToString("dd/MM/yyyy");
            topBar.Controls.Add(dateLabel);

            // 2. PANEL INFERIOR (FOOTER)
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

            // 3. PANEL LATERAL (SIDEBAR)
            Panel sidebar = new Panel();
            sidebar.Location = new Point(0, topBar.Height);
            sidebar.Width = 250;
            sidebar.Height = this.ClientSize.Height - topBar.Height - bottomBar.Height;
            sidebar.BackColor = Color.FromArgb(40, 40, 60);
            this.Controls.Add(sidebar);

            // MENÚ PRINCIPAL
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

            // 4. PANEL DE CONTENIDO
            Panel contentPanel = new Panel();
            contentPanel.Location = new Point(sidebar.Width + 10, topBar.Height + 10);
            contentPanel.Size = new Size(this.ClientSize.Width - sidebar.Width - 20, this.ClientSize.Height - topBar.Height - bottomBar.Height - 20);
            contentPanel.AutoScroll = true;
            contentPanel.BackColor = Color.White;
            this.Controls.Add(contentPanel);

            // TÍTULO SECCIÓN
            Label sectionTitle = new Label();
            sectionTitle.Text = "📦 Actualizar Producto Inventario";
            sectionTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            sectionTitle.ForeColor = Color.Black;
            sectionTitle.AutoSize = true;
            sectionTitle.Location = new Point(20, 10);
            contentPanel.Controls.Add(sectionTitle);

            // FORMULARIO DE ACTUALIZACIÓN (ARRIBA)
            Panel formPanel = new Panel();
            formPanel.Size = new Size(900, 320);
            formPanel.Location = new Point(20, 50);
            formPanel.BackColor = Color.White;
            contentPanel.Controls.Add(formPanel);

            // CAMPOS DEL FORMULARIO
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

            Label lblCodigoArticulo = new Label();
            lblCodigoArticulo.Text = "Seleccione un proveedor:";
            lblCodigoArticulo.Location = new Point(xOffset, yOffsetForm + 110);
            lblCodigoArticulo.AutoSize = true;
            formPanel.Controls.Add(lblCodigoArticulo);

            proveedorCombobox = new ComboBox();
            proveedorCombobox.Location = new Point(xOffset + 120, yOffsetForm + 110);
            proveedorCombobox.Size = new Size(150, 30);
            formPanel.Controls.Add(proveedorCombobox);

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

            //Label lblStock = new Label();
            //lblStock.Text = "Stock:";
            //lblStock.Location = new Point(xOffsetCol2, yOffsetForm + 40);
            //lblStock.AutoSize = true;
            //formPanel.Controls.Add(lblStock);

            //numStock = new NumericUpDown();
            //numStock.Location = new Point(xOffsetCol2 + 120, yOffsetForm + 40);
            //numStock.Size = new Size(100, 30);
            //numStock.Minimum = 0;
            //formPanel.Controls.Add(numStock);

            //Label lblUbicacion = new Label();
            //lblUbicacion.Text = "Ubicación (ej: A03):";
            //lblUbicacion.Location = new Point(xOffsetCol2, yOffsetForm + 80);
            //lblUbicacion.AutoSize = true;
            //formPanel.Controls.Add(lblUbicacion);

            //txtUbicacion = new TextBox();
            //txtUbicacion.Location = new Point(xOffsetCol2 + 120, yOffsetForm + 80);
            //txtUbicacion.Size = new Size(100, 30);
            //formPanel.Controls.Add(txtUbicacion);

            //Label lblVencimiento = new Label();
            //lblVencimiento.Text = "Fecha Vencimiento:";
            //lblVencimiento.Location = new Point(xOffsetCol2, yOffsetForm + 120);
            //lblVencimiento.AutoSize = true;
            //formPanel.Controls.Add(lblVencimiento);

            //dtpVencimiento = new DateTimePicker();
            //dtpVencimiento.Location = new Point(xOffsetCol2 + 120, yOffsetForm + 120);
            //dtpVencimiento.Size = new Size(150, 30);
            //dtpVencimiento.Format = DateTimePickerFormat.Short;
            //formPanel.Controls.Add(dtpVencimiento);

            // Campos de precio y costo
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

            // Botón de actualizar
            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Producto";
            btnActualizar.Location = new Point(xOffsetCol2 + 120, yOffsetForm + 240);
            btnActualizar.Size = new Size(140, 29);
            btnActualizar.BackColor = Color.FromArgb(91, 63, 144);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnActualizar.Click += (sender, e) =>
            {
                if (dataGridViewProductos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un producto para actualizar");
                    return;
                }

                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    MessageBox.Show("Complete los campos obligatorios (Nombre y Ubicación)");
                    return;
                }

                string resultado = CNproducto.CN_Actualizar_Producto(
                    idProductoSeleccionado,
                    txtNombre.Text,
                    idProductoProveedor,
                    Convert.ToInt32(proveedorCombobox.SelectedValue),
                    txtDescripcion.Text,
                    txtCodigoBarras.Text,
                    numPrecio.Value,
                    numCosto.Value
                );

                MessageBox.Show(resultado);
                ActualizarDataGridView();
            };
            formPanel.Controls.Add(btnActualizar);

            // DATAGRIDVIEW PARA SELECCIÓN (ABAJO)
            dataGridViewProductos = new DataGridView();
            dataGridViewProductos.ReadOnly = true;
            dataGridViewProductos.AllowUserToAddRows = false;
            dataGridViewProductos.Location = new Point(20, 390); // Posición debajo del formulario (No mover porfavor)...
            dataGridViewProductos.Size = new Size(contentPanel.Width - 40, 250);
            dataGridViewProductos.BackgroundColor = Color.White;
            dataGridViewProductos.BorderStyle = BorderStyle.None;
            dataGridViewProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(91, 63, 144);
            dataGridViewProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridViewProductos.EnableHeadersVisualStyles = false;
            dataGridViewProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProductos.MultiSelect = false;

            // Evento para cargar datos al seleccionar (No tocar el codigo, aqui es dnde se guardan los datos)...
            dataGridViewProductos.CellClick += (sender, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dataGridViewProductos.Rows[e.RowIndex];
                    CargarDatosProducto(fila);
                }
            };

            contentPanel.Controls.Add(dataGridViewProductos);

            this.ResumeLayout(false);
        }

        private void CargarDatosProducto(DataGridViewRow fila)
        {
            idProductoProveedor = Convert.ToInt32(fila.Cells["id_productopv"].Value);
            idProductoSeleccionado = Convert.ToInt32(fila.Cells["id_producto"].Value);
            txtNombre.Text = fila.Cells["descripcion"].Value.ToString();
            txtDescripcion.Text = fila.Cells["nombre_producto"].Value?.ToString() ?? "";
            txtCodigoBarras.Text = fila.Cells["codigo_barras"].Value?.ToString() ?? "";
            //numStock.Value = Convert.ToInt32(fila.Cells["stock"].Value);
            //txtUbicacion.Text = fila.Cells["ubicacion"].Value.ToString();

            //if (fila.Cells["fecha_vencimiento"].Value != DBNull.Value)
            //{
            //    dtpVencimiento.Value = Convert.ToDateTime(fila.Cells["fecha_vencimiento"].Value);
            //}
            //else
            //{
            //    dtpVencimiento.Value = DateTime.Now.AddMonths(6);
            //}

            numPrecio.Value = Convert.ToDecimal(fila.Cells["precio_venta_producto"].Value);
            numCosto.Value = Convert.ToDecimal(fila.Cells["costo_producto"].Value);

            string nombreproveedor = fila.Cells["nombre_proveedor"].Value.ToString();
            int i = 0;

            foreach (var item in proveedorCombobox.Items)
            {
                var rowView = item as DataRowView;
                if (rowView != null && rowView["nombre"].ToString() == nombreproveedor)
                {
                    proveedorCombobox.SelectedIndex = i;
                    break;
                }
                i++;
            }

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
                proveedorCombobox.DataSource = datos;
                proveedorCombobox.DisplayMember = "nombre";
                proveedorCombobox.ValueMember = "id_proveedor";
            }
            else
            {
                MessageBox.Show("No hay proveedores registrados", "ok");
            }

        }
    }
}