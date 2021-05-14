using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FunctionBuilder.Logic;
using System.Windows.Input;

namespace WPF
{
    public partial class MainWindow : Window
    {
        private List<Point> points = new List<Point>();
        private ScaleDrawer drawer;
        private Point lastMousePosition;
        public MainWindow()
        {
            InitializeComponent();
            drawer = new ScaleDrawer(canvas, points);
            drawer.DrawGraphic();
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
            points.Clear();
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
                    points.Add(new Point((double)i, (double)f.Answer));
                }
                drawer.DrawGraphic();
            }
            catch (Exception ex)
            {
                lbShowXY.Items.Add(new TextBlock().Text = ex.Message);
                drawer.DrawGraphic();
            }
        }

        public void Size(object sender, SizeChangedEventArgs e)
        {
            drawer.DrawGraphic();
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
            {
                drawer.Move(e.GetPosition(canvas).X - lastMousePosition.X, e.GetPosition(canvas).Y - lastMousePosition.Y);
                lastMousePosition = e.GetPosition(canvas);
            }
        }
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lastMousePosition = e.GetPosition(canvas);
        }
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                drawer.ScaleChanged(1);
            else
                drawer.ScaleChanged(-1);
        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            drawer.ShowTip(sender, e);
        }
    }
}
