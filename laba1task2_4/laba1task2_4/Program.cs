using System;

public class Solutions
{
    // Задание 2
    // 1
    public double SafeDiv(int x, int y)
    {
        return y == 0 ? 0 : (double)x / y;
    }
    // 2
    public string MakeDecision(int x, int y)
    {
        if (x > y) return $"{x} > {y}";
        if (x < y) return $"{x} < {y}";
        return $"{x} = {y}";
    }
    // 3
    public bool Sum3(int x, int y, int z)
    {
        return x + y == z || x + z == y || y + z == x;
    }
    // 4
    public string Age(int x)
    {
        if (x % 100 >= 11 && x % 100 <= 14) return $"{x} лет";
        if (x % 10 == 1) return $"{x} год";
        if (x % 10 >= 2 && x % 10 <= 4) return $"{x} года";
        return $"{x} лет";
    }
    // 5
    public void PrintDays(string x)
    {
        switch (x.ToLower())
        {
            case "понедельник":
                Console.WriteLine("Понедельник, Вторник, Среда, Четверг, Пятница, Суббота, Воскресенье");
                break;
            case "вторник":
                Console.WriteLine("Вторник, Среда, Четверг, Пятница, Суббота, Воскресенье");
                break;
            case "среда":
                Console.WriteLine("Среда, Четверг, Пятница, Суббота, Воскресенье");
                break;
            case "четверг":
                Console.WriteLine("Четверг, Пятница, Суббота, Воскресенье");
                break;
            case "пятница":
                Console.WriteLine("Пятница, Суббота, Воскресенье");
                break;
            case "суббота":
                Console.WriteLine("Суббота, Воскресенье");
                break;
            case "воскресенье":
                Console.WriteLine("Воскресенье");
                break;
            default:
                Console.WriteLine("это не день недели");
                break;
        }
    }
    // Задание 3
    // 1
    public string ReverseListNums(int x)
    {
        string result = "";
        for (int i = x; i >= 0; i--)
        {
            result += i + " ";
        }
        return result.Trim();
    }
    // 2
    public int Pow(int x, int y)
    {
        int result = 1;
        for (int i = 0; i < y; i++)
        {
            result *= x;
        }
        return result;
    }
    // 3
    public bool EqualNum(int x)
    {
        int lastDigit = Math.Abs(x % 10);
        while (x != 0)
        {
            if (Math.Abs(x % 10) != lastDigit) return false;
            x /= 10;
        }
        return true;
    }
    // 4
    public void LeftTriangle(int x)
    {
        for (int i = 1; i <= x; i++)
        {
            for (int j = 0; j < i; j++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
    }
    // 5
    public void GuessGame()
    {
        Random rand = new Random();
        int number = rand.Next(10);
        int attempts = 0;
        int guess;

        do
        {
            Console.Write("Угадайте число от 0 до 9: ");
            if (!int.TryParse(Console.ReadLine(), out guess)) continue;
            attempts++;
        }
        while (guess != number);

        Console.WriteLine($"Поздравляю! Вы угадали число {number} за {attempts} попыток.");
    }
    // Задание 4
    // 1
    public int FindLast(int[] arr, int x)
    {
        int lastIndex = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == x) lastIndex = i;
        }
        return lastIndex;
    }
    // 2
    public int[] Add(int[] arr, int x, int pos)
    {
        if (pos < 0 || pos > arr.Length) return arr;

        int[] newArr = new int[arr.Length + 1];
        for (int i = 0; i < pos; i++)
        {
            newArr[i] = arr[i];
        }
        newArr[pos] = x;
        for (int i = pos; i < arr.Length; i++)
        {
            newArr[i + 1] = arr[i];
        }
        return newArr;
    }
    // 3
    public void Reverse(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n / 2; i++)
        {
            int temp = arr[i];
            arr[i] = arr[n - 1 - i];
            arr[n - 1 - i] = temp;
        }
    }
    // 4
    public int[] Concat(int[] arr1, int[] arr2)
    {
        int[] result = new int[arr1.Length + arr2.Length];
        for (int i = 0; i < arr1.Length; i++)
        {
            result[i] = arr1[i];
        }
        for (int i = 0; i < arr2.Length; i++)
        {
            result[arr1.Length + i] = arr2[i];
        }
        return result;
    }
    // 5
    public int[] DeleteNegative(int[] arr)
    {
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] >= 0) count++;
        }
        int[] result = new int[count];
        int index = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] >= 0) result[index++] = arr[i];
        }
        return result;
    }
}

public class Program
{
    public static void Main()
    {
        Solutions solutions = new Solutions();
        // Задание 2
        // 1
        Console.WriteLine("Введите два числа для метода SafeDiv:");
        int x = ReadIntInput();
        int y = ReadIntInput();
        Console.WriteLine("SafeDiv(x, y): " + solutions.SafeDiv(x, y));

        // 2
        Console.WriteLine("Введите два числа для метода MakeDecision:");
        x = ReadIntInput();
        y = ReadIntInput();
        Console.WriteLine("MakeDecision(x, y): " + solutions.MakeDecision(x, y));

        // 3
        Console.WriteLine("Введите три числа для метода Sum3:");
        int a = ReadIntInput();
        int b = ReadIntInput();
        int c = ReadIntInput();
        Console.WriteLine("Sum3(a, b, c): " + solutions.Sum3(a, b, c));

        // 4
        Console.WriteLine("Введите число для метода Age:");
        int age = ReadIntInput();
        Console.WriteLine("Age(age): " + solutions.Age(age));

        // 5
        Console.WriteLine("Введите день недели для метода PrintDays:");
        string day = Console.ReadLine();
        solutions.PrintDays(day);

        // Задание 3
        // 1
        Console.WriteLine("Введите число для метода ReverseListNums:");
        x = ReadIntInput();
        Console.WriteLine("ReverseListNums(x): " + solutions.ReverseListNums(x));

        // 2
        Console.WriteLine("Введите два числа для метода Pow:");
        x = ReadIntInput();
        y = ReadIntInput();
        Console.WriteLine("Pow(x, y): " + solutions.Pow(x, y));

        // 3
        Console.WriteLine("Введите число для метода EqualNum:");
        x = ReadIntInput();
        Console.WriteLine("EqualNum(x): " + solutions.EqualNum(x));

        // 4
        Console.WriteLine("Введите высоту для метода LeftTriangle:");
        x = ReadIntInput();
        solutions.LeftTriangle(x);

        // 5
        Console.WriteLine("Начинаем игру Угадайка:");
        solutions.GuessGame();

        // Задание 4
        // 1
        Console.WriteLine("Введите длину массива для FindLast:");
        int len = ReadIntInput();
        int[] arr = new int[len];
        Console.WriteLine("Введите элементы массива:");
        for (int i = 0; i < len; i++)
        {
            arr[i] = ReadIntInput();
        }
        Console.WriteLine("Введите число для поиска последнего вхождения:");
        x = ReadIntInput();
        Console.WriteLine("FindLast(arr, x): " + solutions.FindLast(arr, x));

        // 2
        Console.WriteLine("Введите позицию для добавления и значение для метода Add:");
        x = ReadIntInput();
        int pos = ReadIntInput();
        Console.WriteLine("Результат добавления в массив:");
        int[] newArr = solutions.Add(arr, x, pos);
        foreach (var item in newArr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        // 3
        Console.WriteLine("Реверс массива:");
        solutions.Reverse(arr);
        foreach (var item in arr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        // 4
        Console.WriteLine("Введите длину второго массива для Concat:");
        int len2 = ReadIntInput();
        int[] arr2 = new int[len2];
        Console.WriteLine("Введите элементы второго массива:");
        for (int i = 0; i < len2; i++)
        {
            arr2[i] = ReadIntInput();
        }
        int[] concatenated = solutions.Concat(arr, arr2);
        Console.WriteLine("Результат объединения массивов:");
        foreach (var item in concatenated)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        // 5
        Console.WriteLine("Результат удаления отрицательных чисел:");
        int[] noNegative = solutions.DeleteNegative(arr);
        foreach (var item in noNegative)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    public static int ReadIntInput()
    {
        int number;
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите целое число:");
        }
        return number;
    }
}
