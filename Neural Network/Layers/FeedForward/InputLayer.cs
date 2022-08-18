using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neural_Network.MatrixLibrary;
using Neural_Network.Layers.FeedForward.Dense;

namespace Neural_Network.Layers.FeedForward.Input
{
    public class InputLayer : BaseLayer
    {

        public InputLayer(Matrix inputDataSet)
        {
            this.contents = inputDataSet;
            this.LAYER_HEIGHT = inputDataSet.rows;
            this.LAYER_WIDTH = inputDataSet.cols;
        }

        public InputLayer(int featureSize, int samplingSize)
        {
            this.LAYER_HEIGHT = featureSize;
            this.LAYER_WIDTH = samplingSize;
        }
        
    }
}
