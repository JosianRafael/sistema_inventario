using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Empleado
    {
        public int id_empleado { get; set; }

        public string cedula { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string telefono { get; set; }

        public string direccion { get; set; }

        public string correo_electronico_empleado { get; set; }

        public string status { get; set; }

        public string acceso { get; set; }

        public Empleado()
        {
        }

        public Empleado(int id_empleado, string cedula, string nombre, string apellido, string telefono, string direccion, string correo_electronico_empleado, string status, string acceso)
        {
            this.id_empleado = id_empleado;
            this.cedula = cedula;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.direccion = direccion;
            this.correo_electronico_empleado = correo_electronico_empleado;
            this.status = status;
            this.acceso = acceso;
        }
        /// <summary>
        /// Insertar un nuevo empleado toma como parametro un objeto de clase empleado.
        /// </summary>
        public string InsertarEmpleado(Empleado empleado)
        {
            string mensaje;
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    SqlCommand command = new SqlCommand("insertar_empleado", conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cedula", empleado.cedula);
                    command.Parameters.AddWithValue("@nombre", empleado.nombre);
                    command.Parameters.AddWithValue("@apellido", empleado.apellido);
                    command.Parameters.AddWithValue("@telefono", empleado.telefono);
                    command.Parameters.AddWithValue("@direccion", empleado.direccion);
                    command.Parameters.AddWithValue("@correo_electronico_empleado", empleado.correo_electronico_empleado);
                    command.Parameters.AddWithValue("@status", empleado.status);
                    command.Parameters.AddWithValue("@acceso", empleado.acceso);


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
        /// Actualiza los datos de un empleado toma como parametro un objeto clase empleado.
        /// </summary>
        public string ActualizarEmpleado(Empleado empleado)
        {
            string mensaje = "";

            try
            {
                //usamos using con el objeto de conexion para gestionar la apertura y cierre de manera automatica
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    //Especificando comando
                    SqlCommand command = new SqlCommand("actualizar_empleado", conexion);
                    //Indicando que es un procedimiento alamcenado
                    command.CommandType = CommandType.StoredProcedure;

                    //añadiendo parametros
                    command.Parameters.AddWithValue("@id_empleado", empleado.id_empleado);
                    command.Parameters.AddWithValue("@nombre", empleado.nombre);
                    command.Parameters.AddWithValue("@apellido", empleado.apellido);
                    command.Parameters.AddWithValue("@cedula", empleado.cedula);
                    command.Parameters.AddWithValue("@telefono", empleado.telefono);
                    command.Parameters.AddWithValue("@direccion", empleado.direccion);
                    command.Parameters.AddWithValue("@correo_electronico_empleado", empleado.correo_electronico_empleado);
                    command.Parameters.AddWithValue("@status", empleado.status);
                    command.Parameters.AddWithValue("@acceso", empleado.acceso);

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
        /// Consulta un empleado usando el id o busca concidencias usando nombre, apellido, devuelve un objeto de clase DataTable.
        /// </summary>
        public DataTable ConsultarEmpleado(string parametro)
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
                    SqlCommand command = new SqlCommand("consultar_empleado", conexion);
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

