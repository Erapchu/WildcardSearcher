using CommunityToolkit.Mvvm.ComponentModel;
using WildcardSearcher.Interfaces;

namespace WildcardSearcher.ViewModels
{
    [INotifyPropertyChanged]
    public partial class MainWindowViewModel
    {
        private readonly IWildcardSearcher _wildcardSearcher;

        public MainWindowViewModel(IWildcardSearcher wildcardSearcher)
        {
            _wildcardSearcher = wildcardSearcher;
        }


    }
}
