using System;
using System.Collections.Generic;
using System.Linq;

namespace LaboratoryWork
{
    public interface IFractionManagement
    {
        double GetDecimalValue();
        void SetNumerator(int numerator);
        void SetDenominator(int denominator);
    }

    public class Fraction : ICloneable, IFractionManagement
    {
        public int Numerator { get; protected set; }
        public int Denominator { get; protected set; }
        private double? cachedDecimalValue = null;

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Знаменатель не может быть равен нулю");

            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = Math.Abs(denominator);
            }
            
            int gcd = CalculateGCD(Math.Abs(numerator), Math.Abs(denominator));
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }

        // нод
        private int CalculateGCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public override string ToString() => $"{Numerator}/{Denominator}";

        // десятичное значение
        public double GetDecimalValue()
        {
            if (!cachedDecimalValue.HasValue)
            {
                cachedDecimalValue = (double)Numerator / Denominator;
            }
            return cachedDecimalValue.Value;
        }

        // числитель
        public void SetNumerator(int numerator)
        {
            Numerator = numerator;
            cachedDecimalValue = null;
        }

        // знаменатель 
        public void SetDenominator(int denominator)
        {
            Denominator = denominator;
            cachedDecimalValue = null;
        }

        // Sum, Minus, Mult, Div
        public Fraction Sum(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator + other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        // сложение 
        public Fraction Sum(int number)
        {
            return Sum(new Fraction(number, 1));
        }

        //вычитание дроби 
        public Fraction Minus(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator - other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        // вычитание целого
        public Fraction Minus(int number)
        {
            return Minus(new Fraction(number, 1));
        }

        // умножение дроби
        public Fraction Mult(Fraction other)
        {
            int newNumerator = Numerator * other.Numerator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        // умножение на число
        public Fraction Mult(int number)
        {
            return Mult(new Fraction(number, 1));
        }

        // деление 
        public Fraction Div(Fraction other)
        {
            if (other.Numerator == 0)
            {
                Console.WriteLine("Нельзя делить на дробь с нулевым числителем");
                return null;
            }

            int newNumerator = Numerator * other.Denominator;
            int newDenominator = Denominator * other.Numerator;
            return new Fraction(newNumerator, newDenominator);
        }

        // деление на целое
        public Fraction Div(int number)
        {
            return Div(new Fraction(number, 1));
        }

        // Проверяет равенство двух дробей
        public override bool Equals(object obj)
        {
            if (obj is Fraction other)
            {
                return Numerator == other.Numerator && Denominator == other.Denominator;
            }
            return false;
        }

        // хэш-код
        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        public object Clone()
        {
            return new Fraction(Numerator, Denominator);
        }
    }

    public class FractionManager
    {
        private List<Fraction> _fractions = new List<Fraction>();

        // добавление дроби
        public void AddFraction()
        {
            try
            {
                Console.Write("Введите числитель дроби: ");
                int numerator = int.Parse(Console.ReadLine());

                Console.Write("Введите знаменатель дроби: ");
                int denominator = int.Parse(Console.ReadLine());

                Fraction newFraction = new Fraction(numerator, denominator);
                _fractions.Add(newFraction);
                Console.WriteLine($"Дробь {newFraction} добавлена");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введено некорректное число.");
            }


        }

        // список дробей
        public void ListFractions()
        {
            if (_fractions.Count == 0)
            {
                Console.WriteLine("Список дробей пуст.");
                return;
            }

            Console.WriteLine("Список дробей:");
            for (int i = 0; i < _fractions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_fractions[i]}");
            }
        }

        // операции 
        public void PerformOperations()
        {
            if (_fractions.Count < 3)
            {
                Console.WriteLine("Добавьте минимум 3 дроби для операций");
                return;
            }

            ListFractions();
            Fraction f1 = _fractions[0];
            Fraction f2 = _fractions[1];
            Fraction f3 = _fractions[2];

            Fraction sumResult = f1.Sum(f2);
            Console.WriteLine($"{f1} + {f2} = {sumResult}");

            Fraction minusResult = f1.Minus(f2);
            Console.WriteLine($"{f1} - {f2} = {minusResult}");

            Fraction multResult = f1.Mult(f2);
            Console.WriteLine($"{f1} * {f2} = {multResult}");

            Fraction divResult = f1.Div(f2);
            if (divResult != null)
            {
                Console.WriteLine($"{f1} / {f2} = {divResult}");
            }

            Fraction sumResult1 = f1.Sum(f2);
            Fraction divResult1 = sumResult.Div(f3);
            if (divResult1 == null)
            {
                Console.WriteLine($"Ошибка: невозможно выполнить деление ({sumResult1}) на {f3}.");
                return;
            }
            else {
                Fraction complexResult = divResult1.Minus(5);

                Console.WriteLine($"Сложная операция ({f1} + {f2}) / {f3} - 5 = {complexResult}");
            }
            

        }
    }

    public interface Meow
    {
        void Meow();
        int MeowCount { get; }
        string Name { get; }
    }

    public class Cat : Meow
    {
        public string Name { get; private set; }
        public int MeowCount { get; private set; } = 0;

        public Cat(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя кота не может быть пустым");

            if (name.Length > 20)
                throw new ArgumentException("Имя кота не должно превышать 20 символов");

            Name = name;
        }

        // одно мяуканье
        public void Meow()
        {
            Console.WriteLine($"{Name}: мяу!");
            MeowCount++;
        }

        public void Meow(int count)
        {
            string meowString = string.Empty;
            if (count < 0)
                throw new ArgumentException("Количество мяуканий не может быть отрицательным");

            for (int i = 0; i < count; i++)
            {
                meowString += "мяу";
                if (i < count - 1)
                {
                    meowString += "-";
                }
            }

            Console.WriteLine($"{Name}: {meowString}!");
            MeowCount += count;
        }

        public override string ToString() => $"кот: {Name}";
    }

    public class MeowCaller
    {
        public static void CallMeows(Meow[] meowers)
        {
            foreach (var meower in meowers)
            {
                meower.Meow();
            }
        }
    }

    public class CatManager
    {
        private List<Cat> _cats = new List<Cat>();

        // добавление кота
        public void AddCat()
        {
            while (true)
            {
                Console.Write("Введите имя кота (или 'отмена' для возврата): ");
                string name = Console.ReadLine().Trim();

                if (name.ToLower() == "отмена")
                    return;

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Имя не может быть пустым!");
                    continue;
                }

                if (name.Length > 20)
                {
                    Console.WriteLine("Имя не должно превышать 20 символов");
                    continue;
                }

                if (_cats.Any(c => c.Name.ToLower() == name.ToLower()))
                {
                    Console.WriteLine("Кот с таким именем уже существует");
                    continue;
                }

                Cat newCat = new Cat(name);
                _cats.Add(newCat);
                Console.WriteLine($"Кот {name} успешно добавлен");
                return;
            }
        }

        // список
        public void ListCats()
        {
            if (_cats.Count == 0)
            {
                Console.WriteLine("Список котов пуст.");
                return;
            }

            Console.WriteLine("Список котов:");
            for (int i = 0; i < _cats.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_cats[i]}");
            }
        }

        // выбранный кот мяукает
        public void MeowCat()
        {
            if (_cats.Count == 0)
            {
                Console.WriteLine("Сначала добавьте кота");
                return;
            }

            ListCats();
            while (true)
            {
                Console.Write("Выберите номер кота (или 0 для отмены): ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Введите корректный номер");
                    continue;
                }

                if (choice == 0) return;

                if (choice < 1 || choice > _cats.Count)
                {
                    Console.WriteLine("Некорректный номер кота");
                    continue;
                }

                Cat selectedCat = _cats[choice - 1];

                Console.Write("Введите количество мяуканий (или 0 для одного мяу): ");
                if (!int.TryParse(Console.ReadLine(), out int meowCount))
                {
                    Console.WriteLine("Введите корректное число");
                    continue;
                }
                if (meowCount == 0)
                    selectedCat.Meow();
                else
                    selectedCat.Meow(meowCount);

                Console.WriteLine($"Всего {selectedCat.Name} мяукал {selectedCat.MeowCount} раз");
                return;
            }
        }

        // все коты мяукают
        public void MeowAllCats()
        {
            if (_cats.Count == 0)
            {
                Console.WriteLine("Сначала добавьте котов");
                return;
            }

            Meow[] meowers = _cats.Cast<Meow>().ToArray();
            MeowCaller.CallMeows(meowers);

            foreach (var cat in _cats)
            {
                Console.WriteLine($"{cat.Name} мяукал {cat.MeowCount} раз");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("\n   Главное меню");
                Console.WriteLine("1. Управление котами");
                Console.WriteLine("2. Работа с дробями");
                Console.WriteLine("0. Выход из программы");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CatMenu();
                        break;
                    case "2":
                        FractionMenu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void CatMenu()
        {
            CatManager manager = new CatManager();

            while (true)
            {
                Console.WriteLine("\n  Меню управления котами");
                Console.WriteLine("1. Добавить кота");
                Console.WriteLine("2. Список котов");
                Console.WriteLine("3. Заставить кота мяукать");
                Console.WriteLine("4. Заставить всех котов мяукать");
                Console.WriteLine("0. Возврат в главное меню");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.AddCat();
                        break;
                    case "2":
                        manager.ListCats();
                        break;
                    case "3":
                        manager.MeowCat();
                        break;
                    case "4":
                        manager.MeowAllCats();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void FractionMenu()
        {
            FractionManager manager = new FractionManager();

            while (true)
            {
                Console.WriteLine("\n   Меню работы с дробями");
                Console.WriteLine("1. Добавить дробь");
                Console.WriteLine("2. Список дробей");
                Console.WriteLine("3. Выполнить операции с дробями");
                Console.WriteLine("0. Возврат в главное меню");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.AddFraction();
                        break;
                    case "2":
                        manager.ListFractions();
                        break;
                    case "3":
                        manager.PerformOperations();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
    }
}
