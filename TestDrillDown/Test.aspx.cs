using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using DevExpress.Utils;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.Drawing;

namespace TestDrillDown
{
    public partial class Test1 : ChartBasePage
    {        

        List<string> categories;
   
        protected void Page_Load(object sender, EventArgs e)
        {
            Class1 cet = new Class1();
            cet.Test();

            //WebChartControl1.DataSource = DevAV.GetTotalSales();
            WebChartControl1.DataSource = DevAV.GetTotalSales(cet.Test());
            WebChartControl1.DataBind();

            var keys = DevAV.CategorizedProducts.Keys;
            categories = new List<string>(keys.Count);
            foreach (string category in DevAV.CategorizedProducts.Keys)
                categories.Add(category);
            XYDiagram diagram = WebChartControl1.Diagram as XYDiagram;
            if (diagram != null)
                diagram.AxisX.Label.Font = new Font(diagram.AxisX.Label.Font, FontStyle.Underline);
        }
        protected void WebChartControl1_BoundDataChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = WebChartControl1.Diagram as XYDiagram;
            if (diagram != null && WebChartControl1.Series.Count > 0)
            {
                diagram.AxisX.DateTimeScaleOptions.ScaleMode = ScaleMode.Automatic;
                diagram.AxisX.DateTimeScaleOptions.AggregateFunction = AggregateFunction.Sum;
                diagram.EnableAxisXScrolling = diagram.EnableAxisXZooming = WebChartControl1.Series[0].ActualArgumentScaleType == ScaleType.DateTime;
            }
        }
        protected void WebChartControl1_DrillDownStateChanged(object sender, DrillDownStateChangedEventArgs e)
        {
            XYDiagram diagram = WebChartControl1.Diagram as XYDiagram;
            if (diagram != null)
            {
                if (e.States.Length == 0)
                {
                    diagram.Rotated = true;
                    diagram.AxisX.Label.Font = new Font(diagram.AxisX.Label.Font, FontStyle.Underline);
                    WebChartControl1.CrosshairOptions.ShowArgumentLine = false;
                    WebChartControl1.CrosshairOptions.CrosshairLabelMode = CrosshairLabelMode.ShowForNearestSeries;
                    diagram.EnableAxisXScrolling = false;
                    diagram.EnableAxisXZooming = false;
                }
                else
                {
                    diagram.Rotated = false;
                    diagram.AxisX.Label.Font = new Font(diagram.AxisX.Label.Font, FontStyle.Regular);
                    WebChartControl1.CrosshairOptions.ShowArgumentLine = true;
                    WebChartControl1.CrosshairOptions.CrosshairLabelMode = CrosshairLabelMode.ShowCommonForAllSeries;
                    diagram.EnableAxisXScrolling = true;
                    diagram.EnableAxisXZooming = true;
                }
            }

            foreach (DrillDownItem item in e.States)
            {
                if (item.Parameters.ContainsKey("ProductCategory"))
                {
                    WebChartControl1.PaletteBaseColorNumber = categories.IndexOf(item.Parameters["ProductCategory"].ToString()) + 1;
                    return;
                }
            }
            WebChartControl1.PaletteBaseColorNumber = 0;
        }
    }
}