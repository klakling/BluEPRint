using BluEPRint.Core;
using BluEPRint.Core.Spectrum;
using ChemSharp.Spectrum;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Linq;
using System.Windows;

namespace BluEPRint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var path =
                @"C:\Users\jenso\PowerFolders\Forschung\EPR\PyOMeIPCoCl\5_55k\Spectrum6";
            var spec = SpectrumFactory.Create<PlottableEPRSpectrum>(path + ".par", path + ".spc");
            var model = new PlotModel();
            var x = new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                AbsoluteMaximum = spec.Data.Max(s => s.X),
                AbsoluteMinimum = spec.Data.Min(s => s.X)
            };
            var g = new LinkedAxis(x, spec.BtoG, spec.GtoB)
            {
                Position = AxisPosition.Top
            };
            var y = new LinearAxis()
            {
                Position = AxisPosition.Left,
                TickStyle = TickStyle.None,
                IsAxisVisible = false
            };
            model.Axes.Add(y);
            model.Axes.Add(x);
            model.Axes.Add(g);

            var series = new LineSeries()
            {
                ItemsSource = spec.Data.Select(s => new DataPoint(s.X, s.Y))
            };
            model.Series.Add(series);
            TestPlotView.Model = model;
        }
    }
}
