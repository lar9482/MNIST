﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MNIST.IO;

namespace MNIST
{
    internal class Program
    {
        public static string basePath = System.IO.Path.GetFullPath(@"..\..\") + "\\dataset\\";

        public static string trainImages = "train-images-idx3-ubyte.gz";
        public static string trainLabels = "train-labels-idx1-ubyte.gz";

        public static string testImages = "t10k-images-idx3-ubyte.gz";
        public static string testLabels = "t10k-labels-idx1-ubyte.gz";


        static void Main(string[] args)
        {
            Console.WriteLine(basePath);
            string testImagePath = String.Join("", new string[] { basePath, testImages });
            string testLabelPath = String.Join("", new string[] { basePath, testLabels });

            List<TestCase> tests = FileReaderMNIST.LoadImagesAndLables(testLabelPath, testImagePath).ToList();

            for (int i = 0; i < tests.Count; i++)
            {
                TestCase testCase = tests[i];
                double[,] data = testCase.AsDouble();
                int label = testCase.Label;
                printArray(data, label);
            }
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
                        Console.Write(Math.Round(data[i, j], 2));
                    }
                    else
                    {
                        Console.Write("---");
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