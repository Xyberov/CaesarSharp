using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using CaesarSharp.Core;

namespace CaesarSharp.GUI.Pages
{
    public sealed partial class TextPage : Page
    {
        private bool _encryptMode = true;

        private static readonly SolidColorBrush ActiveBrush =
            new SolidColorBrush(Windows.UI.Color.FromArgb(255, 60, 174, 17));
        private static readonly SolidColorBrush InactiveBrush =
            new SolidColorBrush(Windows.UI.Color.FromArgb(255, 41, 117, 13));

        public TextPage()
        {
            this.InitializeComponent();
            UpdateModeButtons();
        }

        private void UpdateModeButtons()
        {
            EncryptButton.Background = _encryptMode ? ActiveBrush : InactiveBrush;
            DecryptButton.Background = _encryptMode ? InactiveBrush : ActiveBrush;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            _encryptMode = true;
            UpdateModeButtons();
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            _encryptMode = false;
            UpdateModeButtons();
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            int shift = ShiftBox.SelectedIndex + 1;
            var language = (Language)LanguageBox.SelectedIndex;
            var text = InputBox.Text;

            try
            {
                OutputBox.Text = _encryptMode
                    ? CaesarCipher.Encrypt(text, shift, language)
                    : CaesarCipher.Decrypt(text, shift, language);
            }
            catch (ArgumentException ex)
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
