using CapaDatos;
using System;
using System.Data;

namespace CapaNegocio
{
    public class CNfactura
    {
        public static string CN_Insertar_factura(int id_factura, int id_cliente, int id_empleado, float total, string metodo_pago, DateTime fecha)
        {

            Factura factura = new Factura(id_factura, id_cliente, id_empleado, total, metodo_pago, fecha);
            return factura.InsertarFactura(factura);

        }//Fin metodo insertar inventario

        public static string CN_Actualizar_factura(int id_factura, int id_cliente, int id_empleado, float total, string metodo_pago, DateTime fecha)
        {
            Factura factura = new Factura(id_factura, id_cliente, id_empleado, total, metodo_pago, fecha);
            return factura.ActualizarFactura(factura);

        }//Fin metodo actualizar

        public static DataTable CN_Consultar_factura(string parametrobusqueda)
        {
            Factura factura = new Factura();
            return factura.ConsultarFactura(parametrobusqueda);
        }//Fin  metodo consultar
    }
}