using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WildcardSearcher.Interfaces;

namespace WildcardSearcher.ViewModels
{
    [INotifyPropertyChanged]
    public partial class MainWindowViewModel
    {
        private readonly IWildcardSearcher _wildcardSearcher;
        private CancellationTokenSource _cts;

        private string _pattern;
        public string Pattern
        {
            get => _pattern;
            set
            {
                if (SetProperty(ref _pattern, value))
                {
                    _cts?.Cancel();
                    _cts = new CancellationTokenSource();
                    _ = SearchAsync(_pattern, _cts.Token);
                }
            }
        }

        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();

        public MainWindowViewModel(IWildcardSearcher wildcardSearcher)
        {
            _wildcardSearcher = wildcardSearcher;
        }

        private async Task SearchAsync(string pattern, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(100, cancellationToken);

                var words = _wildcardSearcher.SearchWords(pattern);
                Items.Clear();
                foreach (var word in words)
                {
                    Items.Add(word);
                }
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
