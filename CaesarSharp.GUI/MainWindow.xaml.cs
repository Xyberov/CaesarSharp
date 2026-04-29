using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using CaesarSharp.GUI.Pages;

namespace CaesarSharp.GUI
{
    public sealed partial class MainWindow : Window
    {
        private bool _menuOpen = false;

        public MainWindow()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(HomePage));
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (_menuOpen)
                CloseMenu();
            else
                OpenMenu();
        }

        private void OpenMenu()
        {
            _menuOpen = true;
            Overlay.Visibility = Visibility.Visible;
            Overlay.Opacity = 0.4;
            SideMenu.Visibility = Visibility.Visible;
        }

        private void CloseMenu()
        {
            _menuOpen = false;
            Overlay.Opacity = 0;
            Overlay.Visibility = Visibility.Collapsed;
            SideMenu.Visibility = Visibility.Collapsed;
        }

        private void Overlay_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CloseMenu();
        }

        private void NavTextPage_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(TextPage));
            CloseMenu();
        }

        private void NavCrackPage_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(CrackPage));
            CloseMenu();
        }

        private void NavInstructionPage_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(InstructionPage));
            CloseMenu();
        }
    }
}
