using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaNegocio;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace sistema_inventario.Inventario
{
    public partial class ConsultaMovimientoInventario : Form
    {
        public ConsultaMovimientoInventario()
        {
            InitializeComponent();
            CargarGrafico();
            dataGridView1.DataSource = InventarioListaProductos.ListaMovimientosInventario;
        }

        private async void CargarGrafico()
        {
            // Consulta a la base de datos
            DataTable dt = await CNmovimientoInventario.CN_Consultar_movimiento_inventarioAsync_Reporters(0);

            // Limpiar serie anterior
            chart1.Series.Clear();
            Series serie = new Series("Movimientos por Fecha");
            serie.ChartType = SeriesChartType.Column; // Gráfico de columnas
            serie.IsValueShownAsLabel = true; // Muestra los valores en las barras
            serie.LabelFormat = "#,##0"; // Formato de los números

            // Estilo de las barras (similar a Excel)
            serie.BorderWidth = 1;
            serie.Color = Color.CornflowerBlue; // Color de las barras
            serie.BorderColor = Color.DarkBlue; // Color del borde de las barras
            serie.ShadowOffset = 2; // Sombra de las barras
            serie.IsVisibleInLegend = true; // No mostrar la serie en la leyenda

            // Agregar los puntos al gráfico
            foreach (DataRow row in dt.Rows)
            {
                DateTime fecha = (DateTime)row["fecha"];
                int cantidad = (int)row["cantidad"];
                serie.Points.AddXY(fecha.ToShortDateString(), cantidad);
            }

            // Agregar la serie al gráfico
            chart1.Series.Add(serie);

            // Mejorar el aspecto del área del gráfico
            chart1.ChartAreas[0].AxisX.Title = "Fecha";
            chart1.ChartAreas[0].AxisY.Title = "Cantidad Movida";

            // Mejorar la cuadrícula
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray; // Color de la cuadrícula en el eje X
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray; // Color de la cuadrícula en el eje Y
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash; // Estilo de línea de la cuadrícula
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash; // Estilo de línea de la cuadrícula

            // Habilitar tooltips (detalles cuando el mouse pasa sobre las barras)
            chart1.Series[0].ToolTip = "Fecha: #VALX\nCantidad: #VALY";


            // Consulta a la base de datos para el gráfico de pastel
            dt = await CNmovimientoInventario.CN_Consultar_movimiento_inventarioAsync_Reporters(1);

            // Limpiar serie anterior
            chart2.Series.Clear();
            serie = new Series("Movimientos por Tipo");
            serie.ChartType = SeriesChartType.Pie; // Gráfico de pastel

            // Agregar los puntos al gráfico
            foreach (DataRow row in dt.Rows)
            {
                string tipo = row["tipo"].ToString();
                int cantidad = (int)row["cantidad"];
                serie.Points.AddXY(tipo, cantidad);
            }

            // Agregar la serie al gráfico
            chart2.Series.Add(serie);

            // Agregar un ChartArea si no existe
            if (chart2.ChartAreas.Count == 0)
            {
                chart2.ChartAreas.Add(new ChartArea());
            }
            chart2.ChartAreas[0].Area3DStyle.Enable3D = true; // Habilita 3D en el gráfico de pastel

            chart2.Series[0].LabelFormat = "#PERCENT"; // Mostrar porcentaje en cada sección

            // Habilitar tooltips para el gráfico de pastel
            chart2.Series[0].ToolTip = "#VALX: #PERCENT\nCantidad: #VALY";
        }

    }
}
