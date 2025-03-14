using CapaDatos;
using System;
using System.Data;

namespace CapaNegocio
{
    public class CNCliente
    {
        // Método para insertar un nuevo cliente
        public static string CN_Insertar_Cliente(string nombre, string apellido, string telefono, string direccion, string correo)
        {
            try
            {
                // Validación de campos obligatorios
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido))
                    throw new ArgumentException("Los campos nombre y apellido son obligatorios.");

                // Crear un objeto Cliente con los datos proporcionados
                Cliente cliente = new Cliente(0, nombre, apellido, telefono, direccion, correo);

                // Llamar al método InsertarCliente de la capa de datos para insertar el cliente
                return cliente.InsertarCliente(cliente);
            }
            catch (Exception ex)
            {
                // Capturar y devolver cualquier error que ocurra
                return "Error al insertar cliente: " + ex.Message;
            }
        }

        // Método para actualizar los datos de un cliente existente
        public static string CN_Actualizar_Cliente(int id_cliente, string nombre, string apellido, string telefono, string direccion, string correo)
        {
            try
            {
                // Validar que el ID del cliente sea válido
                if (id_cliente <= 0)
                    throw new ArgumentException("ID de cliente inválido.");

                // Crear un objeto Cliente con los datos proporcionados
                Cliente cliente = new Cliente(id_cliente, nombre, apellido, telefono, direccion, correo);

                // Llamar al método ActualizarCliente de la capa de datos para actualizar el cliente
                return cliente.ActualizarCliente(cliente);
            }
            catch (Exception ex)
            {
                // Capturar y devolver cualquier error que ocurra
                return "Error al actualizar cliente: " + ex.Message;
            }
        }

        // Método para consultar los datos de un cliente según un parámetro de búsqueda
        public static DataTable CN_Consultar_Cliente(string parametrobusqueda)
        {
            try
            {
                // Crear un objeto Cliente y llamar al método ConsultarCliente de la capa de datos
                Cliente cliente = new Cliente();
                return cliente.ConsultarCliente(parametrobusqueda);
            }
            catch (Exception ex)
            {
                // Capturar y devolver cualquier error que ocurra
                throw new Exception("Error al consultar cliente: " + ex.Message);
            }
        }
    } // Fin de la clase CNCliente
} // Fin del namespace