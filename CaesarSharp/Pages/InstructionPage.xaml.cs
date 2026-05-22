using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CaesarSharp.Pages
{
    public sealed partial class InstructionPage : Page
    {
        private const string TextRussian =
            "Добро пожаловать в программу шифрования по шифру Цезаря.\n\n" +
            "Шифр Цезаря — это способ скрыть текст, сдвинув каждую букву на несколько позиций в алфавите. Например, при сдвиге 3 буква «А» превращается в «Г», «Б» в «Д» и так далее.\n\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            "РАБОТА С ТЕКСТОМ\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
            "Шифрование:\n" +
            "1. На главном экране нажмите «Работа с текстом».\n" +
            "2. Убедитесь, что выбран режим «Шифровать» (кнопка подсвечена ярче).\n" +
            "3. Выберите язык вашего текста в выпадающем списке.\n" +
            "4. Выберите сдвиг — любое число от 1 до размера алфавита.\n" +
            "5. Введите текст в верхнее серое поле. Можно также нажать «Загрузить файл» и выбрать .txt файл.\n" +
            "6. Нажмите «Запуск». Зашифрованный текст появится в нижнем поле.\n" +
            "7. Скопируйте результат кнопкой «Копировать» или сохраните кнопкой «Сохранить файл».\n\n" +
            "Расшифровка:\n" +
            "1. Нажмите кнопку «Дешифровать».\n" +
            "2. Выберите тот же язык и тот же сдвиг, который использовался при шифровании.\n" +
            "3. Введите зашифрованный текст в верхнее поле.\n" +
            "4. Нажмите «Запуск». Исходный текст появится в нижнем поле.\n\n" +
            "Важно: если выбрать неправильный язык или сдвиг — результат будет нечитаемым. Убедитесь, что язык и сдвиг совпадают с теми, что использовались при шифровании.\n\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            "ПАКЕТНАЯ ОБРАБОТКА ФАЙЛОВ\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
            "Если нужно зашифровать или расшифровать сразу несколько файлов:\n\n" +
            "1. На главном экране нажмите «Работа с текстом».\n" +
            "2. Выберите режим («Шифровать» или «Дешифровать»), язык и сдвиг.\n" +
            "3. Нажмите «Загрузить файл» и выделите сразу несколько файлов (удерживайте Ctrl при выборе).\n" +
            "4. В верхнем поле появится сообщение о количестве выбранных файлов.\n" +
            "5. Нажмите «Запуск». Программа попросит выбрать папку для сохранения результатов.\n" +
            "6. Укажите папку и нажмите «Выбрать папку». Обработанные файлы появятся в ней с суффиксами _encrypted или _decrypted.\n\n" +
            "Важно: папка для сохранения должна уже существовать — программа её не создаёт.\n\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            "ВЗЛОМ ШИФРА\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
            "Если у вас есть зашифрованный текст, но сдвиг неизвестен — программа попробует определить его автоматически.\n\n" +
            "1. На главном экране нажмите «Взлом шифра».\n" +
            "2. Выберите язык оригинального текста.\n" +
            "3. Введите зашифрованный текст в верхнее поле или загрузите файл.\n" +
            "4. Нажмите «Запуск». Программа автоматически найдёт сдвиг и покажет расшифрованный текст.\n\n" +
            "Поддерживаемые языки для взлома: русский, английский, немецкий, французский, испанский.\n\n" +
            "Важно: чем длиннее текст — тем точнее результат. На коротких фразах (менее 50 слов) программа может ошибиться.\n\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            "КОМАНДНАЯ СТРОКА (CLI)\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
            "Программа также поддерживает работу через командную строку. Это позволяет автоматизировать шифрование, обрабатывать файлы скриптами и использовать пакетную обработку через терминал.\n\n" +
            "Подробная инструкция по работе с CLI находится в файле «Инструкция.pdf» рядом с программой.\n\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            "О ПРОГРАММЕ\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
            "Команда «Три мушкетёра», УрФУ, 2026\n\n" +
            "Разработчики:\n" +
            "— Федорова Алина Александровна\n" +
            "— Белоусов Александр Сергеевич\n" +
            "— Рыбалева Екатерина Алексеевна";

        private const string TextEnglish =
            "Welcome to the Caesar Cipher encryption program.\n\n" +
            "The Caesar cipher is a way to hide text by shifting each letter a fixed number of positions in the alphabet. For example, with a shift of 3, the letter \"A\" becomes \"D\", \"B\" becomes \"E\", and so on.\n\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            "TEXT MODE\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
            "Encryption:\n" +
            "1. On the main screen, click \"Text Mode\".\n" +
            "2. Make sure \"Encrypt\" mode is selected (the button will appear brighter).\n" +
            "3. Select the language of your text from the dropdown list.\n" +
            "4. Choose a shift value — any number from 1 to the size of the alphabet.\n" +
            "5. Type your text into the upper grey field, or click \"Load File\" to open a .txt file.\n" +
            "6. Click \"Run\". The encrypted text will appear in the lower field.\n" +
            "7. Copy the result using the \"Copy\" button, or save it using \"Save File\".\n\n" +
            "Decryption:\n" +
            "1. Click the \"Decrypt\" button.\n" +
            "2. Select the same language and the same shift that were used during encryption.\n" +
            "3. Enter the encrypted text into the upper field.\n" +
            "4. Click \"Run\". The original text will appear in the lower field.\n\n" +
            "Important: if the wrong language or shift is selected, the result will be unreadable. Make sure the language and shift match exactly what was used during encryption.\n\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            "BATCH FILE PROCESSING\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
            "To encrypt or decrypt multiple files at once:\n\n" +
            "1. On the main screen, click \"Text Mode\".\n" +
            "2. Select the mode (\"Encrypt\" or \"Decrypt\"), language, and shift.\n" +
            "3. Click \"Load File\" and select multiple files at once (hold Ctrl while selecting).\n" +
            "4. The upper field will show a message with the number of selected files.\n" +
            "5. Click \"Run\". The program will ask you to choose a folder to save the results.\n" +
            "6. Select a folder and click \"Select Folder\". The processed files will appear there with _encrypted or _decrypted suffixes.\n\n" +
            "Important: the destination folder must already exist — the program will not create it.\n\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            "CIPHER CRACKING\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
            "If you have encrypted text but don't know the shift — the program will try to find it automatically.\n\n" +
            "1. On the main screen, click \"Crack Cipher\".\n" +
            "2. Select the language of the original text.\n" +
            "3. Enter the encrypted text into the upper field, or load a file.\n" +
            "4. Click \"Run\". The program will automatically detect the shift and display the decrypted text.\n\n" +
            "Supported languages for cracking: Russian, English, German, French, Spanish.\n\n" +
            "Important: the longer the text, the more accurate the result. For short texts (fewer than 50 words), the program may produce incorrect results.\n\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            "COMMAND LINE (CLI)\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
            "The program also supports command-line usage. This allows you to automate encryption, process files with scripts, and use batch processing via terminal.\n\n" +
            "Detailed CLI instructions are available in the file \"Инструкция.pdf\" located next to the program.\n\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            "ABOUT\n" +
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
            "Team \"Tri Mushketyora\", UrFU, 2026\n\n" +
            "Developers:\n" +
            "— Fedorova Alina Alexandrovna\n" +
            "— Belousov Alexander Sergeevich\n" +
            "— Rybaleva Ekaterina Alexeevna";

        public InstructionPage()
        {
            this.InitializeComponent();
            LanguageBox.SelectedIndex = 0;
            InstructionText.Text = TextRussian;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void LanguageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageBox.SelectedItem is ComboBoxItem item)
            {
                InstructionText.Text = item.Tag.ToString() == "English" ? TextEnglish : TextRussian;
            }
        }
    }
}
