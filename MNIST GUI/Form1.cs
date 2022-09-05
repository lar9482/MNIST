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
using SerializationTools.Network.FeedForward;

namespace MNIST_GUI
{
    public partial class Form1 : Form
    {
        private const int numCells = 28;
        private double[,] data;
        
        private int cellWidth;
        private int cellHeight;

        private bool drawingWithMouse = false;
        
        public Form1()
        {
            InitializeComponent();

            initializeData();
            updateDataDimension();
        }

        private void predictButton_Click(object sender, EventArgs e)
        {
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

            Console.WriteLine(cellWidth);
            Console.WriteLine(this.Width);
            Console.WriteLine(this.Width % numCells);
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
            
            if ((row < 0 || row >= numCells) && (column < 0 || column >= numCells)) { return; }

            for (int i = row-5; i <= row+5; i++)
            {
                for (int j = column-5; j <= column+5; j++)
                {
                    if ((i < 0 || i >= numCells) && (j < 0 || j >= numCells)) { continue; }

                    int rowDifference = Math.Abs(row - i);
                    int columnDifference = Math.Abs(column - j);

                    int previousData = (int) data[row, column];

                }
            }
            data[row, column] = 0;
            
        }

        private void Form1_MouseDownEvent(object sender, MouseEventArgs e)
        {
            drawingWithMouse = true;
        }

        private void Form1_MouseUpEvent(object sender, MouseEventArgs e)
        {
            drawingWithMouse = false;
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

                updateData(row, col);
                Refresh();
                
                Console.WriteLine(row + ", " + col);
                Console.WriteLine();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
