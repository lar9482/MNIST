using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neural_Network.MatrixLibrary;

namespace Neural_Network.Activation
{
    public class softmax : activationFunction
    {
        public override double activate(double input)
        {
            return 1;
        }


        public override double activateDerivative(double input)
        {
            return 1;
        }

        public override Matrix activateMatrix(Matrix input)
        {

            double[,] activatedData = new double[input.rows, input.cols];

            for (int j = 0; j < input.cols; j++)
            {
                double total = 0;
                double max = -100000;
                for (int i = 0; i < input.rows; i++)
                {
                    max = Math.Max(max, input.data[i,j]);
                }
                max = -max;
                for (int i = 0; i < input.rows; i++)
                {
                    total += Math.Exp(input.data[i, j] + max);
                }
                for (int i = 0; i < input.rows; i++)
                {
                    activatedData[i, j] = Math.Exp(input.data[i, j] + max) / total;
                }
            }
            return new Matrix(activatedData);
        }

        public override Matrix activateDerivativeMatrix(Matrix input)
        {
            double[,] activatedData = new double[input.rows, input.cols];
            for (int i = 0; i < input.rows; i++)
            {
                for (int j = 0; j < input.cols; j++)
                {
                    activatedData[i, j] = input.data[i, j] * (1 - input.data[i, j]);
                }
            }
            return new Matrix(activatedData);
        }
    }
}
