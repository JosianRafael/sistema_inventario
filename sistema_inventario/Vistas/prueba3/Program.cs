using sistema_inventario.vistaFactura;
using sistema_inventario.vistaCliente;
using sistema_inventario.vistaProducto;
using System;
using System.Windows.Forms;

namespace sistema_inventario
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());
        }
    }
}
