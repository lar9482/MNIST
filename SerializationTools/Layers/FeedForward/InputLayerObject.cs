using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SerializationTools.Layers;

using Neural_Network.MatrixLibrary;
using Neural_Network.Layers;
using Neural_Network.Layers.FeedForward.Input;

namespace SerializationTools.Layers.FeedForward.Input
{
    public class InputLayerObject : BaseLayer
    {
        public static InputLayerObject SerializeObject(InputLayer layer)
        {
            InputLayerObject objLayer = new InputLayerObject();
            objLayer.contents = layer.contents;
            objLayer.LAYER_HEIGHT = layer.LAYER_HEIGHT;
            objLayer.LAYER_WIDTH = layer.LAYER_WIDTH;

            return objLayer;
        }

        public static InputLayer DeserializeObject(InputLayerObject objLayer)
        {
            InputLayer layer = new InputLayer(objLayer.contents);
            return layer;
        }
    }
}
