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

namespace sistema_inventario.Vistas.vistaProveedor
{
    public partial class AgregarProveedor : Form
    {
        public AgregarProveedor()
        {
            InitializeComponent();
            button1.Click += GuardarProveedor;
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


            string resultado = CNProveedor.CN_Insertar_Proveedor(nombre,telefono,direccion,correo,Nonbre_representante);

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            MessageBox.Show(resultado);
        }

    }
}
