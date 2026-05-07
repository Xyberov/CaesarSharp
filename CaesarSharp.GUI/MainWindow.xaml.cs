using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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

        public void OpenMenu()
        {
            _menuOpen = true;
            MenuOverlay.Visibility = Visibility.Visible;
        }

        public void CloseMenu()
        {
            _menuOpen = false;
            MenuOverlay.Visibility = Visibility.Collapsed;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
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
