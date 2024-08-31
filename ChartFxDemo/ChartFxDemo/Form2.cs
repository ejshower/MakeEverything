using Infragistics.Win.UltraWinChart;
using SoftwareFX.WinForms.Data.Expressions;
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
    public partial class Form2 : Form
    {
        Random rnd = new Random();

        public Form2()
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

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dt = MakeDataTable();

            ultraChart1.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.StackColumnChart;
            ultraChart1.ColorModel.CustomPalette = new Color[] { Color.Red, Color.Green, Color.Yellow, Color.Blue };
            ultraChart1.Tooltips.FormatString = "<DATA_VALUE_ITEM>";
            ultraChart1.DataSource = dt;

            //ultraChart1.Data.SetRowLabels(new string[4] { "1", "2", "3","4" });

            ultraChart1.DataBind();
        }
    }
}
