using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static practice2MatrixType.Program;

namespace practice2MatrixType
{
    class UISetup
    {
        public static void AddMatrixMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Как Вы хотите задать матрицу?\n1. Строкой из значений матрицы\n2. Задать кол-во строк и столбцов, затем заполнить матрицу\n" +
                    "0. Выход в главное меню");
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        CreateMatrix();
                        break;
                    case '2': break;
                    case '0': return;
                }
            }
        }

        static void CreateMatrix()
        {
            string matrixName = "";
            string initData = "";
            while (true)
            {
                try
                {
                    Console.WriteLine("\nВведите название матрицы: ");
                    matrixName = Console.ReadLine();
                    CheckNames(matrixName);
                    Console.WriteLine("\nВведите значения матрицы в формате: строки - цифры, написанные через пробел; конец строки обозначается запятой\n" +
                "Например 1 1 1, 2 2 2, 3 3 3");
                    initData = Console.ReadLine();
                    dataBase.Add(matrixName, Matrix.Parse(initData));
                    Console.WriteLine("Матрица {0} успешно добавлена", matrixName);
                    Thread.Sleep(1000);
                    return;
                }
                catch (FormatException) { Console.WriteLine("Неверно введена матрица"); }
                catch (ArgumentException) { Console.WriteLine("Матрица с таким названием уже есть"); }
            }
        }

        static void CheckNames(string name)
        {
            if (dataBase.ContainsKey(name)) throw new ArgumentException();
        }

        public static void ShowAllMatrixes()
        {
            Console.Clear();
            if (dataBase.Count == 0)
                Console.WriteLine("Нет сохраненных матриц");
            else
            {
                foreach (var t in dataBase)
                {
                    Console.WriteLine("Матрица {0}\n", t.Key);
                    Console.WriteLine(t.Value.ToString());
                }
            }
            Console.WriteLine("\nНажмите 0, чтобы выйти в главное меню");
            while (true)
            {
                if (Console.ReadKey(true).KeyChar == '0') return;
            }
        }
    }
}
