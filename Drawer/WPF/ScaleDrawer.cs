using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF
{
    class ScaleDrawer
    {
        public ScaleDrawer(Canvas canvas, List<Point> points)
        {
            this.canvas = canvas;
            this.points = points;
        }
        private readonly Canvas canvas;
        private List<Point> points;
        private Point spectator = new Point(0, 0);
        private int scale = 25;
        private const double SCALE_VALUE = 4;

        public void Move(double x, double y)
        {
            spectator.X += x * (1 / (scale * SCALE_VALUE));
            spectator.Y += y * (1 / (scale * SCALE_VALUE));
            DrawGraphic();
        }
        public void ScaleChanged(int zoom)
        {
            if (scale + zoom < 400 && scale + zoom > 0)
            {
                scale += zoom;
                DrawGraphic();
            }
        }
        public void DrawGraphic()
        {
            canvas.Children.Clear();
            var actualPoints = GetActualPoints();
            DrawAxis(actualPoints.Count);
            DrawGraphicLine(actualPoints.ToArray());
        }

        public void ShowTip(object sender, MouseEventArgs e)
        {
            var lab = new Label
            {
                Background = Brushes.LightBlue,
                Content = $"X= {(float)((e.GetPosition(canvas).X - canvas.ActualWidth / 2) / (scale * SCALE_VALUE) - spectator.X)}\n" +
                          $"Y={(float)(-1 * (e.GetPosition(canvas).Y - canvas.ActualHeight / 2) / (scale * SCALE_VALUE) + spectator.Y)}",
                Height = Double.NaN,
                Width = Double.NaN,
                BorderThickness = new Thickness(10)
            };
            lab.MouseLeave += CloseTip;
            lab.SetValue(Canvas.LeftProperty, e.GetPosition(canvas).X);
            lab.SetValue(Canvas.TopProperty, e.GetPosition(canvas).Y);

            canvas.Children.Add(lab);
        }
        private void CloseTip(object sender, MouseEventArgs e)
        {
            canvas.Children.Remove((Label)sender);
        }


        private List<Point> GetActualPoints()
        {
            double height = canvas.ActualHeight;
            double width = canvas.ActualWidth;

            double halfHeight = height / 2;
            double halfWidth = width / 2;

            double scale = this.scale * SCALE_VALUE;

            Func<Point, bool> func = p =>
            {
                double vertical = (spectator.Y - p.Y) * scale + halfHeight;
                double horizontal = (spectator.X + p.X) * scale + halfWidth;
                return (vertical >= 0 && vertical <= height) && (horizontal >= 0 && horizontal <= width);
            };

            return points.Where(p => func(p)).ToList();
        }
        private void DrawAxis(int CountActualPoints)
        {
            double height = canvas.ActualHeight;
            double width = canvas.ActualWidth;
            double scale = this.scale * SCALE_VALUE;

            if (spectator.Y * scale + height / 2 >= 0 && spectator.Y * scale + height / 2 <= height)
            {
                var xLine = new Line
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 4,
                    X1 = 0,
                    X2 = width - 3, //для треугольника
                    Y1 = spectator.Y * scale + height / 2
                };
                xLine.Y2 = xLine.Y1;
                canvas.Children.Add(xLine);

                var xTri = new Polygon { Fill = Brushes.Black };
                xTri.Points.Add(new Point(width, xLine.Y1));
                xTri.Points.Add(new Point(width - 8, xLine.Y1 - 10));
                xTri.Points.Add(new Point(width - 8, xLine.Y1 + 10));
                canvas.Children.Add(xTri);

                if (CountActualPoints != 0)
                {
                    double py = xLine.Y1;

                    double pxRight = width / (2 * scale) - spectator.X;
                    double pxLeft = -width / (2 * scale) - spectator.X;

                    int pxR = (int)pxRight;
                    int pxL = (int)pxLeft;

                    int pxStep = (int)(pxR - pxL) / 20 + 1;

                    double px;
                    for (int i = pxL; i <= pxR; i += pxStep)
                    {
                        var circle = new Ellipse
                        {
                            Fill = Brushes.Black,
                            Width = 10,
                            Height = 10
                        };

                        px = (spectator.X + i) * scale + width / 2;

                        circle.SetValue(Canvas.LeftProperty, px - circle.Width / 2);
                        circle.SetValue(Canvas.TopProperty, py - circle.Height / 2);
                        canvas.Children.Add(circle);

                        var num = new Label { FontSize = 10, Content = i };
                        num.SetValue(Canvas.LeftProperty, px - 5);
                        num.SetValue(Canvas.TopProperty, py + 10);
                        canvas.Children.Add(num);
                    }
                }
            }
            if (spectator.X * scale + width / 2 >= 0 && spectator.X * scale + width / 2 <= width)
            {
                var yLine = new Line
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 4,
                    X1 = spectator.X * scale + width / 2,
                    Y1 = 3, //для треугольника
                    Y2 = height
                };
                yLine.X2 = yLine.X1;
                canvas.Children.Add(yLine);

                var yTri = new Polygon { Fill = Brushes.Black };
                yTri.Points.Add(new Point(yLine.X1, 0));
                yTri.Points.Add(new Point(yLine.X1 - 8, 10));
                yTri.Points.Add(new Point(yLine.X1 + 8, 10));
                canvas.Children.Add(yTri);
                if (CountActualPoints != 0)
                {
                    double px = yLine.X1;

                    double pyBottom = -height / (2 * scale) + spectator.Y;
                    double pyTop = height / (2 * scale) + spectator.Y;

                    int pyT = pyTop >= 0 ? (int)pyTop : (int)Math.Floor(pyTop);
                    int pyB = (int)pyBottom;

                    int pyStep = (int)(pyT - pyB) / 20 + 1;

                    double py;
                    for (int i = pyB; i <= pyT; i += pyStep)
                    {
                        var circle = new Ellipse
                        {
                            Fill = Brushes.Black,
                            Width = 10,
                            Height = 10
                        };

                        py = (spectator.Y - i) * scale + height / 2;

                        circle.SetValue(Canvas.LeftProperty, px - circle.Width / 2);
                        circle.SetValue(Canvas.TopProperty, py - circle.Height / 2);
                        canvas.Children.Add(circle);
                        if (i != 0)
                        {
                            var num = new Label { FontSize = 10, Content = i };
                            num.SetValue(Canvas.LeftProperty, px);
                            num.SetValue(Canvas.TopProperty, py + 5);
                            canvas.Children.Add(num);
                        }
                    }
                }
            }
        }
        private void DrawGraphicLine(Point[] actualPoints)
        {
            double halfHeight = canvas.ActualHeight / 2;
            double halfWidth = canvas.ActualWidth / 2;
            double scale = this.scale * SCALE_VALUE;


            var graphic = new Polyline
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            double x, y;

            float step = 0;     //шаг для проверки на разрыв графика
            if (points.Count >= 2)
                step = (float)(points[1].X - points[0].X);

            for (int i = 0; i < actualPoints.Length; i++)
            {
                x = (spectator.X + actualPoints[i].X) * scale + halfWidth;
                y = (spectator.Y - actualPoints[i].Y) * scale + halfHeight;

                graphic.Points.Add(new Point(x, y));

                if (i <= actualPoints.Length - 2)
                {
                    if ((float)(actualPoints[i + 1].X - actualPoints[i].X) != step)
                    {
                        canvas.Children.Add(graphic);
                        graphic = new Polyline
                        {
                            Stroke = Brushes.Black,
                            StrokeThickness = 2
                        };
                    }
                }
            }
            canvas.Children.Add(graphic);
        }
    }
}
