using BluEPRint.Core;
using ChemSharp.Spectrum;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Linq;
using System.Windows;
using BluEPRint.ViewModel;

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

            var vm = new MainViewModel<EPRSpectrum>(new[] {path + ".par", path + ".spc"});
            TestPlotView.Model = vm.Model;
        }
    }
}
