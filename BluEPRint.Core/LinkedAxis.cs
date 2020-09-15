using OxyPlot.Axes;
using System;
using System.Diagnostics;

namespace BluEPRint.Core
{
    public class LinkedAxis : LinearAxis
    {
        /// <summary>
        /// Parent Axis to Link to.
        /// </summary>
        private readonly Axis _parent;

        /// <summary>
        /// Function that does conversion
        /// </summary>
        private Func<double, double> Converter { get; }

        /// <summary>
        /// function that does inverse conversion
        /// </summary>
        private Func<double, double> InverseConverter { get; }

        /// <summary>
        /// variable to prevent endless looping
        /// </summary>
        private bool _isConverting;

        public LinkedAxis(Axis parent, Func<double, double> converter, Func<double, double> inverseConverter)
        {
            _parent = parent;
            Converter = converter;
            InverseConverter = inverseConverter;
            StartPosition = 1;
            EndPosition = 0;
            Maximum = Converter(_parent.AbsoluteMinimum);
            Minimum = Converter(_parent.AbsoluteMaximum);
            AbsoluteMinimum = Converter(_parent.AbsoluteMaximum);
            AbsoluteMaximum = Converter(_parent.AbsoluteMinimum);
            _parent.AxisChanged += ParentChanged;
            AxisChanged += MeChanged;
        }

        /// <summary>
        /// Transforms the specified coordinate to screen coordinates.
        /// <see cref="Axis.Transform(double)"/>
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Transform(double x)
        {
            var parentValue = InverseConverter(x);
            return _parent.Transform(parentValue);
        }

        /// <summary>
        /// Inverse transforms the specified screen coordinate. This method can only be used with non-polar coordinate systems.
        /// <see cref="Axis.InverseTransform(double)"/>
        /// </summary>
        /// <param name="sx"></param>
        /// <returns></returns>
        public override double InverseTransform(double sx)
        {
            var parentValue = InverseConverter(sx);
            return _parent.InverseTransform(parentValue);
        }

        /// <summary>
        /// Adjust Zoom when parent changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ParentChanged(object sender, AxisChangedEventArgs eventArgs)
        {
            //prevent endless loop
            if (_isConverting) return;
            _isConverting = true;
            Zoom(Converter(_parent.AbsoluteMinimum), Converter(_parent.AbsoluteMaximum));
            _isConverting = false;
        }

        /// <summary>
        /// Adjust Zoom when Axis changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void MeChanged(object sender, AxisChangedEventArgs eventArgs)
        {
            if (_isConverting) return;
            _isConverting = true;
            _parent.Zoom(InverseConverter(ActualMinimum), InverseConverter(ActualMaximum));
            _isConverting = false;
        }
    }
}
