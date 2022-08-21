using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neural_Network.MatrixLibrary;

namespace Neural_Network.Error
{
    public class MSE : errorFunction
    {
        public override double error(double expected, double actual)
        {
            return (expected-actual)*(expected - actual);
        }

        public override double errorDerivative(double expected, double actual)
        {
            return 2*(expected - actual);
        }

        public override Matrix errorMatrix(Matrix expectedMatrix, Matrix actualMatrix)
        {
            if (!expectedMatrix.sameSize(actualMatrix))
            {
                throw new Exception("errorFunction.errorMatrix: The target/actual matrices must have equal row/column amounts.");
            }

            double[,] errorData = new double[expectedMatrix.rows, expectedMatrix.cols];
            double predictionNum = (double)expectedMatrix.rows*expectedMatrix.cols;

            for (int i = 0; i < expectedMatrix.rows; i++)
            {
                for (int j = 0; j < expectedMatrix.cols; j++)
                {
                    errorData[i, j] = error(expectedMatrix.data[i, j], actualMatrix.data[i, j]) / predictionNum;
                }
            }

            return new Matrix(errorData);
        }

        public override Matrix errorDerivativeMatrix(Matrix expectedMatrix, Matrix actualMatrix)
        {
            if (!expectedMatrix.sameSize(actualMatrix))
            {
                throw new Exception("errorFunction.errorDerivativeMatrix: The target/actual matrices must have equal row/column amounts.");
            }

            double[,] errorData = new double[expectedMatrix.rows, expectedMatrix.cols];
            double predictionNum = (double)expectedMatrix.rows * expectedMatrix.cols;

            for (int i = 0; i < expectedMatrix.rows; i++)
            {
                for (int j = 0; j < expectedMatrix.cols; j++)
                {
                    errorData[i, j] = errorDerivative(expectedMatrix.data[i, j], actualMatrix.data[i, j]) / predictionNum;
                }
            }

            return new Matrix(errorData);
        }
    }
}
