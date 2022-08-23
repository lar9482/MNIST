using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neural_Network.MatrixLibrary;
using Neural_Network.Layers.FeedForward.Input;
using Neural_Network.Layers.FeedForward.Dense;
using Neural_Network.Layers.FeedForward.Output;
using Neural_Network.Activation;
using Neural_Network.Error;
using Neural_Network.LearningAlgorithmBase;
using Neural_Network.LearningAlgorithmBase.GradientDescent;
using Neural_Network.Network.FeedForward;

using SerializationTools.Activation;
using SerializationTools.Error;
using SerializationTools.Learning_Algorithm;

using SerializationTools.Layers.FeedForward.Input;
using SerializationTools.Layers.FeedForward.Dense;
using SerializationTools.Layers.FeedForward.Output;

using SerializationTools.Network.FeedForward;

using System.IO;


using Newtonsoft.Json;

namespace SerializationTools
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int inputFeatureSize = 784;
            int outputFeatureSize = 10;
            int samplingSize = 100;

            Matrix inputMatrix = new Matrix(inputFeatureSize, samplingSize);
            Matrix truthMatrix = new Matrix(outputFeatureSize, samplingSize);

            FeedForward_Network network = new FeedForward_Network(inputFeatureSize, outputFeatureSize, samplingSize);
            network.addDenseLayer(324, 0.5, new sigmoid(), new GradientDescent());
            network.compile(0.5, new softmax(), new crossEntropy(), new GradientDescent());
            network.train(inputMatrix, truthMatrix, 1);

            string fileName = "Test.json";
            string fullPath = String.Join("\\", new string[] { System.IO.Path.GetFullPath(@"..\..\"), fileName });

            FeedForward_Network_Object.saveObject(fullPath, network);

            FeedForward_Network newNetwork = FeedForward_Network_Object.loadObject(fullPath);


            /*int inputFeatureSize = 784;
            int outputFeatureSize = 10;
            int samplingSize = 100;

            Matrix inputMatrix = new Matrix(inputFeatureSize, samplingSize);
            Matrix truthMatrix = new Matrix(outputFeatureSize, samplingSize);

            FeedForward_Network network = new FeedForward_Network(inputFeatureSize, outputFeatureSize, samplingSize);
            network.addDenseLayer(324, 0.5, new sigmoid(), new GradientDescent());
            network.compile(0.5, new softmax(), new crossEntropy(), new GradientDescent());
            network.train(inputMatrix, truthMatrix, 1);

            string fileName = "Test.json";
            string fullPath = String.Join("\\", new string[] { System.IO.Path.GetFullPath(@"..\..\"), fileName });

            FeedForward_Network_Object objNetwork = FeedForward_Network_Object.serializeObject(network);
            string networkJson = JsonConvert.SerializeObject(objNetwork);*/
        }
    }
}
