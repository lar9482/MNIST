using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MNIST.IO;

using Neural_Network.MatrixLibrary;
using Neural_Network.Activation;
using Neural_Network.Error;
using Neural_Network.LearningAlgorithmBase.GradientDescent;
using Neural_Network.Network.FeedForward;

using Neural_Network.MatrixLibrary.Utilities;

using SerializationTools.Network.FeedForward;

namespace MNIST
{
    internal class Program
    {
        public const int inputFeatureSize = 784;
        public const int outputFeatureSize = 10;
        public const int samplingSize = 1;

        public static string basePath = System.IO.Path.GetFullPath(@"..\..\");

        public static string trainImages = "train-images-idx3-ubyte.gz";
        public static string trainLabels = "train-labels-idx1-ubyte.gz";

        public static string testImages = "t10k-images-idx3-ubyte.gz";
        public static string testLabels = "t10k-labels-idx1-ubyte.gz";

        public static string fileName = "MNIST_Network.json";


        static void Main(string[] args)
        {
            //trainNetwork();
            testNetwork();
        }

        public static void trainNetwork()
        {
            string trainImagePath = String.Join("\\", new string[] { basePath, "dataset", trainImages });
            string trainLabelPath = String.Join("\\", new string[] { basePath, "dataset", trainLabels });

            List<TestCase> trainingCases = FileReaderMNIST.LoadImagesAndLables(trainLabelPath, trainImagePath).ToList();

            Matrix images = extractImages(trainingCases);
            Matrix labels = extractLabels(trainingCases);

            FeedForward_Network network = new FeedForward_Network(inputFeatureSize, outputFeatureSize, samplingSize);
            network.addDenseLayer(128, 0.5, new sigmoid(), new GradientDescent());
            network.compile(0.2, new softmax(), new crossEntropy(), new GradientDescent());

            network.train(images, labels, 10);


            FeedForward_Network_Object.saveObject(String.Join("\\", new string[] { basePath, fileName }), network);
        }

        public static void testNetwork()
        {
            string testImagePath = String.Join("\\", new string[] { basePath, "dataset", testImages });
            string testLabelPath = String.Join("\\", new string[] { basePath, "dataset", testLabels });

            List<TestCase> testCases = FileReaderMNIST.LoadImagesAndLables(testLabelPath, testImagePath).ToList();

            Matrix testImagesMatrix = extractImages(testCases);
            Matrix testLabelsMatrix = extractLabels(testCases);

            FeedForward_Network network = FeedForward_Network_Object.loadObject(String.Join("\\", new string[] { basePath, fileName }));
            List<int> unusedIndices = Enumerable.Range(0, testLabelsMatrix.cols).ToList();
            Random rand = new Random();
            while (unusedIndices.Count > 0)
            {
                List<int> usedIndices = unusedIndices.OrderBy(x => rand.Next()).Take(samplingSize).ToList();
                unusedIndices = unusedIndices.Except(usedIndices).ToList();

                Matrix input = Matrix_Utilities.getMatrixColumns(testImagesMatrix, usedIndices);

                Matrix predicted = network.predict(input);
                Matrix expected = Matrix_Utilities.getMatrixColumns(testLabelsMatrix, usedIndices);
            }
        }
        public static Matrix extractImages(List<TestCase> tests)
        {
            int samplingSize = tests.Count;
            double[,] newData = new double[inputFeatureSize, samplingSize];
            for (int i = 0; i < samplingSize; i++)
            {
                double[,] localData = tests[i].AsDouble();

                double[] localDataFlatten = new double[inputFeatureSize];
                int index = 0;

                for (int j = 0; j < localData.GetUpperBound(0); j++)
                {
                    for (int k = 0; k < localData.GetUpperBound(1); k++)
                    {
                        localDataFlatten[index] = localData[j, k];
                        index++;
                    }
                }

                for (int j = 0; j < localDataFlatten.Length; j++)
                {
                    newData[j, i] = localDataFlatten[j];
                }
            }

            //printArray(newData);
            return new Matrix(newData);
        }

        public static Matrix extractLabels(List<TestCase> tests)
        {
            int samplingSize = tests.Count;
            double[,] newData = new double[outputFeatureSize, samplingSize];

            for (int i = 0; i < samplingSize; i++)
            {
                int label = tests[i].Label;

                for (int j = 0; j < outputFeatureSize; j++)
                {
                    newData[j, i] = (label == j) ? (1f) : (0f);
                }
            }


            return new Matrix(newData);
        }

        static void printArray(double[,] data, int label)
        {
            Console.WriteLine();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Console.Write("[");
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (Math.Round(data[i, j], 2) != 0)
                    {
                        //Console.Write(Math.Round(data[i, j], 2));
                        Console.Write("-");
                    }
                    else
                    {
                        Console.Write("x");
                    }
                    //Console.Write(Math.Round(data[i, j], 2) + ", ");   
                }
                Console.Write("]\n");
            }
            Console.WriteLine(label);
            Console.WriteLine();
        }
    }
}