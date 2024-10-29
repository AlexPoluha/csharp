using System;
using System.Linq;

public class MatrixOperations
{
    private int[,] matrix;

    // Конструктор для первого массива n x m (ввод с клавиатуры, заполнение справа налево)
    public MatrixOperations(int n, int m, bool isKeyboardInput)
    {
        matrix = new int[n, m];
        if (isKeyboardInput)
        {
            Console.WriteLine("Введите элементы массива n x m (по строкам справа налево):");
            for (int i = 0; i < n; i++)
            {
                for (int j = m - 1; j >= 0; j--)
                {
                    Console.Write($"Элемент [{i},{j}]: ");
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
            }
        }
    }

    // Конструктор для второго массива n x n 
    public MatrixOperations(int n, bool isDiagonalPattern)
    {
        matrix = new int[n, n];
        Random rand = new Random();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (j >= i)
                    matrix[i, j] = rand.Next(100, 10001); // На и выше главной диагонали
                else
                    matrix[i, j] = rand.Next(-17, 37); // Ниже главной диагонали
            }
        }
    }

    // Конструктор для третьего массива n x n
    public MatrixOperations(int n)
    {
        matrix = new int[n, n];
        Random rand = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = (i + j) % 2 == 0 ? rand.Next(1, 100) : -1; 
            }
        }
    }

    // Метод для поиска максимального неповторяющегося элемента
    public int FindMaxUniqueElement()
    {
        Dictionary<int, int> elementCounts = new Dictionary<int, int>();

        foreach (int element in matrix)
        {
            if (elementCounts.ContainsKey(element))
                elementCounts[element]++;
            else
                elementCounts[element] = 1;
        }

        int maxUnique = int.MinValue;
        foreach (var kvp in elementCounts)
        {
            if (kvp.Value == 1 && kvp.Key > maxUnique)
                maxUnique = kvp.Key;
        }

        return maxUnique;
    }


    // Перегрузка оператора для умножения двух матриц
    public static MatrixOperations operator *(MatrixOperations a, MatrixOperations b)
    {
        int n = a.matrix.GetLength(0);
        int[,] result = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < n; k++)
                {
                    result[i, j] += a.matrix[i, k] * b.matrix[k, j];
                }
            }
        }
        return new MatrixOperations(result);
    }

    // Перегрузка оператора для вычитания матриц
    public static MatrixOperations operator -(MatrixOperations a, MatrixOperations b)
    {
        int n = a.matrix.GetLength(0);
        int[,] result = new int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                result[i, j] = a.matrix[i, j] - b.matrix[i, j];
        return new MatrixOperations(result);
    }

    // Перегрузка оператора для умножения матрицы на число
    public static MatrixOperations operator *(int scalar, MatrixOperations a)
    {
        int n = a.matrix.GetLength(0);
        int[,] result = new int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                result[i, j] = scalar * a.matrix[i, j];
        return new MatrixOperations(result);
    }

    // Метод вычисления выражения 2 * A - B * C
    public static MatrixOperations CalculateExpression(MatrixOperations A, MatrixOperations B, MatrixOperations C)
    {
        return (2 * A) - (B * C);
    }

    public override string ToString()
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        string result = "";
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result += $"{matrix[i, j],6} ";
            }
            result += "\n";
        }
        return result;
    }

    private MatrixOperations(int[,] data)
    {
        matrix = data;
    }
}

class Program
{
    static void Main()
    {
        // Ввод размерности матриц
        Console.Write("Введите размер n для массивов n x m и n x n: ");
        int n = int.Parse(Console.ReadLine());

        // Задание 1: Создание массивов с использованием разных конструкторов
        MatrixOperations array1 = new MatrixOperations(n, n, true); // Первый массив
        MatrixOperations array2 = new MatrixOperations(n, true);    // Второй массив
        MatrixOperations array3 = new MatrixOperations(n);          // Третий массив

        // Вывод массивов
        Console.WriteLine("Первый массив:");
        Console.WriteLine(array1);
        Console.WriteLine("Второй массив:");
        Console.WriteLine(array2);
        Console.WriteLine("Третий массив:");
        Console.WriteLine(array3);

        // Задание 2: Поиск максимального неповторяющегося элемента
        int maxUnique = array1.FindMaxUniqueElement();
        Console.WriteLine($"Максимальный неповторяющийся элемент в первом массиве: {maxUnique}");

        // Задание 3: Вычисление выражения 2 * A - B * C
        MatrixOperations result = MatrixOperations.CalculateExpression(array1, array2, array3);
        Console.WriteLine("Результат выражения 2 * A - B * C:");
        Console.WriteLine(result);
    }
}
