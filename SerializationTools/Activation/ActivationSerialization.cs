using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neural_Network.Activation;

namespace SerializationTools.Activation
{
    internal class ActivationSerialization
    {
        internal static string SerializeActivation(activationFunction activate)
        {
            if (activate is sigmoid)
            {
                return "sigmoid";
            }
            if (activate is softmax)
            {
                return "softmax";
            }
            
            return "";
        }

        internal static activationFunction DeserializeActivation(string activate)
        {
            switch(activate)
            {
                case "sigmoid":
                    return new sigmoid();

                case "softmax":
                    return new softmax();


                default:
                    return new softmax();
            } 
        }
    }
}
