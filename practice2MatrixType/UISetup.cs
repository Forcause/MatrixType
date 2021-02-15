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
                    case '3':
                        CreateUnityOrEmptyMatrix('e');
                        break;
                    case '4':
                        CreateUnityOrEmptyMatrix('u');
                        break;
                    case '0': return;
                }
            }
        }

        static void CreateMatrixByValues()
        {
            Console.WriteLine("\nВведите название матрицы: ");
            string matrixName = CreateAndCheckName();
            while (true)
            {
                    try
                    {
                        Console.WriteLine("Введите количество строк:");
                        int rows = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите количество столбцов:");
                        int cols = int.Parse(Console.ReadLine());
                        dataBase.Add(matrixName, new Matrix(rows, cols));
                        FillMatrix(matrixName);
                        Console.WriteLine("Матрица {0} успешно добавлена", matrixName);
                        ShowResult(matrixName);
                        return;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Количество строк и столбцов - числовые значения >= 1");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
            }
        }
        static void FillMatrix(string matrixName)
        {
            Matrix fillMatrix = dataBase[matrixName];
            Console.WriteLine("Вводите значения, которые хотите записать в позиции, выделемые красным цветом:\n");
            try
            {
                for (int i = 0; i < fillMatrix.Rows; i++)
                {
                    for (int j = 0; j < fillMatrix.Columns; j++)
                    {
                        PaintedOutput(i, j, fillMatrix);
                        fillMatrix[i, j] = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine();
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Десятичная часть дроби отделяется запятой");
            }
        }
        static void PaintedOutput(int row, int col, Matrix painted)
        {
            for (int i = 0; i < painted.Rows; i++)
            {
                for (int j = 0; j < painted.Columns; j++)
                {
                    if (i == row && j == col) Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write((painted[i, j] + " ").PadRight(6));
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void CreateMatrixByNumbers()
        {
            Console.WriteLine("\nВведите название матрицы: ");
            string matrixName = CreateAndCheckName();
            while (true)
            {
                try
                {

                    Console.WriteLine("\nВведите значения матрицы в формате: строки - цифры, написанные через пробел; конец строки обозначается запятой\n" +
                "Например 1 1 1, 2 2 2, 3 3 3");
                    string initData = Console.ReadLine();
                    dataBase.Add(matrixName, Matrix.Parse(initData));
                    Console.WriteLine("Матрица {0} успешно добавлена", matrixName);
                    ShowResult(matrixName);
                    return;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        static void CreateUnityOrEmptyMatrix(char choice)
        {
            Console.WriteLine("\nВведите название матрицы: ");
            string matrixName = CreateAndCheckName();
            while (true)
            {
                try
                {
                    Console.WriteLine("\nВведите размер матрицы"); //может быть размер меньше 0
                    int matrixSize = int.Parse(Console.ReadLine());
                    if (choice == 'u') dataBase.Add(matrixName, Matrix.GetUnity(matrixSize));
                    else if (choice == 'e') dataBase.Add(matrixName, Matrix.GetEmpty(matrixSize));
                    Console.WriteLine("Матрица {0} успешно добавлена", matrixName);
                    ShowResult(matrixName);
                    return;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Размер матрицы должен быть >= 1");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public static void Operations()
        {
            Console.Clear();
            Console.WriteLine("\nСписок доступных матриц:\n------------------------------------------");
            if (ShowAllMatrixes() == false) return;
            while (true)
            {
                Console.WriteLine("------------------------------------------\nВыберите желаемую операцию:\n1. '+'\n2. '-'\n3. '*' на матрицу\n4. " +
                "'*' на число\n0. Выход в меню");
                while (true)
                {
                    char op = Console.ReadKey(true).KeyChar;
                    switch (op)
                    {
                        case '1':
                            PlusMinusMultiply('+');
                            return;
                        case '2':
                            PlusMinusMultiply('-');
                            return;
                        case '3':
                            PlusMinusMultiply('*');
                            return;
                        case '4':
                            MultiplyMatrixNumber();
                            return;
                        case '0': return;
                    }
                }
            }
        }
        static void PlusMinusMultiply(char operation)
        {
            Console.WriteLine("Введите название результирующей матрицы: ");
            string matrixName = CreateAndCheckName();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("----------------------\nВыберите первую матрицу в операции\n----------------------\n");
                Matrix fp = ChooseMatrix();
                Console.Clear();
                Console.WriteLine("----------------------\nВыберите вторую матрицу в операции\n----------------------\n");
                Matrix sp = ChooseMatrix();
                Console.Clear();
                try
                {
                    if (operation == '+') dataBase.Add(matrixName, fp + sp);
                    else if (operation == '-') dataBase.Add(matrixName, fp - sp);
                    else if (operation == '*') dataBase.Add(matrixName, fp * sp);
                    Console.WriteLine("Результат {0}:", matrixName);
                    ShowResult(matrixName);
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(3000);
                    return;
                }
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
        static void MultiplyMatrixNumber()
        {
            Console.Clear();
            Console.WriteLine("Введите название результирующей матрицы");
            string matrixName = CreateAndCheckName();
            Console.WriteLine("\nВыберите матрицу");
            Matrix fp = ChooseMatrix();
            Console.Clear();
            Console.WriteLine("Введите число, на которое хотите умножить матрицу");
            double number = Convert.ToDouble(Console.ReadLine());
            dataBase.Add(matrixName, fp * number);
            Console.WriteLine("Результат: {0}", matrixName);
            ShowResult(matrixName);
            Console.WriteLine("Нажмите любую кнопку, чтобы вернуться в главное меню");
            Console.ReadKey(true);
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
        public static void ShowMoreInfo()
        {
            Console.Clear();
            Console.WriteLine("Выберите матрицу, по которой хотите получить дополнительную информацию:");
            Matrix choice = ChooseMatrix();
            Console.WriteLine("\nИнформация по матрице:\nКвадратная: {0}\nНулевая: {1}\nЕдиничная: {2}\nДиагональная: {3}\nСимметричная: {4}",
                choice.IsSquared ? '+' : '-', choice.IsEmpty ? '+' : '-', choice.IsUnity ? '+' : '-', choice.IsDiagonal ? '+' : '-', choice.IsSymmetric ? '+' : '-');
            Console.WriteLine("Нажмите любую кнопку, чтобы вернуться в главное меню");
            Console.ReadKey(true);
        }
        static void ShowResult(string matrixName)
        {
            Console.WriteLine("\n{0}", dataBase[matrixName].ToString());
            Thread.Sleep(3000);
        }
        static string CreateAndCheckName()
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