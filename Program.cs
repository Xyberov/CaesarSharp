using CaesarCipherProg;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Привет! Это программа для шифра Цезаря.");
        Console.WriteLine("Введите текст который хотите зашифровать или расшифровать:");
        string text = Console.ReadLine();

        Console.WriteLine("Введите сдвиг от 1 до 32:");
        int shift;
        while (!int.TryParse(Console.ReadLine(), out shift) || shift < 1 || shift > 25)
        {
            Console.WriteLine("Неверный ввод. Введите число от 1 до 32:");
        }

        Console.WriteLine("Что сделать? Введите 1 чтобы зашифровать, 2 чтобы расшифровать:");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
        {
            Console.WriteLine("Неверный ввод. Введите 1 или 2:");
        }

        string result = "";

        if (choice == 1)
        {
            result = CaesarCipher.Encrypt(text, shift);
            Console.WriteLine("Зашифрованный текст: " + result);
        }
        else
        {
            result = CaesarCipher.Decrypt(text, shift);
            Console.WriteLine("Расшифрованный текст: " + result);
        }
    }
}