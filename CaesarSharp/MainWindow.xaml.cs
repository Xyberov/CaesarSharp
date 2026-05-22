using Microsoft.UI.Xaml;
using CaesarSharp.Pages;

namespace CaesarSharp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            RootFrame.Navigate(typeof(HomePage));
        }
    }
}
