using System;

public class Program
{
    public static void Main(string[] args)
    {
        Program p = new Program();
        int x = 1211;
        int a = 2;
        int b = 0;
        char c = 'H';

        Console.WriteLine("Сумма последних двух цифр: " + p.sum_two_last_nums(x));
        Console.WriteLine("Число положительно: " + p.positive_num(x));
        Console.WriteLine("Символ большой: " + p.capital_letter(c));
        Console.WriteLine("Делится нацело: " + p.is_divisor(a, b));
        Console.WriteLine("Сумма разрядов единиц: " + p.sum_last_num(a, b));
        Console.WriteLine("Деление: " + p.div(a, b));
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
