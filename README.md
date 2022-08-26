# Best network committed (as of 8/26/2022)
### Architecture
   - Feedforward
   
### Input Layer
   - X Dimension: 724
   - Y Dimension: 100
   
### First Hidden layer
   - X Dimension: 128
   - Y Dimension: 100
   - Learning Rate: 0.1
   - Activation: Sigmoid
   - Learning Algorithm: Stochastic Gradient Descent
   
### Output Layer
   - X Dimension: 10
   - Y Dimension: 100
   - Learning Rate: 0.1
   - Activation: Softmax
   - Error: Cross Entropy
   - Learning Algorithm: Stochastic Gradient Descent

### Training strategy
   - Batch Size: 1
   - Trained the network on 60,000 training images of 28*28 pixels with one-hot encoded labels
   - Training using 10 epochs
 
### Performance
   - Reported error: Around 84% accuracy on testing images.
