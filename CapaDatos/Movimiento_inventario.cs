using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Movimiento_inventario
    {
        public int id_movimiento_inventario { get; set; }
        public int id_inventario { get; set; }
        public string tipo { get; set; }
        public int cantidad { get; set; }
        public DateTime fecha { get; set; }

        //Constructor vacío
        public Movimiento_inventario()
        {
        }//fin constructor sin parametros
        //Constructor con parametros
        public Movimiento_inventario(int id_movimiento_inventario, int id_inventario, string tipo, int cantidad, DateTime fecha)
        {
            this.id_movimiento_inventario = id_movimiento_inventario;
            this.id_inventario = id_inventario;
            this.tipo = tipo;
            this.cantidad = cantidad;
            this.fecha = fecha;
        }//fin constructor con parametros

        /// <summary>
        /// Inserta el movimiento de inventario toma como parametro un objeto de clase movimiento inventario.
        /// </summary>
        public string InsertarMovimientoInventario(Movimiento_inventario movimiento_inventario)
        {
            string mensaje = "";

            try
            {
                //usamos using con el objeto de conexion para gestionar la apertura y cierre de manera automatica
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    //Especificando comando
                    SqlCommand command = new SqlCommand("insertar_movimiento_inventario", conexion);
                    //Indicando que es un procedimiento alamcenado
                    command.CommandType = CommandType.StoredProcedure;

                    //añadiendo parametros
                    command.Parameters.AddWithValue("@id_inventario", movimiento_inventario.id_inventario);
                    command.Parameters.AddWithValue("@tipo", movimiento_inventario.tipo);
                    command.Parameters.AddWithValue("@cantidad", movimiento_inventario.cantidad);
                    mensaje = command.ExecuteNonQuery() == 1 ? "Inserción de datos completada correctamente!" : "Hubo un error al insertar";

                }//Fin using conexion

            } //Fin try
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }//Fin cath

            return mensaje;
        } //FIn insertar movmiento inventario

        /// <summary>
        /// Consulta movimiento inventario usando como parametro id del inventario o por concidencias del tipo de movimiento
        /// </summary>
        public DataTable ConsultarMovimientoInventario(string parametro)
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
                    SqlCommand command = new SqlCommand("consultar_movimiento_inventario", conexion);
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
        } //Fin consultar movimiento inventario
    }
}