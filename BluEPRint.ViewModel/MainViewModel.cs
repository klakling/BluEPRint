using System;
using System.Collections.Generic;
using System.Linq;
using BluEPRint.Core;
using OxyPlot;
using ChemSharp.Spectrum;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace BluEPRint.ViewModel
{
    /// <summary>
    /// View Model for MainWindow
    /// </summary>
    public class MainViewModel<T> : AbstractViewModel where T : AbstractSpectrum
    {
        /// <summary>
        /// Shown PlotModel
        /// </summary>
        public DefaultPlotModel Model
        {
            get => Get<DefaultPlotModel>(); 
            set => Set(value);
        }

        /// <summary>
        /// Current Window/Tab 's Spectrum
        /// </summary>
        public AbstractSpectrum Spectrum
        {
            get => Get<AbstractSpectrum>();
            set => Set(value);
        }

        public MainViewModel(IEnumerable<string> paths)
        {
            Spectrum = SpectrumFactory.Create<T>(paths);
            Model = new DefaultPlotModel();

            Model.XAxis.AbsoluteMaximum = Spectrum.Data.Max(s => s.X);
            Model.XAxis.AbsoluteMinimum = Spectrum.Data.Min(s => s.X);

            //check if secondary x axis is implemented
            if (Spectrum is ISecondaryXAxis axis)
            {
                var secondaryXAxis = new LinkedAxis(Model.XAxis,
                    FuncMapping.Map(axis.PrimaryToSecondary),
                    FuncMapping.Map(axis.PrimaryToSecondary))
                {
                    Position = AxisPosition.Top
                };
                Model.Axes.Add(secondaryXAxis);
            }
            var series = new LineSeries()
            {
                ItemsSource = Spectrum.Data.Select(s => new DataPoint(s.X, s.Y))
            };
            Model.Series.Add(series);
        }
    }
}
