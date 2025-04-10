using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistema_inventario.vistaReporte.datasets.DataSetEmpleadosTableAdapters;
using sistema_inventario.vistaReporte.datasets;
namespace sistema_inventario.vistaReporte
{
    public partial class ReporteEmpleados : Form
    {
        public ReporteEmpleados()
        {
            InitializeComponent();
        }

        private void ReporteEmpleados_Load(object sender, EventArgs e)
        {
            DataSetEmpleados setEmpleados = new DataSetEmpleados();

            //Instanciar el  table adapter
            var Empleados = new empleadoTableAdapter();
            // Llenar la tabla desde la base de datos
            Empleados.Fill(setEmpleados.empleado);
            // Establecer el DataSource en el ReportViewer
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
            new Microsoft.Reporting.WinForms.ReportDataSource("ListaEmpleados", (System.Data.DataTable)setEmpleados.empleado)
            );
            this.reportViewer1.RefreshReport();
        }
    }
}
