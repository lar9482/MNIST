using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neural_Network.LearningAlgorithmBase;
using Neural_Network.LearningAlgorithmBase.GradientDescent;

namespace SerializationTools.Learning_Algorithm
{
    internal class LearningAlgorithm_Serialization
    {
        public static string Serialize_LearningAlgorithm(LearningAlgorithm algorithm)
        {
            if (algorithm is GradientDescent)
            {
                return "GradientDescent";
            }

            return "";
        }

        public static LearningAlgorithm Deserialize_LearningAlgorithm(string algorithm)
        {
            switch (algorithm)
            {
                case "GradientDescent":
                    return new GradientDescent();

                default:
                    return new GradientDescent();
            }
        }
    }
}
