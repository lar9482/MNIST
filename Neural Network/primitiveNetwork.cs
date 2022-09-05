using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Neural_Network.MatrixLibrary;
using Neural_Network.Activation;
using Neural_Network.Error;


using Neural_Network.Layers.FeedForward.Input;
using Neural_Network.Layers.FeedForward.Dense;
using Neural_Network.Layers.FeedForward.Output;

using Neural_Network.LearningAlgorithmBase.GradientDescent;

using Neural_Network.MatrixLibrary.Utilities;

namespace Neural_Network
{
    internal class primitiveNetwork
    {
        public static void Main(String[] args)
        {
            /*int sampling = 5;
            int features = 10;

            int layerSize1 = 5;
            int layerSize2 = 3;
            int layerSize3 = 1;

            double learningRate = 0.5;

            activationFunction a = new sigmoid();
            errorFunction e = new crossEntropy();

            Matrix inputVector = new Matrix(features, sampling);

            InputLayer inputLayer = new InputLayer(inputVector);

            DenseLayer hiddenLayer1 = new DenseLayer(layerSize1, learningRate, inputLayer, a, new GradientDescent());
            DenseLayer hiddenLayer2 = new DenseLayer(layerSize2, learningRate, hiddenLayer1, a, new GradientDescent());
            DenseLayer hiddenLayer3 = new DenseLayer(layerSize3, learningRate, hiddenLayer2, a, new GradientDescent());

            Matrix truthVector = new Matrix(10, sampling);
            OutputLayer outputLayer = new OutputLayer(truthVector, learningRate, hiddenLayer3, a, e, new GradientDescent());

            hiddenLayer1.feedForward();
            hiddenLayer2.feedForward();
            hiddenLayer3.feedForward();
            outputLayer.feedForward();

            outputLayer.backPropagate();
            hiddenLayer3.backPropagate();
            hiddenLayer2.backPropagate();
            hiddenLayer1.backPropagate();

            outputLayer.updateWeights();
            outputLayer.updateBias();

            hiddenLayer3.updateWeights();
            hiddenLayer3.updateBias();

            hiddenLayer2.updateWeights();
            hiddenLayer2.updateBias();

            hiddenLayer1.updateWeights();
            hiddenLayer1.updateBias();*/

            //Matrix actualVector = outputLayer.contents;

            /*Matrix inputSubset = Matrix_Utilities.getMatrixColumns(inputVector, new List<int> { 2, 4 });
            inputVector.printMatrix();
            inputSubset.printMatrix();*/

            matrixParallelObservation();
        }

        public static void matrixParallelObservation()
        {
            Matrix matrix1 = new Matrix(1000, 1000);
            Matrix matrix2 = new Matrix(1000, 1000);

            Stopwatch timer1 = new Stopwatch();
            Stopwatch timer2 = new Stopwatch();
            Stopwatch timer3 = new Stopwatch();

            timer1.Start();
            Matrix parallel = matrix1.matrixMultiply(matrix2);
            timer1.Stop();
            timer2.Start();
            Matrix serialMatrix = serialMuliply(matrix1, matrix2);
            timer2.Stop();

            timer3.Start();
            Matrix nestedMatrix = nestedTest(matrix1, matrix2);
            timer3.Stop();
            Console.WriteLine(parallel.Equals(serialMatrix));
            Console.WriteLine(nestedMatrix.Equals(serialMatrix));
            Console.WriteLine(timer1.ElapsedMilliseconds);
            Console.WriteLine(timer2.ElapsedMilliseconds);
            Console.WriteLine(timer3.ElapsedMilliseconds);
        }

        public static Matrix nestedTest(Matrix firstMatrix, Matrix secondMatrix)
        {
            double[,] newData = new double[firstMatrix.rows, secondMatrix.cols];
            Parallel.For(0, firstMatrix.rows, i =>
            {

                Parallel.For(0, secondMatrix.cols, j =>
                {
                    double sum = 0;

                    Parallel.For(0, secondMatrix.rows, k =>
                    {
                        sum += (firstMatrix.data[i, k] * secondMatrix.data[k, j]);
                    });

                    newData[i, j] = sum;
                });
            });
            /*for (int i = 0; i < firstMatrix.rows; i++)
            {
                for (int j = 0; j < secondMatrix.cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < secondMatrix.rows; k++)
                    {
                        sum += (firstMatrix.data[i, k] * secondMatrix.data[k, j]);
                    }

                    newData[i, j] = sum;
                }
            }*/

            return new Matrix(newData);
        }
        public static Matrix serialMuliply(Matrix firstMatrix, Matrix secondMatrix)
        {
            double[,] newData = new double[firstMatrix.rows, secondMatrix.cols];
            for (int i = 0; i < firstMatrix.rows; i++)
            {
                for (int j = 0; j < secondMatrix.cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < secondMatrix.rows; k++)
                    {
                        sum += (firstMatrix.data[i, k] * secondMatrix.data[k, j]);
                    }

                    newData[i, j] = sum;
                }
            }

            return new Matrix(newData);
        }
    }
}
