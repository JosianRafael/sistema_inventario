using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ProveedorProducto
    {
        public int IdProveedorProducto { get; set; }
        public int IdProveedor { get; set; }
        public int IdProducto { get; set; }
        public float Costo { get; set; }
        public float PrecioVenta { get; set; }

        public ProveedorProducto() { }

        public ProveedorProducto(int idProveedorProducto, int idProveedor, int idProducto, float costo, float precioVenta)
        {
            IdProveedorProducto = idProveedorProducto;
            IdProveedor = idProveedor;
            IdProducto = idProducto;
            Costo = costo;
            PrecioVenta = precioVenta;
        }

        /// <summary>
        /// Inserta los productos que te provee un proveedor toma como parametro un objeto de la clase proveedor producto
        /// </summary>
        public string InsertarProveedorProducto(ProveedorProducto proveedorProducto)
        {
            string mensaje;
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    SqlCommand command = new SqlCommand("insertar_proveedor_producto", conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_proveedor", proveedorProducto.IdProveedor);
                    command.Parameters.AddWithValue("@id_producto", proveedorProducto.IdProducto);
                    command.Parameters.AddWithValue("@costo", proveedorProducto.Costo);
                    command.Parameters.AddWithValue("@precio_venta", proveedorProducto.PrecioVenta);

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
        /// Actualiza un proveedor producto toma como parametro un objeto de la clase proveedorproducto
        /// </summary>
        public string ActualizarProveedorProducto(ProveedorProducto proveedorProducto)
        {
            string mensaje;
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    SqlCommand command = new SqlCommand("actualizar_proveedor_producto", conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_proveedor_producto", proveedorProducto.IdProveedorProducto);
                    command.Parameters.AddWithValue("@id_proveedor", proveedorProducto.IdProveedor);
                    command.Parameters.AddWithValue("@id_producto", proveedorProducto.IdProducto);
                    command.Parameters.AddWithValue("@costo", proveedorProducto.Costo);
                    command.Parameters.AddWithValue("@precio_venta", proveedorProducto.PrecioVenta);

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
        /// Consulta un proveedor producto por id de proveedor producto o concidencias de nombre de proveedor
        /// </summary>
        public DataTable ConsultarProveedorProducto(string parametro)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    SqlCommand command = new SqlCommand("consultar_proveedor_producto", conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pvbusqueda", parametro);

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

        public async Task<DataTable> ConsultarProveedorProductoAsync(string parametro)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    await conexion.OpenAsync(); // Asegurarse de abrir la conexión de forma asincrónica

                    using (SqlCommand command = new SqlCommand("consultar_proveedor_producto", conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@pvbusqueda", parametro);

                        using (SqlDataReader leerDatos = await command.ExecuteReaderAsync()) // Ejecutar el lector de datos de manera asincrónica
                        {
                            dt.Load(leerDatos);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                dt = null;
            }
            return dt;
        }

        public async Task<DataTable> ConsultarProveedorProductoAsyncPorNombreProducto(string parametro)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    await conexion.OpenAsync(); // Asegurarse de abrir la conexión de forma asincrónica

                    using (SqlCommand command = new SqlCommand("Consultar_proveedor_producto_por_nombre", conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@pvbusqueda", parametro);

                        using (SqlDataReader leerDatos = await command.ExecuteReaderAsync()) // Ejecutar el lector de datos de manera asincrónica
                        {
                            dt.Load(leerDatos);
                        }
                    }
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