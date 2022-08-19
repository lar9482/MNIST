using System;
using System.Collections.Generic;
using System.Text;
using Neural_Network.MatrixLibrary;

namespace Neural_Network.Error
{
    public abstract class errorFunction
    {

        public abstract double error(double expected, double actual);
        public abstract double errorDerivative(double expected, double actual);
        

        public Matrix errorMatrix(Matrix expectedMatrix, Matrix actualMatrix)
        {
            if (!expectedMatrix.sameSize(actualMatrix))
            {
                throw new Exception("errorFunction.errorMatrix: The target/actual matrices must have equal row/column amounts.");
            }

            double[,] errorData = new double[expectedMatrix.rows, expectedMatrix.cols];
            for (int i = 0; i < expectedMatrix.rows; i++)
            {
                for (int j = 0; j < expectedMatrix.cols; j++)
                {
                    errorData[i, j] = error(expectedMatrix.data[i, j], actualMatrix.data[i, j]);
                }
            }

            return new Matrix(errorData);
        }

        public Matrix errorDerivativeMatrix(Matrix expectedMatrix, Matrix actualMatrix)
        {
            if (!expectedMatrix.sameSize(actualMatrix))
            {
                throw new Exception("errorFunction.errorDerivativeMatrix: The target/actual matrices must have equal row/column amounts.");
            }

            double[,] errorData = new double[expectedMatrix.rows, expectedMatrix.cols];
            for (int i = 0; i < expectedMatrix.rows; i++)
            {
                for (int j = 0; j < expectedMatrix.cols; j++)
                {
                    errorData[i, j] = errorDerivative(expectedMatrix.data[i, j], actualMatrix.data[i, j]);
                }
            }

            return new Matrix(errorData);
        }
    }
}
