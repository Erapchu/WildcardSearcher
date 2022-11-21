using System.Windows;
using WildcardSearcher.ViewModels;

namespace WildcardSearcher.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(MainWindowViewModel mainWindowViewModel) : this()
        {
            ViewModel = mainWindowViewModel;
        }
    }
}
