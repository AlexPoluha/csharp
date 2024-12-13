using System;

namespace FractionLab
{
    // Интерфейс для получения и установки значений дроби
    public interface IFractionManagement
    {
        double GetDecimalValue();
        void SetNumerator(int numerator);
        void SetDenominator(int denominator);
    }

    // Кэширующая версия дроби
    public class CachedFraction : Fraction, IFractionManagement
    {
        private double? cachedDecimalValue = null;

        public CachedFraction(int numerator, int denominator) : base(numerator, denominator) { }

        public double GetDecimalValue()
        {
            if (!cachedDecimalValue.HasValue)
            {
                cachedDecimalValue = (double)Numerator / Denominator;
            }
            return cachedDecimalValue.Value;
        }

        public void SetNumerator(int numerator)
        {
            Numerator = numerator;
            cachedDecimalValue = null; // Сбрасываем кэш
        }

        public void SetDenominator(int denominator)
        {
            Denominator = denominator;
            cachedDecimalValue = null; // Сбрасываем кэш
        }
    }

    public class Fraction : ICloneable
    {
        public int Numerator { get; protected set; }
        public int Denominator { get; protected set; }

        // Конструктор с проверкой входных данных
        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Знаменатель не может быть равен нулю");

            // Корректировка знака для знаменателя
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = Math.Abs(denominator);
            }

            int gcd = CalculateGCD(Math.Abs(numerator), Math.Abs(denominator));
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }

        // Вспомогательный метод для нахождения НОД
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

        // Реализация ToString для вывода дроби
        public override string ToString() => $"{Numerator}/{Denominator}";

        // Операции сложения
        public Fraction Sum(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator + other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Sum(int number)
        {
            return Sum(new Fraction(number, 1));
        }

        // Операции вычитания
        public Fraction Minus(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator - other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Minus(int number)
        {
            return Minus(new Fraction(number, 1));
        }

        // Операции умножения
        public Fraction Mult(Fraction other)
        {
            int newNumerator = Numerator * other.Numerator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Mult(int number)
        {
            return Mult(new Fraction(number, 1));
        }

        // Операции деления
        public Fraction Div(Fraction other)
        {
            if (other.Numerator == 0)
                throw new DivideByZeroException("Нельзя делить на дробь с нулевым числителем");

            int newNumerator = Numerator * other.Denominator;
            int newDenominator = Denominator * other.Numerator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Div(int number)
        {
            return Div(new Fraction(number, 1));
        }

        // Переопределение сравнения
        public override bool Equals(object obj)
        {
            if (obj is Fraction other)
            {
                return Numerator == other.Numerator && Denominator == other.Denominator;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        // Реализация клонирования
        public object Clone()
        {
            return new Fraction(Numerator, Denominator);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Демонстрация работы с дробями
            Fraction f1 = new Fraction(1, 3);
            Fraction f2 = new Fraction(2, 3);
            Fraction f3 = new Fraction(4, 5);

            Console.WriteLine($"{f1} + {f2} = {f1.Sum(f2)}");
            Console.WriteLine($"{f1} - {f2} = {f1.Minus(f2)}");
            Console.WriteLine($"{f1} * {f2} = {f1.Mult(f2)}");
            Console.WriteLine($"{f1} / {f2} = {f1.Div(f2)}");

            // Демонстрация сложной операции
            Fraction result = f1.Sum(f2).Div(f3).Minus(5);
            Console.WriteLine($"f1.sum(f2).div(f3).minus(5) = {result}");

            // Демонстрация кэширования
            CachedFraction cached = new CachedFraction(1, 3);
            Console.WriteLine($"Decimal value: {cached.GetDecimalValue()}");

            // Проверка клонирования
            Fraction clonedFraction = (Fraction)f1.Clone();
            Console.WriteLine($"Original: {f1}, Cloned: {clonedFraction}");
            Console.WriteLine($"Fractions are equal: {f1.Equals(clonedFraction)}");
        }
    }
}