using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Proveedor
    {
        public int id_proveedor { get; set; }

        public string nombre { get; set; }

        public int telefono { get; set; }

        public string direccion { get; set; }


        public string correo_electronico_proveedor { get; set; }

        public string nombre_representante { get; set; }


        public Proveedor()
        {
        }

        public Proveedor(int id_proveedor, string nombre, int telefono, string direccion, string correo_electronico_proveedor, string nombre_representante)
        {
            this.id_proveedor = id_proveedor;
            this.nombre = nombre;
            this.telefono = telefono;
            this.direccion = direccion;
            this.correo_electronico_proveedor = correo_electronico_proveedor;

        }

        /// <summary>
        /// Inserta un proveedor toma como parametro un objeto de clase proveedor.
        /// </summary>
        public string InsertarProveedor(Proveedor proveedor)
        {
            string mensaje;
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    SqlCommand command = new SqlCommand("insertar_proveedor", conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nombre", proveedor.nombre);
                    command.Parameters.AddWithValue("@telefono", proveedor.telefono);
                    command.Parameters.AddWithValue("@direccion", proveedor.direccion);
                    command.Parameters.AddWithValue("@correo_electronico_proveedor", proveedor.correo_electronico_proveedor);
                    command.Parameters.AddWithValue("@nombre_representante", proveedor.nombre_representante);


                    mensaje = command.ExecuteNonQuery() == 1 ? "Datos completados correctamente" : "Hubo un problema :(";
                }

            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }

        /// <summary>
        /// Actualiza un proveedor toma como parametro un objeto de clase Proveedor
        /// </summary>
        public string ActualizarProveedor(Proveedor proveedor)
        {
            string mensaje = "";

            try
            {
                //usamos using con el objeto de conexion para gestionar la apertura y cierre de manera automatica
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    //Especificando comando
                    SqlCommand command = new SqlCommand("actualizar_proveedor", conexion);
                    //Indicando que es un procedimiento alamcenado
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nombre", proveedor.nombre);
                    command.Parameters.AddWithValue("@telefono", proveedor.telefono);
                    command.Parameters.AddWithValue("@direccion", proveedor.direccion);
                    command.Parameters.AddWithValue("@correo_electronico_proveedor", proveedor.correo_electronico_proveedor);
                    command.Parameters.AddWithValue("@nombre_representante", proveedor.nombre_representante);

                    mensaje = command.ExecuteNonQuery() == 1 ? "Actualización de datos completada correctamente!" : "Hubo un error al actualizar";

                }//Fin using conexion

            } //Fin try
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }//Fin cath

            return mensaje;
        } //FIn actualizar producto

        /// <summary>
        /// Consulta un proveedor a traves de su id o nombre de representante
        /// </summary>
        public DataTable ConsultarProveedor(string parametro)
        {
            //Data table que tomara los datos de los proveedores
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
                    SqlCommand command = new SqlCommand("consultar_proveedor", conexion);
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


    }
}


