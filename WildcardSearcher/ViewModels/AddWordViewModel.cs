using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace WildcardSearcher.ViewModels
{
    [INotifyPropertyChanged]
    public partial class AddWordViewModel
    {
        public event Action AcceptRequested;

        [ObservableProperty]
        private string _word;

        public AddWordViewModel()
        {

        }

        [RelayCommand]
        private void Accept()
        {
            AcceptRequested?.Invoke();
        }
    }
}
