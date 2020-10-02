using OxyPlot;
using OxyPlot.Axes;

namespace BluEPRint.Core.Plotting
{
    public class DefaultPlotModel : PlotModel
    {
        public LinearAxis XAxis { get; }
        public LinearAxis YAxis { get; }
        public DefaultPlotModel()
        {
            Padding = new OxyThickness(2);
            IsLegendVisible = false;
            LegendPosition = LegendPosition.RightTop;
            DefaultFontSize = 14;
            LegendFontSize = 14;
            DefaultFont = "Arial";
            PlotAreaBorderThickness = new OxyThickness(2);
            TitleFontWeight = 200;

            XAxis = new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Key = "X"
            };
            Axes.Add(XAxis);
            YAxis = new LinearAxis()
            {
                Position = AxisPosition.Left,
                Key = "Y"
            };
            Axes.Add(YAxis);
        }
    }
}
