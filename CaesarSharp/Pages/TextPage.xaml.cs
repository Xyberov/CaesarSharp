using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using CaesarSharp.Core;

namespace CaesarSharp.Pages
{
    public sealed partial class TextPage : Page
    {
        private bool _encryptMode = true;
        private IReadOnlyList<StorageFile> _batchFiles = null;

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

        private async void RunButton_Click(object sender, RoutedEventArgs e)
        {
            int shift = ShiftBox.SelectedIndex + 1;
            var language = (Language)LanguageBox.SelectedIndex;

            try
            {
                if (_batchFiles != null && _batchFiles.Count > 0)
                {
                    var folderPicker = new FolderPicker();
                    folderPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                    folderPicker.FileTypeFilter.Add("*");

                    var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
                    WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

                    var folder = await folderPicker.PickSingleFolderAsync();
                    if (folder == null) return;

                    var paths = _batchFiles.Select(f => f.Path).ToList();
                    if (_encryptMode)
                        BatchProcessor.Encrypt(paths, shift, language, folder.Path);
                    else
                        BatchProcessor.Decrypt(paths, shift, language, folder.Path);

                    OutputBox.Text = $"Готово. Обработано файлов: {_batchFiles.Count}.\nРезультаты сохранены в: {folder.Path}";
                    _batchFiles = null;
                }
                else
                {
                    var text = InputBox.Text;
                    OutputBox.Text = _encryptMode
                        ? CaesarCipher.Encrypt(text, shift, language)
                        : CaesarCipher.Decrypt(text, shift, language);
                }
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

            var files = await picker.PickMultipleFilesAsync();
            if (files == null || files.Count == 0) return;

            if (files.Count == 1)
            {
                _batchFiles = null;
                InputBox.Text = await FileIO.ReadTextAsync(files[0]);
            }
            else
            {
                _batchFiles = files;
                InputBox.Text = $"Пакетная обработка: выбрано файлов — {files.Count}.\nВыберите нужные настройки и нажмите «Запустить» для продолжения.";
                OutputBox.Text = string.Empty;
            }
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
