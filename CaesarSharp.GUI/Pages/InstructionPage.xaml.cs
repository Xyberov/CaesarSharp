using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CaesarSharp.GUI.Pages
{
    public sealed partial class InstructionPage : Page
    {
        public InstructionPage()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
