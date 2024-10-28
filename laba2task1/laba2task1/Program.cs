using System;

public class StringClass
{
    // Поле для хранения строки
    protected string _text;

    // Конструктор с параметром
    public StringClass(string text)
    {
        _text = text;
    }

    // Конструктор копирования
    public StringClass(StringClass other)
    {
        _text = other._text;
    }

    // Метод для получения строки из первого и последнего символов
    public string GetFirstAndLastCharacters()
    {
        if (string.IsNullOrEmpty(_text))
            return string.Empty;

        if (_text.Length == 1)
            return _text;

        return $"{_text[0]}{_text[^1]}";
    }

    public override string ToString()
    {
        return $"StringClass: {_text}";
    }
}

// Дочерний класс расширяющий функциональность StringClass
public class ExtendedStringClass : StringClass
{
    // Дополнительные поля
    public int WordCount { get; private set; }

    // Конструктор принимающий строку и инициализирующий поля
    public ExtendedStringClass(string text) : base(text)
    {
        WordCount = CountWords(text);
    }

    // Конструктор копирования
    public ExtendedStringClass(ExtendedStringClass other) : base(other)
    {
        WordCount = other.WordCount;
    }

    // Метод для подсчета количества слов в строке
    private int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        return text.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    // Метод для проверки содержит ли строка определенное слово
    public bool ContainsWord(string word)
    {
        if (string.IsNullOrEmpty(_text) || string.IsNullOrEmpty(word))
            return false;

        return _text.Contains(word, StringComparison.OrdinalIgnoreCase);
    }

    // Метод для вывода строки в верхнем регистре
    public string ToUpperCase()
    {
        return _text.ToUpper();
    }

    public override string ToString()
    {
        return $"ExtendedStringClass: {_text}, Word Count: {WordCount}";
    }
}

// Тестирование классов
public class Program
{
    public static void Main()
    {
        // Тест базового класса
        StringClass baseObj = new StringClass("Hello World!");
        Console.WriteLine("Базовый объект (baseObj): " + baseObj.ToString());
        Console.WriteLine("Первый и последний символы baseObj: " + baseObj.GetFirstAndLastCharacters());

        // Тест конструктора копирования базового класса
        StringClass copyBaseObj = new StringClass(baseObj);
        Console.WriteLine("Скопированный объект базового класса (copyBaseObj): " + copyBaseObj.ToString());

        // Тест дочернего класса
        ExtendedStringClass extendedObj = new ExtendedStringClass("Hello C# World!");
        Console.WriteLine("Дочерний объект (extendedObj): " + extendedObj.ToString());
        Console.WriteLine("Количество слов в extendedObj: " + extendedObj.WordCount);
        Console.WriteLine("Проверка наличия слова 'C#' в extendedObj: " + extendedObj.ContainsWord("C#"));
        Console.WriteLine("extendedObj в верхнем регистре: " + extendedObj.ToUpperCase());

        // Тест конструктора копирования дочернего класса
        ExtendedStringClass copyExtendedObj = new ExtendedStringClass(extendedObj);
        Console.WriteLine("Скопированный дочерний объект (copyExtendedObj): " + copyExtendedObj.ToString());
    }
}
