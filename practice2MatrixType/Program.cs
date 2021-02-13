using System;
using System.Collections.Generic;
using static practice2MatrixType.Matrix;
using static practice2MatrixType.UISetup;

namespace practice2MatrixType
{
    class Program
    {
        public static Dictionary<string, Matrix> dataBase = new Dictionary<string, Matrix>();
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Работа с матрицами\n---------------------------------\n\t1 - Ввод матрицы\n\t2 - Операции\n\t3 - Вывод результатов\n\t0 - Выход");
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': AddMatrixMenu(); break;
                    case '2': Operations(); break;
                    case '3':
                        ShowAllMatrixes();
                        Console.WriteLine("Нажмите любую кнопку, чтобы перейти в главное меню");
                        Console.ReadKey(true);
                        break;
                    case '0': Environment.Exit(0); break;
                    default: break;
                }
            }
        }
    }
}