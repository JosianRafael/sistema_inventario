using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema_inventario.Vistas.Inventario
{
    public partial class ConsultaMovimientoInventario : Form
    {
        public ConsultaMovimientoInventario()
        {
            InitializeComponent();
            chart1.DataSource = InventarioListaProductos.ListaMovimientosInventario;
            chart1.Series.Add("Movimientos");
            chart1.Series["Movimientos"].XValueMember = "fecha_de_operacion";
            chart1.Series["Movimientos"].YValueMembers = "cantidad_movida";
            chart1.Series["Movimientos"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            dataGridView1.DataSource = InventarioListaProductos.ListaMovimientosInventario;
        }
    }
}
