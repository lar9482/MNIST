using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SerializationTools.Layers.FeedForward.Input;
using SerializationTools.Layers.FeedForward.Dense;
using SerializationTools.Layers.FeedForward.Output;

using Neural_Network.Layers.FeedForward.Input;
using Neural_Network.Layers.FeedForward.Dense;
using Neural_Network.Layers.FeedForward.Output;

using Neural_Network.Network.FeedForward;


using Newtonsoft.Json;

namespace SerializationTools.Network.FeedForward
{
    public class FeedForward_Network_Object
    {
        public InputLayerObject inputLayerObject { get; set; }
        public List<DenseLayerObject> hiddenLayerObjects { get; set; }
        public OutputLayerObject outputLayerObject { get; set; }

        public int samplingSize { get; set; }
        public int inputFeatureSize { get; set; }
        public int outputFeatureSize { get; set; }

        public static FeedForward_Network_Object serializeObject(FeedForward_Network network)
        {
            FeedForward_Network_Object objNetwork = new FeedForward_Network_Object();
            objNetwork.inputLayerObject = InputLayerObject.SerializeObject(network.inputLayer);

            objNetwork.hiddenLayerObjects = new List<DenseLayerObject>(network.hiddenLayers.Count);
            for (int i = 0; i < network.hiddenLayers.Count; i++)
            {
                objNetwork.hiddenLayerObjects.Add
                    (DenseLayerObject.serializeObject(network.hiddenLayers[i])
                    );
            }
            objNetwork.outputLayerObject = OutputLayerObject.serializeObject(network.outputLayer);

            objNetwork.samplingSize = network.samplingSize;
            objNetwork.inputFeatureSize = network.inputFeatureSize;
            objNetwork.outputFeatureSize = network.outputFeatureSize;

            return objNetwork;
        }

        public static FeedForward_Network deserializeObject(FeedForward_Network_Object objNetwork)
        {
            FeedForward_Network network = new FeedForward_Network(
                                          objNetwork.inputFeatureSize, objNetwork.outputFeatureSize, objNetwork.samplingSize);

            network.inputLayer = InputLayerObject.DeserializeObject(objNetwork.inputLayerObject);
            network.outputLayer = OutputLayerObject.deserializeObject(objNetwork.outputLayerObject);

            network.hiddenLayers = new List<DenseLayer>(objNetwork.hiddenLayerObjects.Count);
            for (int i = 0; i < objNetwork.hiddenLayerObjects.Count; i++)
            {
                network.hiddenLayers.Add(
                        DenseLayerObject.deserializeObject(objNetwork.hiddenLayerObjects[i])
                    ); 
            }

            for (int i = 0; i < network.hiddenLayers.Count; i++)
            {
                if (i == 0)
                {
                    network.hiddenLayers[i].previousLayer = network.inputLayer;
                }
                else
                {
                    network.hiddenLayers[i].previousLayer = network.hiddenLayers[i - 1];
                }

                if (i == network.hiddenLayers.Count-1)
                {
                    network.hiddenLayers[i].nextLayer = network.outputLayer;
                }
                else
                {
                    network.hiddenLayers[i].nextLayer = network.hiddenLayers[i + 1];
                }
            }

            return network;
        }

        public static void saveObject(string fullPath, FeedForward_Network network)
        {
            FeedForward_Network_Object objNetwork = serializeObject(network);
            string networkJson = JsonConvert.SerializeObject(objNetwork);
            try
            {
                File.WriteAllText(fullPath, networkJson);
            }
            catch(IOException e)
            {
                throw new Exception(e.Message);
            }
        }

        
        public static FeedForward_Network loadObject(string fullPath)
        {
            FeedForward_Network network = null;
            try
            {
                string networkJson = File.ReadAllText(fullPath);
                FeedForward_Network_Object objNetwork = (FeedForward_Network_Object)
                                                         JsonConvert.DeserializeObject<FeedForward_Network_Object>(networkJson);

                network = deserializeObject(objNetwork);
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }

            return network;
        }
    }
}
