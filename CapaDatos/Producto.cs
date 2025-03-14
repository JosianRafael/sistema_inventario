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

        //Constructor vacio
        public Producto()
        {
        }

        //Constructor con using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
        public Producto(int id_producto, string nombre)
        {
            this.id_producto = id_producto;
            this.nombre = nombre;
        }

        /// <summary>
        /// Inserta un nuevo producto toma como parametro un objeto de clase producto
        /// </summary>
        public string InsertarProducto(Producto producto)
        {
            string mensaje;
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    conexion.Open();
                    SqlCommand command = new SqlCommand("insertar_producto", conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nombre", producto.nombre);
                    mensaje = command.ExecuteNonQuery() == 1 ? "Datos completados correctamente" : "Hubo un problema";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"El error es: {ex.Message}");
                mensaje = ex.Message;
            }
            return mensaje;
        }//FIn metodo insertar

        /// <summary>
        /// Actualiza producto toma como parametro un objeto de clase producto
        /// </summary>
        public string ActualizarProducto(Producto producto)
        {
            string mensaje = "";

            try
            {
                //usamos using con el objeto de conexion para gestionar la apertura y cierre de manera automatica
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    conexion.Open();
                    //Especificando comando
                    SqlCommand command = new SqlCommand("actualizar_producto", conexion);
                    //Indicando que es un procedimiento alamcenado
                    command.CommandType = CommandType.StoredProcedure;

                    //añadiendo using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                    command.Parameters.AddWithValue("@id_producto", producto.id_producto);
                    command.Parameters.AddWithValue("@nombre_producto", producto.nombre);
                    mensaje = command.ExecuteNonQuery() == 1 ? "Actualización de datos completada correctamente!" : "Hubo un error al actualizar";

                }//Fin using conexion

            } //Fin try
            catch (Exception ex)
            {
                Console.WriteLine($"El error es: {ex.Message}");
                mensaje = ex.Message;
            }//Fin cath

            return mensaje;
        }//FIn metodo actualizar

        /// <summary>
        /// Consulta un producto por id o nombre devuelve un objeto DataTable
        /// </summary>
        public DataTable ConsultarProducto(string parametro)
        {
            //Data table que tomara los dato
            DataTable dt = new DataTable();

            //Creando el data reader
            SqlDataReader leerDatos;


            try
            {
                //usamos using con el objeto de conexion para gestionar la apertura y cierre de manera automatica
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    conexion.Open();
                    //Especificando comando
                    SqlCommand command = new SqlCommand("consultar_producto", conexion);
                    //Indicando que es un procedimiento alamcenado
                    command.CommandType = CommandType.StoredProcedure;

                    //añadiendo valor abuscar
                    command.Parameters.AddWithValue("@pvbusqueda", parametro);
                    leerDatos = command.ExecuteReader(); //GUardamos los datos resultantes en leerdatos
                    dt.Load(leerDatos); //Se cargan los datos devueltos en dt

                }//Fin using conexion

            } //Fin try
            catch (Exception ex)
            {
                Console.WriteLine($"El error que ocurrio fue: {ex.Message}");
                dt = null; //Si hay un error se anula el dt
            }//Fin cath

            return dt;
        } //Fin consultar producto

    }//Fin clase
}//Fin namespace
