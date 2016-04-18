using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MyApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private int count;
        private RelayCommand _click;
        private string _buttonText;
        private RelayCommand _addItem;
        private string _text;
        private RelayCommand _reloadItems;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        [PreferredConstructor]
        public MainViewModel(IWebService webService, IDialogService dialogService)
        {
            WebService = webService;
            DialogService = dialogService;

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }

            Items = new ObservableCollection<Item>();

            ButtonText = "Hello World, Click Me!";
        }

        public MainViewModel()
        {

        }

        public async Task InitializeAsync()
        {
            try
            {
                var items = await WebService.GetItemsAsync();
                Items.Clear();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception e)
            {
                await DialogService.ShowError("Could not load items.", "Error", "OK", () =>
                {

                });
            }
        }

        public RelayCommand AddItem
        {
            get
            {
                return _addItem ?? (_addItem = new RelayCommand(async () =>
                {
                    try
                    {
                        var item = new Item()
                        {
                            Title = Text
                        };
                        item = await WebService.PostItemAsync(item);
                        Items.Add(item);

                        Text = string.Empty;
                    }
                    catch (Exception e)
                    {
                        await DialogService.ShowError("Could not post item.", "Error", "OK", () =>
                        {

                        });
                    }
                }, () => !string.IsNullOrWhiteSpace(Text)));
            }
        }

        public RelayCommand ReloadItems
        {
            get
            {
                return _reloadItems ?? (_reloadItems = new RelayCommand(async () =>
                {
                    await InitializeAsync();
                }));
            }
        }

        public ObservableCollection<Item> Items { get; }

        public RelayCommand Click
        {
            get
            {
                return _click ?? (_click = new RelayCommand(() =>
                {
                    ButtonText = string.Format("{0} clicks!", ++count);
                }));
            }
        }

        public string ButtonText
        {
            get
            {
                return _buttonText;
            }

            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (_text != value)
                {
                    _text = value;
                    RaisePropertyChanged();

                    AddItem.RaiseCanExecuteChanged();
                }
            }
        }

        public IWebService WebService { get; private set; }

        public IDialogService DialogService { get; private set; }
    }
}