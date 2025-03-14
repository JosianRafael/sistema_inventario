using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Factura
    {
        public int id_factura { get; set; }
        public int id_cliente { get; set; }

        public int id_empleado { get; set; }

        public float total { get; set; }

        public string metodo_pago { get; set; }

        public DateTime fecha { get; set; }

        public Factura()
        {
        }

        public Factura(int id_factura, int id_cliente, int id_empleado, float total, string metodo_pago, DateTime fecha)
        {
            this.id_factura = id_factura;
            this.id_cliente = id_cliente;
            this.id_empleado = id_empleado;
            this.total = total;
            this.metodo_pago = metodo_pago;
            this.fecha = fecha;
        }

        /// <summary>
        /// Inserta una factura en la base de datos, toma como parametro un objeto de clase Factura.
        /// </summary>
        public string InsertarFactura(Factura factura)
        {
            string mennsage;
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("insertar_factura", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_cliente", factura.id_cliente);
                        cmd.Parameters.AddWithValue("@id_empleado", factura.id_empleado);
                        cmd.Parameters.AddWithValue("@metodo_pago", factura.metodo_pago);
                        cmd.Parameters.AddWithValue("@total", factura.total);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Factura insertada correctamente.");
                        mennsage = "Factura insertada correctamente";
                    }
                }
            }
            catch (SqlException ex)
            {
                mennsage = "Hubo un error al insertar factura";
                Console.WriteLine($"Error: {ex.Message}");
            }

            return mennsage;
        }

        /// <summary>
        /// Actualiza los datos de una factura, toma como parametro los objetosde clase Factura
        /// </summary>
        public string ActualizarFactura(Factura factura)
        {
            string mennsage;
            try
            {
                using (SqlConnection conexion = new SqlConnection(inventarioconexion.ObtenerConexion() ))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("actualizar_factura", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_factura", factura.id_factura);
                        cmd.Parameters.AddWithValue("@id_cliente", factura.id_cliente);
                        cmd.Parameters.AddWithValue("@id_empleado", factura.id_empleado);
                        cmd.Parameters.AddWithValue("@metodo_pago", factura.metodo_pago);
                        cmd.Parameters.AddWithValue("@total", factura.total);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Factura insertada correctamente.");
                        mennsage = "Factura insertada correctamente";
                    }
                }
            }
            catch (SqlException ex)
            {
                mennsage = "Hubo un error al insertar factura";
                Console.WriteLine($"Error: {ex.Message}");
            }

            return mennsage;
        }

        /// <summary>
        /// Consulta una factura usando su id devuelve objeto de calse DataTable
        /// </summary>
        public DataTable ConsultarFactura(string parametro)
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
                    SqlCommand command = new SqlCommand("consultar_factura", conexion);
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
        }
    }
}