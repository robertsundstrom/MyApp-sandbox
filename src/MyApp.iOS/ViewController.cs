using GalaSoft.MvvmLight.Helpers;
using MyApp.ViewModel;
using System;

using UIKit;

namespace MyApp
{
    public partial class ViewController : UIViewController
    {
        public MainViewModel ViewModel => ViewModelLocator.Instance.Main;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            var binding = this.SetBinding(() => ViewModel.ButtonText)
                   .WhenSourceChanges(() => {
                       Label.Text = ViewModel.ButtonText;
                   });

            Button.SetCommand("TouchUpInside", ViewModel.Click);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}