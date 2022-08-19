using System;
using System.Collections.Generic;
using System.Text;

namespace Neural_Network.Activation
{
    public class sigmoid : activationFunction 
    {
        public override double activate(double input)
        {
            return (double) ((1) / (1 + Math.Exp(-input)));
        }


        public override double activateDerivative(double input)
        {
            return (double) ((activate(input)) * (1 - activate(input)));
        }
    }
}
