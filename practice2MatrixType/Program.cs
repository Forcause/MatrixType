using System;
using System.Drawing;
using static practice2MatrixType.Matrix;

namespace practice2MatrixType
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Работа с матрицами\n---------------------------------\n\t1 - Ввод матрицы\n\t2 - Операции\n\t3 - Вывод результатов\n\t0 - Выход");
                Matrix m1 = new Matrix(10, 10);
                double[,] test = { { 1, 0, 0 }, { 0, 0, 0 }, { 0, 0, 1 } };
                Matrix m2 = new Matrix(test);
                //m1.Fill(test);
                bool temp = m2.IsUnity;
                Console.WriteLine(temp);
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '0': Environment.Exit(0); break;
                    default: break;
                }
            }
        }
    }
}