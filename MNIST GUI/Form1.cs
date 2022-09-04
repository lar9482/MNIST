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

namespace MNIST_GUI
{
    public partial class Form1 : Form
    {
        private const int numCells = 28;
        private int[,] data;

        private int cellWidth;
        private int cellHeight;
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
                    SolidBrush brush = new SolidBrush(Color.FromArgb(data[i, j], data[i, j], data[i, j]));
                    Pen pen = new Pen(Color.White);
                    Rectangle newCell = new Rectangle(cellWidth * i, cellHeight * j, cellWidth, cellHeight);

                    e.Graphics.FillRectangle(brush, newCell);
                    e.Graphics.DrawRectangle(pen, newCell);
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

        private void Form1_MouseDown(Object sender, MouseEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void initializeData()
        {
            data = new int[numCells, numCells];
            for (int i = 0; i < numCells; i++)
            {
                for (int j = 0; j < numCells; j++)
                {
                    data[i, j] = 0;
                }
            }
        }

        private void updateDataDimension()
        {
            cellWidth = this.Width / numCells;
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

        }
    }
}
