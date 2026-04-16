using System;
using System.Collections.Generic;
using System.Linq;

namespace CaesarSharp.Core
{
    internal static class CaesarCracker
    {
        private static readonly Dictionary<Language, string> FrequentLetters =
            new Dictionary<Language, string>()
        {
            [Language.Russian] = "оеаинт",
            [Language.English] = "etaoin",
            [Language.German]  = "enisra",
            [Language.French]  = "esaitn",
            [Language.Spanish] = "eaosin",
        };

        public static int Crack(string cipherText, Language language)
        {
            var (Lower, Upper) = Alphabets.Dictionary[language];
            int alphabetSize = Lower.Length;
            string frequent = FrequentLetters[language];

            int[] counts = new int[alphabetSize];
            int totalLetters = 0;

            foreach (char c in cipherText)
            {
                int index = Lower.IndexOf(char.ToLower(c));
                if (index != -1)
                {
                    counts[index]++;
                    totalLetters++;
                }
            }

            return Enumerable.Range(1, alphabetSize)
                .OrderByDescending(shift => frequent
                    .Select((ch, i) => (double)counts[(Lower.IndexOf(ch) + shift) % alphabetSize] / totalLetters * (frequent.Length - i))
                    .Sum())
                .First();
        }
    }
}
