using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteWPF
{
    public static class Estatica
    {
        public static ServiceReference1.Ficha[][] ConvertirMatriz<T>(ServiceReference1.Ficha[,] matriz)
        {
            var cols = matriz.GetLength(0);
            var rows = matriz.GetLength(1);
            var jArray = new ServiceReference1.Ficha[cols][];
            for (int i = 0; i < cols; i++)
            {
                jArray[i] = new ServiceReference1.Ficha[rows];
                for (int j = 0; j < rows; j++)
                {
                    jArray[i][j] = matriz[i, j];
                }
            }
            return jArray;
        }

        public static T[,] AMultiDim<T>(T[][] jArray)
        {
            int i = jArray.Count();
            int j = jArray.Select(x => x.Count()).Aggregate(0, (current, c) => (current > c) ? current : c);


            var mArray = new T[i, j];

            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    mArray[ii, jj] = jArray[ii][jj];
                }
            }

            return mArray;
        }
    }
}
