using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neural_Network.MatrixLibrary;
using Neural_Network.Activation;

using Neural_Network.Layers.FeedForward.Input;
using Neural_Network.Layers.FeedForward.Output;

using Neural_Network.LearningAlgorithmBase;


namespace Neural_Network.Layers.FeedForward.Dense
{
    public class DenseLayer : BaseLayer
    {
        public BaseLayer previousLayer { get; set; }
        public DenseLayer nextLayer { get; set; }


        public Matrix weights { get; set; }
        public Matrix bias { get; set; }
        public Matrix nonActivatatedContents { get; set; }

        public activationFunction activation { get; set; }
        public LearningAlgorithm algorithm { get; set; }

        protected double learningRate { get; set; }

        protected double gradientClippingTolerance = -1;


        public DenseLayer(int layerSize, double learningRate, InputLayer previousLayer, activationFunction activation, LearningAlgorithm algorithm,
                          double gradientClippingTolerance = -1
           
        )
        {

            this.learningRate = learningRate;

            this.previousLayer = previousLayer;

            this.LAYER_HEIGHT = layerSize;
            this.LAYER_WIDTH = previousLayer.LAYER_WIDTH;

            this.weights = new Matrix(LAYER_HEIGHT, previousLayer.LAYER_HEIGHT);
            this.bias = new Matrix(LAYER_HEIGHT, previousLayer.LAYER_WIDTH);

            this.activation = activation;
            this.algorithm = algorithm;

            this.gradientClippingTolerance = gradientClippingTolerance;
        }

        public DenseLayer(int layerSize, double learningRate, DenseLayer previousLayer, activationFunction activation, LearningAlgorithm algorithm,
                          double gradientClippingTolerance = -1
        )
        {
            this.learningRate = learningRate;

            this.previousLayer = previousLayer;
            previousLayer.nextLayer = this;

            this.LAYER_HEIGHT = layerSize;
            this.LAYER_WIDTH = previousLayer.LAYER_WIDTH;

            this.weights = new Matrix(layerSize, previousLayer.LAYER_HEIGHT);
            this.bias = new Matrix(layerSize, previousLayer.LAYER_WIDTH);

            this.activation = activation;
            this.algorithm = algorithm;

            this.gradientClippingTolerance = gradientClippingTolerance;
        }

        public void feedForward()
        {

            nonActivatatedContents = (weights.matrixMultiply(previousLayer.contents))
                                     .matrixAdd(bias);
            
            contents = activation.activateMatrix(nonActivatatedContents);
        }

        public virtual void backPropagate()
        {
            algorithm.backPropagateDense(this);
        }

        public void updateWeights()
        {
            Matrix localChange = algorithm.localChange;
            Matrix previousContent = previousLayer.contents.transpose();

            Matrix weightChange = localChange.matrixMultiply(previousContent);
            weightChange = weightChange.scalarMultiply(-learningRate);

            if (gradientClippingTolerance != -1)
            {
                weightChange = weightChange.scalarMultiply((double)(gradientClippingTolerance / weightChange.norm()));
            }

            Console.WriteLine("Gradient weight norm: " + weightChange.norm());
            weights = weights.matrixAdd(weightChange);
        }

        public void updateBias()
        {
            Matrix localChange = algorithm.localChange;
            Matrix biasChange = localChange.scalarMultiply(-learningRate);

            if (gradientClippingTolerance != -1)
            {
                biasChange = biasChange.scalarMultiply((double)(gradientClippingTolerance / bias.norm()));
            }

            //Console.WriteLine("Gradient bias norm: " + biasChange.norm());

            bias = bias.matrixAdd(biasChange);
        }
    }
}
