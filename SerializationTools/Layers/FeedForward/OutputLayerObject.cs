using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SerializationTools.Layers.FeedForward.Dense;
using SerializationTools.Activation;
using SerializationTools.Learning_Algorithm;
using SerializationTools.Error;

using Neural_Network.MatrixLibrary;
using Neural_Network.Layers.FeedForward.Output;

namespace SerializationTools.Layers.FeedForward.Output
{
    public class OutputLayerObject : DenseLayerObject
    {
        public Matrix truthMatrix { get; set; }
        public string errorFunction { get; set; }

        public static OutputLayerObject serializeObject(OutputLayer layer)
        {
            OutputLayerObject objLayer = new OutputLayerObject();

            objLayer.contents = layer.contents;
            objLayer.LAYER_HEIGHT = layer.LAYER_HEIGHT;
            objLayer.LAYER_WIDTH = layer.LAYER_WIDTH;

            objLayer.weights = layer.weights;
            objLayer.bias = layer.bias;
            objLayer.nonActivatatedContents = layer.nonActivatatedContents;
            objLayer.activation = ActivationSerialization.SerializeActivation(layer.activation);
            objLayer.learningAlgorithm = LearningAlgorithm_Serialization.Serialize_LearningAlgorithm(layer.algorithm);
            objLayer.learningRate = layer.learningRate;

            objLayer.truthMatrix = layer.truthMatrix;
            objLayer.errorFunction = ErrorSerialization.SerializeError(layer.errorFunction);
            return objLayer;
        }

        public static OutputLayer deserializeObject(OutputLayerObject objLayer)
        {
            OutputLayer layer = new OutputLayer();
            layer.contents = objLayer.contents;
            layer.LAYER_HEIGHT = objLayer.LAYER_HEIGHT;
            layer.LAYER_WIDTH = objLayer.LAYER_WIDTH;

            layer.weights = objLayer.weights;
            layer.bias = objLayer.bias;
            layer.nonActivatatedContents = objLayer.nonActivatatedContents;
            layer.activation = ActivationSerialization.DeserializeActivation(objLayer.activation);
            layer.algorithm = LearningAlgorithm_Serialization.Deserialize_LearningAlgorithm(objLayer.learningAlgorithm);
            layer.learningRate = objLayer.learningRate;

            layer.truthMatrix = objLayer.truthMatrix;
            layer.errorFunction = ErrorSerialization.DeserializeError(objLayer.errorFunction);
            return layer;
        }
    }
}
