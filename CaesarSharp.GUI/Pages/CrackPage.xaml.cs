using CaesarSharp.Core;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using CoreLanguage = CaesarSharp.Core.Language;

namespace CaesarSharp.GUI.Pages
{
    public sealed partial class CrackPage : Page
    {
        public CrackPage()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)LanguageBox.SelectedItem;
            if (LanguageBox.SelectedItem is not ComboBoxItem langItem) return;
            CoreLanguage language = (CoreLanguage)Enum.Parse(typeof(CoreLanguage), langItem.Tag.ToString());
            string text = InputBox.Text;

            try
            {
                int shift = CaesarCracker.Crack(text, language);
                string decrypted = CaesarCipher.Decrypt(text, shift, language);
                OutputBox.Text = $"Сдвиг: {shift}\n{decrypted}";
            }
            catch (ArgumentException ex)
            {
                OutputBox.Text = $"Ошибка: {ex.Message}";
            }
            catch (NotSupportedException ex)
            {
                OutputBox.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void PasteInputButton_Click(object sender, RoutedEventArgs e)
        {
            var dataPackage = Clipboard.GetContent();
            if (dataPackage.Contains(StandardDataFormats.Text))
            {
                _ = dataPackage.GetTextAsync().AsTask().ContinueWith(t =>
                {
                    DispatcherQueue.TryEnqueue(() => InputBox.Text = t.Result);
                });
            }
        }

        private void CopyOutputButton_Click(object sender, RoutedEventArgs e)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(OutputBox.Text);
            Clipboard.SetContent(dataPackage);
        }

        private async void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add("*");

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
                InputBox.Text = await FileIO.ReadTextAsync(file);
        }

        private async void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileSavePicker();
            picker.FileTypeChoices.Add("Текстовый файл", new[] { ".txt" });
            picker.SuggestedFileName = "result";

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
                await FileIO.WriteTextAsync(file, OutputBox.Text);
        }
    }
}
