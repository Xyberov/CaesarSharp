using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipherProg
{
    internal class CaesarCipher
    {
        static string lowerRu = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        static string upperRu = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        public static string Encrypt(string text, int shift)
        {
            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                char letter = text[i];

                if (upperRu.IndexOf(letter) != -1)
                {
                    int index = (upperRu.IndexOf(letter) + shift) % 33;
                    letter = upperRu[index];
                }
                else if (lowerRu.IndexOf(letter) != -1)
                {
                    int index = (lowerRu.IndexOf(letter) + shift) % 33;
                    letter = lowerRu[index];
                }
                else if (letter >= 'a' && letter <= 'z')
                {
                    int index = (letter - 'a' + shift) % 26;
                    letter = (char)('a' + index);
                }
                else if (letter >= 'A' && letter <= 'Z')
                {
                    int index = (letter - 'A' + shift) % 26;
                    letter = (char)('A' + index);
                }

                result = result + letter;
            }

            return result;
        }

        public static string Decrypt(string text, int shift)
        {
            int reverseShiftRu = 33 - shift % 33;
            int reverseShiftEn = 26 - shift % 26;

            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                char letter = text[i];

                if (upperRu.IndexOf(letter) != -1)
                {
                    int index = (upperRu.IndexOf(letter) + reverseShiftRu) % 33;
                    letter = upperRu[index];
                }
                else if (lowerRu.IndexOf(letter) != -1)
                {
                    int index = (lowerRu.IndexOf(letter) + reverseShiftRu) % 33;
                    letter = lowerRu[index];
                }
                else if (letter >= 'a' && letter <= 'z')
                {
                    int index = (letter - 'a' + reverseShiftEn) % 26;
                    letter = (char)('a' + index);
                }
                else if (letter >= 'A' && letter <= 'Z')
                {
                    int index = (letter - 'A' + reverseShiftEn) % 26;
                    letter = (char)('A' + index);
                }

                result = result + letter;
            }

            return result;
        }
    }
}
