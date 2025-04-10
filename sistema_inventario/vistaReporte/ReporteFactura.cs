using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistema_inventario.vistaReporte.datasets;
using sistema_inventario.vistaReporte.datasets.DataSetFacturasTableAdapters;
namespace sistema_inventario.vistaReporte
{
    public partial class ReporteFactura : Form
    {
        public ReporteFactura()
        {
            InitializeComponent();
        }

        private void ReporteFactura_Load(object sender, EventArgs e)
        {
            DataSetFacturas setFacturas = new DataSetFacturas();

            //Instanciar el  table adapter
            var EvolucionVentasMez = new EvolucionVentasPorMesTableAdapter();
            var TotalObtenidoMetodoPago = new TotalObtenidoMetodoPagoTableAdapter();
            var TotalMontoPorEstadoFactura = new TotalMontoPorEstadoFacturaTableAdapter();
            var TotalVentasPorDia = new TotalVentasPorDiasTableAdapter();
            var CleinteMetodoPago = new ClienteMetodoPagoTableAdapter();
            // Llenar la tabla desde la base de datos
            EvolucionVentasMez.Fill(setFacturas.EvolucionVentasPorMes);
            TotalObtenidoMetodoPago.Fill(setFacturas.TotalObtenidoMetodoPago);
            TotalMontoPorEstadoFactura.Fill(setFacturas.TotalMontoPorEstadoFactura);
            TotalVentasPorDia.Fill(setFacturas.TotalVentasPorDias);
            CleinteMetodoPago.Fill(setFacturas.ClienteMetodoPago);
            // Establecer el DataSource en el ReportViewer
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
            new Microsoft.Reporting.WinForms.ReportDataSource("EvolucionVentasPorMes", (System.Data.DataTable)setFacturas.EvolucionVentasPorMes)
            );
            reportViewer1.LocalReport.DataSources.Add(
                new Microsoft.Reporting.WinForms.ReportDataSource("TotalObtenidoMetodoPago", (System.Data.DataTable)setFacturas.TotalObtenidoMetodoPago)
            );
            reportViewer1.LocalReport.DataSources.Add(
               new Microsoft.Reporting.WinForms.ReportDataSource("TotalMontoPorEstadoFactura", (System.Data.DataTable)setFacturas.TotalMontoPorEstadoFactura)
           );
            reportViewer1.LocalReport.DataSources.Add(
               new Microsoft.Reporting.WinForms.ReportDataSource("TotalVentasPorDia", (System.Data.DataTable)setFacturas.TotalVentasPorDias)
           );
            reportViewer1.LocalReport.DataSources.Add(
              new Microsoft.Reporting.WinForms.ReportDataSource("TotalPorClienteMetodoPago", (System.Data.DataTable)setFacturas.ClienteMetodoPago)
          );

            this.reportViewer1.RefreshReport();
        }
    }
}
