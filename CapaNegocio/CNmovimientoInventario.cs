using CapaDatos;
using System;
using System.Data;

namespace CapaNegocio
{
    public class CNmovimientoInventario
    {
        public static string CN_Insertar_movimiento_inventario(int id_movimiento_inventario, int id_inventario, string tipo, int cantidad, DateTime fecha)
        {
            Movimiento_inventario movimiento_Inventario = new Movimiento_inventario(id_movimiento_inventario, id_inventario, tipo, cantidad, fecha);
            return movimiento_Inventario.InsertarMovimientoInventario(movimiento_Inventario);
        }//Fin metodo insertar

        public static DataTable CN_Consultar_movimiento_inventario(string parametrobusqueda)
        {
            Movimiento_inventario movimiento_Inventario = new Movimiento_inventario();
            return movimiento_Inventario.ConsultarMovimientoInventario(parametrobusqueda);
        }//Fin  metodo consultar

    }//Fin clase
}//FIn namcespace
