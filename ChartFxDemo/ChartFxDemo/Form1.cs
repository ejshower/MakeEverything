using ChartFX.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChartFxDemo
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private DataTable MakeDataTable()
        {
            List<string> lsMtrl = new List<string>() //Series
            {
                "HBM","A10","SKSC"
            };

            DataTable dt = new DataTable();
            //자재별 분기별 실적
            dt.Columns.Add("MTRL", typeof(string));  //Series
            dt.Columns.Add("1st", typeof(int));      //X축
            dt.Columns.Add("2st", typeof(int));      //X축
            dt.Columns.Add("3st", typeof(int));      //X축
            dt.Columns.Add("4st", typeof(int));      //X축

            for (int i = 0; i < lsMtrl.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["MTRL"] = lsMtrl[i];

                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    dr[j] = rnd.Next(1, 18000);
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        private void SetChart(DataTable dt)
        {
            DoubleBuffered = true;

            chart1.Series.Clear();
            chart1.Data.Series = dt.Rows.Count;
            chart1.Data.Points = 4;

            chart1.Gallery = ChartFX.WinForms.Gallery.Bar;

            chart1.AllSeries.Stacked = ChartFX.WinForms.Stacked.Normal;
            chart1.AllSeries.PointLabels.Visible       = true;
            chart1.AllSeries.PointLabels.Alignment     = StringAlignment.Center;
            chart1.AllSeries.PointLabels.LineAlignment = StringAlignment.Center;

            chart1.AxisX.Title.Text = "Quarter";
            chart1.AxisY.Title.Text = "Qty";
            chart1.AxisY.Step = new ChartFX.WinForms.DataUnit(3000);
            chart1.AxisY.Max = 18000;
            chart1.AxisY.Min = 0;

            chart1.LegendBox.Dock = ChartFX.WinForms.DockArea.Top;
            chart1.LegendBox.ContentLayout = ChartFX.WinForms.ContentLayout.Center;
            chart1.LegendBox.Titles.Add(new ChartFX.WinForms.TitleDockable("Types of Chip"));


            for (int i = 0;i < dt.Rows.Count;i++) //Series 별로 차트데이터 입력
            {
                chart1.Series[i].Text = dt.Rows[i]["MTRL"].ToString();

                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    //[Series, X축]
                    chart1.AxisX.Labels[j - 1] = string.Format("{0}", dt.Columns[j].ColumnName);//X축 
                    chart1.Data[i, j-1] = Convert.ToInt32(dt.Rows[i][j]);
                }
                    
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = MakeDataTable();
            SetChart(dt);
        }
    }
}
