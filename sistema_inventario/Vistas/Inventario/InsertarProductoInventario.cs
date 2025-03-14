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
        public InsertarProductoInventario()
        {
            InitializeComponent();
            CargarComboBox();
            button1.Click += guardarinventario;
            CargarListBox();
        }

        private void CargarComboBox()
        {
            if (InventarioListaProductos.ListaProductos != null && InventarioListaProductos.ListaProductos.Rows.Count > 0)
            {
                // Asignar el DataTable como fuente de datos del ComboBox
                comboBox1.DataSource = InventarioListaProductos.ListaProductos;

                // Especificar qué columna se mostrará en el ComboBox
                comboBox1.DisplayMember = "nombre_producto";

                // Especificar qué columna se asociará como valor (ID del producto)
                comboBox1.ValueMember = "id_productopv"; // Reemplaza con el nombre real de la columna
            }
        }

        private void CargarListBox()
        {
            if (InventarioListaProductos.ListaProductos != null && InventarioListaProductos.ListaProductos.Rows.Count > 0)
            {
                // Asignar el DataTable como fuente de datos del ComboBox
                listBox1.DataSource = InventarioListaProductos.ListaProductos;

                // Especificar qué columna se mostrará en el ComboBox
                listBox1.DisplayMember = "nombre_producto";

                // Especificar qué columna se asociará como valor (ID del producto)
                listBox1.ValueMember = "id_productopv"; // Reemplaza con el nombre real de la columna
            }
        }

        private void guardarinventario (object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(comboBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text) 
                || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Debe completar todos los campos.");
            }

            int id_proveedor_producto = (int)comboBox1.SelectedValue;
            int cantidad = int.Parse(textBox1.Text);
            string ubicacion = textBox2.Text;
            int stock_minimo = int.Parse(textBox3.Text);

            CNinventario.CN_Insertar_inventario(0,id_proveedor_producto,cantidad,ubicacion,stock_minimo);
            MessageBox.Show("Insercción completa.");
        }

    }

}
