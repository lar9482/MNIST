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

namespace MNIST
{
    internal class Program
    {
        public const int inputFeatureSize = 784;
        public const int outputFeatureSize = 10;
        public const int samplingSize = 1;

        public static string basePath = System.IO.Path.GetFullPath(@"..\..\") + "\\dataset\\";

        public static string trainImages = "train-images-idx3-ubyte.gz";
        public static string trainLabels = "train-labels-idx1-ubyte.gz";

        public static string testImages = "t10k-images-idx3-ubyte.gz";
        public static string testLabels = "t10k-labels-idx1-ubyte.gz";


        static void Main(string[] args)
        {
            Console.WriteLine(basePath);
            string testImagePath = String.Join("", new string[] { basePath, trainImages });
            string testLabelPath = String.Join("", new string[] { basePath, trainLabels });

            List<TestCase> tests = FileReaderMNIST.LoadImagesAndLables(testLabelPath, testImagePath).ToList();


            Matrix images = extractImages(tests);
            Matrix labels = extractLabels(tests);

            FeedForward_Network network = new FeedForward_Network(inputFeatureSize, outputFeatureSize, samplingSize);
            //network.addDenseLayer(128, 0.01, new sigmoid(), new GradientDescent());
            //network.addDenseLayer(64, 0.5, new sigmoid(), new GradientDescent());
            network.compile(0.01, new sigmoid(), new crossEntropy(), new GradientDescent());

            network.train(images, labels, 1);
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