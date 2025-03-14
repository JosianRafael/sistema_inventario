using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class CNProveedor
    {
        public static string CN_Insertar_Proveedor(string nombre, int telefono, string direccion, string correo_electronico_proveedor, string nombre_representante)
        {
            Proveedor proveedor = new Proveedor(0, nombre, telefono, direccion, correo_electronico_proveedor, nombre_representante);
            return proveedor.InsertarProveedor(proveedor);
        }//Fin metodo insertar

        public static string CN_Actualizar_Proveedor(int id_proveedor, string nombre, int telefono, string direccion, string correo_electronico_proveedor, string nombre_representante)
        {
            Proveedor proveedor = new Proveedor(id_proveedor, nombre, telefono, direccion, correo_electronico_proveedor, nombre_representante);
            return proveedor.ActualizarProveedor(proveedor);
        }//Fin metodo actualizar

        public static DataTable CN_Consultar_Proveedor(string parametrobusqueda)
        {
            Proveedor proveedor = new Proveedor();
            return proveedor.ConsultarProveedor(parametrobusqueda);
        }//Fin  metodo consultar
    }//Fin clase
}//Fin namespace