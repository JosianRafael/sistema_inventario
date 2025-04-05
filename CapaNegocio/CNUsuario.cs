using CapaDatos;
using System;
using System.Data;

namespace CapaNegocio
{
    public class CNUsuario
    {
        public static DataTable CN_ValidarUsuario(string codigoAcceso)
        {
            try
            {
                if (string.IsNullOrEmpty(codigoAcceso))
                    throw new ArgumentException("El código de acceso no puede estar vacío.");

                Usuario usuario = new Usuario();
                return usuario.ValidarUsuarioPorCodigo(codigoAcceso);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al validar usuario: " + ex.Message);
            }
        }

        public static string CN_Insertar_Usuario(string codigoAcceso, string nombre, string apellido,
                                               string email, string contrasena, string rol, bool activo)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrEmpty(codigoAcceso))
                    throw new ArgumentException("El código de acceso es requerido.");

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido))
                    throw new ArgumentException("Nombre y apellido son requeridos.");

                if (string.IsNullOrEmpty(rol) || (rol != "Administrador" && rol != "Empleado"))
                    throw new ArgumentException("Rol inválido. Debe ser 'Administrador' o 'Empleado'.");

                Usuario usuario = new Usuario(0, codigoAcceso, nombre, apellido, email,
                                             contrasena, rol, activo, DateTime.Now);

                return usuario.InsertarUsuario(usuario);
            }
            catch (Exception ex)
            {
                return "Error al insertar usuario: " + ex.Message;
            }
        }

        public static DataTable CN_Consultar_Usuarios(string parametroBusqueda)
        {
            try
            {
                Usuario usuario = new Usuario();
                return usuario.ConsultarUsuarios(parametroBusqueda);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar usuarios: " + ex.Message);
            }
        }
    }
}