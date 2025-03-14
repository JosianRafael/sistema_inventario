using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaNegocio;

namespace sistema_inventario.Vistas
{
    public class InventarioListaProductos
    {
        public static DataTable ListaProductos;
        public static async Task CargarListaProductos()
        {
            ListaProductos = await CNproveedorProducto.CN_Consultar_proveedor_producto_async("");
        }
    }
}
