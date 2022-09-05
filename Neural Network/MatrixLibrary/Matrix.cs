using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neural_Network.MatrixLibrary
{
    public class Matrix
    {
        public double[,] data { get; set; }
        public int rows { get; set; }
        public int cols { get; set; }

        public Matrix() { }

        public Matrix(double[,] data, int rows, int cols)
        {
            this.data = data;
            this.rows = rows;
            this.cols = cols;
        }

        public Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;

            this.data = generateRandomArray(rows, cols);
        }

        public Matrix(double[,] data)
        {
            this.data = data;
            this.rows = data.GetLength(0);
            this.cols = data.GetLength(1);
            
        }

        private double[,] generateRandomArray(int rows, int cols, int lowerBound = -1, int upperBound = 1)
        {
            double[,] data = new double[rows, cols];
            Random rand = new Random();
            for (int cur_Row = 0; cur_Row < rows; cur_Row++)
            {
                for(int cur_Col = 0; cur_Col < cols; cur_Col++)
                {
                    data[cur_Row,cur_Col] = rand.NextDouble() * (upperBound - lowerBound) + lowerBound;
                }
            }
            return data;
        }

        public Matrix scalarAdd(double scalar)
        {
            double[,] newData = new double[rows, cols];

            for (int cur_Row = 0; cur_Row < rows; cur_Row++)
            {
                for (int cur_Col = 0; cur_Col < cols; cur_Col++)
                {
                    newData[cur_Row, cur_Col] = data[cur_Row, cur_Col] + scalar; 
                }
            }

            return new Matrix(newData);
        }

        public Matrix scalarMultiply(double scalar)
        {
            double[,] newData = new double[rows, cols];

            Parallel.For(0, rows, cur_Row =>
            {
                for (int cur_Col = 0; cur_Col < cols; cur_Col++)
                {
                    newData[cur_Row, cur_Col] = data[cur_Row, cur_Col] * scalar;
                }
            });

            
            /*for (int cur_Row = 0; cur_Row < rows; cur_Row++)
            {
                for (int cur_Col = 0; cur_Col < cols; cur_Col++)
                {
                    newData[cur_Row, cur_Col] = data[cur_Row, cur_Col] * scalar;
                }
            }*/

            return new Matrix(newData);
        }

        public Matrix transpose()
        {
            double[,] newData = new double[cols, rows];

            Parallel.For(0, cols, i =>
            {
                for (int j = 0; j < rows; j++)
                {
                    newData[i, j] = data[j, i];
                }
            });

            /*for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    newData[i, j] = data[j, i];
                }
            }*/
            return new Matrix(newData);
        }
        
        public Matrix matrixAdd(Matrix secondMatrix)
        {
            if (!sameSize(secondMatrix))
            {
                throw new Exception("Matrices must be of the same rows and columns.");
            }

            double[,] newData = new double[rows, cols];
            Parallel.For(0, rows, i =>
            {
                for (int j = 0; j < cols; j++)
                {
                    /*if (data[i, j] + secondMatrix.data[i, j] > 1)
                    {
                        newData[i, j] = 1;
                    }
                    else
                    {
                        newData[i, j] = data[i, j] + secondMatrix.data[i, j];
                    }*/
                    newData[i, j] = data[i, j] + secondMatrix.data[i, j];
                }
            });

            /*for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    newData[i, j] = data[i, j] + secondMatrix.data[i, j];
                }
            }*/
            return new Matrix(newData);
        }

        public Matrix matrixMultiply(Matrix secondMatrix)
        {

            if (cols != secondMatrix.rows)
            {
                throw new Exception("The columns of the first matrix and rows of the second matrix must be equal.");
            }
            double[,] newData = new double[rows, secondMatrix.cols];

            Parallel.For(0, rows, i =>
            {
                for (int j = 0; j < secondMatrix.cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < secondMatrix.rows; k++)
                    {
                        sum += (data[i, k] * secondMatrix.data[k, j]);
                    }

                    newData[i, j] = sum;
                }
            });

            /*for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < secondMatrix.cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < secondMatrix.rows; k++)
                    {
                        sum += (data[i, k] * secondMatrix.data[k, j]);
                    }

                    newData[i, j] = sum;
                }
            }*/


            return new Matrix(newData);
        }

        public Matrix elementWiseMultiply(Matrix secondMatrix)
        {

            if (!sameSize(secondMatrix))
            {
                throw new Exception("Matrices must be of the same rows and columns.");
            }

            double[,] newData = new double[rows, cols];

            Parallel.For(0, rows, i =>
            {
                for (int j = 0; j < cols; j++)
                {
                    newData[i, j] = data[i, j] * secondMatrix.data[i, j];
                }
            });

            /*for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    newData[i, j] = data[i, j] * secondMatrix.data[i, j];
                }
            }*/

            return new Matrix(newData);
        }

        public double norm()
        {
            double result = 0;
            Parallel.For(0, rows, i =>
            {
                for (int j = 0; j < cols; j++)
                {
                    result += (data[i, j] * data[i, j]);
                }
            });

            return Math.Sqrt(result);
        }

        public Matrix invertElements()
        {
            double[,] newData = new double[rows, cols];
            Parallel.For(0, rows, i =>
            {
                for (int j = 0; j < cols; j++)
                {
                    newData[i, j] = 1 - data[i, j];
                }
            });

            return new Matrix(newData);
        }

        public bool sameSize(Matrix matrix)
        {
            return (rows == matrix.rows && cols == matrix.cols);
        }

        public bool Equals(Matrix matrix)
        {
            if (!sameSize(matrix))
            {
                return false;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (data[i, j] != matrix.data[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void printMatrix()
        {
            Console.WriteLine();
            for (int i = 0; i < rows; i++)
            {
                Console.Write("[");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(Math.Round(data[i,j], 2)+ ", ");
                }
                Console.Write("]\n");
            }

            Console.WriteLine();
        }
    }
}
