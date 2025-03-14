using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Cliente
    {
        public int id_cliente { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }

        public Cliente()
        {
        }

        /// <summary>
        /// Constructor de cliente
        /// </summary>
        public Cliente(int id_cliente, string nombre, string apellido, string telefono, string direccion, string correo)
        {
            this.id_cliente = id_cliente;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.direccion = direccion;
            this.correo = correo;
        }

        /// <summary>
        /// Funcion de capa de datos para insertar cliente, toma como parametro la clase cliente.
        /// </summary>
        public string InsertarCliente(Cliente cliente)
        {
            string mensaje;
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    SqlCommand command = new SqlCommand("insertar_cliente", conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nombre", cliente.nombre);
                    command.Parameters.AddWithValue("@apellido", cliente.apellido);
                    command.Parameters.AddWithValue("@telefono", cliente.apellido);
                    command.Parameters.AddWithValue("@direccion", cliente.apellido);
                    command.Parameters.AddWithValue("@correo", cliente.apellido);
                    mensaje = command.ExecuteNonQuery() == 1 ? "Datos completados correctamente" : "Hubo un problema :(";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"El error es: {ex.Message}");
                mensaje = ex.Message;
            }
            return mensaje;
        }

        /// <summary>
        /// Actualizar campos de cliente toma como parametro la clase cliente
        /// </summary>
        public string ActualizarCliente(Cliente cliente)
        {
            string mensaje = "";

            try
            {
                //usamos using con el objeto de conexion para gestionar la apertura y cierre de manera automatica
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    //Especificando comando
                    SqlCommand command = new SqlCommand("actualizar_cliente",conexion);
                    //Indicando que es un procedimiento alamcenado
                    command.CommandType = CommandType.StoredProcedure;

                    //añadiendo parametros
                    command.Parameters.AddWithValue("@id_cliente", cliente.id_cliente);
                    command.Parameters.AddWithValue("@nombre", cliente.nombre);
                    command.Parameters.AddWithValue("@apellido", cliente.apellido);
                    command.Parameters.AddWithValue("@telefono", cliente.telefono);
                    command.Parameters.AddWithValue("@direccion", cliente.direccion);
                    command.Parameters.AddWithValue("@correo", cliente.correo);

                    mensaje = command.ExecuteNonQuery() == 1 ? "Actualización de datos completada correctamente!" : "Hubo un error al actualizar";

                }//Fin using conexion

            } //Fin try
            catch (Exception ex)
            {
                Console.WriteLine($"El error es: {ex.Message}");
                mensaje = ex.Message;
            }//Fin cath

            return mensaje;
        } //FIn actualizar producto

        /// <summary>
        /// Permite consultar un unico cliente usando su id o buscar concidencias de nombre o apellido, toma como parametro un string de numero o caracteres.
        /// </summary>
        public DataTable ConsultarCliente(string parametro)
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
                    SqlCommand command = new SqlCommand("consultar_cliente", conexion);
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
        } //FIn consultar producto


    }//Fin clase
}//Fin namespace
