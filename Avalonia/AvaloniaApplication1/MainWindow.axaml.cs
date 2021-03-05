using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FunctionBuilder.Logic;
using System;
using System.Text;

namespace AvaloniaApplication1
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {

            AvaloniaXamlLoader.Load(this);
            
        }
        public void Calcul1(object sender, RoutedEventArgs e)
        {
            var x = this.FindControl<TextBox>("boxAloneX");
            var exp = this.FindControl<TextBox>("boxExp");
            var answer = this.FindControl<TextBlock>("tbAnswer");
            try
            {
                decimal _x;
                if (x.Text != null && x.Text != "")
                    _x = decimal.Parse(x.Text);
                else
                    _x = 0;
                var f = new Function(exp.Text, new Variable('x', _x));
                answer.Text = $"ŒÚ‚ÂÚ: {f.Answer}  Œœ«: {f.ReversePolishNotation}";
            }
            catch(Exception ex)
            {
                answer.Text = ex.Message;
            }
        }
        public void Calcul2(object sender, RoutedEventArgs e)
        {
            var str = new StringBuilder();
            var xBefore = this.FindControl<TextBox>("boxBeforeX");
            var xAfter = this.FindControl<TextBox>("boxAfterX");
            var xStep = this.FindControl<TextBox>("boxStep");
            var exp = this.FindControl<TextBox>("boxExp");
            var output = this.FindControl<TextBlock>("tpShowXY");
            try
            {
                decimal before = decimal.Parse(xBefore.Text);
                decimal after = decimal.Parse(xAfter.Text);
                decimal step = decimal.Parse(xStep.Text);

                var f = new Function(exp.Text, new Variable('x', before));

                for (decimal i = before; i <= after; i += step)
                {
                    f.X = i;
                    str.Append($"X = {i}; Y = {f.Answer}\n");
                }
                output.Text = str.ToString();
            }
            catch(Exception ex)
            {
                output.Text = ex.Message;
            }
        }
    }
}