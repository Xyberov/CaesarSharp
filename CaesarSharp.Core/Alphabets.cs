using System.Collections.Generic;

namespace CaesarSharp.Core
{
    public enum Language
    {
        Belarusian,
        Czech,
        English,
        French,
        German,
        Greek,
        Italian,
        Kazakh,
        Polish,
        Portuguese,
        Russian,
        Spanish,
        Ukrainian
    }

    internal static class Alphabets
    {
        public static readonly Dictionary<Language, (string Lower, string Upper)> Dictionary =
            new Dictionary<Language, (string Lower, string Upper)>()
        {
            [Language.Russian] = (
                "абвгдеёжзийклмнопрстуфхцчшщъыьэюя",
                "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"
            ),
            [Language.Belarusian] = (
                "абвгдеёжзійклмнопрстуўфхцчшыьэюя",
                "АБВГДЕЁЖЗІЙКЛМНОПРСТУЎФХЦЧШЫЬЭЮЯ"
            ),
            [Language.Ukrainian] = (
                "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя",
                "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ"
            ),
            [Language.Kazakh] = (
                "аәбвгғдеёжзийкқлмнңоөпрстуұүфхһцчшщъыіьэюя",
                "АӘБВГҒДЕЁЖЗИЙКҚЛМНҢОӨПРСТУҰҮФХҺЦЧШЩЪЫІЬЭЮЯ"
            ),
            [Language.German] = (
                "abcdefghijklmnopqrstuvwxyzäöüß",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÜẞ"
            ),
            [Language.French] = (
                "aàâäbcçdeéèêëfghiîïjklmnoôpqrstuùûüvwxyÿzæœ",
                "AÀÂÄBCÇDEÉÈÊËFGHIÎÏJKLMNOÔPQRSTUÙÛÜVWXYŸZÆŒ"
            ),
            [Language.Italian] = (
                "aàbcdeéèfghiìíîjklmnoòópqrstuùúvwxyz",
                "AÀBCDEÉÈFGHIÌÍÎJKLMNOÒÓPQRSTUÙÚVWXYZ"
            ),
            [Language.Spanish] = (
                "abcdefghijklmnñopqrstuvwxyz",
                "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ"
            ),
            [Language.Greek] = (
                "αβγδεζηθικλμνξοπρστυφχψω",
                "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ"
            ),
            [Language.Czech] = (
                "aábcčdďeéěfghijklmnňoópqrřsštťuúůvwxyýzž",
                "AÁBCČDĎEÉĚFGHIJKLMNŇOÓPQRŘSŠTŤUÚŮVWXYÝZŽ"
            ),
            [Language.Polish] = (
                "aąbcćdeęfghijklłmnńoópqrsśtuvwxyzźż",
                "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŹŻ"
            ),
            [Language.English] = (
                "abcdefghijklmnopqrstuvwxyz",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            ),
            [Language.Portuguese] = (
                "aàâãábcçdeéêfghiíjklmnoóôõpqrstuúvwxyz",
                "AÀÂÃÁBCÇDEÉÊFGHIÍJKLMNOÓÔÕPQRSTUÚVWXYZ"
            ),
        };
    }
}
