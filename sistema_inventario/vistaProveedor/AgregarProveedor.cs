using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace sistema_inventario.vistaProveedor
{
    public partial class AgregarProveedor : Form
    {
        private int id_proveedor;
        public AgregarProveedor()
        {
            InitializeComponent();
            button1.Click += GuardarProveedor;
            CargarDataView();
            dataGridView1.SelectionChanged += SeleccionarProveedor;
            button2.Click += LimpiarCampos;
        }

        private void GuardarProveedor(object sender, EventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(textBox1.Text) || 
                string.IsNullOrWhiteSpace(textBox2.Text) || 
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text)
                )
            {
                MessageBox.Show("Debe completar todos los campos");
                return;
            }

            string nombre = textBox1.Text;
            string telefono = textBox2.Text;
            string direccion = textBox3.Text;
            string correo = textBox4.Text;
            string Nonbre_representante = textBox5.Text;

            if (ComprobarIDproveedor(id_proveedor))
            {
                string mensaje = CNProveedor.CN_Actualizar_Proveedor(id_proveedor, nombre, telefono, direccion, correo, Nonbre_representante);
                MessageBox.Show(mensaje);
                LimpiarCampos();
                CargarDataView();
                return;
            }

            string resultado = CNProveedor.CN_Insertar_Proveedor(nombre,telefono,direccion,correo,Nonbre_representante);

            LimpiarCampos();

            MessageBox.Show(resultado);

            CargarDataView();
        }

        private void CargarDataView()
        {
            DataTable table = CNProveedor.CN_Consultar_Proveedor("");
            dataGridView1.DataSource = table;
        }

        private void SeleccionarProveedor(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0]; //Primera fila seleccionada
                id_proveedor = (int)filaSeleccionada.Cells[0].Value;
                // Accede a los valores de las celdas por índice de columna
                textBox1.Text = filaSeleccionada.Cells[1].Value?.ToString(); //Nombre empresa
                textBox2.Text = filaSeleccionada.Cells[2].Value?.ToString(); //Telefono
                textBox3.Text = filaSeleccionada.Cells[3].Value?.ToString(); //Direccion
                textBox4.Text = filaSeleccionada.Cells[4].Value?.ToString(); //Correo electronico
                textBox5.Text = filaSeleccionada.Cells[5].Value?.ToString(); //Nombre representante
            }
        }

        private bool ComprobarIDproveedor(int id_proveedor)
        {
            // Consulta el proveedor por ID (debe devolver un DataTable con los resultados)
            DataTable table = CNProveedor.CN_Consultar_Proveedor(id_proveedor.ToString());

            // Verifica si la tabla tiene filas y si el ID coincide
            return table.Rows.Count > 0 && Convert.ToInt32(table.Rows[0]["id_proveedor"]) == id_proveedor;
        }

        private void LimpiarCampos()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            id_proveedor = -1;
        }

        private void LimpiarCampos(object sender, EventArgs e) 
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            id_proveedor = -1;
        }

    }
}
