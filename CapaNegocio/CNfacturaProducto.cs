using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class CNfacturaProducto
    {
        public static string CN_Insertar_factura_producto(int idFactura, int Idinventario, int Cantidad)
        {
            FacturaProducto facturaProducto = new FacturaProducto(0, idFactura, Idinventario, Cantidad);
            return facturaProducto.InsertarFacturaProducto(facturaProducto);
        }//Fin metodo insertar

        public static string CN_Actualizar_factura_producto(int IDFactura_producto, int idFactura, int Idinventario, int Cantidad)
        {
            FacturaProducto facturaProducto = new FacturaProducto(IDFactura_producto, idFactura, Idinventario, Cantidad);
            return facturaProducto.ActualizarFacturaProducto(facturaProducto);
        }//Fin metodo actualizar

        public static DataTable CN_Consultar_factura_producto(string parametrobusqueda)
        {
            FacturaProducto facturaProducto = new FacturaProducto();
            return facturaProducto.ConsultarFacturaProducto(parametrobusqueda);
        }//Fin  metodo consultar
    }
}
