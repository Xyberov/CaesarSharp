using System;
using System.CommandLine;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;
using CaesarSharp.Core;

namespace CaesarSharp
{
    public static class ProgramCLI
    {
        private const int AttachParentProcess = -1;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                EnsureConsole();
                return RunCli(args);
            }

            WinRT.ComWrappersSupport.InitializeComWrappers();
            Application.Start(p =>
            {
                var context = new Microsoft.UI.Dispatching.DispatcherQueueSynchronizationContext(
                    Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread());
                System.Threading.SynchronizationContext.SetSynchronizationContext(context);
                new App();
            });
            return 0;
        }

        private static void EnsureConsole()
        {
            if (!AttachConsole(AttachParentProcess))
                AllocConsole();

            try
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
                Console.SetError(new StreamWriter(Console.OpenStandardError()) { AutoFlush = true });
                Console.SetIn(new StreamReader(Console.OpenStandardInput()));
            }
            catch (IOException)
            {
            }
        }

        private static int RunCli(string[] args)
        {
            PrepareConsoleLine();

            var langOption = new Option<string>("--lang")
            {
                Description = $"Язык текста. Доступные: {string.Join(", ", Enum.GetNames(typeof(Language)))}.",
                Required = true
            };
            var inputOption = new Option<string?>("--input") { Description = "Входной текст." };
            var fileOption = new Option<string?>("--file") { Description = "Путь к входному файлу." };
            var outputOption = new Option<string?>("--output") { Description = "Путь к выходному файлу или папке." };
            var keyOption = new Option<int>("--key") { Description = "Сдвиг.", Required = true };
            var filesOption = new Option<string[]?>("--files")
            {
                Description = "Несколько файлов для пакетной обработки.",
                AllowMultipleArgumentsPerToken = true
            };

            var encryptCommand = new Command("encrypt", "Зашифровать текст.");
            encryptCommand.Options.Add(langOption);
            encryptCommand.Options.Add(keyOption);
            encryptCommand.Options.Add(inputOption);
            encryptCommand.Options.Add(fileOption);
            encryptCommand.Options.Add(outputOption);
            encryptCommand.Options.Add(filesOption);
            encryptCommand.SetAction(parseResult =>
            {
                try
                {
                    var language = ParseLanguage(parseResult.GetValue(langOption)!);
                    var files = parseResult.GetValue(filesOption);
                    if (files != null && files.Length > 0)
                        BatchProcessor.Encrypt(files, parseResult.GetValue(keyOption), language, parseResult.GetValue(outputOption));
                    else
                    {
                        var text = ReadInput(parseResult.GetValue(inputOption), parseResult.GetValue(fileOption));
                        WriteOutput(CaesarCipher.Encrypt(text, parseResult.GetValue(keyOption), language), parseResult.GetValue(outputOption));
                    }
                }
                catch (Exception ex) { PrintError(ex.Message); }
            });

            var decryptCommand = new Command("decrypt", "Дешифровать текст.");
            decryptCommand.Options.Add(langOption);
            decryptCommand.Options.Add(keyOption);
            decryptCommand.Options.Add(inputOption);
            decryptCommand.Options.Add(fileOption);
            decryptCommand.Options.Add(outputOption);
            decryptCommand.Options.Add(filesOption);
            decryptCommand.SetAction(parseResult =>
            {
                try
                {
                    var language = ParseLanguage(parseResult.GetValue(langOption)!);
                    var files = parseResult.GetValue(filesOption);
                    if (files != null && files.Length > 0)
                        BatchProcessor.Decrypt(files, parseResult.GetValue(keyOption), language, parseResult.GetValue(outputOption));
                    else
                    {
                        var text = ReadInput(parseResult.GetValue(inputOption), parseResult.GetValue(fileOption));
                        WriteOutput(CaesarCipher.Decrypt(text, parseResult.GetValue(keyOption), language), parseResult.GetValue(outputOption));
                    }
                }
                catch (Exception ex) { PrintError(ex.Message); }
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
                    Console.WriteLine($"Определён сдвиг: {shift}");
                    WriteOutput(CaesarCipher.Decrypt(text, shift, language), parseResult.GetValue(outputOption));
                }
                catch (Exception ex) { PrintError(ex.Message); }
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
                throw new ArgumentException($"Неизвестный язык: '{lang}'.");
            return language;
        }

        private static string ReadInput(string? input, string? file)
        {
            if (!string.IsNullOrWhiteSpace(input)) return input;
            if (!string.IsNullOrWhiteSpace(file))
            {
                if (!File.Exists(file)) throw new ArgumentException($"Файл не найден: '{file}'.");
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

        private static void PrepareConsoleLine()
        {
            if (!Console.IsOutputRedirected)
                Console.Out.WriteLine();
        }

        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Ошибка: {message}");
            Console.ResetColor();
        }
    }
}