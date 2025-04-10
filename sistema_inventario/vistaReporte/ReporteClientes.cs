using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistema_inventario.vistaReporte.datasets.DataSetClientesTableAdapters;
using sistema_inventario.vistaReporte.datasets;

namespace sistema_inventario.vistaReporte
{
    public partial class ReporteClientes : Form
    {
        public ReporteClientes()
        {
            InitializeComponent();
        }

        private void ReporteClientes_Load(object sender, EventArgs e)
        {
            DataSetClientes setClientes = new DataSetClientes();

            //Instanciar el  table adapter
            var Clientes = new clienteTableAdapter();
            var CantidadGastadaPorCliente = new CantidadGastadaPorClienteTableAdapter();
            var ComprasHechasPorCliente = new ComprasHechasClienteTableAdapter();
            var ClienteMetodoPago = new ClienteMetodoPagoTableAdapter();
            // Llenar la tabla desde la base de datos
            Clientes.Fill(setClientes.cliente);
            CantidadGastadaPorCliente.Fill(setClientes.CantidadGastadaPorCliente);
            ComprasHechasPorCliente.Fill(setClientes.ComprasHechasCliente);
            ClienteMetodoPago.Fill(setClientes.ClienteMetodoPago);
            // Establecer el DataSource en el ReportViewer
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
            new Microsoft.Reporting.WinForms.ReportDataSource("ListaClientes", (System.Data.DataTable)setClientes.cliente)
            );
            reportViewer1.LocalReport.DataSources.Add(
                new Microsoft.Reporting.WinForms.ReportDataSource("CantidadGastadaPorCliente", (System.Data.DataTable)setClientes.CantidadGastadaPorCliente)
            );
            reportViewer1.LocalReport.DataSources.Add(
               new Microsoft.Reporting.WinForms.ReportDataSource("ComprasHechasCliente", (System.Data.DataTable)setClientes.ComprasHechasCliente)
           );
            reportViewer1.LocalReport.DataSources.Add(
               new Microsoft.Reporting.WinForms.ReportDataSource("ClienteMetodoPago", (System.Data.DataTable)setClientes.ClienteMetodoPago)
           );

            this.reportViewer1.RefreshReport();
        }
    }
}
