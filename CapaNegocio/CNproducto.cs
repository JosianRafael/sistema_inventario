using CapaDatos;
using System.Data;
using System;

namespace CapaNegocio
{
    public class CNproducto
    {
        public static string CN_Insertar_Producto(string nombre, int id_proveedor, string ubicacion,DateTime fecha_vencimiento,string descripcion = null,
                                                 string codigoBarras = null,
                                                 decimal precio = 0, decimal costo = 0, int cantidad_inicial = 0, int stock_minimo = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre))
                    return "El nombre del producto es obligatorio";

                if (precio < 0)
                    return "El precio no puede ser negativo";

                if (costo < 0)
                    return "El costo no puede ser negativo";
                if (cantidad_inicial < 0)
                    return "La cantidad inicial no puede ser negativa";

                Producto producto = new Producto(
                    id: 0,
                    nombre: nombre,
                    descripcion: descripcion,
                    codigoBar: codigoBarras
                );

                int idGenerado = 0;
                string resultado = producto.InsertarProducto(out idGenerado, producto);
                int IDproveedorProducto = 0;
                int idinventariogenerado = 0;
                CNproveedorProducto.CN_Insertar_proveedor_producto(out IDproveedorProducto,id_proveedor, idGenerado, (float)costo, (float)precio);
                CNinventario.CN_Insertar_inventario(out idinventariogenerado,0, IDproveedorProducto, cantidad_inicial, ubicacion, stock_minimo,fecha_vencimiento);
                CNmovimientoInventario.CN_Insertar_movimiento_inventario(0, idinventariogenerado,"entrada", cantidad_inicial, DateTime.Now);

                return idGenerado > 0 ?
                    $"Producto insertado correctamente. ID: {idGenerado}" :
                    resultado;
            }
            catch (Exception ex)
            {
                return "Error al insertar producto: " + ex.Message;
            }
        }

        public static string CN_Actualizar_Producto(int idProducto, string nombre,int id_productoproveedor,int id_proveedor, string descripcion = null,
                                                   string codigoBarras = null,
                                                   decimal precio = 0, decimal costo = 0)
        {
            try
            {
                if (idProducto <= 0)
                    return "ID de producto inválido";

                if (string.IsNullOrEmpty(nombre))
                    return "El nombre del producto es obligatorio";


                if (precio < 0)
                    return "El precio no puede ser negativo";

                if (costo < 0)
                    return "El costo no puede ser negativo";

                Producto producto = new Producto(
                    id: idProducto,
                    nombre: nombre,
                    descripcion: descripcion,
                    codigoBar: codigoBarras
                );
                string mensaje = producto.ActualizarProducto(producto);

                CNproveedorProducto.CN_Actualizar_proveedor_producto(id_productoproveedor, id_proveedor,idProducto,(float)costo,(float)precio);

                return mensaje;
            }
            catch (Exception ex)
            {
                return "Error al actualizar producto: " + ex.Message;
            }
        }

        public static DataTable CN_Consultar_Producto(string parametroBusqueda = "")
        {
            try
            {
                //Producto producto = new Producto();
                //DataTable dt = producto.ConsultarProducto(parametroBusqueda);
                DataTable dt = CNproveedorProducto.CN_Consultar_proveedor_producto("");

                if (dt != null)
                {
                    //// Formatear columnas monetarias
                    //dt.Columns["precio"].ColumnName = "Precio";
                    //dt.Columns["costo"].ColumnName = "Costo";
                }

                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar productos: " + ex.Message);
                return null;
            }
        }

        private static string GenerarCodigoArticulo()
        {
            Random random = new Random();
            return "PRD-" + DateTime.Now.ToString("yyMMdd") + "-" + random.Next(1000, 9999);
        }

    }
}