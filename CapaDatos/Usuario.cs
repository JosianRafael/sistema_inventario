using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string CodigoAcceso { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Usuario() { }

        public Usuario(int idUsuario, string codigoAcceso, string nombre, string apellido,
                      string email, string contrasena, string rol, bool activo, DateTime fechaCreacion)
        {
            IdUsuario = idUsuario;
            CodigoAcceso = codigoAcceso;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Contrasena = contrasena;
            Rol = rol;
            Activo = activo;
            FechaCreacion = fechaCreacion;
        }

        public DataTable ValidarUsuarioPorCodigo(string codigoAcceso)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_ValidarUsuarioPorCodigo", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CodigoAcceso", codigoAcceso);

                    SqlDataReader leerDatos = command.ExecuteReader();
                    dt.Load(leerDatos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al validar usuario: {ex.Message}");
                dt = null;
            }
            return dt;
        }

        public string InsertarUsuario(Usuario usuario)
        {
            string mensaje;
            try
            {
                using (SqlConnection connection = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_InsertarUsuario", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CodigoAcceso", usuario.CodigoAcceso);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                    command.Parameters.AddWithValue("@Rol", usuario.Rol);
                    command.Parameters.AddWithValue("@Activo", usuario.Activo);

                    int result = command.ExecuteNonQuery();
                    mensaje = result == 1 ? "Usuario insertado correctamente." : "No se pudo insertar el usuario.";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al insertar usuario: " + ex.Message;
            }
            return mensaje;
        }

        public DataTable ConsultarUsuarios(string parametro)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(inventarioconexion.ObtenerConexion()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_ConsultarUsuarios", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ParametroBusqueda", string.IsNullOrEmpty(parametro) ? (object)DBNull.Value : parametro);

                    SqlDataReader leerDatos = command.ExecuteReader();
                    dt.Load(leerDatos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar usuarios: {ex.Message}");
                dt = null;
            }
            return dt;
        }
    }
}