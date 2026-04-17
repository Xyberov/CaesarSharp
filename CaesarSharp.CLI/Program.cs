using System;
using System.CommandLine;
using System.IO;
using CaesarSharp.Core;

namespace CaesarSharp.CLI
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var langOption = new Option<string>("--lang")
            {
                Description = $"Язык текста. Доступные: {string.Join(", ", Enum.GetNames(typeof(Language)))}.",
                Required = true
            };

            var inputOption = new Option<string?>("--input")
            {
                Description = "Входной текст."
            };

            var fileOption = new Option<string?>("--file")
            {
                Description = "Путь к входному файлу."
            };

            var outputOption = new Option<string?>("--output")
            {
                Description = "Путь к выходному файлу (если не указан — вывод в консоль)."
            };

            var keyOption = new Option<int>("--key")
            {
                Description = "Сдвиг для шифрования/дешифрования.",
                Required = true
            };
        }
    }
}
