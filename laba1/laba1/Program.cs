using System;

public class Program
{
    public static void Main(string[] args)
    {
        Program p = new Program();

        // Ввод данных с клавиатуры с проверками
        int x = InputInt("Введите целое число x: ");
        int a = InputInt("Введите целое число a: ");
        int b = InputInt("Введите целое число b: ");
        char c = InputChar("Введите символ c: ");

        // Выполнение методов с выводом на экран
        Console.WriteLine("Сумма последних двух цифр: " + p.sum_two_last_nums(x));
        Console.WriteLine("Число положительно: " + p.positive_num(x));
        Console.WriteLine("Символ большой: " + p.capital_letter(c));
        Console.WriteLine("Делится нацело: " + p.is_divisor(a, b));
        Console.WriteLine("Сумма разрядов единиц: " + p.sum_last_num(a, b));
        Console.WriteLine("Деление: " + p.div(a, b));
    }

    public static int InputInt(string prompt)
    {
        int result;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out result))
                return result;
            Console.WriteLine("Ошибка ввода! Пожалуйста, введите корректное целое число.");
        }
    }

    public static char InputChar(string prompt)
    {
        char result;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && char.TryParse(input, out result))
                return result;
            Console.WriteLine("Ошибка ввода! Пожалуйста, введите один символ.");
        }
    }

    // Задание 1
    // 2
    public int sum_two_last_nums(int x)
    {
        return (x % 10) + (x / 10 % 10);
    }

    // 4
    public bool positive_num(int x)
    {
        return x > 0;
    }

    // 6
    public bool capital_letter(char x)
    {
        return char.IsUpper(x);
    }

    // 8
    public string is_divisor(int a, int b)
    {
        return (a == 0 || b == 0) ? "Поделить невозможно" : (a % b == 0 || b % a == 0).ToString();
    }

    // 10
    public int sum_last_num(int a, int b)
    {
        return (a % 10) + (b % 10);
    }

    // Задание 2
    // 2
    public double div(int x, int y)
    {
        if (y == 0) return 0;
        return (double)x / y;
    }
}
