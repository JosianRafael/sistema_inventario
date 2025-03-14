using CapaDatos;
using System;
using System.Data;

namespace CapaNegocio
{
    public class CNEmpleado
    {
        // Método para insertar un nuevo empleado
        public static string CN_Insertar_Empleado(string cedula, string nombre, string apellido, string telefono, string direccion, string correo_electronico_empleado, string status, string acceso)
        {
            try
            {
                // Validación de campos obligatorios
                if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido))
                    throw new ArgumentException("Los campos cédula, nombre y apellido son obligatorios.");

                // Crear un objeto Empleado con los datos proporcionados
                Empleado empleado = new Empleado(0, cedula, nombre, apellido, telefono, direccion, correo_electronico_empleado, status, acceso);

                // Llamar al método InsertarEmpleado de la capa de datos para insertar el empleado
                return empleado.InsertarEmpleado(empleado);
            }
            catch (Exception ex)
            {
                // Capturar y devolver cualquier error que ocurra
                return "Error al insertar empleado: " + ex.Message;
            }
        }

        // Método para actualizar los datos de un empleado existente
        public static string CN_Actualizar_Empleado(int id_empleado, string cedula, string nombre, string apellido, string telefono, string direccion, string correo_electronico_empleado, string status, string acceso)
        {
            try
            {
                // Validar que el ID del empleado sea válido
                if (id_empleado <= 0)
                    throw new ArgumentException("ID de empleado inválido.");

                // Crear un objeto Empleado con los datos proporcionados
                Empleado empleado = new Empleado(id_empleado, cedula, nombre, apellido, telefono, direccion, correo_electronico_empleado, status, acceso);

                // Llamar al método ActualizarEmpleado de la capa de datos para actualizar el empleado
                return empleado.ActualizarEmpleado(empleado);
            }
            catch (Exception ex)
            {
                // Capturar y devolver cualquier error que ocurra
                return "Error al actualizar empleado: " + ex.Message;
            }
        }

        // Método para consultar los datos de un empleado según un parámetro de búsqueda
        public static DataTable CN_Consultar_Empleado(string parametrobusqueda)
        {
            try
            {
                // Crear un objeto Empleado y llamar al método ConsultarEmpleado de la capa de datos
                Empleado empleado = new Empleado();
                return empleado.ConsultarEmpleado(parametrobusqueda);
            }
            catch (Exception ex)
            {
                // Capturar y devolver cualquier error que ocurra
                throw new Exception("Error al consultar empleado: " + ex.Message);
            }
        }
    } // Fin de la clase CNEmpleado
} // Fin del namespace