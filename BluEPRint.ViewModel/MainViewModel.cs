using System;
using BluEPRint.Core;
using ChemSharp.Spectrum;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;

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
        public T Spectrum
        {
            get => Get<T>();
            set => Set(value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="paths"></param>
        public MainViewModel(IEnumerable<string> paths) => Initialize(paths);

        /// <summary>
        /// Initializes Object, not necessary when constructor is used
        /// </summary>
        /// <param name="paths"></param>
        public void Initialize(IEnumerable<string> paths)
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
