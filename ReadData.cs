using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job4
{
    class ReadData
    {
        public List<int[]> RintArr(string sourcePath)
        {
            string str;

            List<int[]> strokeRage = new List<int[]>();

            StreamReader file = new StreamReader(sourcePath);
            while ((str = file.ReadLine()) != null)
            {
                strokeRage.Add(Array.ConvertAll(str.Split(','), int.Parse));
            }

            return strokeRage;
        }
        public List<int[]>RintArr_noTI(string sourcePath, int rage)
        {
            int i = 1;
            string str;

            List<int[]> strokeRage = new List<int[]>();

            StreamReader file = new StreamReader(sourcePath);
            while ((str = file.ReadLine()) != null)
            {
                strokeRage.Add(new int[] { i, (i - rage), (i + rage) });
                i++;
            }

            return strokeRage;
        }

        ///讀入資料(四面) 4-Loop [strokes][23][四面][index]
        public List<List<List<int[]>>> RfourDirection(string sourcePath)
        {
            int i, j, n;
            string str;

            StreamReader file;
            List<List<List<int[]>>> featureData = new List<List<List<int[]>>>();
            foreach (string filePath in Directory.GetFileSystemEntries(sourcePath, "*.txt"))
            {
                int needCapacity = int.Parse(Path.GetFileNameWithoutExtension(filePath));
                if (featureData.Count < needCapacity)
                {
                    int difference = needCapacity - featureData.Count;
                    for (i = 0; i < difference; i++)
                        featureData.Add(new List<List<int[]>>());
                }

                file = new StreamReader(filePath);
                str = file.ReadLine();
                j = 0;
                n = 0;
                while (str != null)
                {
                    featureData[needCapacity - 1].Add(new List<int[]>());
                    for (i = 0; i < 4; i++)
                    {
                        featureData[needCapacity - 1][j].Add(Array.ConvertAll(str.Split(','), int.Parse));
                        str = file.ReadLine();
                        n++;
                    }
                    j++;
                }
            }

            return featureData;
        }

        public List<List<List<int[]>>> RnoFourDirection(string sourcePath)
        {
            int i, j;
            string str;

            StreamReader file;
            List<List<List<int[]>>> featureData = new List<List<List<int[]>>>();
            foreach (string filePath in Directory.GetFileSystemEntries(sourcePath, "*.txt"))
            {
                int needCapacity = int.Parse(Path.GetFileNameWithoutExtension(filePath));
                if (featureData.Count < needCapacity)
                {
                    int difference = needCapacity - featureData.Count;
                    for (i = 0; i < difference; i++)
                        featureData.Add(new List<List<int[]>>());
                }

                file = new StreamReader(filePath);
                str = file.ReadLine();
                j = 0;
                while (str != null)
                {
                    featureData[needCapacity - 1].Add(new List<int[]>());
                    featureData[needCapacity - 1][j].Add(Enumerable.Range(0, needCapacity).ToArray<int>());
                    for (i = 0; i < 4; i++)
                        str = file.ReadLine();
                    j++;
                }
            }

            return featureData;
        }


        //讀入資料(string) 2-Loop [strokes][23]str
        public List<List<string>> Rstring(string sourcePath)
        {
            int i;
            string str;

            StreamReader file;
            List<List<string>> featureData = new List<List<string>>();
            foreach (string filePath in Directory.GetFileSystemEntries(sourcePath, "*.txt"))
            {
                int needCapacity = int.Parse(Path.GetFileNameWithoutExtension(filePath));
                if (featureData.Count < needCapacity)
                {
                    int difference = needCapacity - featureData.Count;
                    for (i = 0; i < difference; i++)
                        featureData.Add(new List<string>());
                }

                file = new StreamReader(filePath);
                while ((str = file.ReadLine()) != null)
                {
                    featureData[needCapacity - 1].Add(str);
                }
            }

            return featureData;
        }

        //讀入資料(double[]) 3-Loop [strokes][23][index]
        public List<List<double[]>> Rdouble(string sourcePath)
        {
            int i;
            string str;

            StreamReader file;
            List<List<double[]>> featureData = new List<List<double[]>>();
            foreach (string filePath in Directory.GetFileSystemEntries(sourcePath, "*.txt"))
            {
                int needCapacity = int.Parse(Path.GetFileNameWithoutExtension(filePath));
                if (featureData.Count < needCapacity)
                {
                    int difference = needCapacity - featureData.Count;
                    for (i = 0; i < difference; i++)
                        featureData.Add(new List<double[]>());
                }

                file = new StreamReader(filePath);
                while ((str = file.ReadLine()) != null)
                {
                    featureData[needCapacity - 1].Add(Array.ConvertAll(str.Split(','), Double.Parse));
                }
            }

            return featureData;
        }

        //讀入資料(double[]) 2-Loop [strokes][23][index]
        public List<double[]> R2double(string sourcePath)
        {
            int i;
            string str;

            StreamReader file;
            List<double[]> featureData = new List<double[]>();
            file = new StreamReader(sourcePath);
            while ((str = file.ReadLine()) != null)
            {
                featureData.Add(Array.ConvertAll(str.Split(','), Double.Parse));
            }

            return featureData;
        }

        //讀入資料(int[]) 3-Loop [strokes][23][index]
        public List<List<int[]>> Rint(string sourcePath)
        {
            int i;
            string str;            

            StreamReader file;
            List<List<int[]>> featureData = new List<List<int[]>>();
            foreach (string filePath in Directory.GetFileSystemEntries(sourcePath, "*.txt"))
            {
                int needCapacity = int.Parse(Path.GetFileNameWithoutExtension(filePath));
                if (featureData.Count < needCapacity)
                {
                    int difference = needCapacity - featureData.Count;
                    for (i = 0; i < difference; i++)
                        featureData.Add(new List<int[]>());
                }

                file = new StreamReader(filePath);
                while ((str = file.ReadLine()) != null)
                {
                    featureData[needCapacity - 1].Add(Array.ConvertAll(str.Split(','), int.Parse));
                }
            }

            return featureData;
        }
    }
}
