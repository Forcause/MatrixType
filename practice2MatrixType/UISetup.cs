using System;
using System.Threading;
using System.Collections.Generic;
using static practice2MatrixType.Program;

namespace practice2MatrixType
{
    class UISetup
    {
        public static void AddMatrixMenu(Dictionary<string, Matrix> dataBase)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Как Вы хотите задать матрицу?\n1. Строкой из значений матрицы\n2. Задать кол-во строк и столбцов, затем заполнить матрицу\n" +
                    "0. Выход в главное меню");
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        CreateMatrixByNumbers(dataBase);
                        break;
                    case '2':
                        CreateMatrixByValues(dataBase);
                        break;
                    case '0': return;
                }
            }
        }

        static void CreateMatrixByValues(Dictionary<string, Matrix> dataBase)
        {
            string matrixName = "";
            int rows = 0, cols = 0;
            while (true)
            {
                Console.WriteLine("\nВведите название матрицы: ");
                try
                {
                    matrixName = CreateAndCheckNames(dataBase);
                    Console.WriteLine("Введите количество строк:");
                    rows = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите количество столбцов:");
                    cols = int.Parse(Console.ReadLine());
                    dataBase.Add(matrixName, new Matrix(rows, cols));
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(1000);
                }
            }
        }

        static void CreateMatrixByNumbers(Dictionary<string, Matrix> dataBase)
        {
            string matrixName = "";
            string initData = "";
            while (true)
            {
                Console.WriteLine("\nВведите название матрицы: ");
                try
                {
                    matrixName = CreateAndCheckNames(dataBase);
                    Console.WriteLine("\nВведите значения матрицы в формате: строки - цифры, написанные через пробел; конец строки обозначается запятой\n" +
                "Например 1 1 1, 2 2 2, 3 3 3");
                    initData = Console.ReadLine();
                    dataBase.Add(matrixName, Matrix.Parse(initData));
                    Console.WriteLine("Матрица {0} успешно добавлена", matrixName);
                    Thread.Sleep(1000);
                    return;
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }

        public static void Operations(Dictionary<string, Matrix> dataBase)
        {
            Console.Clear();
            Console.WriteLine("\nВыберите матрицу, над которой хотите произвести операцию\nСписок доступных матриц");
            if (ShowAllMatrixes(dataBase) == false) return;
            while (true)
            {
                Console.WriteLine("\nВыберите желаемую операцию:\n1. '+'\n2. '-'\n3. '*' на матрицу\n4. " +
                "'*' на число\n0. Выход в меню");
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        Console.WriteLine("----------------------\nВыберите первую матрицу в операции\n----------------------\n");
                        Matrix firOp = ChooseMatrix(dataBase);
                        Console.WriteLine("----------------------\nВыберите вторую матрицу в операции\n----------------------\n");
                        Matrix secOp = ChooseMatrix(dataBase); //можно вынести в один блок для операций с матрицами и отдельно умножение на число
                        Console.WriteLine("\nВведите название результирующей матрицы: ");
                        try
                        {
                            dataBase.Add(Console.ReadLine(), firOp + secOp);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Thread.Sleep(1000);
                        }
                        return;
                    case '0': return;
                }
            }
        }
        static Matrix ChooseMatrix(Dictionary<string, Matrix> dataBase)
        {
            ShowAllMatrixes(dataBase);
            while (true)
            {
                string choosenMatrixName = Console.ReadLine();
                if (dataBase.ContainsKey(choosenMatrixName)) return dataBase[choosenMatrixName];
                else Console.WriteLine("Матрицы с заданным именем нет. Повторите ввод");
            }
        }

        public static bool ShowAllMatrixes(Dictionary<string, Matrix> dataBase)
        {
            if (dataBase.Count == 0)
            {
                Console.WriteLine("Нет сохраненных матриц");
                return false;
            }
            else
            {
                foreach (var t in dataBase)
                {
                    Console.WriteLine("Матрица {0}\n", t.Key);
                    Console.WriteLine(t.Value.ToString());
                }
            }
            return true;
        }
        static string CreateAndCheckNames(Dictionary<string, Matrix> dataBase)
        {
            string matrixName = Console.ReadLine();
            if (dataBase.ContainsKey(matrixName)) throw new Exception("Матрица с таким названием уже есть");
            else return matrixName;
        }
    }
}