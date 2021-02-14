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
                        Console.Clear();
                        if (ShowAllMatrixes())
                        {
                            while (true)
                            {
                                Console.WriteLine("Нажмите M, чтобы получить дополнительную информацию по матрице\nНажмите 0, чтобы вернуться в главное меню");
                                if (char.ToLower(Console.ReadKey(true).KeyChar) == 'm') { ShowMoreInfo(); break; }
                                else if (char.ToLower(Console.ReadKey(true).KeyChar) == '0') break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Нажмите любую кнопку, чтобы выйти");
                            Console.ReadKey();
                        }
                        break;
                    case '0': Environment.Exit(0); break;
                    default: break;
                }
            }

        }
    }
}