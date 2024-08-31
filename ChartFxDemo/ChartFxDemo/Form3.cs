using Infragistics.UltraChart.Core.Layers;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Shared;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.DataVisualization;
using ChartFX.WinForms;

namespace ChartFxDemo
{
    public partial class Form3 : Form
    {
        Random rnd = new Random();
        public Form3()
        {
            InitializeComponent();

            DataTable dt = MakeDataTable();
            //DataTable dt = Infragistics.UltraChart.Data.DemoTable.AllPositive();

            ultraChart1.DataSource = dt;
            ultraChart1.DataBind();

            DataTable dt2 = MakeDataTable2();
            MakeUltraDataChart(dt2);

        }

        private void MakeUltraDataChart(DataTable dt)
        {
            //NumericYAxis yAxis1 = new NumericYAxis();
            //NumericYAxis yAxis2 = new NumericYAxis();
            //CategoryYAxis yAxis3 = new CategoryYAxis();
            //NumericXAxis xAxis1 = new NumericXAxis();
            //CategoryXAxis xAxis2 = new CategoryXAxis();

            //UltraLegend ultraLegend1 = new UltraLegend();
            ////ultraLegend1.Target = ultraDataChart1;
            //ultraLegend1.Visible = true;

            CategoryXAxis xAxis = new CategoryXAxis()
            {
                Label = "QUATER",
                DataSource = dt.DefaultView.ToTable(true, "QUATER")
            };
            NumericYAxis yAxis = new NumericYAxis();

            ColumnSeries series1 = new ColumnSeries();
            series1.DataSource = dt.AsEnumerable().Where(r => r.Field<string>("MTRL").Equals("HBM")).CopyToDataTable();
            series1.ValueMemberPath = "QTY";
            series1.XAxis = xAxis;
            series1.YAxis = yAxis;
            series1.Title = "HBM";
            series1.Legend = ultraLegend1;
            series1.Legend.Visible = true;
            series1.LegendItemVisibility = Infragistics.Portable.Components.UI.Visibility.Visible;
            ultraDataChart1.Series.Add(series1);

            //ColumnSeries series2 = new ColumnSeries();
            //series2.DataSource = dt.AsEnumerable().Where(r => r.Field<string>("MTRL").Equals("A10")).CopyToDataTable();
            //series2.ValueMemberPath = "QTY";
            //series2.XAxis = xAxis;
            //series2.YAxis = yAxis;
            //series2.Title = "2st";
            //series2.Legend = ultraLegend1;
            //series2.Legend.Visible = true;
            //series2.LegendItemVisibility = Infragistics.Portable.Components.UI.Visibility.Visible;
            //ultraDataChart1.Series.Add(series2);

            //ColumnSeries series3 = new ColumnSeries();
            //series3.DataSource = dt.AsEnumerable().Where(r => r.Field<string>("MTRL").Equals("SKSC")).CopyToDataTable();
            //series3.ValueMemberPath = "QTY";
            //series3.XAxis = xAxis;
            //series3.YAxis = yAxis;
            //series3.Title = "3st";
            //series3.Legend = ultraLegend1;
            //series3.Legend.Visible = true;
            //series3.LegendItemVisibility = Infragistics.Portable.Components.UI.Visibility.Visible;
            //ultraDataChart1.Series.Add(series3);

            LineSeries series2 = new LineSeries();
            series2.DataSource = dt.AsEnumerable().Where(r => r.Field<string>("MTRL").Equals("A10")).CopyToDataTable();
            series2.ValueMemberPath = "QTY";
            series2.XAxis = xAxis;
            series2.YAxis = yAxis;
            series2.Title = "A10";
            series2.Legend = ultraLegend1;
            series2.Legend.Visible = true;
            series2.LegendItemVisibility = Infragistics.Portable.Components.UI.Visibility.Visible;
            ultraDataChart1.Series.Add(series2);

            LineSeries series3 = new LineSeries();
            series3.DataSource = dt.AsEnumerable().Where(r => r.Field<string>("MTRL").Equals("SKSC")).CopyToDataTable();
            series3.ValueMemberPath = "QTY";
            series3.XAxis = xAxis;
            series3.YAxis = yAxis;
            series3.Title = "SKSC";
            series3.Legend = ultraLegend1;
            series3.Legend.Visible = true;
            series3.LegendItemVisibility = Infragistics.Portable.Components.UI.Visibility.Visible;
            ultraDataChart1.Series.Add(series3);

            ultraDataChart1.Legend = ultraLegend1;
            ultraDataChart1.Dock = DockStyle.Fill;
            ultraDataChart1.Legend.BorderStyle = BorderStyle.FixedSingle;
            //ultraDataChart1.Legend.Location = new System.Drawing.Point(ultraDataChart1.Right/2 - ultraLegend1.Width/2, ultraDataChart1.Height - 50);
            //ultraDataChart1.Legend.Size = new System.Drawing.Size(500, 30);
            //ultraDataChart1.Legend.BackColor = ultraDataChart1.BackColor;
            //ultraDataChart1.Legend.Dock = DockStyle.Bottom;
            ultraDataChart1.Legend.Visible = true;

            ultraDataChart1.Axes.Add(yAxis);
            ultraDataChart1.Axes.Add(xAxis);

        }

        private DataTable MakeDataTable()
        {
            List<string> lsMtrl = new List<string>() //Series
            {
                "HBM","A10","SKSC","TOTAL","PERCENT"
            };

            DataTable dt = new DataTable();
            //자재별 분기별 실적
            dt.Columns.Add("MTRL", typeof(string));  //Series
            dt.Columns.Add("1st", typeof(int));      //X축
            dt.Columns.Add("2st", typeof(int));      //X축
            dt.Columns.Add("3st", typeof(int));      //X축
            dt.Columns.Add("4st", typeof(int));      //X축
            dt.Columns.Add("Tot", typeof(int));      //X축

            for (int i = 0; i < lsMtrl.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["MTRL"] = lsMtrl[i];

                if (i < 3)
                {
                    int tot = 0;

                    for (int j = 1; j < dt.Columns.Count; j++)
                    {
                        if (j == dt.Columns.Count - 1)
                        {
                            dr[j] = tot;
                        }
                        else
                        {
                            int iRndS = rnd.Next(1, 18000);
                            int iRndE = rnd.Next(1, 18000);

                            if (iRndS < iRndE)
                            {
                                dr[j] = rnd.Next(iRndS, iRndE);
                                tot += (int)dr[j];
                            }
                            else
                            {
                                dr[j] = rnd.Next(iRndE, iRndS);
                                tot += (int)dr[j];
                            }
                        }
                    }

                    dt.Rows.Add(dr);
                }
                else if(i == 3)
                {
                    #region 분기별 합계
                    for (int j = 1; j < dt.Columns.Count; j++)
                    {
                        dr[j] = dt.AsEnumerable().Sum(r => (int)r[j]);
                    }
                    #endregion

                    dt.Rows.Add(dr);
                }
                else
                {
                    #region 분기별 PERCENT
                    for (int j = 1; j < dt.Columns.Count; j++)
                    {
                        int sum = (int)dt.Rows[3]["Tot"];
                        int tot = (int)dt.Rows[3][j];
                        dr[j] = (int)Math.Round((tot * 1d / sum) * 100);
                    }
                    #endregion

                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
        private DataTable MakeDataTable2()
        {
            List<string> lsMtrl = new List<string>() //Series
            {
                "HBM","A10","SKSC"
            };
            List<string> lstQ = new List<string>() //Series
            {
                "1st","2st","3st","4st"
            };

            DataTable dt = new DataTable();
            //자재별 분기별 실적
            dt.Columns.Add("MTRL"  , typeof(string));  //Series
            dt.Columns.Add("QUATER", typeof(string));      //X축
            dt.Columns.Add("QTY"   , typeof(int));      //X축

            for (int i = 0; i < lsMtrl.Count; i++)
            {
                for (int j = 0; j < lstQ.Count; j++)
                {
                    DataRow dr = dt.NewRow();
                    dr["MTRL"] = lsMtrl[i];
                    dr["QUATER"] = lstQ[j];

                    int iRndS = rnd.Next(1, 18000);
                    int iRndE = rnd.Next(1, 18000);

                    if(iRndS < iRndE)
                    {
                        dr["QTY"] = rnd.Next(iRndS, iRndE);
                    }
                    else
                    {
                        dr["QTY"] = rnd.Next(iRndE, iRndS);
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //this.ultraChart1.Legend.Visible = false;
            //this.ultraChart1.Legend.Location = LegendLocation.Bottom;
            //this.ultraChart1.Legend.Margins.Left = 5;
            //this.ultraChart1.Legend.Margins.Right = 10;
            //this.ultraChart1.Legend.Margins.Top = 15;
            //this.ultraChart1.Legend.Margins.Bottom = 15;
            //this.ultraChart1.Legend.SpanPercentage = 15;


            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    XYSeries series = new XYSeries();
            //    series.Data.DataSource = dt;
            //    series.leg

            //    ultraChart1.Series.Add(series);
            //}



            //ultraChart1.Legend.a
            //ultraChart1.Legend.Visible = true;
            //ultraChart1.Legend.SpanPercentage = 10;

            //ChartLayerAppearance columnLayer = new ChartLayerAppearance();
            //ChartLayerAppearance lineLayer = new ChartLayerAppearance();
            //CompositeLegend chartLegend = new CompositeLegend();

            //columnLayer.LegendItem = LegendItemType.Series;
            //lineLayer.LegendItem = LegendItemType.Series;
            //chartLegend.ChartLayers.Add(columnLayer);
            //chartLegend.ChartLayers.Add(lineLayer);
            //chartLegend.BoundsMeasureType = MeasureType.Percentage;
            //chartLegend.Bounds = new Rectangle(34, 96, 20, 4);

            //chartLegend.LabelStyle.FontSizeBestFit = true;
            //ultraChart1.CompositeChart.Legends.Add(chartLegend);
            //this.ultraChrt1.Legend.Location = LegendLocation.Bottom;




        }



    }
}
