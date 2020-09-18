using BluEPRint.Core;
using BluEPRint.ViewModel;
using ChemSharp.Spectrum;
using OxyPlot.Wpf;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BluEPRint.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnFileDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var fileName in files)
            {
                if (!SupportedFiles.Files(fileName).Any()) return;
                AddTabPage(CreateViewModel(fileName));
            }
        }

        private void AddTabPage(dynamic viewModel)
        {
            var item = new TabItem()
            {
                Header = viewModel.Spectrum.Files[0].Path,
                DataContext = viewModel
            };
            item.Content = new PlotView() {Model = viewModel.Model};
            TabContentView.Items.Add(item);
        }

        private static dynamic CreateViewModel(string path)
        {
            var ext = Path.GetExtension(path);
            if(string.IsNullOrEmpty(ext)) ext = Path.GetFileName(path);
            return ext switch
            {
                ".par" => new MainViewModel<EPRSpectrum>(SupportedFiles.Files(path)),
                ".spc" => new MainViewModel<EPRSpectrum>(SupportedFiles.Files(path)),
                "fid" => new MainViewModel<NMRSpectrum>(SupportedFiles.Files(path)),
                "acqus" => new MainViewModel<NMRSpectrum>(SupportedFiles.Files(path)),
                ".DSW" => new MainViewModel<UVVisSpectrum>(SupportedFiles.Files(path)),
                _ => null
            };
        }
    }
}
