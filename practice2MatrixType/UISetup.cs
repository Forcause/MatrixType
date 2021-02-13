using System;
using System.Threading;
using System.Collections.Generic;
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
                Console.WriteLine("Как Вы хотите задать матрицу?\n1. Строкой из значений матрицы\n2. Задать кол-во строк и столбцов, затем заполнить матрицу" +
                    "\n3. Задать нулевую матрицу\n4. Задать единичную матрицу\n0. Выход в главное меню");
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        CreateMatrixByNumbers();
                        break;
                    case '2':
                        CreateMatrixByValues();
                        break;
                    case '4':
                        CreateUnityMatrix();
                        break;
                    case '0': return;
                }
            }
        }

        static void CreateMatrixByValues()
        {
            string matrixName = "";
            int rows = 0, cols = 0;
            while (true)
            {
                Console.WriteLine("\nВведите название матрицы: ");
                matrixName = CreateAndCheckNames();
                Console.WriteLine("Введите количество строк:");
                rows = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите количество столбцов:");
                cols = int.Parse(Console.ReadLine());
                dataBase.Add(matrixName, new Matrix(rows, cols));
                Console.WriteLine("Матрица {0} успешно добавлена", matrixName);
                Thread.Sleep(1000);
                return;
            }
        }


        static void CreateMatrixByNumbers()
        {
            string matrixName = "";
            string initData = "";
            while (true)
            {
                Console.WriteLine("\nВведите название матрицы: ");
                try
                {
                    matrixName = CreateAndCheckNames();
                    Console.WriteLine("\nВведите значения матрицы в формате: строки - цифры, написанные через пробел; конец строки обозначается запятой\n" +
                "Например 1 1 1, 2 2 2, 3 3 3");
                    initData = Console.ReadLine();
                    dataBase.Add(matrixName, Matrix.Parse(initData));
                    Console.WriteLine("Матрица {0} успешно добавлена", matrixName);
                    Thread.Sleep(1000);
                    return;
                }
                catch
                {
                   //обработчик добавь
                }
            }
        }
        static void CreateUnityMatrix()
        {
            string matrixName = "";
            int matrixSize = 0;
            while (true)
            {
                Console.WriteLine("\nВведите название матрицы: ");
                try
                {
                    matrixName = CreateAndCheckNames();
                    Console.WriteLine("\nВведите размер матрицы"); //может быть размер меньше 0
                    matrixSize = int.Parse(Console.ReadLine());
                    dataBase.Add(matrixName, Matrix.GetUnity(matrixSize));
                    Console.WriteLine("Матрица {0} успешно добавлена", matrixName);
                    Thread.Sleep(1000);
                    return;
                }
                catch
                {
                    //обработчик добавь
                }
            }
        }

        public static void Operations()
        {
            Console.Clear();
            Console.WriteLine("\nВыберите матрицу, над которой хотите произвести операцию\nСписок доступных матриц");
            if (ShowAllMatrixes() == false) return;
            Matrix firOp = new Matrix(1, 1), secOp = new Matrix(1, 1);
            while (true)
            {
                Console.WriteLine("\nВыберите желаемую операцию:\n1. '+'\n2. '-'\n3. '*' на матрицу\n4. " +
                "'*' на число\n0. Выход в меню");
                while (true)
                {
                    char op = Console.ReadKey(true).KeyChar;
                    switch (op)
                    {
                        case '1':
                            OperationsTwoMatrixes(firOp, secOp, '+');
                            return;
                        case '2':
                            OperationsTwoMatrixes(firOp, secOp, '-');
                            return;
                        case '3':
                            OperationsTwoMatrixes(firOp, secOp, '*');
                            return;
                        case '0': return;
                    }
                }
            }
        }
        static void OperationsTwoMatrixes(Matrix fp, Matrix sp, char operation)
        {
            Console.WriteLine("----------------------\nВыберите первую матрицу в операции\n----------------------\n");
            fp = ChooseMatrix();
            Console.Clear();
            Console.WriteLine("----------------------\nВыберите вторую матрицу в операции\n----------------------\n");
            sp = ChooseMatrix();
            Console.Clear();
            Console.WriteLine("\nВведите название результирующей матрицы: ");
            try
            {
                if (operation == '+') dataBase.Add(Console.ReadLine(), fp + sp);
                else if (operation == '-') dataBase.Add(Console.ReadLine(), fp - sp);
                else if (operation == '*') dataBase.Add(Console.ReadLine(), fp * sp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(1000);
            }
        }
        static Matrix ChooseMatrix()
        {
            ShowAllMatrixes();
            while (true)
            {
                string choosenMatrixName = Console.ReadLine();
                if (dataBase.ContainsKey(choosenMatrixName)) return dataBase[choosenMatrixName];
                else Console.WriteLine("Матрицы с заданным именем нет. Повторите ввод");
            }
        }

        public static bool ShowAllMatrixes()
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
        static string CreateAndCheckNames()
        {
            while (true)
            {
                string matrixName = Console.ReadLine();
                if (dataBase.ContainsKey(matrixName)) Console.WriteLine("Матрица с таким названием уже есть");
                else return matrixName;
            }
        }
    }
}