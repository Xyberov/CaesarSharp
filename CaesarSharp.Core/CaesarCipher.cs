using System;
using System.Text;

namespace CaesarSharp.Core
{
    public class CaesarCipher
    {
        public static string Encrypt(string text, int shift, Language language)
        {
            ValidateText(text);
            ValidateShift(shift, language);

            var (Lower, Upper) = Alphabets.Dictionary[language];
            var result = new StringBuilder();

            foreach (char letter in text)
            {
                result.Append(ShiftChar(letter, shift, Lower, Upper));
            }

            return result.ToString();
        }

        public static string Decrypt(string text, int shift, Language language)
        {
            ValidateText(text);
            ValidateShift(shift, language);

            var (Lower, Upper) = Alphabets.Dictionary[language];
            int size = Lower.Length;
            var result = new StringBuilder();

            foreach (char letter in text)
            {
                int idxLower = Lower.IndexOf(letter);
                int idxUpper = Upper.IndexOf(letter);

                if (idxLower != -1)
                    result.Append(Lower[(idxLower + size - shift % size) % size]);
                else if (idxUpper != -1)
                    result.Append(Upper[(idxUpper + size - shift % size) % size]);
                else
                    result.Append(letter);
            }

            return result.ToString();
        }

        private static char ShiftChar(char letter, int shift, string lower, string upper)
        {
            int size = lower.Length;
            int idxLower = lower.IndexOf(letter);
            int idxUpper = upper.IndexOf(letter);

            if (idxLower != -1)
                return lower[(idxLower + shift) % size];

            if (idxUpper != -1)
                return upper[(idxUpper + shift) % size];

            return letter;
        }

        private static void ValidateText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Текст не может быть пустым.");
        }

        private static void ValidateShift(int shift, Language language)
        {
            if (shift <= 0)
                throw new ArgumentException($"Сдвиг должен быть положительным числом. Получено: {shift}.");

            int alphabetSize = Alphabets.Dictionary[language].Lower.Length;
            if (shift >= alphabetSize)
                throw new ArgumentException(
                    $"Сдвиг ({shift}) должен быть меньше размера алфавита ({alphabetSize}) для языка {language}.");
        }
    }
}
