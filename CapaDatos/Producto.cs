using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Producto
    {
        //Propiedades
        public int id_producto { get; set; }
        public string nombre { get; set; }
         public string descripcion { get; set; }
        public string codigo_barras { get; set; }


        //Constructor vacio
        public Producto()
        {
        }

        //Constructor con using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
        public Producto(int id, string nombre, string descripcion,
                      string codigoBar)
        {
            this.id_producto = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.codigo_barras = codigoBar;
        }

        /// <summary>
        /// Inserta un nuevo producto toma como parametro un objeto de clase producto
        /// </summary>
        public string InsertarProducto(out int idProducto, Producto producto)
        {
            idProducto = 0;
            string mensaje;
            try
            {
                using (SqlConnection connection = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    SqlCommand command = new SqlCommand("insertar_producto", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramId = new SqlParameter("@idproducto", SqlDbType.Int);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("@nombre", producto.nombre);
                    command.Parameters.AddWithValue("@descripcion",
                        string.IsNullOrEmpty(producto.descripcion) ? (object)DBNull.Value : producto.descripcion);
                    command.Parameters.AddWithValue("@codigo_barras",
                        string.IsNullOrEmpty(producto.codigo_barras) ? (object)DBNull.Value : producto.codigo_barras);
                    connection.Open();
                    command.ExecuteNonQuery();

                    idProducto = Convert.ToInt32(paramId.Value);

                    mensaje = "Producto insertado correctamente";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al insertar producto: " + ex.Message;
            }
            return mensaje;
        }


        /// <summary>
        /// Actualiza producto toma como parametro un objeto de clase producto
        /// </summary>
        public string ActualizarProducto(Producto producto)
        {
            string mensaje;
            try
            {
                using (SqlConnection connection = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    SqlCommand command = new SqlCommand("actualizar_producto", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_producto", producto.id_producto);
                    command.Parameters.AddWithValue("@nombre", producto.nombre);
                    command.Parameters.AddWithValue("@descripcion",
                        string.IsNullOrEmpty(producto.descripcion) ? (object)DBNull.Value : producto.descripcion);
                    command.Parameters.AddWithValue("@codigo_barras",
                        string.IsNullOrEmpty(producto.codigo_barras) ? (object)DBNull.Value : producto.codigo_barras);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    mensaje = result == 1 ? "Producto actualizado correctamente!" : "Producto actualizado correctamente!";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar producto: " + ex.Message;
            }
            return mensaje;
        }


        /// <summary>
        /// Consulta un producto por id o nombre devuelve un objeto DataTable
        /// </summary>
        public DataTable ConsultarProducto(string parametro)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    SqlCommand command = new SqlCommand("consultar_producto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pvbusqueda", string.IsNullOrEmpty(parametro) ? (object)DBNull.Value : parametro);

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar productos: {ex.Message}");
                dt = null;
            }
            return dt;
        }


    }//Fin clase
}//Fin namespace
