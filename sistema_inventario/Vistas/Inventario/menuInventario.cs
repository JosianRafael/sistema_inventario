using System.Windows.Forms;

namespace sistema_inventario.Vistas.Inventario
{
    public partial class menuInventario : Form
    {
        public menuInventario()
        {
            InitializeComponent();
        }

        // Este es el método que usas para cargar los formularios dentro del panel contentPanel
        private void CargarForma(Form childForm)
        {
            if (childForm == null)
            {
                MessageBox.Show("La vista aún no está disponible.");
                return;
            }

            // Prevenir recursividad
            if (childForm == this)
            {
                MessageBox.Show("Este formulario no se puede cargar dentro de sí mismo.");
                return;
            }

            // Cargar el formulario en el contentPanel del formulario principal (Menu)
            if (this.Owner is Menu menu)
            {
                menu.SetContentPanel(childForm);
                childForm.Show();
            }
            else
            {
                MessageBox.Show("Formulario principal no accesible.");
            }
        }

    }
}
