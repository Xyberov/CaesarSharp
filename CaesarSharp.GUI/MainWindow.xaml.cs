using Microsoft.UI.Xaml;
using CaesarSharp.GUI.Pages;

namespace CaesarSharp.GUI
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
