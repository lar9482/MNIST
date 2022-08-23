using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neural_Network.MatrixLibrary;
using Neural_Network.Layers;
using Neural_Network.Layers.FeedForward.Dense;

using SerializationTools.Activation;
using SerializationTools.Learning_Algorithm;

namespace SerializationTools.Layers.FeedForward.Dense
{
    public class DenseLayerObject : BaseLayer
    {
        public Matrix weights { get; set; }
        public Matrix bias { get; set; }
        public Matrix nonActivatatedContents { get; set; }
        public string activation { get; set; }
        public string learningAlgorithm { get; set; }

        public double learningRate { get; set; }


        public static DenseLayerObject serializeObject(DenseLayer layer)
        {
            DenseLayerObject objLayer = new DenseLayerObject();

            objLayer.contents = layer.contents;
            objLayer.LAYER_HEIGHT = layer.LAYER_HEIGHT;
            objLayer.LAYER_WIDTH = layer.LAYER_WIDTH;

            objLayer.weights = layer.weights;
            objLayer.bias = layer.bias;
            objLayer.nonActivatatedContents = layer.nonActivatatedContents;
            objLayer.activation = ActivationSerialization.SerializeActivation(layer.activation);
            objLayer.learningAlgorithm = LearningAlgorithm_Serialization.Serialize_LearningAlgorithm(layer.algorithm);

            objLayer.learningRate = layer.learningRate;

            return objLayer;
        }

        public static DenseLayer deserializeObject(DenseLayerObject objLayer)
        {
            DenseLayer layer = new DenseLayer();

            layer.contents = objLayer.contents;
            layer.LAYER_HEIGHT = objLayer.LAYER_HEIGHT;
            layer.LAYER_WIDTH = objLayer.LAYER_WIDTH;

            layer.weights = objLayer.weights;
            layer.bias = objLayer.bias;
            layer.nonActivatatedContents = objLayer.nonActivatatedContents;

            layer.activation = ActivationSerialization.DeserializeActivation(objLayer.activation);
            layer.algorithm = LearningAlgorithm_Serialization.Deserialize_LearningAlgorithm(objLayer.learningAlgorithm);

            layer.learningRate = objLayer.learningRate;

            return layer;
        }
    }
}
