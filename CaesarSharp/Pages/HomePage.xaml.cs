using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CaesarSharp.Pages
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
