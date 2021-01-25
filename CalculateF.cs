using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job4
{
    class CalculateF
    {
        //計算特徵1
        public double Feature1(double[] a, int[] aidx, double[] b, int[] bidx)
        {
            int i, j;
            double feature = 0, temp, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    temp = Math.Abs(a[i] - b[j]);
                    if (temp > 180)
                        temp = 360 - temp;
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }
                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //計算特徵2
        public double Feature2(double[] a, int[] aidx, double[] b, int[] bidx)
        {
            int i, j;
            double feature = 0, temp, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    temp = Math.Abs(a[i] - b[j]);
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }
                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //計算特徵3
        public double Feature3(double[] a, int[] aidx, double[] b, int[] bidx)
        {
            int i, j;
            double feature = 0, temp, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    temp = Math.Abs(a[i] - b[j]);
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }
                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //計算特徵4
        public double Feature4(int[] a, int[] aidx, int[] b, int[] bidx)
        {
            int i, j;
            double feature = 0, temp, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    temp = Math.Sqrt(((a[i * 4] - b[j * 4]) * (a[i * 4] - b[j * 4]))
                    + ((a[(i * 4) + 1] - b[(j * 4) + 1]) * (a[(i * 4) + 1] - b[(j * 4) + 1])));
                    temp += Math.Sqrt(((a[(i * 4) + 2] - b[(j * 4) + 2]) * (a[(i * 4) + 2] - b[(j * 4) + 2]))
                        + ((a[(i * 4) + 3] - b[(j * 4) + 3]) * (a[(i * 4) + 3] - b[(j * 4) + 3])));
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }
                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //計算特徵4-1
        public double Feature4_1(double[] a, int[] aidx, double[] b, int[] bidx)
        {
            int i, j;
            double feature = 0, temp, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    temp = Math.Sqrt(((a[i * 5] - b[j * 5]) * (a[i * 5] - b[j * 5]))
                    + ((a[(i * 5) + 1] - b[(j * 5) + 1]) * (a[(i * 5) + 1] - b[(j * 5) + 1])));
                    temp += Math.Sqrt(((a[(i * 5) + 2] - b[(j * 5) + 2]) * (a[(i * 5) + 2] - b[(j * 5) + 2]))
                        + ((a[(i * 5) + 3] - b[(j * 5) + 3]) * (a[(i * 5) + 3] - b[(j * 5) + 3])));
                    temp += Math.Abs(a[(i * 5) + 4] - b[(j * 5) + 4]);
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }
                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //計算特徵4-2
        public double Feature4_2(int[] a, int[] aidx, int[] b, int[] bidx)
        {
            int i, j;
            double feature = 0, temp, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            double wh = aidx.Length * bidx.Length;

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    temp = Math.Sqrt(((a[i * 4] - b[j * 4]) * (a[i * 4] - b[j * 4]))
                    + ((a[(i * 4) + 1] - b[(j * 4) + 1]) * (a[(i * 4) + 1] - b[(j * 4) + 1])));
                    temp += Math.Sqrt(((a[(i * 4) + 2] - b[(j * 4) + 2]) * (a[(i * 4) + 2] - b[(j * 4) + 2]))
                        + ((a[(i * 4) + 3] - b[(j * 4) + 3]) * (a[(i * 4) + 3] - b[(j * 4) + 3])));
                    temp *= (1 + (Math.Abs(((i + 1) * bidx.Length) - ((j + 1) * aidx.Length)) / wh));
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }
                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //計算特徵4-3
        public double Feature4_3(int[] a, double[] a2, int[] aidx, int[] b, double[] b2, int[] bidx)
        {
            int i, j;
            double feature = 0, temp, temp2, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            double wh = aidx.Length * bidx.Length;

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    temp = Math.Sqrt(((a[i * 4] - b[j * 4]) * (a[i * 4] - b[j * 4]))
                    + ((a[(i * 4) + 1] - b[(j * 4) + 1]) * (a[(i * 4) + 1] - b[(j * 4) + 1])));
                    temp += Math.Sqrt(((a[(i * 4) + 2] - b[(j * 4) + 2]) * (a[(i * 4) + 2] - b[(j * 4) + 2]))
                        + ((a[(i * 4) + 3] - b[(j * 4) + 3]) * (a[(i * 4) + 3] - b[(j * 4) + 3])));
                    temp2 = Math.Abs(a2[i] - b2[j]);
                    if (temp2 > 180) temp2 = 360 - temp2;
                    temp2 /= 40;
                    temp = temp * (1 + (Math.Abs(((i + 1) * bidx.Length) - ((j + 1) * aidx.Length)) / wh)) * (1 + temp2);
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }
                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //計算特徵4-4
        public double Feature4_4(int[] a, double[] a2, int[] aidx, int[] b, double[] b2, int[] bidx)
        {
            int i, j;
            double feature, temp, temp2, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            double wh = aidx.Length * bidx.Length;

            for (i = 0; i < aidx.Length; i++)
            {
                if (a[(i * 7) + 6] == 1)
                {
                    for (j = 0; j < bidx.Length; j++)
                    {
                        temp = Math.Sqrt(((a[i * 7] - b[j * 7]) * (a[i * 7] - b[j * 7]))
                            + ((a[(i * 7) + 1] - b[(j * 7) + 1]) * (a[(i * 7) + 1] - b[(j * 7) + 1])));
                        temp += Math.Sqrt(((a[(i * 7) + 2] - b[(j * 7) + 2]) * (a[(i * 7) + 2] - b[(j * 7) + 2]))
                            + ((a[(i * 7) + 3] - b[(j * 7) + 3]) * (a[(i * 7) + 3] - b[(j * 7) + 3])));
                        temp += Math.Sqrt(((a[(i * 7) + 4] - b[(j * 7) + 4]) * (a[(i * 7) + 4] - b[(j * 7) + 4]))
                            + ((a[(i * 7) + 5] - b[(j * 7) + 5]) * (a[(i * 7) + 5] - b[(j * 7) + 5])));
                        temp2 = Math.Abs(a2[i] - b2[j]);
                        if (temp2 > 180) temp2 = 360 - temp2;
                        temp2 /= 40;
                        temp = temp * (1 + (Math.Abs(((i + 1) * bidx.Length) - ((j + 1) * aidx.Length)) / wh)) * (1 + temp2);
                        table[i, j] = temp;

                        if (temp < rowMin || j == 0)
                            rowMin = temp;
                    }
                }
                else
                {
                    for (j = 0; j < bidx.Length; j++)
                    {
                        temp = Math.Sqrt(((a[i * 7] - b[j * 7]) * (a[i * 7] - b[j * 7]))
                            + ((a[(i * 7) + 1] - b[(j * 7) + 1]) * (a[(i * 7) + 1] - b[(j * 7) + 1])));
                        temp += Math.Sqrt(((a[(i * 7) + 2] - b[(j * 7) + 2]) * (a[(i * 7) + 2] - b[(j * 7) + 2]))
                            + ((a[(i * 7) + 3] - b[(j * 7) + 3]) * (a[(i * 7) + 3] - b[(j * 7) + 3])));
                        temp2 = Math.Abs(a2[i] - b2[j]);
                        if (temp2 > 180) temp2 = 360 - temp2;
                        temp2 /= 40;
                        temp = temp * (1 + (Math.Abs(((i + 1) * bidx.Length) - ((j + 1) * aidx.Length)) / wh)) * (1 + temp2);
                        table[i, j] = temp;

                        if (temp < rowMin || j == 0)
                            rowMin = temp;
                    }
                }

                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //計算特徵4-5
        public double Feature4_5(double[] a, double[] a2, int[] aidx, double[] b, double[] b2, int[] bidx, int magnification)
        {
            int i, j;
            double feature, temp, temp2, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            double wh = aidx.Length * bidx.Length;

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    temp = Math.Sqrt(((a[i * 7] - b[j * 7]) * (a[i * 7] - b[j * 7]))
                        + ((a[(i * 7) + 1] - b[(j * 7) + 1]) * (a[(i * 7) + 1] - b[(j * 7) + 1])));
                    temp += Math.Sqrt(((a[(i * 7) + 2] - b[(j * 7) + 2]) * (a[(i * 7) + 2] - b[(j * 7) + 2]))
                        + ((a[(i * 7) + 3] - b[(j * 7) + 3]) * (a[(i * 7) + 3] - b[(j * 7) + 3])));
                    temp += Math.Sqrt(((a[(i * 7) + 4] - b[(j * 7) + 4]) * (a[(i * 7) + 4] - b[(j * 7) + 4]))
                        + ((a[(i * 7) + 5] - b[(j * 7) + 5]) * (a[(i * 7) + 5] - b[(j * 7) + 5])))
                        * Math.Abs(a[(i * 7) + 6] - b[(j * 7) + 6]) * (a[(i * 7) + 6] + b[(j * 7) + 6]) * magnification;
                    temp2 = Math.Abs(a2[i] - b2[j]);
                    if (temp2 > 180) temp2 = 360 - temp2;
                    temp2 /= 40;
                    temp = temp * (1 + (Math.Abs(((i + 1) * bidx.Length) - ((j + 1) * aidx.Length)) / wh)) * (1 + temp2);
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }

                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //計算特徵4-6
        public double Feature4_6(double[] a, double[] a2, int[] aidx, double[] b, double[] b2, int[] bidx, int magnification)
        {
            int i, j, dL = 9; //dL 資料長度 
            double feature, temp, temp2, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            double wh = aidx.Length * bidx.Length;

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    temp = Math.Sqrt(((a[i * dL] - b[j * dL]) * (a[i * dL] - b[j * dL]))
                        + ((a[(i * dL) + 1] - b[(j * dL) + 1]) * (a[(i * dL) + 1] - b[(j * dL) + 1])));
                    temp += Math.Sqrt(((a[(i * dL) + 2] - b[(j * dL) + 2]) * (a[(i * dL) + 2] - b[(j * dL) + 2]))
                        + ((a[(i * dL) + 3] - b[(j * dL) + 3]) * (a[(i * dL) + 3] - b[(j * dL) + 3])));
                    temp += Math.Sqrt(((a[(i * dL) + 4] - b[(j * dL) + 4]) * (a[(i * dL) + 4] - b[(j * dL) + 4]))
                        + ((a[(i * dL) + 5] - b[(j * dL) + 5]) * (a[(i * dL) + 5] - b[(j * dL) + 5])))
                        * Math.Abs((a[(i * dL) + 7] / a[(i * dL) + 8]) - (b[(j * dL) + 7] / b[(j * dL) + 8])) * magnification;
                    temp2 = Math.Abs(a2[i] - b2[j]);
                    if (temp2 > 180) temp2 = 360 - temp2;
                    temp2 /= 40;
                    temp = temp * (1 + (Math.Abs(((i + 1) * bidx.Length) - ((j + 1) * aidx.Length)) / wh)) * (1 + temp2);
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }

                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //計算特徵4-6m
        public double Feature4_6ma(double[] a, double[] a2, int[] aidx, double[] b, double[] b2, int[] bidx, int magnification)
        {
            int i, j, dL = 9; //dL 資料長度 
            double feature, temp, temp2, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            double wh = aidx.Length * bidx.Length;

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    temp = Math.Sqrt(((a[i * dL] - b[j * dL]) * (a[i * dL] - b[j * dL]))
                        + ((a[(i * dL) + 1] - b[(j * dL) + 1]) * (a[(i * dL) + 1] - b[(j * dL) + 1])));
                    temp += Math.Sqrt(((a[(i * dL) + 2] - b[(j * dL) + 2]) * (a[(i * dL) + 2] - b[(j * dL) + 2]))
                        + ((a[(i * dL) + 3] - b[(j * dL) + 3]) * (a[(i * dL) + 3] - b[(j * dL) + 3])));
                    temp += Math.Sqrt(((a[(i * dL) + 4] - b[(j * dL) + 4]) * (a[(i * dL) + 4] - b[(j * dL) + 4]))
                        + ((a[(i * dL) + 5] - b[(j * dL) + 5]) * (a[(i * dL) + 5] - b[(j * dL) + 5])))
                        * Math.Abs((a[(i * dL) + 7] / a[(i * dL) + 8]) - (b[(j * dL) + 7] / b[(j * dL) + 8]))
                        * ((a[(i * dL) + 7] / a[(i * dL) + 8]) + (b[(j * dL) + 7] / b[(j * dL) + 8]))
                        * magnification;
                    temp2 = Math.Abs(a2[i] - b2[j]);
                    if (temp2 > 180) temp2 = 360 - temp2;
                    temp2 /= 40;
                    temp = temp * (1 + (Math.Abs(((i + 1) * bidx.Length) - ((j + 1) * aidx.Length)) / wh)) * (1 + temp2);
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }

                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        public double Feature4_8(double[] a, double[] a2, int[] aidx, double[] b, double[] b2, int[] bidx, double magnification)
        {
            int i, j, dL = 9; //dL 資料長度 
            
            double feature, aAngle, temp, temp1, temp2, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            double wh = aidx.Length * bidx.Length;

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    temp = Math.Sqrt(((a[i * dL] - b[j * dL]) * (a[i * dL] - b[j * dL]))
                        + ((a[(i * dL) + 1] - b[(j * dL) + 1]) * (a[(i * dL) + 1] - b[(j * dL) + 1])));
                    temp += Math.Sqrt(((a[(i * dL) + 2] - b[(j * dL) + 2]) * (a[(i * dL) + 2] - b[(j * dL) + 2]))
                        + ((a[(i * dL) + 3] - b[(j * dL) + 3]) * (a[(i * dL) + 3] - b[(j * dL) + 3])));
                    temp += Math.Sqrt(((a[(i * dL) + 4] - b[(j * dL) + 4]) * (a[(i * dL) + 4] - b[(j * dL) + 4]))
                        + ((a[(i * dL) + 5] - b[(j * dL) + 5]) * (a[(i * dL) + 5] - b[(j * dL) + 5])))
                        * Math.Abs((a[(i * dL) + 7] / a[(i * dL) + 8]) - (b[(j * dL) + 7] / b[(j * dL) + 8])) * magnification;
                    temp2 = Math.Abs(a2[i] - b2[j]);
                    if (temp2 > 180) temp2 = 360 - temp2;
                    temp2 /= 40;
                    temp = temp * (1 + temp2);

                    // 反轉

                    temp1 = Math.Sqrt(((a[(i * dL) + 1] - b[j * dL]) * (a[(i * dL) + 1] - b[j * dL]))
                        + ((a[i * dL] - b[(j * dL) + 1]) * (a[i * dL] - b[(j * dL) + 1])));
                    temp1 += Math.Sqrt(((a[(i * dL) + 3] - b[(j * dL) + 2]) * (a[(i * dL) + 3] - b[(j * dL) + 2]))
                        + ((a[(i * dL) + 2] - b[(j * dL) + 3]) * (a[(i * dL) + 2] - b[(j * dL) + 3])));
                    temp1 += Math.Sqrt(((a[(i * dL) + 5] - b[(j * dL) + 4]) * (a[(i * dL) + 5] - b[(j * dL) + 4]))
                        + ((a[(i * dL) + 4] - b[(j * dL) + 5]) * (a[(i * dL) + 4] - b[(j * dL) + 5])))
                        * Math.Abs((a[(i * dL) + 7] / a[(i * dL) + 8]) - (b[(j * dL) + 7] / b[(j * dL) + 8])) * 5;

                    if (a2[i] > 0)
                        aAngle = a2[i] - 180;
                    else
                        aAngle = 180 + a2[i];

                    temp2 = Math.Abs(aAngle - b2[j]);
                    if (temp2 > 180) temp2 = 360 - temp2;
                    temp2 /= 40;
                    temp1 = temp1 * (1 + temp2) * 2;

                    ///

                    if (temp1 < temp)
                        temp = temp1;

                    temp = temp * (1 + (Math.Abs(((i + 1) * bidx.Length) - ((j + 1) * aidx.Length)) / wh));
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }

                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }

        //計算特徵5
        public double Feature5(double[] a, int[] aidx, double[] b, int[] bidx)
        {
            int i, j;
            double feature = 0, temp, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    double x = a[(i * 4) + 2] - b[(j * 4) + 2];
                    double y = a[(i * 4) + 3] - b[(j * 4) + 3];
                    double Aa = Math.Abs(a[(i * 4)] - b[(j * 4)]);
                    double Ab = Math.Abs(a[(i * 4) + 1] - b[(j * 4) + 1]);
                    temp = Math.Sqrt((x * x) + (y * y)) *
                        (Aa + Ab);
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }
                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //計算特徵5s
        public double Feature5s(double[] a, int[] aidx, double[] b, int[] bidx)
        {
            int i, j;
            double feature = 0, temp, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[aidx.Length, bidx.Length];

            for (i = 0; i < aidx.Length; i++)
            {
                for (j = 0; j < bidx.Length; j++)
                {
                    double x = a[(i * 4) + 2] - b[(j * 4) + 2];
                    double y = a[(i * 4) + 3] - b[(j * 4) + 3];
                    double Aa = Math.Abs(a[(i * 4)] - b[(j * 4)]);
                    double Ab = Math.Abs(a[(i * 4) + 1] - b[(j * 4) + 1]);
                    temp = Math.Sqrt(Math.Sqrt((x * x) + (y * y)) *
                        (Aa + Ab));
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }
                rowSum += rowMin;
            }
            rowValue = rowSum / aidx.Length;

            for (i = 0; i < bidx.Length; i++)
            {
                for (j = 0; j < aidx.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / bidx.Length;

            feature = rowValue + colValue;
            return feature;
        }
        //手寫板 計算特徵5s
        public double Calculate_Feature4_6(double[] a, double[] a2, double[] b, double[] b2)
        {
            int i, j, dL = 9; //dL 資料長度 
            double feature, temp, temp2, rowMin = 0, rowSum = 0, rowValue, colMin = 0, colSum = 0, colValue;
            double[,] table = new double[a2.Length, b2.Length];

            double wh = a2.Length * b2.Length;

            for (i = 0; i < a2.Length; i++)
            {
                for (j = 0; j < b2.Length; j++)
                {
                    temp = Math.Sqrt(((a[i * dL] - b[j * dL]) * (a[i * dL] - b[j * dL]))
                        + ((a[(i * dL) + 1] - b[(j * dL) + 1]) * (a[(i * dL) + 1] - b[(j * dL) + 1])));
                    temp += Math.Sqrt(((a[(i * dL) + 2] - b[(j * dL) + 2]) * (a[(i * dL) + 2] - b[(j * dL) + 2]))
                        + ((a[(i * dL) + 3] - b[(j * dL) + 3]) * (a[(i * dL) + 3] - b[(j * dL) + 3])));
                    temp += Math.Sqrt(((a[(i * dL) + 4] - b[(j * dL) + 4]) * (a[(i * dL) + 4] - b[(j * dL) + 4]))
                        + ((a[(i * dL) + 5] - b[(j * dL) + 5]) * (a[(i * dL) + 5] - b[(j * dL) + 5])))
                        * Math.Abs((a[(i * dL) + 7] / a[(i * dL) + 8]) - (b[(j * dL) + 7] / b[(j * dL) + 8])) * 5;
                    temp2 = Math.Abs(a2[i] - b2[j]);
                    if (temp2 > 180) temp2 = 360 - temp2;
                    temp2 /= 75;
                    temp = temp * (1 + (Math.Abs(((i + 1) * b2.Length) - ((j + 1) * a2.Length)) / wh)) * (1 + temp2);
                    table[i, j] = temp;

                    if (temp < rowMin || j == 0)
                        rowMin = temp;
                }

                rowSum += rowMin;
            }
            rowValue = rowSum / a2.Length;

            for (i = 0; i < b2.Length; i++)
            {
                for (j = 0; j < a2.Length; j++)
                {
                    if (table[j, i] < colMin || j == 0)
                        colMin = table[j, i];
                }
                colSum += colMin;
            }
            colValue = colSum / b2.Length;

            feature = rowValue + colValue;
            return feature;
        }
    }
}
