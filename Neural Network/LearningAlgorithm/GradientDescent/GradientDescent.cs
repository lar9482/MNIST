using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neural_Network.MatrixLibrary;
using Neural_Network.Activation;
using Neural_Network.Error;

using Neural_Network.Layers;
using Neural_Network.Layers.FeedForward.Dense;
using Neural_Network.Layers.FeedForward.Output;

namespace Neural_Network.LearningAlgorithmBase.GradientDescent
{
    public class GradientDescent : LearningAlgorithm
    {

        public GradientDescent()
        {
        }

        public override void backPropagateDense(DenseLayer layer)
        {
            activationFunction activate = layer.activation;

            Matrix nextLocalChange = layer.nextLayer.algorithm.localChange;
            Matrix nextWeights = layer.nextLayer.weights.transpose();
            Matrix nextWeights_Times_LocalChange = nextWeights.matrixMultiply(nextLocalChange);

            Matrix activationDerivativeResults = activate.activateDerivativeMatrix(layer.nonActivatatedContents);


            localChange = nextWeights_Times_LocalChange.elementWiseMultiply(activationDerivativeResults);
        }

        public override void backPropagateOutput(OutputLayer layer)
        {
            errorFunction error = layer.errorFunction;
            activationFunction activate = layer.activation;

            Matrix errorResultDerivative = error.errorDerivativeMatrix(layer.truthMatrix, layer.contents);
            Matrix activationDerivativeResult = activate.activateDerivativeMatrix(layer.nonActivatatedContents);

            localChange = errorResultDerivative.elementWiseMultiply(activationDerivativeResult);
        }
    }
}
