using System.Data.SqlClient;

namespace CapaDatos
{
    class inventarioconexion
    {
        private static string cadenaconexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Inventario\CapaDatos\BaseDeDatos.mdf;Integrated Security=True";
  
        public static string ObtenerConexion() 
        {
            return cadenaconexion;
        }
    }

}
