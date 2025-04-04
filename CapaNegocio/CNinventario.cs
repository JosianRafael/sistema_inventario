using CapaDatos;
using System.Data;
using System.Threading.Tasks;
using System;

namespace CapaNegocio
{
    public class CNinventario
    {
        public static string CN_Insertar_inventario(out int Idinventariogenerado, int id_inventario, int id_proveedor_producto, int Cantidad, string ubicacion, int stock_minimo,DateTime fecha_vencimiento)
        {
            Inventario inventario = new Inventario(id_inventario, id_proveedor_producto, Cantidad, ubicacion, stock_minimo,fecha_vencimiento);
            return inventario.InsertarInventario(out Idinventariogenerado,inventario);

        }//Fin metodo insertar inventario

        public static string CN_Actualizar_inventario(int id_inventario, int id_proveedor_producto, int cantidad, string ubicacion, int stock_minimo,DateTime fecha_vencimiento)
        {
            Inventario inventario = new Inventario(id_inventario, id_proveedor_producto, cantidad, ubicacion, stock_minimo,fecha_vencimiento);
            return inventario.ActualizarInventario(inventario);

        }//Fin metodo actualizar

        public static DataTable CN_Consultar_inventario(string parametrobusqueda)
        {
            Inventario inventario = new Inventario();
            return inventario.ConsultarInventario(parametrobusqueda);
        }//Fin  metodo consultar

        public static async Task <DataTable> CN_Consultar_inventarioAsync(string parametrobusqueda)
        {
            Inventario inventario = new Inventario();
            return await inventario.ConsultarInventarioAsync("");
        }//Fin  metodo consultar
    }
}