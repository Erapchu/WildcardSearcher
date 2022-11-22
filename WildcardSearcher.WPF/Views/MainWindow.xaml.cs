using System.Windows;
using WildcardSearcher.ViewModels;

namespace WildcardSearcher.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(MainWindowViewModel mainWindowViewModel) : this()
        {
            mainWindowViewModel.BeforeShowDialog += MainWindowViewModel_BeforeShowDialog;
            ViewModel = mainWindowViewModel;
            DataContext = mainWindowViewModel;
        }

        private void MainWindowViewModel_BeforeShowDialog(Window obj)
        {
            obj.Owner = this;
        }
    }
}
