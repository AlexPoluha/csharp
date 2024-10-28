using System;

public class Time
{
    // Поля
    public byte Hours { get; private set; }
    public byte Minutes { get; private set; }

    // Конструктор
    public Time(byte hours, byte minutes)
    {
        Hours = (byte)(hours % 24); 
        Minutes = (byte)(minutes % 60);
    }

    // Метод добавления минут к объекту Time
    public Time AddMinutes(uint minutesToAdd)
    {
        uint totalMinutes = (uint)Hours * 60 + Minutes + minutesToAdd;
        byte newHours = (byte)((totalMinutes / 60) % 24);
        byte newMinutes = (byte)(totalMinutes % 60);
        return new Time(newHours, newMinutes);
    }

    // Унарный оператор ++
    public static Time operator ++(Time t)
    {
        return t.AddMinutes(1);
    }

    // Унарный оператор --
    public static Time operator --(Time t)
    {
        if (t.Hours == 0 && t.Minutes == 0)
            return new Time(23, 59);

        uint totalMinutes = (uint)t.Hours * 60 + t.Minutes - 1;
        byte newHours = (byte)((totalMinutes / 60) % 24);
        byte newMinutes = (byte)(totalMinutes % 60);
        return new Time(newHours, newMinutes);
    }

    // Бинарный оператор +
    public static Time operator +(Time t, uint minutes)
    {
        return t.AddMinutes(minutes);
    }

    // Бинарный оператор -
    public static Time operator -(Time t, uint minutes)
    {
        uint totalMinutes = (uint)t.Hours * 60 + t.Minutes;
        if (minutes >= totalMinutes)
            return new Time(0, 0);

        totalMinutes -= minutes;
        byte newHours = (byte)((totalMinutes / 60) % 24);
        byte newMinutes = (byte)(totalMinutes % 60);
        return new Time(newHours, newMinutes);
    }

    // Переопределение ToString для вывода
    public override string ToString()
    {
        return $"{Hours:D2}:{Minutes:D2}";
    }
}

public class Program
{
    public static void Main()
    {
        Time t = new Time(10, 30);
        Console.WriteLine($"Начальное время: {t}");

        while (true)
        {
            Console.Write("Введите команду (++ / -- / + <минуты> / - <минуты> / '0' для выхода): ");
            string input = Console.ReadLine();

            if (input == "0")
                break;

            if (input == "++")
            {
                t++;
                Console.WriteLine($"После добавления минуты (++): {t}");
            }
            else if (input == "--")
            {
                t--;
                Console.WriteLine($"После вычитания минуты (--): {t}");
            }
            else if (input.StartsWith("+"))
            {
                if (uint.TryParse(input.Substring(1).Trim(), out uint minutesToAdd))
                {
                    t = t + minutesToAdd;
                    Console.WriteLine($"После добавления {minutesToAdd} минут: {t}");
                }
                else
                {
                    Console.WriteLine("Некорректный ввод минут для операции +.");
                }
            }
            else if (input.StartsWith("-"))
            {
                if (uint.TryParse(input.Substring(1).Trim(), out uint minutesToSubtract))
                {
                    t = t - minutesToSubtract;
                    Console.WriteLine($"После вычитания {minutesToSubtract} минут: {t}");
                }
                else
                {
                    Console.WriteLine("Некорректный ввод минут для операции -.");
                }
            }
            else
            {
                Console.WriteLine("Некорректная команда.");
            }
        }
    }
}
