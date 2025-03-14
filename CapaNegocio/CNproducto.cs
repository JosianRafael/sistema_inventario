using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class CNproducto
    {
        public static string CN_Insertar_producto(int id_producto, string nombre)
        {
            Producto producto = new Producto(id_producto, nombre);
            return producto.InsertarProducto(producto);

        }//Fin metodo insertar inventario

        public static string CN_Actualizar_producto(int id_producto, string nombre)
        {
            Producto producto = new Producto(id_producto, nombre);
            return producto.ActualizarProducto(producto);

        }//Fin metodo actualizar

        public static DataTable CN_Consultar_producto(string parametrobusqueda)
        {
            Producto producto = new Producto();
            return producto.ConsultarProducto(parametrobusqueda);
        }//Fin  metodo consultar
    }
}