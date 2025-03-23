using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistema_inventario.Vistas;
using CapaNegocio;

namespace sistema_inventario.Vistas.Inventario
{
    public partial class InsertarProductoInventario : Form
    {
        private DataTable dtProductos = InventarioListaProductos.ListaProductosInventario;
        public InsertarProductoInventario()
        {
            InitializeComponent();
            button1.Click += guardarinventario;
            CargarListBox();
            textBox1.KeyPress += comprobarnumero;
            textBox3.KeyPress += comprobarnumero;
            textBox4.TextChanged += buscarproducto;
            listBox1.SelectedValueChanged += ListBox1_ValueMemberChanged;
            comboBox1.Items.Add("entrada");
            comboBox1.Items.Add("ajuste");
        }

        private void ListBox1_ValueMemberChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue == null)
            {
                return;
            }
            int id_inventario = (int)listBox1.SelectedValue;
            string stock_minimo;
            string cantidad;
            string ubicacion;
            for (int i = 0; i < dtProductos.Rows.Count; i++)
            {
                DataRow fila = dtProductos.Rows[i];
                if ((int)fila["id_inventario"] == id_inventario)
                {
                    stock_minimo = fila["stock_minimo"].ToString();
                    cantidad = fila["cantidad"].ToString();
                    ubicacion = fila["ubicacion"].ToString();
                    textBox3.Text = stock_minimo;
                    textBox2.Text = ubicacion;
                    textBox5.Text = cantidad;
                    break;
                }
            }
            

        }

        private void buscarproducto (object sender , EventArgs e)
        {
            if (textBox4.Text == "")
            {
                //Si esta vacio mostrar todos los productos
                listBox1.DataSource = dtProductos;
                listBox1.DisplayMember = "producto_nombre";
                listBox1.ValueMember = "id_inventario";
                return;
            }

            // Filtrar las filas que contienen el texto
            string texto = textBox4.Text.ToLower();
            var filasFiltradas = dtProductos.AsEnumerable()
            .Where(row => row.Field<string>("producto_nombre").ToLower().Contains(texto))
 .          ToList();

            // Crear un DataTable vacío con la misma estructura que dtProductos
            DataTable dtFiltrado = dtProductos.Clone();

            // Agregar las filas filtradas al DataTable nuevo
            foreach (var fila in filasFiltradas)
            {
                dtFiltrado.ImportRow(fila);
            }

            // Asignar el DataTable filtrado al ListBox
            listBox1.DataSource = dtFiltrado;
            listBox1.DisplayMember = "producto_nombre"; // Mostrar el nombre del producto
            listBox1.ValueMember = "id_inventario";    // Usar el id de inventario como valor
        }

        private void CargarListBox()
        {
            if (dtProductos != null && dtProductos.Rows.Count > 0)
            {
                // Asignar el DataTable como fuente de datos del ComboBox
                listBox1.DataSource = dtProductos;

                // Especificar qué columna se mostrará en el ComboBox
                listBox1.DisplayMember = "producto_nombre";
                listBox1.ValueMember = "id_inventario";
                listBox1.ClearSelected();
            }
        }

        private void guardarinventario(object sender, EventArgs e)
        {
            int cantidad;

            if (string.IsNullOrWhiteSpace(listBox1.SelectedItem.ToString()) || string.IsNullOrWhiteSpace(textBox1.Text)
                || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Debe completar todos los campos.");
                return;
            }

            int id_inventario = (int)listBox1.SelectedValue;

            int id_producto = -1;

            // Buscando id del producto
            for (int i = 0; i < dtProductos.Rows.Count; i++)
            {
                DataRow fila = dtProductos.Rows[i];
                if ((int)fila["id_inventario"] == id_inventario)
                {
                    id_producto = (int)fila["id_proveedor_producto"];
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("No se seleccionó una acción");
                return;
            }

            if (id_producto == -1)
            {
                MessageBox.Show("No se encontró el producto en el inventario.");
                return;
            }


            if (comboBox1.Text == "entrada")
            {
                cantidad = int.Parse(textBox1.Text) + int.Parse(textBox5.Text);
            }

            cantidad = int.Parse(textBox1.Text);
            string ubicacion = textBox2.Text;
            int stock_minimo = int.Parse(textBox3.Text);
            string texto = CNinventario.CN_Actualizar_inventario(id_inventario, id_producto, cantidad, ubicacion, stock_minimo);
            CNmovimientoInventario.CN_Insertar_movimiento_inventario(0, id_inventario, comboBox1.Text, cantidad, DateTime.Now);
            MessageBox.Show(texto);

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedIndex = -1;
            listBox1.ClearSelected();
        }

        private void comprobarnumero(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquea la entrada si no es número
            }
        }

    }

}
