using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace CaesarSharp.GUI.Pages
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        private void NavTextPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TextPage));
        }

        private void NavCrackPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CrackPage));
        }

        private void NavInstructionPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InstructionPage));
        }
    }
}
