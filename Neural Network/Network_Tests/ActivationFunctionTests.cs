using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neural_Network.MatrixLibrary;
using Neural_Network.Activation;

namespace Network_Tests
{
    [TestClass]
    public class ActivationFunctionTests 
    {
        [TestMethod()]
        public void sigmoidTests_1x1()
        {
            double[,] initialData = new double[,]
            {
                { 1.0}
            };

            double[,] expectedData = new double[,]
            {
                { 0.731058578630074}
            };

            activationFunction sigmoid = new sigmoid();

            Matrix expectedMatrix = new Matrix(expectedData);
            Matrix actualMatrix = sigmoid.activateMatrix(new Matrix(initialData));

            System.Diagnostics.Debug.WriteLine(Double.Equals(expectedMatrix.data[0,0], actualMatrix.data[0, 0]));

        }
    }
}
