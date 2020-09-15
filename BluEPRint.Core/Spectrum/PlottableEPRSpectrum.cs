using ChemSharp;
using ChemSharp.Math.Unit;
using ChemSharp.Spectrum;

namespace BluEPRint.Core.Spectrum
{
    public class PlottableEPRSpectrum : EPRSpectrum
    {
        public double BtoG(double s) => GValue((float)s, Frequency, Unit);
        public double GtoB(double s) => BValue((float) s, Frequency, Unit);

        public static float BValue(float xInput, float frequency, string unit = "G")
        {
            var converter = new MagneticUnitConverter("T", unit);
            //convert input to Tesla unit
            var val = Constants.Planck * frequency * 1e9 / (Constants.BohrM * xInput);

            return (float) converter.Convert(val);
        }
    }
}
