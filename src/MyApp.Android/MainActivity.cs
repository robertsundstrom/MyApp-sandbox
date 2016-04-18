using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MyApp.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using MyApp.Services;
using GalaSoft.MvvmLight.Views;

namespace MyApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ActivityBase
    {
        int count = 1;

        public Button Button;
        public ListView ItemsListView;
        public Button AddItemButton;
        public Button ReloadButton;
        public EditText EditText;

        public MainViewModel ViewModel => ViewModelLocator.Instance.Main;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button = FindViewById<Button>(Resource.Id.MyButton);
            ItemsListView = FindViewById<ListView>(Resource.Id.ListView);
            AddItemButton = FindViewById<Button>(Resource.Id.AddItemButton);
            ReloadButton = FindViewById<Button>(Resource.Id.ReloadButton);

            EditText = FindViewById<EditText>(Resource.Id.EditText);

            Button.SetCommand("Click", ViewModel.Click);
            AddItemButton.SetCommand("Click", ViewModel.AddItem);
            ReloadButton.SetCommand("Click", ViewModel.ReloadItems);

            this.SetBinding(() => ViewModel.ButtonText, () => Button.Text, BindingMode.TwoWay);
            this.SetBinding(() => ViewModel.Text, () => EditText.Text, BindingMode.TwoWay);

            ItemsListView.Adapter = ViewModel.Items.GetAdapter(GetItemView);

            await ViewModel.InitializeAsync();
            
            //ViewModel.Text = Resources.GetString(Resource.String.Hello);
        }

        private View GetItemView(int position, Item item, View convertView)
        {
            View view = convertView ?? LayoutInflater.Inflate(Resource.Layout.Item, null);

            var title = view.FindViewById<TextView>(Resource.Id.Title);

            title.Text = item.Title;

            return view;
        }
    }
}

