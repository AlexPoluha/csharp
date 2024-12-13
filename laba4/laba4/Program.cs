using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using static LabTasks;

public class LabTasks
{
    // задача 1: удаление элементов из списка
    public static List<int> RemoveElements()
    {
        Console.WriteLine("Введите элементы списка через пробел:");
        var input = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

        Console.WriteLine("Введите элемент для удаления:");
        int elementToRemove = int.Parse(Console.ReadLine());

        input.RemoveAll(x => x == elementToRemove);
        return input;
    }

    // задача 1: вывод списка в обратном порядке
    public static void PrintListReverse(List<int> list)
    {
        Console.WriteLine("Список в обратном порядке:");
        for (int i = list.Count - 1; i >= 0; i--)
        {
            Console.Write(list[i] + " ");
        }
        Console.WriteLine();
    }

    // задача 2: закупки компьютерной техники
    public class School
    {
        public string Name { get; set; }
        public List<string> PurchasedFrom { get; set; }
    }

    public static void AnalyzePurchases()
    {
        List<School> schools = new List<School>();

        Console.WriteLine("Введите количество школ:");
        int schoolCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < schoolCount; i++)
        {
            Console.WriteLine($"Введите название школы {i + 1}:");
            string schoolName = Console.ReadLine();

            Console.WriteLine($"Введите фирмы, в которых закупала школа {schoolName} (через запятую):");
            var purchasedFrom = Console.ReadLine().Split(',').Select(s => s.Trim()).ToList();

            schools.Add(new School { Name = schoolName, PurchasedFrom = purchasedFrom });
        }

        Console.WriteLine("\nАнализ закупок:");
        // 1. в каких фирмах закупка производилась каждым из заведений
        foreach (var school in schools)
        {
            Console.WriteLine($"{school.Name} закупал в фирмах: {string.Join(", ", school.PurchasedFrom)}");
        }

        // 2. в каких фирмах закупка производилась хотя бы одним заведением
        var allPurchasedFrom = schools.SelectMany(s => s.PurchasedFrom).Distinct();
        Console.WriteLine("Фирмы, где производилась закупка: " + string.Join(", ", allPurchasedFrom));

        // 3. в каких фирмах ни одно из заведений не закупало компьютеры
        Console.WriteLine("Введите список всех компаний (через запятую):");
        var allCompanies = new HashSet<string>(Console.ReadLine().Split(',').Select(s => s.Trim()));
        var purchasedCompanies = new HashSet<string>(allPurchasedFrom);
        var noPurchaseCompanies = allCompanies.Except(purchasedCompanies);
        Console.WriteLine("Фирмы без закупок: " + string.Join(", ", noPurchaseCompanies));
    }

    // задача 3: звонкие согласные буквы
    public static void PrintSonorConsonants()
    {
        string filePath = "words.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }
        string text = File.ReadAllText(filePath);

        string[] sonorConsonants = { "б", "в", "г", "д", "ж", "з", "л", "м", "н", "р" };
        var words = text.ToLower().Split(new char[] { ' ', '.', ',', '!', '?', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        var uniqueSonorConsonants = words
            .SelectMany(word => word)
            .Where(c => sonorConsonants.Contains(c.ToString())) 
            .Distinct() 
            .OrderBy(c => c) 
            .ToList();

        Console.WriteLine("Звонкие согласные, встречающиеся хотя бы в одном слове, в алфавитном порядке:");
        Console.WriteLine(string.Join(", ", uniqueSonorConsonants));
    }

    // задача 4: генерация уникальных логинов
    public static void GenerateUniqueLoginsAndSaveToXml()
    {
        //Console.InputEncoding = System.Text.Encoding.UTF8;
        //Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Ввод данных с клавиатуры
        int participantCount;
        while (true)
        {
            Console.WriteLine("Введите количество участников:");
            if (int.TryParse(Console.ReadLine(), out participantCount) && participantCount > 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Ошибка: введите корректное число участников.");
            }
        }

        var participants = new List<string>();
        for (int i = 0; i < participantCount; i++)
        {
            string fullName;
            while (true)
            {
                Console.WriteLine($"Введите ФИ участника {i + 1} (Фамилия Имя):");
                fullName = Console.ReadLine().Trim();
                if (fullName.Split().Length == 2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите корректное ФИО (Фамилия Имя).");
                }
            }
            participants.Add(fullName);
        }

        var loginCount = new Dictionary<string, int>();
        var students = new List<Student>();

        foreach (var participant in participants)
        {
            string lastName = participant.Split()[0]; 
            string login = loginCount.ContainsKey(lastName)
                ? lastName + (++loginCount[lastName]) 
                : lastName;

            loginCount[lastName] = loginCount.ContainsKey(lastName) ? loginCount[lastName] + 1 : 1;
            students.Add(new Student { Name = participant, Login = login });
        }
        //foreach (var student in students)
        //{
        //    Console.WriteLine($"{student.Name}: {student.Login}");
        //}
        SaveToXmlFile(students, "nicknames_manual_input.xml");

        Console.WriteLine("Сгенерированные логины сохранены в файл: nicknames_manual_input.xml");

        Console.WriteLine("\nСгенерированные логины:");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.Name}: {student.Login}");
        }
    }


    // задача 5: XML сериализация
    [Serializable]
    public class StudentsData
    {
        public List<Student> Students { get; set; }
    }

    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Login { get; set; }
    }

    //считывание
    public static List<string> ReadNamesFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не найден: " + filePath);
        }

        return File.ReadAllLines(filePath).ToList();
    }

    // генерация логинов
    public static List<Student> GenerateUniqueLogins(List<string> names)
    {
        var loginCount = new Dictionary<string, int>();
        var students = new List<Student>();

        foreach (var fullName in names)
        {
            string lastName = fullName.Split()[0];
            string login = loginCount.ContainsKey(lastName)
                ? lastName + (++loginCount[lastName])
                : lastName;

            loginCount[lastName] = loginCount.ContainsKey(lastName) ? loginCount[lastName] + 1 : 1;
            students.Add(new Student { Name = fullName, Login = login });
        }

        return students;
    }
    public static void CreateXmlFile()
    {
        string inputFilePath = "names.txt";
        string outputFilePath = "nicknames.xml";

        try
        {
            // Считывание 
            var names = ReadNamesFromFile(inputFilePath);

            // Генерация
            var students = GenerateUniqueLogins(names);

            // XML файл
            SaveToXmlFile(students, outputFilePath);

            Console.WriteLine("Результаты успешно сохранены в файл: " + outputFilePath);
            Console.WriteLine("\nСгенерированные логины:");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name}: {student.Login}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }
// сохранение в XML файл
public static void SaveToXmlFile(List<Student> students, string filePath)
    {
        var studentsData = new StudentsData { Students = students };
        var serializer = new XmlSerializer(typeof(StudentsData));

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, studentsData);
        }
    }

    public static void Main(string[] args)
    {

        while (true)
        {
            Console.WriteLine("\nВыберите задачу:");
            Console.WriteLine("1. Удаление элементов из списка");
            Console.WriteLine("2. Анализ закупок");
            Console.WriteLine("3. Звонкие согласные");
            Console.WriteLine("4. Генерация логинов");
            Console.WriteLine("5. XML сериализация");
            Console.WriteLine("0. Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var list = RemoveElements();
                    PrintListReverse(list);
                    break;
                case "2":
                    AnalyzePurchases();
                    break;
                case "3":
                    PrintSonorConsonants();
                    break;
                case "4":
                    GenerateUniqueLoginsAndSaveToXml();
                    break;
                case "5":
                    CreateXmlFile();
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