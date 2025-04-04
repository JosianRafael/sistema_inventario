using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Inventario
    {
        public int id_inventario { get; set; }

        public int id_proveedor_producto { get; set; }

        public int cantidad { get; set; }

        public string ubicacion { get; set; }

        public int stock_minimo { get; set; }

        public DateTime fecha_vencimiento { get; set; }

        //Constructor vacío
        public Inventario()
        {
        }//fin constructor sin parametros
        //Constructor con parametros
        public Inventario(int id_inventario, int id_proveedor_producto, int cantidad, string ubicacion, int stock_minimo, DateTime fecha_vencimiento)
        {
            this.id_inventario = id_inventario;
            this.id_proveedor_producto = id_proveedor_producto;
            this.cantidad = cantidad;
            this.ubicacion = ubicacion;
            this.stock_minimo = stock_minimo;
            this.fecha_vencimiento = fecha_vencimiento;

        }//fin constructor con parametros

        /// <summary>
        /// Insertar inventario en la base de datos, toma como parametro un objeto de clase Iventario
        /// </summary>
        public string InsertarInventario(out int Idinventariogenerado,Inventario inventario)
        {
            string mensaje = "";
            Idinventariogenerado = 0;
            try
            {
                //usamos using con el objeto de conexion para gestionar la apertura y cierre de manera automatica
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion()))
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
                    command.Parameters.AddWithValue("@fecha_vencimiento", inventario.fecha_vencimiento);

                    // Parámetro de salida para el ID generado
                    SqlParameter outputIdParam = new SqlParameter("@id_inventario", SqlDbType.Int);
                    outputIdParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputIdParam);

                    mensaje = command.ExecuteNonQuery() == 1 ? "Inserción de datos completada correctamente!" : "Hubo un error al insertar";

                    Idinventariogenerado = (int)command.Parameters["@id_inventario"].Value;

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

        public async Task<DataTable> ConsultarInventarioAsync(string parametro)
        {
            //Data table que tomara los datos de los suplidores
            DataTable dt = new DataTable();

            //Creando el data reader
            SqlDataReader leerDatos;


            try
            {
                //usamos using con el objeto de conexion para gestionar la apertura y cierre de manera automatica
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    await conexion.OpenAsync();
                    //Especificando comando
                    SqlCommand command = new SqlCommand("consultar_inventario", conexion);
                    //Indicando que es un procedimiento alamcenado
                    command.CommandType = CommandType.StoredProcedure;

                    //añadiendo valor abuscar
                    command.Parameters.AddWithValue("@pvbusqueda", parametro);
                    leerDatos = await command.ExecuteReaderAsync(); //GUardamos los datos resultantes en leerdatos
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