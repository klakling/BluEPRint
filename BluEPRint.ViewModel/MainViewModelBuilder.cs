using ChemSharp.Spectrum;
using System.Collections.Generic;

namespace BluEPRint.ViewModel
{
    public static class MainViewModelBuilder
    {

        public static MainViewModel<EPRSpectrum> EPRViewModel(IEnumerable<string> paths) => new MainViewModel<EPRSpectrum>(paths);
        public static MainViewModel<UVVisSpectrum> UVVisViewModel(IEnumerable<string> paths) => new MainViewModel<UVVisSpectrum>(paths);
        public static MainViewModel<NMRSpectrum> NMRViewModel(IEnumerable<string> paths) => new MainViewModel<NMRSpectrum>(paths);
    }
}
