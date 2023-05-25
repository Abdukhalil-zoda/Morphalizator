using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;

namespace Morphalizator.Views.Pages {
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : INavigableView<ViewModels.DashboardViewModel> {
        public ViewModels.DashboardViewModel ViewModel {
            get;
        }

        public DashboardPage(ViewModels.DashboardViewModel viewModel) {
            ViewModel = viewModel;

            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var iss = dataGrid.ItemsSource;
            //MessageBox.Show("bp");
        }
    }
}