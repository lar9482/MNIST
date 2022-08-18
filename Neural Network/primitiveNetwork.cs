using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            int sampling = 5;
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
            hiddenLayer1.updateBias();

            //Matrix actualVector = outputLayer.contents;

            /*Matrix inputSubset = Matrix_Utilities.getMatrixColumns(inputVector, new List<int> { 2, 4 });
            inputVector.printMatrix();
            inputSubset.printMatrix();*/
        }
    }
}
