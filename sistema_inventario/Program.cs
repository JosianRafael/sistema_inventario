using System;
using System.Windows.Forms;
using sistema_inventario.splashscreen;
using sistema_inventario.Inventario;
using sistema_inventario.vistaProveedor;
using sistema_inventario.FCliente;

namespace sistema_inventario
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (screen splash = new screen())
            {
                splash.ShowDialog();
            }
            Application.Run(new formulariocliente());
        }
    }
}
