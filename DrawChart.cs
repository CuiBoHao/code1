using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace CoorLib
{
    public class DrawChart
    {
        public static Series PointSeries(List<PointInfo> points)
        {
            Series series = new Series();
            series.ChartType = SeriesChartType.Point;

            DataPoint p;
            for (int i = 0; i < points.Count; i++)
            {
                p = new DataPoint();
                p.Label = points[i].Name;
                p.SetValueXY(points[i].y, points[i].x);
                series.Points.Add(p);
            }
            //for (int i = 0; i < points.Count; i++)
            //{
            //    series.Points.AddXY(points[i].y, points[i].x);
            //}
            // Series1Para(ref series);
            return series;
        }

        public static void DrawPoints(string imgFile, List<PointInfo> points, int width, int height)
        {
            var chart1 = new Chart();
            chart1.Size = new System.Drawing.Size(width, height);
            ChartArea chartArea1 = new ChartArea();
            chartArea1.Name = "ChartArea1";
            ChartAreaPara(ref chartArea1, points);//para什么意思？
            chart1.ChartAreas.Add(chartArea1);//定义图表?

            //绘制点
            Series series1 = PointSeries(points);//这个是什么用法？
            series1.ChartArea = "ChartArea1";

            Series1Para(ref series1);
            chart1.Series.Add(series1);//将Series1加入chart1中

            chart1.DataBind();//数据连接
            chart1.SaveImage(imgFile, ChartImageFormat.Png);//指定图标的图像类型，保存png
        }

        public static void Series1Para(ref Series series)//重点之一：ref和out的用法和区别
        {
            series.XValueType = ChartValueType.Int32;//获取数据中一系列X的值
            series.YValueType = ChartValueType.Int32;
            series.MarkerSize = 8;//设置或获取标记的大小
            series.MarkerStyle = MarkerStyle.Circle;//。。。。标记的样式
            series.MarkerColor = Color.Green;//。。。。标记的颜色
        }

        public static void ChartAreaPara(ref ChartArea chartArea, List<PointInfo> points)
        {
            double xMax, xMin, yMax, yMin;
            MaxMin(points, out xMax, out xMin, out yMax, out yMin);

            int margin = 100;//：边缘

            chartArea.AxisX.Minimum = Math.Floor(yMin / 100) * 100-margin;
            chartArea.AxisX.Maximum = Math.Ceiling(yMax / 100) * 100+margin;
            chartArea.AxisY.Minimum = Math.Floor(xMin / 100) * 100-margin;
            chartArea.AxisY.Maximum = Math.Ceiling(xMax / 100) * 100+margin;

            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false; 
            chartArea.AxisX.MinorGrid.Enabled = false;
            chartArea.AxisY.MinorGrid.Enabled = false;
            chartArea.AxisX.Title = "y(m)";
            chartArea.AxisY.Title = "x(m)";
            chartArea.BorderWidth = 1;
            chartArea.BorderColor = Color.Gray;
        }

        public static void MaxMin(List<PointInfo> points, out double xMax, out double xMin,
                 out double yMax, out double yMin)
        {
            xMax = points[0].x;
            xMin = points[0].x;
            yMax = points[0].y;
            yMin = points[0].y;
            for (int i = 1; i < points.Count; i++)
            {
                if (xMax < points[i].x)
                    xMax = points[i].x;
                if (xMin > points[i].x)
                    xMin = points[i].x;
                if (yMax < points[i].y)
                    yMax = points[i].y;
                if (yMin > points[i].y)
                    yMin = points[i].y;
            }
        }
    }
}
