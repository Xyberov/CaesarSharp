using System.Text;

namespace CaesarSharp.Core
{
    internal class CaesarCipher
    {
        public static string Encrypt(string text, int shift, Language language)
        {
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
            var (Lower, Upper) = Alphabets.Dictionary[language];
            var result = new StringBuilder();

            foreach (char letter in text)
            {
                int size = Lower.Length;
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
    }
}
