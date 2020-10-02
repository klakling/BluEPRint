using System.Collections.Generic;
using BluEPRint.Core.Spectrum;
using BluEPRint.ViewModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MaterialDesignThemes.Wpf;
using OxyPlot.Wpf;

namespace BluEPRint.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TabItem> TabItems = new List<TabItem>();
        public MainWindow()
        {
            InitializeComponent();
            TabContentView.ItemsSource = TabItems;
        }

        private void OnFileDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files == null && !files.Any()) return;
            foreach (var fileName in files)
            {
                var checkedFiles = SupportedFiles.Files(fileName);
                if (!checkedFiles.Any()) return;
                var methodInfo = typeof(MainViewModelBuilder).GetMethods();
                var createMethod = methodInfo.FirstOrDefault(s => s.Name == SupportedFiles.SpectrumType(fileName) + "ViewModel");
                var result = createMethod?.Invoke(null, new object[] { SupportedFiles.Files(fileName) });
                AddTabPage(result);
            }
        }

        private void AddTabPage(dynamic viewModel)
        {
            var page = new TabItem {DataContext = viewModel, Content = new PlotView() {Model = viewModel.Model}};
            TabItems.Add(page);
        }
    }
}
