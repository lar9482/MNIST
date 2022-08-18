using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neural_Network.MatrixLibrary;
using Neural_Network.Layers.FeedForward.Dense;
using Neural_Network.Layers.FeedForward.Output;
using Neural_Network.Activation;
using Neural_Network.Error;

namespace Neural_Network.LearningAlgorithmBase
{
    public abstract class LearningAlgorithm
    {
        public Matrix localChange { get; set; }

        public abstract void backPropagateDense(DenseLayer layer);
        public abstract void backPropagateOutput(OutputLayer layer);
    }
}
