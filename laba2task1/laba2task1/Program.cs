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
    public int WordCount { get; private set; }

    public ExtendedStringClass(string text) : base(text)
    {
        WordCount = CountWords(text);
    }

    public ExtendedStringClass(ExtendedStringClass other) : base(other)
    {
        WordCount = other.WordCount;
    }

    // Метод для подсчета количества слов в строке
    private int CountWords(string text)
    {

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Текст пустой или состоит только из пробелов.");
            return 0;
        }

        int wordCount = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;

        Console.WriteLine($"Количество слов: {wordCount}");
        return wordCount;
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

public class Program
{
    public static void Main()
    {
        string baseText = InputString("Введите текст для базового объекта: ");
        StringClass baseObj = new StringClass(baseText);
        
        Console.WriteLine("Базовый объект (baseObj): " + baseObj.ToString());
        Console.WriteLine("Первый и последний символы baseObj: " + baseObj.GetFirstAndLastCharacters());

        StringClass copyBaseObj = new StringClass(baseObj);
        Console.WriteLine("Скопированный объект базового класса (copyBaseObj): " + copyBaseObj.ToString());

        string extendedText = InputString("Введите текст для дочернего объекта: ");
        ExtendedStringClass extendedObj = new ExtendedStringClass(extendedText);
        
        Console.WriteLine("Дочерний объект (extendedObj): " + extendedObj.ToString());
        Console.WriteLine("Количество слов в extendedObj: " + extendedObj.WordCount);

        string wordToFind = InputString("Введите слово для проверки наличия в extendedObj: ");
        Console.WriteLine($"Проверка наличия слова '{wordToFind}' в extendedObj: " + extendedObj.ContainsWord(wordToFind));

        Console.WriteLine("extendedObj в верхнем регистре: " + extendedObj.ToUpperCase());

        ExtendedStringClass copyExtendedObj = new ExtendedStringClass(extendedObj);
        Console.WriteLine("Скопированный дочерний объект (copyExtendedObj): " + copyExtendedObj.ToString());
    }

    public static string InputString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}
