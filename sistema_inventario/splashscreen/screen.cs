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

namespace sistema_inventario.Vistas.splashscreen
{
    public partial class screen : Form
    {
        private Timer timer;
        private int animationStep = 0;
        private string textooriginal;

        public screen()
        {
            InitializeComponent();

            //// Definir un color cuando el ratón pasa sobre el botón
            button1.MouseEnter += Button1_MouseEnter;
            button1.MouseLeave += Button1_MouseLeave;
            button1.Click += Cancelar;

            this.DoubleBuffered = true;
            AjustarTitulo();
            this.Load += SplashScreen_Load;
        }

        private async void SplashScreen_Load(object sender, EventArgs e)
        {
            estado.Visible = true;

            estado.Text = "Cargando Lista movimientos inventario";
            textooriginal = estado.Text;
            AjustarTextoCentrado();
            StartLoadingAnimation();

            await InventarioListaProductos.CargarListaMovimientosInventaario();
            progressBar1.Value = 30;

            estado.Text = "Cargando Listado de productos inventario";
            textooriginal = estado.Text;
            AjustarTextoCentrado();

            await InventarioListaProductos.CargarListaProductosInventario();
            progressBar1.Value = 60;

            // Ejecutando progresos
            estado.Text = "Cargando listado de productos";
            textooriginal = estado.Text;
            AjustarTextoCentrado();
            await InventarioListaProductos.CargarListaProductos();
            progressBar1.Value = 100;

            StopLoadingAnimation();

            this.Close();
        }

        private void StartLoadingAnimation()
        {
            timer = new Timer();
            timer.Interval = 500; // Cambiar cada 500ms
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Detener la animación
        private void StopLoadingAnimation()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string Textobase = textooriginal;

            animationStep++;
            if (animationStep > 3)
            {
                animationStep = 0;
                Textobase = textooriginal;
            }
                
            switch (animationStep)
            {
                case 0:
                    estado.Text = Textobase + " .";
                    break;
                case 1:
                    estado.Text = Textobase + " ..";
                    break;
                case 2:
                    estado.Text = Textobase + " ...";
                    break;
                case 3:
                    estado.Text = Textobase + " ...."; // Reinicia a 0 después de 3 puntos
                    break;
            }
        }

        private void AjustarTextoCentrado()
        {
            // Asegurarse de que el texto esté visible y listo para ser centrado
            estado.Visible = true;

            // Calculamos la posición X para centrar el texto
            int posicionX = (this.ClientSize.Width - estado.PreferredWidth) / 2;

            // Establecer la nueva ubicación del Label
            estado.Location = new Point(posicionX, estado.Location.Y);

            // Centrar el texto dentro del Label
            estado.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void AjustarTitulo()
        {
 
            // Calculamos la posición X para centrar el texto
            int posicionX = (this.ClientSize.Width - label1.PreferredWidth) / 2;

            // Establecer la nueva ubicación del Label
            label1.Location = new Point(posicionX, label1.Location.Y);

        }

        // Cambiar el color cuando el ratón pasa sobre el botón
        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Crimson;  // Cambiar el color cuando el ratón pasa por encima
        }

        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.DarkMagenta;  // Restaurar color original al salir el ratón
        }

        // Cambiar el color cuando el botón es presionado
        private void Button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.BackColor = Color.DeepSkyBlue;  // Cambiar color cuando se presiona
            button1.ForeColor = Color.Black;  // Cambiar color del texto cuando se presiona
        }

        private void Cancelar(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo de confirmación
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas salir?",  // Texto del mensaje
                "Confirmar salida",  // Título del cuadro de mensaje
                MessageBoxButtons.YesNo,  // Botones disponibles
                MessageBoxIcon.Question  // Icono que muestra el cuadro de mensaje
            );

            // Verificar si el usuario seleccionó 'Sí'
            if (result == DialogResult.Yes)
            {
                // Si es así, cerrar la aplicación y finalizar todos los procesos
                Application.Exit();
            }
            else
            {

            }
        }

    }
}
