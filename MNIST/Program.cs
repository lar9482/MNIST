using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using MNIST.IO;

namespace MNIST
{
    internal class Program
    {

        public static string basePath = "C:\\Users\\luker\\source\\repos\\MNIST\\";

        public static string trainImages = "train-images-idx3-ubyte.gz";
        public static string trainLabels = "train-labels-idx1-ubyte.gz";

        public static string testImages = "t10k-images-idx3-ubyte.gz";
        public static string testLabels = "t10k-labels-idx1-ubyte.gz";


        static void Main(string[] args)
        {
            string testImagePath = String.Join("", new string[] { basePath, testImages});
            string testLabelPath = String.Join("", new string[] { basePath, testLabels });

            List<TestCase> tests = FileReaderMNIST.LoadImagesAndLables(testLabelPath, testImagePath).ToList();

            /*for (int i = 0; i < tests.Count; i++)
            {
                TestCase testCase = tests[i];
                double[,] data = testCase.AsDouble();
                int label = testCase.Label;

                printArray(data, label);
            }*/

            for (int i = 1; i <= 1; i++)
            {
                List<int> unusedIndices = Enumerable.Range(0, 60000).ToList();
                Random rand = new Random();
                while (unusedIndices.Count > 0)
                {
                    List<int> usedIndices = unusedIndices.OrderBy(x => rand.Next()).Take(100).ToList();
                    unusedIndices = unusedIndices.Except(usedIndices).ToList();
                }
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
                    if (Math.Round(data[i, j], 2) != 0) {
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
