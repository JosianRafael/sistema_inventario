using CapaDatos;
using System.Data;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNproveedorProducto
    {
        public static string CN_Insertar_proveedor_producto(out int IDproveedorProducto,int id_proveedor, int id_producto, float costo, float precio_venta)
        {
            ProveedorProducto proveedorProducto = new ProveedorProducto(0, id_proveedor, id_producto, costo, precio_venta);
            return proveedorProducto.InsertarProveedorProducto(out IDproveedorProducto,proveedorProducto);
        }//Fin metodo insertar

        public static string CN_Actualizar_proveedor_producto(int id_proveedor_producto, int id_proveedor, int id_producto, float costo, float precio_venta)
        {
            ProveedorProducto proveedorProducto = new ProveedorProducto(id_proveedor_producto, id_proveedor, id_producto, costo, precio_venta);
            return proveedorProducto.ActualizarProveedorProducto(proveedorProducto);
        }//Fin metodo actualizar

        public static DataTable CN_Consultar_proveedor_producto(string parametrobusqueda)
        {
            ProveedorProducto proveedorProducto = new ProveedorProducto();
            return proveedorProducto.ConsultarProveedorProducto(parametrobusqueda);
        }//Fin  metodo consultar

        public static async Task<DataTable> CN_Consultar_proveedor_producto_async(string parametrobusqueda) 
        {
            return await new ProveedorProducto().ConsultarProveedorProductoAsync(parametrobusqueda);
        }

        public static async Task<DataTable> CN_Consultar_proveedor_producto_async_por_nombre_producto(string parametrobusqueda)
        {
            return await new ProveedorProducto().ConsultarProveedorProductoAsyncPorNombreProducto(parametrobusqueda);
        }

    }//Fin clase
}//Fin namespace
