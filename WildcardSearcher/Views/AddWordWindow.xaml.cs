using System.Windows;
using WildcardSearcher.ViewModels;

namespace WildcardSearcher.Views
{
    /// <summary>
    /// Interaction logic for AddWordWindow.xaml
    /// </summary>
    public partial class AddWordWindow : Window
    {
        public AddWordViewModel ViewModel { get; }

        public AddWordWindow()
        {
            InitializeComponent();
        }

        public AddWordWindow(AddWordViewModel addWordViewModel) : this()
        {
            ViewModel = addWordViewModel;
            DataContext = addWordViewModel;
            ViewModel.AcceptRequested += ViewModel_AcceptRequested;
        }

        private void ViewModel_AcceptRequested()
        {
            DialogResult = true;
        }
    }
}
