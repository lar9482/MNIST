using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network.MatrixLibrary.Utilities
{
    public static class Matrix_Utilities
    {
        public static Matrix getMatrixColumns(Matrix matrix, List<int> indices)
        {
            if (!indiceColumnCheck(0, matrix.cols-1, indices))
            {
                return null;
            }

            double[,] newData = new double[matrix.rows, indices.Count];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < indices.Count; j++)
                {
                    int currentColumn = indices[j];

                    newData[i, j] = matrix.data[i, currentColumn]; 
                }
            }

            return new Matrix(newData);
        }

        private static bool indiceColumnCheck(int first, int last, List<int> indices)
        {
            for (int i = 0; i < indices.Count; i++)
            {
                if (indices[i] < first || indices[i] > last)
                {
                    return false;
                }
            }

            if (indices.Count > (last-first))
            {
                return false;
            }
            return true;
        }
    }
}
