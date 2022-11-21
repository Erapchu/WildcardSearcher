using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WildcardSearcher.Interfaces;
using WildcardSearcher.Views;

namespace WildcardSearcher.ViewModels
{
    [INotifyPropertyChanged]
    public partial class MainWindowViewModel
    {
        public event Action<Window> BeforeShowDialog;

        private readonly IWildcardSearcher _wildcardSearcher;
        private readonly IServiceProvider _serviceProvider;
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

        public MainWindowViewModel(
            IWildcardSearcher wildcardSearcher,
            IServiceProvider serviceProvider)
        {
            _wildcardSearcher = wildcardSearcher;
            _serviceProvider = serviceProvider;
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

        [RelayCommand]
        private void AddNewWord()
        {
            var addWordWindow = _serviceProvider.GetService<AddWordWindow>();
            BeforeShowDialog?.Invoke(addWordWindow);
            var dialogResult = addWordWindow.ShowDialog();
            if (dialogResult == true)
            {
                try
                {
                    _wildcardSearcher.AddWord(addWordWindow.ViewModel.Word);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
