using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Inventario
    {
        public int id_inventario { get; set; }

        public int id_proveedor_producto { get; set; }

        public int cantidad { get; set; }

        public string ubicacion { get; set; }

        public int stock_minimo { get; set; }

        //Constructor vacío
        public Inventario()
        {
        }//fin constructor sin parametros
        //Constructor con parametros
        public Inventario(int id_inventario, int id_proveedor_producto, int cantidad, string ubicacion, int stock_minimo)
        {
            this.id_inventario = id_inventario;
            this.id_proveedor_producto = id_proveedor_producto;
            this.cantidad = cantidad;
            this.ubicacion = ubicacion;
            this.stock_minimo = stock_minimo;

        }//fin constructor con parametros

        /// <summary>
        /// Insertar inventario en la base de datos, toma como parametro un objeto de clase Iventario
        /// </summary>
        public string InsertarInventario(Inventario inventario)
        {
            string mensaje = "";

            try
            {
                //usamos using con el objeto de conexion para gestionar la apertura y cierre de manera automatica
                using (SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Inventario\CapaDatos\BaseDeDatos.mdf;Integrated Security=True"))
                {
                    conexion.Open();
                    //Especificando comando
                    SqlCommand command = new SqlCommand("insertar_inventario", conexion);
                    //Indicando que es un procedimiento alamcenado
                    command.CommandType = CommandType.StoredProcedure;

                    //añadiendo parametros
                    command.Parameters.AddWithValue("@id_proveedor_producto", inventario.id_proveedor_producto);
                    command.Parameters.AddWithValue("@cantidad", inventario.cantidad);
                    command.Parameters.AddWithValue("@ubicacion", inventario.ubicacion);
                    command.Parameters.AddWithValue("@stock_minimo", inventario.stock_minimo);
                    mensaje = command.ExecuteNonQuery() == 1 ? "Inserción de datos completada correctamente!" : "Hubo un error al insertar";

                }//Fin using conexion

            } //Fin try
            catch (Exception ex)
            {
                mensaje = ex.Message;
                Console.WriteLine($"Error insertar {mensaje}");
            }//Fin cath

            return mensaje;
        } //FIn insertar producto

        /// <summary>
        /// Actualizar el inventario inventario toma como parametro un objeto de clase Inventario
        /// </summary>
        public string ActualizarInventario(Inventario inventario)
        {
            string mensaje = "";

            try
            {
                //usamos using con el objeto de conexion para gestionar la apertura y cierre de manera automatica
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    //Especificando comando
                    SqlCommand command = new SqlCommand("actualizar_inventario", conexion);
                    //Indicando que es un procedimiento alamcenado
                    command.CommandType = CommandType.StoredProcedure;

                    //añadiendo parametros
                    command.Parameters.AddWithValue("@id_inventario", inventario.id_inventario);
                    command.Parameters.AddWithValue("@id_proveedor_producto", inventario.id_proveedor_producto);
                    command.Parameters.AddWithValue("@cantidad", inventario.cantidad);
                    command.Parameters.AddWithValue("@ubicacion", inventario.ubicacion);
                    command.Parameters.AddWithValue("@stock_minimo", inventario.stock_minimo);

                    mensaje = command.ExecuteNonQuery() == 1 ? "Actualización de datos completada correctamente!" : "Hubo un error al actualizar";

                }//Fin using conexion

            } //Fin try
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }//Fin cath

            return mensaje;
        } //FIn actualizar producto

        /// <summary>
        /// Consulta inventario usando el id, o busca concidencias en base a nombre del producto
        /// </summary>
        public DataTable ConsultarInventario(string parametro)
        {
            //Data table que tomara los datos de los suplidores
            DataTable dt = new DataTable();

            //Creando el data reader
            SqlDataReader leerDatos;


            try
            {
                //usamos using con el objeto de conexion para gestionar la apertura y cierre de manera automatica
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    //Especificando comando
                    SqlCommand command = new SqlCommand("consultar_inventario", conexion);
                    //Indicando que es un procedimiento alamcenado
                    command.CommandType = CommandType.StoredProcedure;

                    //añadiendo valor abuscar
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
        } //Fin consultar producto

    } //Fin class
} //Fin namespace