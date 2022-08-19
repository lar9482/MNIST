using System;
using System.Collections.Generic;
using System.Text;
using Neural_Network.MatrixLibrary;

namespace Neural_Network.Activation
{
    public abstract class activationFunction
    {
        public abstract double activate(double input);
        public abstract double activateDerivative(double input);

        public virtual Matrix activateMatrix(Matrix input)
        {
            double[,] activatedData = new double[input.rows,input.cols];

            for (int i = 0; i < input.rows; i++)
            {
                for (int j = 0; j < input.cols; j++)
                {
                    activatedData[i, j] = activate(input.data[i, j]);
                }
            }

            return new Matrix(activatedData);
        }

        public virtual Matrix activateDerivativeMatrix(Matrix input)
        {
            double[,] activatedData = new double[input.rows, input.cols];

            for (int i = 0; i < input.rows; i++)
            {
                for (int j = 0; j < input.cols; j++)
                {
                    activatedData[i, j] = activateDerivative(input.data[i, j]);
                }
            }

            return new Matrix(activatedData);
        }
    }
}
