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
        public static DataTable ListaProductosInventario;
        public static DataTable ListaMovimientosInventario;
        public static async Task CargarListaProductos()
        {
            ListaProductos = await CNproveedorProducto.CN_Consultar_proveedor_producto_async("");
        }

        public static async Task CargarListaProductosInventario()
        {
            ListaProductosInventario = await CNinventario.CN_Consultar_inventarioAsync("");
        }

        public static async Task CargarListaMovimientosInventaario()
        {
            ListaMovimientosInventario = await CNmovimientoInventario.CN_Consultar_movimiento_inventarioAsync("");
        }

    }
}
