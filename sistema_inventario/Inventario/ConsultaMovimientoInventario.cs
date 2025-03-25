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
            CargarFechas();
            ConfiguraBotones();
            dataGridView1.SelectionChanged += BuscarProductoPorFecha;

        }

        private void ConfiguraBotones()
        {
            button1.BackColor = Color.FromArgb(91, 63, 144);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Arial", 10, FontStyle.Bold);
            button1.Click += BuscarPorFecha;
            button2.BackColor = Color.FromArgb(91, 63, 144);
            button2.ForeColor = Color.White;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Arial", 10, FontStyle.Bold);
            button2.Click += RecargarInterfaz;
        }

        private async void CargarFechas()
        {
            var fechasdb = await CNmovimientoInventario.CN_Consultar_movimiento_inventarioAsync_Reporters(3);
            //Fecha inicio
            dateTimePicker1.MaxDate = Convert.ToDateTime(fechasdb.Rows[0][1]);
            dateTimePicker1.MinDate = Convert.ToDateTime(fechasdb.Rows[0][0]);
            //Valor predeterminado
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            //Fecha fin
            dateTimePicker2.MinDate = Convert.ToDateTime(fechasdb.Rows[0][0]);
            dateTimePicker2.MaxDate = Convert.ToDateTime(fechasdb.Rows[0][1]);
            //Valor predeterminado
            dateTimePicker2.Value = dateTimePicker2.MaxDate;

        }

        private void RecargarInterfaz(object sender, EventArgs e)
        {
            CargarGrafico();
            dataGridView1.ClearSelection();
        }

        private async void BuscarProductoPorFecha(object sender, EventArgs e)
        {
            int idProducto;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0]; //Primera fila seleccionada
                idProducto = (int)filaSeleccionada.Cells["id_producto"].Value;
            }
            else
            {
                return;
            }

            if (dateTimePicker1.Value == null || dateTimePicker2.Value == null)
            {
                MessageBox.Show("Favor seleccione un rango de fechas");
                return;
            }

            // Obtener los datos de manera asíncrona
            DataTable movientosProducto = await CNmovimientoInventario.CN_Consultar_movimiento_inventarioAsync_Reporters(9, dateTimePicker1.Value, dateTimePicker2.Value,idProducto);

            // Limpiar serie anterior
            chart1.Series.Clear();

            // Crear una nueva serie
            Series serie = new Series("Movimientos por Fecha")
            {
                ChartType = SeriesChartType.Column, // Tipo de gráfico: Columnas
                IsValueShownAsLabel = true, // Muestra valores en las barras
                LabelFormat = "#,##0", // Formato de números
                BorderWidth = 1,
                Color = Color.CornflowerBlue, // Color de barras
                BorderColor = Color.DarkBlue, // Color del borde
                ShadowOffset = 2, // Sombra
                IsVisibleInLegend = true // Mostrar en la leyenda
            };

            // Agregar los puntos al gráfico
            foreach (DataRow row in movientosProducto.Rows)
            {
                DateTime fecha = (DateTime)row["fecha"];  // Asegúrate de que la columna "fecha" existe
                int cantidad = Convert.ToInt32(row["cantidad"]); // Manejo seguro de conversión

                serie.Points.AddXY(fecha.ToShortDateString(), cantidad);
            }

            // Agregar la serie al gráfico
            chart1.Series.Add(serie);

            // Configuración del área del gráfico
            chart1.ChartAreas[0].AxisX.Title = "Fecha";
            chart1.ChartAreas[0].AxisY.Title = "Cantidad Movida";

            // Configurar la cuadrícula
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // Habilitar tooltips (detalles al pasar el mouse)
            chart1.Series[0].ToolTip = "Fecha: #VALX\nCantidad: #VALY";

            DataTable Movimientostotal = await CNmovimientoInventario.CN_Consultar_movimiento_inventarioAsync_Reporters(10, dateTimePicker1.Value, dateTimePicker2.Value,idProducto);

            // Limpiar serie anterior
            chart2.Series.Clear();
            serie = new Series("Movimientos por Tipo");
            serie.ChartType = SeriesChartType.Pie; // Gráfico de pastel

            // Agregar los puntos al gráfico
            foreach (DataRow row in Movimientostotal.Rows)
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

            DataTable dt = await CNmovimientoInventario.CN_Consultar_movimiento_inventarioAsync_Reporters(11, dateTimePicker1.Value, dateTimePicker2.Value,idProducto);

            // Limpiar el gráfico antes de agregar datos nuevos
            chart3.Series.Clear();
            chart3.ChartAreas.Clear();
            chart3.ChartAreas.Add(new ChartArea("MainArea"));

            // Crear un diccionario para almacenar las series (Entrada, Ajuste, Salida)
            Dictionary<string, Series> seriesDict = new Dictionary<string, Series>();

            // Recorrer los datos y agregarlos al gráfico
            foreach (DataRow row in dt.Rows)
            {
                DateTime fecha = Convert.ToDateTime(row["fecha"]);
                string tipo = row["tipo"].ToString();
                int cantidad = Convert.ToInt32(row["cantidad"]);

                // Si la serie para este tipo de movimiento no existe, la creamos
                if (!seriesDict.ContainsKey(tipo))
                {
                    Series nuevaSerie = new Series(tipo)
                    {
                        ChartType = SeriesChartType.Line, // Gráfico de líneas
                        BorderWidth = 3,
                        IsValueShownAsLabel = true
                    };
                    seriesDict[tipo] = nuevaSerie;
                    chart3.Series.Add(nuevaSerie);
                }

                // Agregar el punto al gráfico en la serie correspondiente
                seriesDict[tipo].Points.AddXY(fecha.ToShortDateString(), cantidad);
            }

            // Configurar el área del gráfico
            chart3.ChartAreas["MainArea"].AxisX.Title = "Fecha";
            chart3.ChartAreas["MainArea"].AxisY.Title = "Cantidad Movida";
            chart3.ChartAreas["MainArea"].AxisX.Interval = 1; // Un día por punto
            chart3.ChartAreas["MainArea"].AxisX.LabelStyle.Angle = -45; // Rotar etiquetas para mejor visibilidad

        }

        private async void BuscarPorFecha(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value == null || dateTimePicker2.Value == null)
            {
                MessageBox.Show("Favor seleccione un rango de fechas");
                return;
            }

            // Obtener los datos de manera asíncrona
            DataTable movientosProducto = await CNmovimientoInventario.CN_Consultar_movimiento_inventarioAsync_Reporters(5, dateTimePicker1.Value, dateTimePicker2.Value);

            // Limpiar serie anterior
            chart1.Series.Clear();

            // Crear una nueva serie
            Series serie = new Series("Movimientos por Fecha")
            {
                ChartType = SeriesChartType.Column, // Tipo de gráfico: Columnas
                IsValueShownAsLabel = true, // Muestra valores en las barras
                LabelFormat = "#,##0", // Formato de números
                BorderWidth = 1,
                Color = Color.CornflowerBlue, // Color de barras
                BorderColor = Color.DarkBlue, // Color del borde
                ShadowOffset = 2, // Sombra
                IsVisibleInLegend = true // Mostrar en la leyenda
            };

            // Agregar los puntos al gráfico
            foreach (DataRow row in movientosProducto.Rows)
            {
                DateTime fecha = (DateTime)row["fecha"];  // Asegúrate de que la columna "fecha" existe
                int cantidad = Convert.ToInt32(row["cantidad"]); // Manejo seguro de conversión

                serie.Points.AddXY(fecha.ToShortDateString(), cantidad);
            }

            // Agregar la serie al gráfico
            chart1.Series.Add(serie);

            // Configuración del área del gráfico
            chart1.ChartAreas[0].AxisX.Title = "Fecha";
            chart1.ChartAreas[0].AxisY.Title = "Cantidad Movida";

            // Configurar la cuadrícula
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // Habilitar tooltips (detalles al pasar el mouse)
            chart1.Series[0].ToolTip = "Fecha: #VALX\nCantidad: #VALY";

            DataTable Movimientostotal = await CNmovimientoInventario.CN_Consultar_movimiento_inventarioAsync_Reporters(6,dateTimePicker1.Value,dateTimePicker2.Value);

            // Limpiar serie anterior
            chart2.Series.Clear();
            serie = new Series("Movimientos por Tipo");
            serie.ChartType = SeriesChartType.Pie; // Gráfico de pastel

            // Agregar los puntos al gráfico
            foreach (DataRow row in Movimientostotal.Rows)
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

            DataTable TablaMovimientos = await CNmovimientoInventario.CN_Consultar_movimiento_inventarioAsync_Reporters(4, dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView1.DataSource = TablaMovimientos;
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


            dt = await CNmovimientoInventario.CN_Consultar_movimiento_inventarioAsync_Reporters(12, dateTimePicker1.Value, dateTimePicker2.Value);

            // Limpiar el gráfico antes de agregar datos nuevos
            chart3.Series.Clear();
            chart3.ChartAreas.Clear();
            chart3.ChartAreas.Add(new ChartArea("MainArea"));

            // Crear un diccionario para almacenar las series (Entrada, Ajuste, Salida)
            Dictionary<string, Series> seriesDict = new Dictionary<string, Series>();

            // Recorrer los datos y agregarlos al gráfico
            foreach (DataRow row in dt.Rows)
            {
                DateTime fecha = Convert.ToDateTime(row["fecha"]);
                string tipo = row["tipo"].ToString();
                int cantidad = Convert.ToInt32(row["cantidad"]);

                // Si la serie para este tipo de movimiento no existe, la creamos
                if (!seriesDict.ContainsKey(tipo))
                {
                    Series nuevaSerie = new Series(tipo)
                    {
                        ChartType = SeriesChartType.Line, // Gráfico de líneas
                        BorderWidth = 3,
                        IsValueShownAsLabel = true
                    };
                    seriesDict[tipo] = nuevaSerie;
                    chart3.Series.Add(nuevaSerie);
                }

                // Agregar el punto al gráfico en la serie correspondiente
                seriesDict[tipo].Points.AddXY(fecha.ToShortDateString(), cantidad);
            }

            // Configurar el área del gráfico
            chart3.ChartAreas["MainArea"].AxisX.Title = "Fecha";
            chart3.ChartAreas["MainArea"].AxisY.Title = "Cantidad Movida";
            chart3.ChartAreas["MainArea"].AxisX.Interval = 1; // Un día por punto
            chart3.ChartAreas["MainArea"].AxisX.LabelStyle.Angle = -45; // Rotar etiquetas para mejor visibilidad

        }

    }
}
