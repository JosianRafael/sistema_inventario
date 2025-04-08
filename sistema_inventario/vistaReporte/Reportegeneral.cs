using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistema_inventario.vistaReporte.datasets.DatasetProductosTableAdapters;
using sistema_inventario.vistaReporte.datasets;

namespace sistema_inventario.vistaReporte
{
    public partial class Reportegeneral : Form
    {
        public Reportegeneral()
        {
            InitializeComponent();
        }

        private void Reportegeneral_Load(object sender, EventArgs e)
        {
            DatasetProductos DataSets = new DatasetProductos();

            //Instanciar el  table adapter
            var productosAdapter = new ReporteProductosTableAdapter();
            var productos_menos_que_stock_minimo = new ProductosMenorStockMinimoTableAdapter();
            var productos_sin_moimiento_reciente = new ProductosSinMovimientoRecienteTableAdapter();
            var productos_que_vencen_en_30_dias = new ProductosVencer30diasTableAdapter();
            var total_inventario_por_proveedor = new TotalinventarioPorProveedorTableAdapter();
            var valor_total_inventario_por_producto = new ValorTotalEnInventarioPorProductoTableAdapter();
            var margen_beneficios = new MargenGananciaTableAdapter();
            var movimientos_inventario = new FrecuenciaMovimientoInventarioTableAdapter();
            // Llenar la tabla desde la base de datos
            productosAdapter.Fill(DataSets.ReporteProductos);
            productos_menos_que_stock_minimo.Fill(DataSets.ProductosMenorStockMinimo);
            productos_sin_moimiento_reciente.Fill(DataSets.ProductosSinMovimientoReciente);
            productos_que_vencen_en_30_dias.Fill(DataSets.ProductosVencer30dias);
            total_inventario_por_proveedor.Fill(DataSets.TotalinventarioPorProveedor);
            valor_total_inventario_por_producto.Fill(DataSets.ValorTotalEnInventarioPorProducto);
            margen_beneficios.Fill(DataSets.MargenGanancia);
            movimientos_inventario.Fill(DataSets.FrecuenciaMovimientoInventario);
            // Establecer el DataSource en el ReportViewer
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
            new Microsoft.Reporting.WinForms.ReportDataSource("Productos", (System.Data.DataTable)DataSets.ReporteProductos)
            );
            reportViewer1.LocalReport.DataSources.Add(
                new Microsoft.Reporting.WinForms.ReportDataSource("ProductosConMenosStockMinimo",(System.Data.DataTable)DataSets.ProductosMenorStockMinimo)
            );
            reportViewer1.LocalReport.DataSources.Add(
               new Microsoft.Reporting.WinForms.ReportDataSource("ProductoSinMovimientoReciente", (System.Data.DataTable)DataSets.ProductosSinMovimientoReciente)
           );
            reportViewer1.LocalReport.DataSources.Add(
               new Microsoft.Reporting.WinForms.ReportDataSource("Productosvencen30dias", (System.Data.DataTable)DataSets.ProductosVencer30dias)
           );
            reportViewer1.LocalReport.DataSources.Add(
               new Microsoft.Reporting.WinForms.ReportDataSource("TotalinventarioPorProveedor", (System.Data.DataTable)DataSets.TotalinventarioPorProveedor)
           );
            reportViewer1.LocalReport.DataSources.Add(
               new Microsoft.Reporting.WinForms.ReportDataSource("ValorTotalInventarioPorProducto", (System.Data.DataTable)DataSets.ValorTotalEnInventarioPorProducto)
           );
            reportViewer1.LocalReport.DataSources.Add(
             new Microsoft.Reporting.WinForms.ReportDataSource("MargenGanancia", (System.Data.DataTable)DataSets.MargenGanancia)
            );
            reportViewer1.LocalReport.DataSources.Add(
            new Microsoft.Reporting.WinForms.ReportDataSource("FrecuenciaMovimientoInventario", (System.Data.DataTable)DataSets.FrecuenciaMovimientoInventario)
            );
            this.reportViewer1.RefreshReport();
        }
    }
}
