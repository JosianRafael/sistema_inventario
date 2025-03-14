using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class FacturaProducto
    {
        public int IdFacturaProducto { get; set; }
        public int IdFactura { get; set; }
        public int IdInventario { get; set; }
        public int Cantidad { get; set; }

        public FacturaProducto() { }

        public FacturaProducto(int idFacturaProducto, int idFactura, int idInventario, int cantidad)
        {
            IdFacturaProducto = idFacturaProducto;
            IdFactura = idFactura;
            IdInventario = idInventario;
            Cantidad = cantidad;
        }

        /// <summary>
        /// Insertar factura producto, (los productos de una factura) toma como parametro un objeto de la clase FacturaProducto
        /// </summary>
        public string InsertarFacturaProducto(FacturaProducto factura)
        {
            string mensaje;
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    conexion.Open();
                    SqlCommand command = new SqlCommand("insertar_factura_producto", conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_factura", factura.IdFactura);
                    command.Parameters.AddWithValue("@id_inventario", factura.IdInventario);
                    command.Parameters.AddWithValue("@cantidad", factura.Cantidad);

                    mensaje = command.ExecuteNonQuery() == 1 ? "Datos insertados correctamente" : "Error al insertar datos";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }
        /// <summary>
        /// Actualizar factura producto actualiza los productos de una factura toma
        /// </summary>
        public string ActualizarFacturaProducto(FacturaProducto factura)
        {
            string mensaje;
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    conexion.Open();
                    SqlCommand command = new SqlCommand("actualizar_factura_producto", conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_factura_producto", factura.IdFacturaProducto);
                    command.Parameters.AddWithValue("@id_factura", factura.IdFactura);
                    command.Parameters.AddWithValue("@id_inventario", factura.IdInventario);
                    command.Parameters.AddWithValue("@cantidad", factura.Cantidad);

                    mensaje = command.ExecuteNonQuery() == 1 ? "Actualización completada" : "Error al actualizar";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }

        /// <summary>
        /// Consulta los productos de una factura usando el id de una factura devuelve un objeto DataTable
        /// </summary>
        public DataTable ConsultarFacturaProducto(string parametro)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    conexion.Open();
                    SqlCommand command = new SqlCommand("consultar_factura_producto", conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@parametro", parametro);

                    SqlDataReader leerDatos = command.ExecuteReader();
                    dt.Load(leerDatos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                dt = null;
            }
            return dt;
        }
    }
}