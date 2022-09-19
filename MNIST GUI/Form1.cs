using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using Neural_Network.MatrixLibrary;
using Neural_Network.MatrixLibrary.Utilities;
using Neural_Network.Network.FeedForward;
using SerializationTools.Network.FeedForward;

namespace MNIST_GUI
{
    public partial class Form1 : Form
    {
        private const int numCells = 28;
        private const int gridWidthUpdate = 5;

        private const int gridSlope = 50;
        private const int gridConstant = 55;

        private static string basePath = System.IO.Path.GetFullPath(@"..\..\");
        private const string networkFile = "MNIST_NeuralNetwork_100.json";

        private double[,] data;

        private int cellWidth;
        private int cellHeight;

        private bool drawingWithMouse = false;
        private List<Point> usedPoints;

        private FeedForward_Network network;
        
        public Form1()
        {
            InitializeComponent();

            initializeData();
            updateDataDimension();

            network = FeedForward_Network_Object.loadObject(String.Join("\\", new string[] { basePath, networkFile }));
        }

        private void predictButton_Click(object sender, EventArgs e)
        {
            Matrix dataMatrix = new Matrix(data);
            dataMatrix = dataMatrix.flatten();
            dataMatrix = dataMatrix.scalarMultiply(-1);
            dataMatrix = dataMatrix.scalarAdd(255);
            textBox1.Text = "test";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            for (int i = 0; i < numCells; i++)
            {
                for (int j = 0; j < numCells; j++)
                {
                    SolidBrush brush = new SolidBrush(Color.FromArgb((int)data[i, j], (int)data[i, j], (int)data[i, j]));
                    //Pen pen = new Pen(Color.White);
                    Rectangle newCell = new Rectangle(cellWidth * i, cellHeight * j, cellWidth, cellHeight);

                    e.Graphics.FillRectangle(brush, newCell);
                    //e.Graphics.DrawRectangle(pen, newCell);
                }
            }
        }

        private void Form1_ResizeEnd(Object sender, EventArgs e)
        {
            updateDataDimension();
            updatePredictButtonLocation();
            updateTextBoxLocation();
            Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void initializeData()
        {
            data = new double[numCells, numCells];
            for (int i = 0; i < numCells; i++)
            {
                for (int j = 0; j < numCells; j++)
                {
                    data[i, j] = 255;
                }
            }
        }

        private void updateDataDimension()
        {
            cellWidth = (this.Width / numCells);
            cellHeight = ((int) (0.75 * this.Height)) / numCells;
        }

        private void updatePredictButtonLocation()
        {
            int currentWidth = this.predictButton.Width;
            int newHeight = (int) (0.75 * this.Height);


            this.predictButton.Location = new Point((this.Width/2) - (currentWidth/2), newHeight);
        }
        private void updateTextBoxLocation()
        {
            
            int currentWidth = this.textBox1.Width;

            this.textBox1.Location = new Point((this.Width / 2) - (currentWidth / 2), 
                                                this.predictButton.Location.Y + this.predictButton.Height);
        }

        private void updateData(int row, int column)
        {
            
            if ((row < 0 || row >= numCells) || (column < 0 || column >= numCells)) { return; }
            int gridRadius = (int) gridWidthUpdate / 2;
            for (int i = row - gridRadius; i <= row+ gridRadius; i++)
            {
                for (int j = column- gridRadius; j <= column+ gridRadius; j++)
                {
                    if ((i < 0 || i >= numCells) || (j < 0 || j >= numCells)) { continue; }

                    if (i == row && j == column)
                    {
                        Console.WriteLine();
                    }
                    int newData = 0;
                    if ((int)data[i, j] == 255)
                    {
                        int rowAggregate = gridSlope * Math.Abs(i - row) + gridConstant;
                        int columnAggregate = gridSlope * Math.Abs(j - column) + gridConstant;

                        newData = rowAggregate + columnAggregate;
                    }
                    else
                    {
                        newData = (int)data[i, j] - 55;
                    }
                    
                    if (newData > 255)
                    {
                        data[i, j] = 255;
                    }
                    else if (newData < 0)
                    {
                        data[i, j] = 0;
                    }
                    else
                    {
                        data[i, j] = newData;
                    }
                }
            }
            
        }

        private void Form1_MouseDownEvent(object sender, MouseEventArgs e)
        {
            drawingWithMouse = true;
            usedPoints = new List<Point>();
        }

        private void Form1_MouseUpEvent(object sender, MouseEventArgs e)
        {
            drawingWithMouse = false;
            usedPoints.Clear();
        }

        private void Form1_MouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (drawingWithMouse)
            {
                Point mousePoint = PointToClient(MousePosition);

                int x = mousePoint.X;
                int y = mousePoint.Y;

                int baseWidth = (this.Width) - (this.Width % numCells);
                int baseDifference = (baseWidth / numCells);

                int row = (x - (x % baseDifference)) / baseDifference;
                int col = (y - (y % cellHeight)) / (cellHeight);

                Point usedPoint = new Point(row, col);
                
                if (!usedPoints.Contains(usedPoint))
                {
                    updateData(row, col);
                    usedPoints.Add(usedPoint);
                }


                //updateData(row, col);
                Refresh();
              
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
