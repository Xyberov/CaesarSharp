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

            var encryptCommand = new Command("encrypt", "Зашифровать текст.");
            encryptCommand.Options.Add(langOption);
            encryptCommand.Options.Add(keyOption);
            encryptCommand.Options.Add(inputOption);
            encryptCommand.Options.Add(fileOption);
            encryptCommand.Options.Add(outputOption);

            encryptCommand.SetAction(parseResult =>
            {
                try
                {
                    var language = ParseLanguage(parseResult.GetValue(langOption)!);
                    var text = ReadInput(parseResult.GetValue(inputOption), parseResult.GetValue(fileOption));
                    var result = CaesarCipher.Encrypt(text, parseResult.GetValue(keyOption), language);
                    WriteOutput(result, parseResult.GetValue(outputOption));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine($"Ошибка: {ex.Message}");
                    Console.ResetColor();
                }
            });

            var decryptCommand = new Command("decrypt", "Дешифровать текст.");
            decryptCommand.Options.Add(langOption);
            decryptCommand.Options.Add(keyOption);
            decryptCommand.Options.Add(inputOption);
            decryptCommand.Options.Add(fileOption);
            decryptCommand.Options.Add(outputOption);

            decryptCommand.SetAction(parseResult =>
            {
                try
                {
                    var language = ParseLanguage(parseResult.GetValue(langOption)!);
                    var text = ReadInput(parseResult.GetValue(inputOption), parseResult.GetValue(fileOption));
                    var result = CaesarCipher.Decrypt(text, parseResult.GetValue(keyOption), language);
                    WriteOutput(result, parseResult.GetValue(outputOption));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine($"Ошибка: {ex.Message}");
                    Console.ResetColor();
                }
            });

            var crackCommand = new Command("crack", "Автоматически определить сдвиг и дешифровать текст.");
            crackCommand.Options.Add(langOption);
            crackCommand.Options.Add(inputOption);
            crackCommand.Options.Add(fileOption);
            crackCommand.Options.Add(outputOption);

            crackCommand.SetAction(parseResult =>
            {
                try
                {
                    var language = ParseLanguage(parseResult.GetValue(langOption)!);
                    var text = ReadInput(parseResult.GetValue(inputOption), parseResult.GetValue(fileOption));
                    var shift = CaesarCracker.Crack(text, language);
                    var result = CaesarCipher.Decrypt(text, shift, language);
                    Console.WriteLine($"Определён сдвиг: {shift}");
                    WriteOutput(result, parseResult.GetValue(outputOption));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine($"Ошибка: {ex.Message}");
                    Console.ResetColor();
                }
            });

            var rootCommand = new RootCommand("CaesarSharp — шифрование и взлом шифра Цезаря.");
            rootCommand.Subcommands.Add(encryptCommand);
            rootCommand.Subcommands.Add(decryptCommand);
            rootCommand.Subcommands.Add(crackCommand);

            return rootCommand.Parse(args).Invoke();
        }

        private static Language ParseLanguage(string lang)
        {
            if (!Enum.TryParse<Language>(lang, ignoreCase: true, out var language))
                throw new ArgumentException(
                    $"Неизвестный язык: '{lang}'. Доступные: {string.Join(", ", Enum.GetNames(typeof(Language)))}.");
            return language;
        }

        private static string ReadInput(string? input, string? file)
        {
            if (!string.IsNullOrWhiteSpace(input))
                return input;

            if (!string.IsNullOrWhiteSpace(file))
            {
                if (!File.Exists(file))
                    throw new ArgumentException($"Файл не найден: '{file}'.");
                return File.ReadAllText(file);
            }

            throw new ArgumentException("Укажите текст (--input) или файл (--file).");
        }

        private static void WriteOutput(string result, string? output)
        {
            if (!string.IsNullOrWhiteSpace(output))
                File.WriteAllText(output, result);
            else
                Console.WriteLine(result);
        }
    }
}
