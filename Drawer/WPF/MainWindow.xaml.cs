using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FunctionBuilder.Logic;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawCanvas();
        }

        public void Calcul1(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal _x;
                if (boxAloneX.Text != null && boxAloneX.Text != "")
                    _x = decimal.Parse(boxAloneX.Text);
                else
                    _x = 0;
                var f = new Function(boxExp.Text, new Variable('x', _x));
                tbAnswer.Text = $"Ответ: {f.Answer}  ОПЗ: {f.ReversePolishNotation}";
            }
            catch (Exception ex)
            {
                tbAnswer.Text = ex.Message;
            }
        }

        public void Calcul2(object sender, RoutedEventArgs e)
        {
            lbShowXY.Items.Clear();
            lbShowXY.Visibility = Visibility.Visible;
            list.Clear();
            canvas.Children.Clear();
            try
            {
                decimal before = decimal.Parse(boxBeforeX.Text);
                decimal after = decimal.Parse(boxAfterX.Text);
                decimal step = decimal.Parse(boxStep.Text);

                var f = new Function(boxExp.Text, new Variable('x', before));

                for (decimal i = before; i <= after; i += step)
                {
                    f.X = i;
                    lbShowXY.Items.Add(new TextBlock().Text=$"X = {i}; Y = {f.Answer}\n");
                    list.Add(new Point((double)i, (double)f.Answer));
                }
                DrawCanvas();
                SetAspect();
                DrawPointsCanvas();
            }
            catch (Exception ex)
            {
                lbShowXY.Items.Add(new TextBlock().Text = ex.Message);
                DrawCanvas();
            }
        }

        public void Size(object sender, SizeChangedEventArgs e)
        {
            DrawCanvas();
            DrawPointsCanvas();
        }

        public void DrawCanvas()
        {
            canvas.Children.Clear();

            var h = canvas.ActualHeight;
            var w = canvas.ActualWidth;

            var xLine = new Line();
            var yLine = new Line();

            xLine.Stroke = Brushes.Black;
            xLine.StrokeThickness = 4;
            yLine.Stroke = Brushes.Black;
            yLine.StrokeThickness = 4;

            xLine.Y1 = h / 2;
            xLine.Y2 = xLine.Y1;
            xLine.X1 = 0;
            xLine.X2 = w - 4;

            yLine.X1 = w / 2;
            yLine.X2 = yLine.X1;
            yLine.Y1 = 4;
            yLine.Y2 = h;

            canvas.Children.Add(xLine);
            canvas.Children.Add(yLine);

            var xTri = new Polygon();
            var yTri = new Polygon();

            xTri.Fill = Brushes.Black;
            yTri.Fill = Brushes.Black;

            xTri.Points.Add(new Point(w, h / 2));
            xTri.Points.Add(new Point(w - 8, h / 2 - 10));
            xTri.Points.Add(new Point(w - 8, h / 2 + 10));

            yTri.Points.Add(new Point(w / 2, 0));
            yTri.Points.Add(new Point(w / 2 - 8, 10));
            yTri.Points.Add(new Point(w / 2 + 8, 10));

            canvas.Children.Add(xTri);
            canvas.Children.Add(yTri);
        }

        List<Point> list = new List<Point>();
        double aspect = 0;
        public void SetAspect()
        {
            aspect = 0;
            foreach (var point in list)
            {
                if (Math.Abs(point.X) > aspect)
                    aspect = Math.Abs(point.X);
                if (Math.Abs(point.Y) > aspect)
                    aspect = Math.Abs(point.Y);
            }
        }

        public void DrawPointsCanvas() //требования: лист каунт != 0
        {
            var h = canvas.ActualHeight;
            var w = canvas.ActualWidth;
            var graphic = new Polyline();
            graphic.Stroke = Brushes.Black;
            graphic.StrokeThickness = 2;
            double x, y;
            foreach (var point in list)
            {
                var circle = new Ellipse();
                circle.Fill = Brushes.Yellow;
                circle.Width = 10;
                circle.Height = 10;
                x = (point.X / aspect) * (w / 2) + w / 2;
                y = h / 2 + (-1 * point.Y / aspect) * (h / 2);
                circle.SetValue(Canvas.LeftProperty, x - circle.Width / 2);
                circle.SetValue(Canvas.TopProperty, y - circle.Height / 2);
                canvas.Children.Add(circle);
                graphic.Points.Add(new Point(x, y));
            }
            canvas.Children.Add(graphic);
        }
    }
}
