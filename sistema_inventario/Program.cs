using System;
using System.Windows.Forms;
using sistema_inventario.Vistas.splashscreen;
using sistema_inventario.Vistas.Inventario;
using sistema_inventario.Vistas.vistaProveedor;

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
            Application.Run(new AgregarProveedor());
        }
    }
}
