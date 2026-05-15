using System.Collections.Generic;
using System.IO;

namespace CaesarSharp.Core
{
    public static class BatchProcessor
    {
        public static void Encrypt(IEnumerable<string> files, int key, Language language, string? outputDir = null)
        {
            foreach (var file in files)
                ProcessFile(file, key, language, outputDir, encrypt: true);
        }

        public static void Decrypt(IEnumerable<string> files, int key, Language language, string? outputDir = null)
        {
            foreach (var file in files)
                ProcessFile(file, key, language, outputDir, encrypt: false);
        }

        private static void ProcessFile(string file, int key, Language language, string? outputDir, bool encrypt)
        {
            if (!File.Exists(file))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Error.WriteLine($"Предупреждение: файл не найден, пропущен: '{file}'.");
                Console.ResetColor();
                return;
            }

            var text = File.ReadAllText(file);
            var result = encrypt
                ? CaesarCipher.Encrypt(text, key, language)
                : CaesarCipher.Decrypt(text, key, language);

            File.WriteAllText(GetOutputPath(file, outputDir, encrypt), result);
        }

        private static string GetOutputPath(string file, string? outputDir, bool encrypt)
        {
            var dir = outputDir ?? Path.GetDirectoryName(file) ?? ".";
            var suffix = encrypt ? "_encrypted" : "_decrypted";
            return Path.Combine(dir,
                Path.GetFileNameWithoutExtension(file) + suffix + Path.GetExtension(file));
        }
    }
}