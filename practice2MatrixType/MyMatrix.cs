﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace practice2MatrixType
{
    class Matrix
    {
        private int nRows;
        private int nCols;
        private double[,] data;

        public Matrix(int nRows, int nCols)
        {
            if (nRows < 1 || nCols < 1) throw new Exception("Строк и столбцов должно быть >= 1");
            else
            {
                this.nRows = nRows;
                this.nCols = nCols;
                this.data = new double[nRows, nCols];
            }
        }
        public Matrix(double[,] initData)
        {
            if (initData.Length != 0)
            {
                this.data = (double[,])initData.Clone();
                nRows = data.GetUpperBound(0) + 1;
                nCols = data.GetUpperBound(1) + 1;
            }
            else throw new Exception("Значений в матрице должно быть >= 1");
        }
        public double? this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= nRows || j >= nCols) return null;
                else return this.data[i, j];
            }
            set { this.data[i, j] = (double)value; } //генерация ошибки при индексах < 0
        }
        public int Rows => nRows;
        public int Columns => nCols;
        public int? Size { get { if (nRows != nCols) return null; else return nRows * nCols; } }

        public bool IsSquared => nRows == nCols;
        public bool IsEmpty
        {
            get
            {
                foreach (var t in data)
                {
                    if (t != 0) return false;
                }
                return true;
            }
        }
        public bool IsDiagonal
        {
            get
            {
                if (!IsSquared) return false;
                for (int i = 0; i < nRows; i++)
                {
                    for (int j = 0; j < nCols; j++)
                    {
                        if (i != j && this[i, j] != 0) return false;
                    }
                }
                return true;
            }
        }
        public bool IsUnity
        {
            get
            {
                if (!IsDiagonal) return false;
                for (int i = 0; i < nRows; i++)
                {
                    if (this[i, i] != 1) return false;
                }
                return true;
            }
        }
        public bool IsSymmetric
        {
            get
            {
                if (!IsSquared) return false;
                for (int i = 0; i < nRows; i++)
                {
                    for (int j = 0; j < nCols; j++)
                    {
                        if (this[i, j] != this[j, i]) return false;
                    }
                }
                return true;
            }
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if ((m1.nRows != m2.nRows) || (m1.nCols != m2.nCols))
            {
                throw new Exception("Количество строк и столбцов в матрицах должно совпадать");
            }
            Matrix matrix = new Matrix(m1.data);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix[i, j] += m2[i, j];
                }
            }
            return matrix;
        }
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if ((m1.nRows != m2.nRows) || (m1.nCols != m2.nCols))
            {
                throw new Exception("Количество строк и столбцов в матрицах должно совпадать");
            }
            Matrix matrix = new Matrix(m1.data);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix[i, j] -= m2[i, j];
                }
            }
            return matrix;
        }
        public static Matrix operator *(Matrix m1, double d)
        {
            Matrix matrix = new Matrix(m1.data);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix[i, j] *= d;
                }
            }
            return matrix;
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.nCols != m2.nRows)
            {
                throw new Exception("Кол-во столбцов 1 матрицы должно быть равно кол-ву строк 2");
            }
            Matrix matrix = new Matrix(m1.nRows, m2.nCols);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    for (int k = 0; k < m1.Columns; k++)
                    {
                        matrix[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return matrix;
        }

        public static explicit operator Matrix(double[,] arr) { return new Matrix(arr); }

        public Matrix Transpose()
        {
            Matrix matrix = new Matrix(this.nCols, this.nRows);
            for (int i = 0; i < this.Columns; i++)
            {
                for (int j = 0; j < this.Rows; j++)
                {
                    matrix[i, j] = this[j, i];
                }
            }
            return matrix;
        }

        public double Trace()
        {
            if (!this.IsSquared)
            {
                throw new Exception("Матрица должна быть квадратной");
            }
            double num = 0.0;
            for (int i = 0; i < nCols; i++)
            {
                num += (double)this[i, i];
            }
            return num;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    str += Convert.ToString(this[i, j] + " ").PadRight(6);
                }
                str += "\n";
            }
            return str;
        }

        public static Matrix GetUnity(int Size)
        {
            if (Size < 1)
            {
                throw new Exception("Размер должен быть больше 0");
            }
            Matrix matrix = new Matrix(Size, Size);
            for (int i = 0; i < Size; i++)
            {
                matrix[i, i] = 1;
            }
            return matrix;
        }
        public static Matrix GetEmpty(int Size)
        {
            if (Size < 1)
            {
                throw new Exception("Размер должен быть больше 0");
            }
            return new Matrix(Size, Size);
        }

        public static Matrix Parse(string s)
        {
            string[] strArray = s.Split(", ");
            Regex pattern = new Regex(@"\s+");
            string target = " ";
            for (int i = 0; i < strArray.Length - 1; i++)
            {
                if (pattern.Replace(strArray[i], target).Trim(' ').Split(' ').Length != pattern.Replace(strArray[i + 1], target).Trim(' ').Split(' ').Length)
                    throw new FormatException("Неверно введена матрица");
            }
            Matrix matrix = new Matrix(strArray.Length, pattern.Replace(strArray[0], target).Trim(' ').Split(' ').Length);
            for (int i = 0; i < strArray.Length; i++)
            {
                for (int j = 0; j < pattern.Replace(strArray[i], target).Trim(' ').Split(' ').Length; j++)
                {
                    double tmp = 0;
                    if (double.TryParse(pattern.Replace(strArray[i], target).Trim(' ').Split(' ')[j], out tmp))
                        matrix[i, j] = tmp;
                    else throw new FormatException("Неверно введена матрица");
                }
            }
            return matrix;
        }

        public static bool TryParse(string s, out Matrix m)
        {
            try
            {
                m = Parse(s);
                return true;
            }
            catch (FormatException)
            {
                m = null;
                return false;
            }
        }
    }
}