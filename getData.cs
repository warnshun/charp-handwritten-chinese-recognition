using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job4
{
    class GetData
    {
        //全域
        DirectoryMethod dm = new DirectoryMethod();
        

        //取編號名稱
        public void Name(List<List<List<string>>> data, string savePath, string sampleNamePath)
        {
            int i, j, k, l;
            string str, samplePath, testPath;
            List<List<int>> strokesData = new List<List<int>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";

            //確保儲存位置
            dm.DeleteAndCreate(samplePath); //創立-刪除 save資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 save資料夾

            for (i = 0; i < data.Count; i++)
            {
                strokesData.Add(new List<int>());
                for (j = 0; j < data[i].Count; j++)
                {
                    if (data[i][j].Count != 0)
                    {
                        l = 1;
                        for (k = 2; k < data[i][j].Count; k++)
                        {
                            if (data[i][j][k].Substring(6, 2) != data[i][j][k - 1].Substring(6, 2))
                                l++;
                        }
                        strokesData[i].Add(l);
                    }
                    else
                        strokesData[i].Add(0);
                }
            }

            int number;
            List<List<string>> sampleData = new List<List<string>>();
            List<List<string>> testData = new List<List<string>>();
            StreamReader file = new StreamReader(sampleNamePath);
            for (i = 0; i < strokesData.Count; i++)
            {
                str = file.ReadLine();
                number = int.Parse(str.Split('_')[1]) - 1;
                for (j = 0; j < strokesData[i].Count; j++)
                {
                    if (strokesData[i][j] != 0)
                    {
                        if (j == number)
                        {
                            if (sampleData.Count < strokesData[i][j])
                            {
                                int needCapacity = strokesData[i][j] - sampleData.Count;
                                for (k = 0; k < needCapacity; k++)
                                    sampleData.Add(new List<string>());
                            }
                            sampleData[strokesData[i][j] - 1].Add((i + 1) + "_" + (j + 1));
                        }
                        else
                        {
                            if (testData.Count < strokesData[i][j])
                            {
                                int needCapacity = strokesData[i][j] - testData.Count;
                                for (k = 0; k < needCapacity; k++)
                                    testData.Add(new List<string>());
                            }
                            testData[strokesData[i][j] - 1].Add((i + 1) + "_" + (j + 1));
                        }
                    }
                }
            }

            for (i = 0; i < sampleData.Count; i++)
            {
                if (sampleData[i].Count != 0)
                {
                    using (StreamWriter sw = File.CreateText(samplePath + (i + 1) + ".txt"))
                    {
                        for (j = 0; j < sampleData[i].Count; j++)
                        {
                            sw.WriteLine(sampleData[i][j]);
                        }
                    }
                }
            }
            for (i = 0; i < testData.Count; i++)
            {
                if (testData[i].Count != 0)
                {
                    using (StreamWriter sw = File.CreateText(testPath + (i + 1) + ".txt"))
                    {
                        for (j = 0; j < testData[i].Count; j++)
                        {
                            sw.WriteLine(testData[i][j]);
                        }
                    }
                }
            }

        }
        //取四面資料
        public void FourDirection(List<List<List<string>>> data, string savePath, string nameDirPath)
        {
            int i, j, k;
            string str, strUp, samplePath, testPath, sampleNamePath, testNamePath, strDown, strLeft, strRight;
            List<List<List<int>>> upList = new List<List<List<int>>>();
            List<List<List<int>>> downList = new List<List<List<int>>>();
            List<List<List<int>>> leftList = new List<List<List<int>>>();
            List<List<List<int>>> rightList = new List<List<List<int>>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";
            sampleNamePath = nameDirPath + @"sample\";
            testNamePath = nameDirPath + @"test\";

            dm.DeleteAndCreate(samplePath); //創立-刪除 Feature-FourDirection-sample資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 Feature-FourDirection-test資料夾

            for (i = 0; i < data.Count; i++)
            {
                upList.Add(new List<List<int>>());
                downList.Add(new List<List<int>>());
                leftList.Add(new List<List<int>>());
                rightList.Add(new List<List<int>>());
                for (j = 0; j < data[i].Count; j++)
                {
                    upList[i].Add(new List<int>());
                    downList[i].Add(new List<int>());
                    leftList[i].Add(new List<int>());
                    rightList[i].Add(new List<int>());
                    if (data[i][j].Count != 0)
                    {
                        List<int> headIndex = new List<int>();
                        headIndex.Add(1);
                        for (k = 2; k < data[i][j].Count; k++)
                        {
                            if (data[i][j][k].Substring(6, 2) != data[i][j][k - 1].Substring(6, 2))
                            {
                                headIndex.Add(k);
                            }
                        }
                        headIndex.Add(k);

                        int l = 1, x, y, up, down, left, right;
                        for (k = 1; k < headIndex.Count; k++)
                        {
                            up = left = int.MaxValue;
                            down = right = int.MinValue;
                            while (l < headIndex[k])
                            {
                                x = int.Parse(data[i][j][l].Substring(0, 3));
                                y = int.Parse(data[i][j][l].Substring(3, 3));

                                if (y < up)
                                    up = y;
                                if (y > down)
                                    down = y;
                                if (x < left)
                                    left = x;
                                if (x > right)
                                    right = x;
                                l++;
                            }
                            upList[i][j].Add(up);
                            downList[i][j].Add(down);
                            leftList[i][j].Add(left);
                            rightList[i][j].Add(right);
                        }
                    }
                }
            }

            //寫入txt
            StreamReader file;
            foreach (string filePath in Directory.GetFileSystemEntries(sampleNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(samplePath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        var sorted = upList[number[0] - 1][number[1] - 1]
                                    .Select((x, y) => new KeyValuePair<int, int>(x, y))
                                    .OrderBy(x => x.Key)
                                    .ToList();
                        List<int> upIndex = sorted.Select(x => x.Value).ToList();
                        sorted = downList[number[0] - 1][number[1] - 1]
                            .Select((x, y) => new KeyValuePair<int, int>(x, y))
                            .OrderBy(x => x.Key)
                            .ToList();
                        List<int> downIndex = sorted.Select(x => x.Value).ToList();
                        sorted = leftList[number[0] - 1][number[1] - 1]
                            .Select((x, y) => new KeyValuePair<int, int>(x, y))
                            .OrderBy(x => x.Key)
                            .ToList();
                        List<int> leftIndex = sorted.Select(x => x.Value).ToList();
                        sorted = rightList[number[0] - 1][number[1] - 1]
                            .Select((x, y) => new KeyValuePair<int, int>(x, y))
                            .OrderBy(x => x.Key)
                            .ToList();
                        List<int> rightIndex = sorted.Select(x => x.Value).ToList();

                        strUp = strDown = strLeft = strRight = null;
                        for (i = 0; i < upList[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (strUp != null)
                            {
                                strUp += ",";
                                strLeft += ",";
                            }
                            strUp += upIndex[i];
                            strLeft += leftIndex[i];
                        }
                        for (i = downList[number[0] - 1][number[1] - 1].Count - 1; i >= 0; i--)
                        {
                            if (strDown != null)
                            {
                                strDown += ",";
                                strRight += ",";
                            }
                            strDown += downIndex[i];
                            strRight += rightIndex[i];
                        }
                        sw.WriteLine(strUp);
                        sw.WriteLine(strDown);
                        sw.WriteLine(strLeft);
                        sw.WriteLine(strRight);
                    }
                }

            }
            foreach (string filePath in Directory.GetFileSystemEntries(testNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(testPath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        var sorted = upList[number[0] - 1][number[1] - 1]
                                    .Select((x, y) => new KeyValuePair<int, int>(x, y))
                                    .OrderBy(x => x.Key)
                                    .ToList();
                        List<int> upIndex = sorted.Select(x => x.Value).ToList();
                        sorted = downList[number[0] - 1][number[1] - 1]
                            .Select((x, y) => new KeyValuePair<int, int>(x, y))
                            .OrderBy(x => x.Key)
                            .ToList();
                        List<int> downIndex = sorted.Select(x => x.Value).ToList();
                        sorted = leftList[number[0] - 1][number[1] - 1]
                            .Select((x, y) => new KeyValuePair<int, int>(x, y))
                            .OrderBy(x => x.Key)
                            .ToList();
                        List<int> leftIndex = sorted.Select(x => x.Value).ToList();
                        sorted = rightList[number[0] - 1][number[1] - 1]
                            .Select((x, y) => new KeyValuePair<int, int>(x, y))
                            .OrderBy(x => x.Key)
                            .ToList();
                        List<int> rightIndex = sorted.Select(x => x.Value).ToList();

                        strUp = strDown = strLeft = strRight = null;
                        for (i = 0; i < upList[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (strUp != null)
                            {
                                strUp += ",";
                                strLeft += ",";
                            }
                            strUp += upIndex[i];
                            strLeft += leftIndex[i];
                        }
                        for (i = downList[number[0] - 1][number[1] - 1].Count - 1; i >= 0; i--)
                        {
                            if (strDown != null)
                            {
                                strDown += ",";
                                strRight += ",";
                            }
                            strDown += downIndex[i];
                            strRight += rightIndex[i];
                        }
                        sw.WriteLine(strUp);
                        sw.WriteLine(strDown);
                        sw.WriteLine(strLeft);
                        sw.WriteLine(strRight);
                    }
                }
            }
        }

        //取特徵1
        public void Feature1(List<List<List<string>>> data, string savePath, string nameDirPath)
        {
            int i, j, k;
            string str, samplePath, testPath, sampleNamePath, testNamePath, forWBPath;
            List<List<List<double>>> angleData = new List<List<List<double>>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";
            sampleNamePath = nameDirPath + @"sample\";
            testNamePath = nameDirPath + @"test\";
            forWBPath = savePath + @"forWB\";
            

            dm.DeleteAndCreate(samplePath); //創立-刪除 Feature-Feature1-sample資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 Feature-Feature1-test資料夾
            dm.DeleteAndCreate(forWBPath); //創立-刪除 Feature-Feature4_6-forWB資料夾

            for (i = 0; i < data.Count; i++)
            {
                angleData.Add(new List<List<double>>());
                for (j = 0; j < data[i].Count; j++)
                {
                    angleData[i].Add(new List<double>());
                    if (data[i][j].Count != 0)
                    {
                        List<int> headIndex = new List<int>();
                        headIndex.Add(1);
                        for (k = 2; k < data[i][j].Count; k++)
                        {
                            if (data[i][j][k].Substring(6, 2) != data[i][j][k - 1].Substring(6, 2))
                            {
                                headIndex.Add(k);
                            }
                        }
                        headIndex.Add(k);
                        double hx, hy, tx, ty;
                        for (k = 1; k < headIndex.Count; k++)
                        {
                            hx = double.Parse(data[i][j][headIndex[k - 1]].Substring(0, 3));
                            hy = double.Parse(data[i][j][headIndex[k - 1]].Substring(3, 3));
                            tx = double.Parse(data[i][j][headIndex[k] - 1].Substring(0, 3));
                            ty = double.Parse(data[i][j][headIndex[k] - 1].Substring(3, 3));

                            if (tx != hx)
                            {
                                double temp = Math.Atan((hy - ty) / (tx - hx)) * (180 / Math.PI);
                                if (tx < hx)
                                {
                                    if (ty <= hy)
                                    {
                                        temp += 180;
                                    }
                                    else
                                    {
                                        temp -= 180;
                                    }
                                }
                                angleData[i][j].Add(temp);
                            }
                            else
                            {
                                if (ty < hy)
                                    angleData[i][j].Add(90);
                                else if (ty > hy)
                                    angleData[i][j].Add(-90);
                                else
                                    angleData[i][j].Add(0);
                            }
                        }
                    }
                }
            }

            StreamReader file;
            foreach (string filePath in Directory.GetFileSystemEntries(sampleNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(samplePath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < angleData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += angleData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }

            }
            foreach (string filePath in Directory.GetFileSystemEntries(testNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(testPath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < angleData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += angleData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }
            }

            str = null;
            for (i = 0; i < angleData.Count; i++)
            {
                using (StreamWriter sw = File.CreateText(forWBPath + (i + 1) + ".txt"))
                {
                    for (j = 0; j < angleData[i].Count; j++)
                    {
                        str = null;
                        for (k = 0; k < angleData[i][j].Count; k++)
                        {
                            if (k != 0) str += ",";
                            str += angleData[i][j][k].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }
            }
        }

        //取特徵2
        public void Feature2(List<List<List<string>>> data, string savePath, string nameDirPath)
        {
            int i, j, k, l;
            string str, samplePath, testPath, sampleNamePath, testNamePath;
            List<List<List<double>>> lengthRatioData = new List<List<List<double>>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";
            sampleNamePath = nameDirPath + @"sample\";
            testNamePath = nameDirPath + @"test\";

            dm.DeleteAndCreate(samplePath); //創立-刪除 Feature-Feature2-sample資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 Feature-Feature2-test資料夾


            for (i = 0; i < data.Count; i++)
            {
                lengthRatioData.Add(new List<List<double>>());
                for (j = 0; j < data[i].Count; j++)
                {
                    lengthRatioData[i].Add(new List<double>());
                    if (data[i][j].Count != 0)
                    {
                        int maxIdx = data[i][j].Count - 1, hx, hy, tx, ty;

                        List<double> lengths = new List<double>();

                        lengths.Add(0);
                        l = 0;

                        k = 1;
                        hx = int.Parse(data[i][j][k].Substring(0, 3));
                        hy = int.Parse(data[i][j][k].Substring(3, 3));

                        while (k < maxIdx)
                        {
                            k++;
                            tx = int.Parse(data[i][j][k].Substring(0, 3));
                            ty = int.Parse(data[i][j][k].Substring(3, 3));

                            if (data[i][j][k].Substring(6, 2) == data[i][j][k - 1].Substring(6, 2))
                            {
                                lengths[l] += (Math.Sqrt(((hx - tx) * (hx - tx)) + ((hy - ty) * (hy - ty))));
                            }
                            else
                            {
                                lengths.Add(0);
                                l++;
                            }

                            hx = tx;
                            hy = ty;
                        }

                        List<int> headIndex = new List<int>();
                        headIndex.Add(1);
                        for (k = 2; k < data[i][j].Count; k++)
                        {
                            if (data[i][j][k].Substring(6, 2) != data[i][j][k - 1].Substring(6, 2))
                            {
                                headIndex.Add(k);
                            }
                        }
                        headIndex.Add(k);


                        for (k = 1; k < headIndex.Count; k++)
                        {
                            hx = int.Parse(data[i][j][headIndex[k - 1]].Substring(0, 3));
                            hy = int.Parse(data[i][j][headIndex[k - 1]].Substring(3, 3));
                            tx = int.Parse(data[i][j][headIndex[k] - 1].Substring(0, 3));
                            ty = int.Parse(data[i][j][headIndex[k] - 1].Substring(3, 3));

                            lengthRatioData[i][j].Add(lengths[k - 1] / Math.Sqrt(((tx - hx) * (tx - hx)) + ((ty - hy) * (ty - hy))));
                        }
                    }
                }
            }

            StreamReader file;
            foreach (string filePath in Directory.GetFileSystemEntries(sampleNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(samplePath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < lengthRatioData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += lengthRatioData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }

            }
            foreach (string filePath in Directory.GetFileSystemEntries(testNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(testPath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < lengthRatioData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += lengthRatioData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }
            }
        }

        //取特徵3
        public void Feature3(List<List<List<string>>> data, string savePath, string nameDirPath)
        {
            int i, j, k, l;
            string str, samplePath, testPath, sampleNamePath, testNamePath;
            List<List<List<double>>> lengthRatioData = new List<List<List<double>>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";
            sampleNamePath = nameDirPath + @"sample\";
            testNamePath = nameDirPath + @"test\";

            dm.DeleteAndCreate(samplePath); //創立-刪除 Feature-Feature3-sample資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 Feature-Feature3-test資料夾

            for (i = 0; i < data.Count; i++)
            {
                lengthRatioData.Add(new List<List<double>>());
                for (j = 0; j < data[i].Count; j++)
                {
                    lengthRatioData[i].Add(new List<double>());
                    if (data[i][j].Count != 0)
                    {
                        int maxIdx = data[i][j].Count - 1, hx, hy, tx, ty;

                        List<double> lengths = new List<double>();

                        lengths.Add(0);
                        l = 0;

                        k = 1;
                        hx = int.Parse(data[i][j][k].Substring(0, 3));
                        hy = int.Parse(data[i][j][k].Substring(3, 3));

                        while (k < maxIdx)
                        {
                            k++;
                            tx = int.Parse(data[i][j][k].Substring(0, 3));
                            ty = int.Parse(data[i][j][k].Substring(3, 3));

                            if (data[i][j][k].Substring(6, 2) == data[i][j][k - 1].Substring(6, 2))
                            {
                                lengths[l] += (Math.Sqrt(((hx - tx) * (hx - tx)) + ((hy - ty) * (hy - ty))));
                            }
                            else
                            {
                                lengths.Add(0);
                                l++;
                            }

                            hx = tx;
                            hy = ty;
                        }

                        double total = lengths.Sum();
                        for (k = 0; k < lengths.Count; k++)
                        {
                            lengthRatioData[i][j].Add(lengths[k] / total);
                        }
                    }
                }
            }

            StreamReader file;
            foreach (string filePath in Directory.GetFileSystemEntries(sampleNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(samplePath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < lengthRatioData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += lengthRatioData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }

            }
            foreach (string filePath in Directory.GetFileSystemEntries(testNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(testPath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < lengthRatioData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += lengthRatioData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }
            }
        }

        //取特徵4
        public void Feature4(List<List<List<string>>> data, string savePath, string nameDirPath)
        {
            int i, j, k;
            string str, samplePath, testPath, sampleNamePath, testNamePath;
            List<List<List<int>>> HeadTailData = new List<List<List<int>>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";
            sampleNamePath = nameDirPath + @"sample\";
            testNamePath = nameDirPath + @"test\";

            dm.DeleteAndCreate(samplePath); //創立-刪除 Feature-Feature4-sample資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 Feature-Feature4-test資料夾

            for (i = 0; i < data.Count; i++)
            {
                HeadTailData.Add(new List<List<int>>());
                for (j = 0; j < data[i].Count; j++)
                {
                    HeadTailData[i].Add(new List<int>());
                    if (data[i][j].Count != 0)
                    {
                        int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
                        List<int[]> XandY = new List<int[]>();
                        for (k = 1; k < data[i][j].Count; k++)
                        {
                            int x = int.Parse(data[i][j][k].Substring(0, 3));
                            int y = int.Parse(data[i][j][k].Substring(3, 3));

                            XandY.Add(new int[] { x, y });

                            if (x < minX)
                                minX = x;
                            if (x > maxX)
                                maxX = x;
                            if (y < minY)
                                minY = y;
                            if (y > maxY)
                                maxY = y;
                        }

                        List<int> headIndex = new List<int>();
                        headIndex.Add(0);
                        for (k = 2; k < data[i][j].Count; k++)
                        {
                            if (data[i][j][k].Substring(6, 2) != data[i][j][k - 1].Substring(6, 2))
                            {
                                headIndex.Add(k - 1);
                            }
                        }
                        headIndex.Add(k - 1);

                        for (k = 0; k < headIndex.Count - 1; k++)
                        {
                            HeadTailData[i][j].Add((int)Math.Round(((XandY[headIndex[k]][0] - minX) / (double)(maxX - minX)) * 149));
                            HeadTailData[i][j].Add((int)Math.Round(((XandY[headIndex[k]][1] - minY) / (double)(maxY - minY)) * 149));
                            HeadTailData[i][j].Add((int)Math.Round(((XandY[headIndex[k + 1] - 1][0] - minX) / (double)(maxX - minX)) * 149));
                            HeadTailData[i][j].Add((int)Math.Round(((XandY[headIndex[k + 1] - 1][1] - minY) / (double)(maxY - minY)) * 149));
                        }
                    }
                }
            }

            StreamReader file;
            foreach (string filePath in Directory.GetFileSystemEntries(sampleNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(samplePath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += HeadTailData[number[0] - 1][number[1] - 1][i];
                        }
                        sw.WriteLine(str);
                    }
                }

            }
            foreach (string filePath in Directory.GetFileSystemEntries(testNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(testPath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += HeadTailData[number[0] - 1][number[1] - 1][i];
                        }
                        sw.WriteLine(str);
                    }
                }
            }
        }
        //取特徵4-1
        public void Feature4_1(List<List<List<string>>> data, string savePath, string nameDirPath)
        {
            int i, j, k, l;
            string str, samplePath, testPath, sampleNamePath, testNamePath;
            List<List<List<double>>> HeadTailData = new List<List<List<double>>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";
            sampleNamePath = nameDirPath + @"sample\";
            testNamePath = nameDirPath + @"test\";

            dm.DeleteAndCreate(samplePath); //創立-刪除 Feature-Feature4_1-sample資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 Feature-Feature4_1-test資料夾

            for (i = 0; i < data.Count; i++)
            {
                HeadTailData.Add(new List<List<double>>());
                for (j = 0; j < data[i].Count; j++)
                {
                    HeadTailData[i].Add(new List<double>());
                    if (data[i][j].Count != 0)
                    {
                        int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
                        List<int[]> XandY = new List<int[]>();
                        for (k = 1; k < data[i][j].Count; k++)
                        {
                            int x = int.Parse(data[i][j][k].Substring(0, 3));
                            int y = int.Parse(data[i][j][k].Substring(3, 3));

                            XandY.Add(new int[] { x, y });

                            if (x < minX)
                                minX = x;
                            if (x > maxX)
                                maxX = x;
                            if (y < minY)
                                minY = y;
                            if (y > maxY)
                                maxY = y;
                        }

                        for (k = 0; k < XandY.Count; k++)
                        {
                            XandY[k][0] = (int)Math.Round(((XandY[k][0] - minX) / (double)(maxX - minX)) * 149);
                            XandY[k][1] = (int)Math.Round(((XandY[k][1] - minY) / (double)(maxY - minY)) * 149);
                        }

                        List<int> headIndex = new List<int>();
                        headIndex.Add(0);
                        for (k = 2; k < data[i][j].Count; k++)
                        {
                            if (data[i][j][k].Substring(6, 2) != data[i][j][k - 1].Substring(6, 2))
                            {
                                headIndex.Add(k - 1);
                            }
                        }
                        headIndex.Add(k - 1);

                        int hx, hy, tx, ty;
                        double lenght;
                        List<double> lenghts = new List<double>();
                        for (k = 1; k < headIndex.Count; k++)
                        {
                            lenght = 0;
                            hx = XandY[headIndex[k - 1]][0];
                            hy = XandY[headIndex[k - 1]][1];
                            for (l = headIndex[k - 1] + 1; l < headIndex[k]; l++)
                            {
                                tx = XandY[l][0];
                                ty = XandY[l][1];

                                lenght += Math.Sqrt(((tx - hx) * (tx - hx)) + ((ty - hy) * (ty - hy)));

                                hx = tx;
                                hy = ty;
                            }
                            lenghts.Add(lenght);
                        }

                        for (k = 0; k < headIndex.Count - 1; k++)
                        {
                            HeadTailData[i][j].Add(XandY[headIndex[k]][0]);
                            HeadTailData[i][j].Add(XandY[headIndex[k]][1]);
                            HeadTailData[i][j].Add(XandY[headIndex[k + 1] - 1][0]);
                            HeadTailData[i][j].Add(XandY[headIndex[k + 1] - 1][1]);
                            HeadTailData[i][j].Add(lenghts[k]);
                        }
                    }
                }
            }

            StreamReader file;
            foreach (string filePath in Directory.GetFileSystemEntries(sampleNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(samplePath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);
                        int n = 0;
                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            n++;
                            if (i != 0) str += ",";
                            if (n == 5)
                            {
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                                n = 0;
                            }
                            else
                                str += HeadTailData[number[0] - 1][number[1] - 1][i];

                        }
                        sw.WriteLine(str);
                    }
                }

            }
            foreach (string filePath in Directory.GetFileSystemEntries(testNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(testPath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);
                        int n = 0;
                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            n++;
                            if (i != 0) str += ",";
                            if (n == 5)
                            {
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                                n = 0;
                            }
                            else
                                str += HeadTailData[number[0] - 1][number[1] - 1][i];
                        }
                        sw.WriteLine(str);
                    }
                }
            }
        }
        //取特徵4-4
        public void Feature4_4(List<List<List<string>>> data, string savePath, string nameDirPath)
        {
            int i, j, k, l;
            string str, samplePath, testPath, sampleNamePath, testNamePath;
            List<List<List<int>>> HeadTailData = new List<List<List<int>>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";
            sampleNamePath = nameDirPath + @"sample\";
            testNamePath = nameDirPath + @"test\";

            dm.DeleteAndCreate(samplePath); //創立-刪除 Feature-Feature4_4-sample資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 Feature-Feature4_4-test資料夾

            for (i = 0; i < data.Count; i++)
            {
                HeadTailData.Add(new List<List<int>>());
                for (j = 0; j < data[i].Count; j++)
                {
                    HeadTailData[i].Add(new List<int>());
                    if (data[i][j].Count != 0)
                    {
                        int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
                        List<int[]> XandY = new List<int[]>();
                        for (k = 1; k < data[i][j].Count; k++)
                        {
                            int x = int.Parse(data[i][j][k].Substring(0, 3));
                            int y = int.Parse(data[i][j][k].Substring(3, 3));

                            XandY.Add(new int[] { x, y });

                            if (x < minX)
                                minX = x;
                            if (x > maxX)
                                maxX = x;
                            if (y < minY)
                                minY = y;
                            if (y > maxY)
                                maxY = y;
                        }

                        for (k = 0; k < XandY.Count; k++)
                        {
                            XandY[k][0] = (int)Math.Round(((XandY[k][0] - minX) / (double)(maxX - minX)) * 149);
                            XandY[k][1] = (int)Math.Round(((XandY[k][1] - minY) / (double)(maxY - minY)) * 149);
                        }

                        List<int> headIndex = new List<int>();
                        headIndex.Add(0);
                        for (k = 2; k < data[i][j].Count; k++)
                        {
                            if (data[i][j][k].Substring(6, 2) != data[i][j][k - 1].Substring(6, 2))
                            {
                                headIndex.Add(k - 1);
                            }
                        }
                        headIndex.Add(k - 1);

                        for (k = 1; k < headIndex.Count; k++)
                        {
                            int hx = XandY[headIndex[k - 1]][0], hy = XandY[headIndex[k - 1]][1];
                            int tx = XandY[headIndex[k] - 1][0], ty = XandY[headIndex[k] - 1][1];

                            int a = ty - hy;
                            int b = -(tx - hx);
                            int c = -((a * hx) + (b * hy));
                            double d = Math.Sqrt((a * a) + (b * b));

                            int mx = 0, my = 0;
                            double maxH = -1, height = 0;
                            for (l = (headIndex[k - 1] + 1); l < headIndex[k]; l++)
                            {
                                int nx = XandY[l][0], ny = XandY[l][1];
                                height = Math.Abs(a * nx + b * ny + c) / d;

                                if (height > maxH || maxH == -1)
                                {
                                    maxH = height;
                                    mx = nx;
                                    my = ny;
                                }
                            }
                            double width = Math.Sqrt(((tx - hx) * (tx - hx)) + ((ty - hy) * (ty - hy)));
                            HeadTailData[i][j].Add(hx);
                            HeadTailData[i][j].Add(hy);
                            HeadTailData[i][j].Add(tx);
                            HeadTailData[i][j].Add(ty);
                            HeadTailData[i][j].Add(mx);
                            HeadTailData[i][j].Add(my);

                            if (width > 40)
                            {
                                double proportionality = maxH / width;
                                if (proportionality > 0.3)
                                    HeadTailData[i][j].Add(1);
                                else
                                    HeadTailData[i][j].Add(0);
                            }
                            else
                                HeadTailData[i][j].Add(0);
                        }
                    }
                }
            }

            StreamReader file;
            foreach (string filePath in Directory.GetFileSystemEntries(sampleNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(samplePath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += HeadTailData[number[0] - 1][number[1] - 1][i];
                        }
                        sw.WriteLine(str);
                    }
                }

            }
            foreach (string filePath in Directory.GetFileSystemEntries(testNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(testPath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += HeadTailData[number[0] - 1][number[1] - 1][i];
                        }
                        sw.WriteLine(str);
                    }
                }
            }
        }

        //取特徵4-5
        public void Feature4_5(List<List<List<string>>> data, string savePath, string nameDirPath)
        {
            int i, j, k, l;
            string str, samplePath, testPath, sampleNamePath, testNamePath;
            List<List<List<double>>> HeadTailData = new List<List<List<double>>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";
            sampleNamePath = nameDirPath + @"sample\";
            testNamePath = nameDirPath + @"test\";

            dm.DeleteAndCreate(samplePath); //創立-刪除 Feature-Feature4_5-sample資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 Feature-Feature4_5-test資料夾

            for (i = 0; i < data.Count; i++)
            {
                HeadTailData.Add(new List<List<double>>());
                for (j = 0; j < data[i].Count; j++)
                {
                    HeadTailData[i].Add(new List<double>());
                    if (data[i][j].Count != 0)
                    {
                        int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
                        List<int[]> XandY = new List<int[]>();
                        for (k = 1; k < data[i][j].Count; k++)
                        {
                            int x = int.Parse(data[i][j][k].Substring(0, 3));
                            int y = int.Parse(data[i][j][k].Substring(3, 3));

                            XandY.Add(new int[] { x, y });

                            if (x < minX)
                                minX = x;
                            if (x > maxX)
                                maxX = x;
                            if (y < minY)
                                minY = y;
                            if (y > maxY)
                                maxY = y;
                        }

                        for (k = 0; k < XandY.Count; k++)
                        {
                            XandY[k][0] = (int)Math.Round(((XandY[k][0] - minX) / (double)(maxX - minX)) * 149);
                            XandY[k][1] = (int)Math.Round(((XandY[k][1] - minY) / (double)(maxY - minY)) * 149);
                        }

                        List<int> headIndex = new List<int>();
                        headIndex.Add(0);
                        for (k = 2; k < data[i][j].Count; k++)
                        {
                            if (data[i][j][k].Substring(6, 2) != data[i][j][k - 1].Substring(6, 2))
                            {
                                headIndex.Add(k - 1);
                            }
                        }
                        headIndex.Add(k - 1);

                        for (k = 1; k < headIndex.Count; k++)
                        {
                            int hx = XandY[headIndex[k - 1]][0], hy = XandY[headIndex[k - 1]][1];
                            int tx = XandY[headIndex[k] - 1][0], ty = XandY[headIndex[k] - 1][1];

                            int a = ty - hy;
                            int b = -(tx - hx);
                            int c = -((a * hx) + (b * hy));
                            double d = Math.Sqrt((a * a) + (b * b));

                            int mx = 0, my = 0;
                            double maxH = -1, height = 0;
                            double width = Math.Sqrt(((tx - hx) * (tx - hx)) + ((ty - hy) * (ty - hy)));
                            if (width != 0)
                            {
                                for (l = (headIndex[k - 1] + 1); l < headIndex[k]; l++)
                                {
                                    int nx = XandY[l][0], ny = XandY[l][1];
                                    height = Math.Abs(a * nx + b * ny + c) / d;

                                    if (height > maxH || maxH == -1)
                                    {
                                        maxH = height;
                                        mx = nx;
                                        my = ny;
                                    }
                                }
                            }
                            else
                            {
                                for (l = (headIndex[k - 1] + 1); l < headIndex[k]; l++)
                                {
                                    int nx = XandY[l][0], ny = XandY[l][1];
                                    height = Math.Sqrt(((nx - hx) * (nx - hx)) + ((ny - hy) * (ny - hy)));

                                    if (height > maxH || maxH == -1)
                                    {
                                        maxH = height;
                                        mx = nx;
                                        my = ny;
                                    }
                                }
                            }

                            HeadTailData[i][j].Add(hx);
                            HeadTailData[i][j].Add(hy);
                            HeadTailData[i][j].Add(tx);
                            HeadTailData[i][j].Add(ty);
                            HeadTailData[i][j].Add(mx);
                            HeadTailData[i][j].Add(my);

                            HeadTailData[i][j].Add(maxH / width);
                        }
                    }
                }
            }

            StreamReader file;
            foreach (string filePath in Directory.GetFileSystemEntries(sampleNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(samplePath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            if ((i % 7) < 6)
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0");
                            else
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }

            }
            foreach (string filePath in Directory.GetFileSystemEntries(testNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(testPath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            if ((i % 7) < 6)
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0");
                            else
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }
            }
        }
        //取特徵4-6
        public void Feature4_6(List<List<List<string>>> data, string savePath, string nameDirPath)
        {
            int i, j, k, l;
            string str, samplePath, testPath, sampleNamePath, testNamePath, forWBPath;
            List<List<List<double>>> HeadTailData = new List<List<List<double>>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";
            sampleNamePath = nameDirPath + @"sample\";
            testNamePath = nameDirPath + @"test\";
            forWBPath = savePath + @"forWB\"; 

            dm.DeleteAndCreate(samplePath); //創立-刪除 Feature-Feature4_6-sample資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 Feature-Feature4_6-test資料夾
            dm.DeleteAndCreate(forWBPath); //創立-刪除 Feature-Feature4_6-forWB資料夾

            for (i = 0; i < data.Count; i++)
            {
                HeadTailData.Add(new List<List<double>>());
                for (j = 0; j < data[i].Count; j++)
                {
                    HeadTailData[i].Add(new List<double>());
                    if (data[i][j].Count != 0)
                    {
                        int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
                        List<int[]> XandY = new List<int[]>();
                        for (k = 1; k < data[i][j].Count; k++)
                        {
                            int x = int.Parse(data[i][j][k].Substring(0, 3));
                            int y = int.Parse(data[i][j][k].Substring(3, 3));

                            XandY.Add(new int[] { x, y });

                            if (x < minX)
                                minX = x;
                            if (x > maxX)
                                maxX = x;
                            if (y < minY)
                                minY = y;
                            if (y > maxY)
                                maxY = y;
                        }

                        for (k = 0; k < XandY.Count; k++)
                        {
                            XandY[k][0] = (int)Math.Round(((XandY[k][0] - minX) / (double)(maxX - minX)) * 149);
                            XandY[k][1] = (int)Math.Round(((XandY[k][1] - minY) / (double)(maxY - minY)) * 149);
                        }

                        List<int> headIndex = new List<int>();
                        headIndex.Add(0);
                        for (k = 2; k < data[i][j].Count; k++)
                        {
                            if (data[i][j][k].Substring(6, 2) != data[i][j][k - 1].Substring(6, 2))
                            {
                                headIndex.Add(k - 1);
                            }
                        }
                        headIndex.Add(k - 1);

                        for (k = 1; k < headIndex.Count; k++)
                        {
                            int hx = XandY[headIndex[k - 1]][0], hy = XandY[headIndex[k - 1]][1];
                            int tx = XandY[headIndex[k] - 1][0], ty = XandY[headIndex[k] - 1][1];
                            int px = hx, py = hy;

                            int a = ty - hy;
                            int b = -(tx - hx);
                            int c = -((a * hx) + (b * hy));
                            double d = Math.Sqrt((a * a) + (b * b));

                            double width = Math.Sqrt(((tx - hx) * (tx - hx)) + ((ty - hy) * (ty - hy)));

                            int mx = 0, my = 0;
                            double maxH = -1, height = 0;
                            double length = 0;
                            if (width != 0)
                            {
                                for (l = (headIndex[k - 1] + 1); l < headIndex[k]; l++)
                                {
                                    int nx = XandY[l][0], ny = XandY[l][1];
                                    height = Math.Abs(a * nx + b * ny + c) / d;

                                    length += Math.Sqrt(((nx - px) * (nx - px)) + ((ny - py) * (ny - py)));

                                    if (height > maxH || maxH == -1)
                                    {
                                        maxH = height;
                                        mx = nx;
                                        my = ny;
                                    }

                                    px = nx;
                                    py = ny;
                                }
                            }
                            else
                            {
                                for (l = (headIndex[k - 1] + 1); l < headIndex[k]; l++)
                                {
                                    int nx = XandY[l][0], ny = XandY[l][1];
                                    height = Math.Sqrt(((nx - hx) * (nx - hx)) + ((ny - hy) * (ny - hy)));

                                    length += Math.Sqrt(((nx - px) * (nx - px)) + ((ny - py) * (ny - py)));

                                    if (height > maxH || maxH == -1)
                                    {
                                        maxH = height;
                                        mx = nx;
                                        my = ny;
                                    }

                                    px = nx;
                                    py = ny;
                                }
                            }

                            HeadTailData[i][j].Add(hx);
                            HeadTailData[i][j].Add(hy);
                            HeadTailData[i][j].Add(tx);
                            HeadTailData[i][j].Add(ty);
                            HeadTailData[i][j].Add(mx);
                            HeadTailData[i][j].Add(my);

                            HeadTailData[i][j].Add(width);

                            HeadTailData[i][j].Add(maxH);

                            HeadTailData[i][j].Add(length);
                        }
                    }
                }
            }

            StreamReader file;
            foreach (string filePath in Directory.GetFileSystemEntries(sampleNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(samplePath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            if ((i % 9) < 6)
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0");
                            else
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }

            }
            foreach (string filePath in Directory.GetFileSystemEntries(testNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(testPath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            if ((i % 9) < 6)
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0");
                            else
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }
            }

            str = null;
            for (i = 0; i < HeadTailData.Count; i++)
            {
                using (StreamWriter sw = File.CreateText(forWBPath + (i + 1) + ".txt"))
                {
                    for(j = 0; j < HeadTailData[i].Count; j++)
                    {
                        str = null;
                        for (k = 0; k < HeadTailData[i][j].Count; k++)
                        {
                            if (k != 0) str += ",";
                            if ((k % 9) < 6)
                                str += HeadTailData[i][j][k].ToString("0");
                            else
                                str += HeadTailData[i][j][k].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }                    
                }
            }

            
        }
        //取特徵4-7
        public void Feature4_7(List<List<List<string>>> data, string savePath, string nameDirPath)
        {
            int i, j, k, l;
            string str, samplePath, testPath, sampleNamePath, testNamePath;
            List<List<List<double>>> HeadTailData = new List<List<List<double>>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";
            sampleNamePath = nameDirPath + @"sample\";
            testNamePath = nameDirPath + @"test\";

            dm.DeleteAndCreate(samplePath); //創立-刪除 Feature-Feature4_6-sample資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 Feature-Feature4_6-test資料夾

            for (i = 0; i < data.Count; i++)
            {
                HeadTailData.Add(new List<List<double>>());
                for (j = 0; j < data[i].Count; j++)
                {
                    HeadTailData[i].Add(new List<double>());
                    if (data[i][j].Count != 0)
                    {
                        int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
                        List<int[]> XandY = new List<int[]>();
                        for (k = 1; k < data[i][j].Count; k++)
                        {
                            int x = int.Parse(data[i][j][k].Substring(0, 3));
                            int y = int.Parse(data[i][j][k].Substring(3, 3));

                            XandY.Add(new int[] { x, y });

                            if (x < minX)
                                minX = x;
                            if (x > maxX)
                                maxX = x;
                            if (y < minY)
                                minY = y;
                            if (y > maxY)
                                maxY = y;
                        }

                        for (k = 0; k < XandY.Count; k++)
                        {
                            XandY[k][0] = (int)Math.Round(((XandY[k][0] - minX) / (double)(maxX - minX)) * 149);
                            XandY[k][1] = (int)Math.Round(((XandY[k][1] - minY) / (double)(maxY - minY)) * 149);
                        }

                        List<int> headIndex = new List<int>();
                        headIndex.Add(0);
                        for (k = 2; k < data[i][j].Count; k++)
                        {
                            if (data[i][j][k].Substring(6, 2) != data[i][j][k - 1].Substring(6, 2))
                            {
                                headIndex.Add(k - 1);
                            }
                        }
                        headIndex.Add(k - 1);

                        for (k = 1; k < headIndex.Count; k++)
                        {
                            int hx = XandY[headIndex[k - 1]][0], hy = XandY[headIndex[k - 1]][1];
                            int tx = XandY[headIndex[k] - 1][0], ty = XandY[headIndex[k] - 1][1];
                            int sumX = hx, sumY = hy;
                            int px = hx, py = hy;

                            int a = ty - hy;
                            int b = -(tx - hx);
                            int c = -((a * hx) + (b * hy));
                            double d = Math.Sqrt((a * a) + (b * b));

                            double width = Math.Sqrt(((tx - hx) * (tx - hx)) + ((ty - hy) * (ty - hy)));

                            int mx = 0, my = 0;
                            double length = 0;
                            for (l = (headIndex[k - 1] + 1); l < headIndex[k]; l++)
                            {
                                int nx = XandY[l][0], ny = XandY[l][1];
                                sumX += nx;
                                sumY += ny;

                                length += Math.Sqrt(((nx - px) * (nx - px)) + ((ny - py) * (ny - py)));

                                px = nx;
                                py = ny;
                            }

                            mx = Convert.ToInt32(sumX / (double)(headIndex[k] - headIndex[k - 1]));
                            my = Convert.ToInt32(sumY / (double)(headIndex[k] - headIndex[k - 1]));

                            double height;
                            if (width != 0)
                            {
                                height = Math.Abs(a * mx + b * my + c) / d;
                            }
                            else
                            {
                                height = Math.Sqrt(((mx - hx) * (mx - hx)) + ((my - hy) * (my - hy)));
                            }

                            HeadTailData[i][j].Add(hx);
                            HeadTailData[i][j].Add(hy);
                            HeadTailData[i][j].Add(tx);
                            HeadTailData[i][j].Add(ty);
                            HeadTailData[i][j].Add(mx);
                            HeadTailData[i][j].Add(my);

                            HeadTailData[i][j].Add(width);

                            HeadTailData[i][j].Add(height);

                            HeadTailData[i][j].Add(length);
                        }
                    }
                }
            }

            StreamReader file;
            foreach (string filePath in Directory.GetFileSystemEntries(sampleNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(samplePath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            if ((i % 9) < 6)
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0");
                            else
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }

            }
            foreach (string filePath in Directory.GetFileSystemEntries(testNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(testPath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < HeadTailData[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            if ((i % 9) < 6)
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0");
                            else
                                str += HeadTailData[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }
            }
        }
        //取特徵5
        public void Feature5(List<List<List<string>>> data, string savePath, string nameDirPath)
        {
            int i, j, k, l;
            string str, samplePath, testPath, sampleNamePath, testNamePath;
            List<List<List<double>>> Angle = new List<List<List<double>>>();

            //預設值
            samplePath = savePath + @"sample\";
            testPath = savePath + @"test\";
            sampleNamePath = nameDirPath + @"sample\";
            testNamePath = nameDirPath + @"test\";

            dm.DeleteAndCreate(samplePath); //創立-刪除 Feature-Feature5-sample資料夾
            dm.DeleteAndCreate(testPath); //創立-刪除 Feature-Feature5-test資料夾

            for (i = 0; i < data.Count; i++)
            {
                Angle.Add(new List<List<double>>());
                for (j = 0; j < data[i].Count; j++)
                {
                    Angle[i].Add(new List<double>());
                    if (data[i][j].Count != 0)
                    {
                        int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
                        List<int[]> XandY = new List<int[]>();
                        for (k = 1; k < data[i][j].Count; k++)
                        {
                            int x = int.Parse(data[i][j][k].Substring(0, 3));
                            int y = int.Parse(data[i][j][k].Substring(3, 3));

                            XandY.Add(new int[] { x, y });

                            if (x < minX)
                                minX = x;
                            if (x > maxX)
                                maxX = x;
                            if (y < minY)
                                minY = y;
                            if (y > maxY)
                                maxY = y;
                        }

                        for (k = 0; k < XandY.Count; k++)
                        {
                            XandY[k][0] = (int)Math.Round(((XandY[k][0] - minX) / (double)(maxX - minX)) * 149);
                            XandY[k][1] = (int)Math.Round(((XandY[k][1] - minY) / (double)(maxY - minY)) * 149);
                        }

                        List<int> headIndex = new List<int>();
                        headIndex.Add(0);
                        for (k = 2; k < data[i][j].Count; k++)
                        {
                            if (data[i][j][k].Substring(6, 2) != data[i][j][k - 1].Substring(6, 2))
                            {
                                headIndex.Add(k - 1);
                            }
                        }
                        headIndex.Add(k - 1);

                        for (k = 1; k < headIndex.Count; k++)
                        {
                            int hx = XandY[headIndex[k - 1]][0], hy = XandY[headIndex[k - 1]][1];
                            int tx = XandY[headIndex[k] - 1][0], ty = XandY[headIndex[k] - 1][1];

                            int a = ty - hy;
                            int b = -(tx - hx);
                            int c = -((a * hx) + (b * hy));
                            double d = Math.Sqrt((a * a) + (b * b));

                            int mx = 0, my = 0;
                            double maxD = -1;
                            for (l = (headIndex[k - 1] + 1); l < headIndex[k]; l++)
                            {
                                int nx = XandY[l][0], ny = XandY[l][1];
                                double distance = Math.Abs(a * nx + b * ny + c) / d;

                                if (distance > maxD || maxD == -1)
                                {
                                    maxD = distance;
                                    mx = nx;
                                    my = ny;
                                }
                            }

                            int a1 = (tx - hx), b1 = (ty - hy);
                            int a2 = (mx - hx), b2 = (my - hy);
                            int a3 = (hx - tx), b3 = (hy - ty);
                            int a4 = (mx - tx), b4 = (my - ty);

                            Angle[i][j].Add(Math.Acos(((a1 * a2) + (b1 * b2)) / Math.Sqrt(((a1 * a1) + (b1 * b1)) * ((a2 * a2) + (b2 * b2)))) * (180 / Math.PI));
                            Angle[i][j].Add(Math.Acos(((a3 * a4) + (b3 * b4)) / Math.Sqrt(((a3 * a3) + (b3 * b3)) * ((a4 * a4) + (b4 * b4)))) * (180 / Math.PI));
                            Angle[i][j].Add(mx);
                            Angle[i][j].Add(my);
                        }
                    }
                }
            }

            StreamReader file;
            foreach (string filePath in Directory.GetFileSystemEntries(sampleNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(samplePath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < Angle[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += Angle[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }

            }
            foreach (string filePath in Directory.GetFileSystemEntries(testNamePath, "*.txt"))
            {
                int strokes = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                using (StreamWriter sw = File.CreateText(testPath + strokes + ".txt"))
                {
                    file = new StreamReader(filePath);
                    while ((str = file.ReadLine()) != null)
                    {
                        int[] number = Array.ConvertAll(str.Split('_'), int.Parse);

                        str = null;
                        for (i = 0; i < Angle[number[0] - 1][number[1] - 1].Count; i++)
                        {
                            if (i != 0) str += ",";
                            str += Angle[number[0] - 1][number[1] - 1][i].ToString("0.0000");
                        }
                        sw.WriteLine(str);
                    }
                }
            }
        }

        //手寫板 取特徵1
        public List<double> GetFeature1(List<string> data)
        {
            int i;
            List<double> angleData = new List<double>();


            if (data.Count != 0)
            {
                List<int> headIndex = new List<int>();
                headIndex.Add(1);
                for (i = 2; i < data.Count; i++)
                {
                    if (data[i].Substring(6, 2) != data[i - 1].Substring(6, 2))
                    {
                        headIndex.Add(i);
                    }
                }
                headIndex.Add(i);
                double hx, hy, tx, ty;
                for (i = 1; i < headIndex.Count; i++)
                {
                    hx = double.Parse(data[headIndex[i - 1]].Substring(0, 3));
                    hy = double.Parse(data[headIndex[i - 1]].Substring(3, 3));
                    tx = double.Parse(data[headIndex[i] - 1].Substring(0, 3));
                    ty = double.Parse(data[headIndex[i] - 1].Substring(3, 3));

                    if (tx != hx)
                    {
                        double temp = Math.Atan((hy - ty) / (tx - hx)) * (180 / Math.PI);
                        if (tx < hx)
                        {
                            if (ty <= hy)
                            {
                                temp += 180;
                            }
                            else
                            {
                                temp -= 180;
                            }
                        }
                        angleData.Add(temp);
                    }
                    else
                    {
                        if (ty < hy)
                            angleData.Add(90);
                        else if (ty > hy)
                            angleData.Add(-90);
                        else
                            angleData.Add(0);
                    }
                }
            }
            return angleData;
        }
        //手寫板 取特徵2
        public List<double> GetFeature2(List<string> data)
        {
            int i, j;
            List<double> HeadTailData = new List<double>();


            if (data.Count != 0)
            {
                int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
                List<int[]> XandY = new List<int[]>();
                for (i = 1; i < data.Count; i++)
                {
                    int x = int.Parse(data[i].Substring(0, 3));
                    int y = int.Parse(data[i].Substring(3, 3));

                    XandY.Add(new int[] { x, y });

                    if (x < minX)
                        minX = x;
                    if (x > maxX)
                        maxX = x;
                    if (y < minY)
                        minY = y;
                    if (y > maxY)
                        maxY = y;
                }

                for (i = 0; i < XandY.Count; i++)
                {
                    XandY[i][0] = (int)Math.Round(((XandY[i][0] - minX) / (double)(maxX - minX)) * 149);
                    XandY[i][1] = (int)Math.Round(((XandY[i][1] - minY) / (double)(maxY - minY)) * 149);
                }

                List<int> headIndex = new List<int>();
                headIndex.Add(0);
                for (i = 2; i < data.Count; i++)
                {
                    if (data[i].Substring(6, 2) != data[i - 1].Substring(6, 2))
                    {
                        headIndex.Add(i - 1);
                    }
                }
                headIndex.Add(i - 1);

                for (i = 1; i < headIndex.Count; i++)
                {
                    int hx = XandY[headIndex[i - 1]][0], hy = XandY[headIndex[i - 1]][1];
                    int tx = XandY[headIndex[i] - 1][0], ty = XandY[headIndex[i] - 1][1];
                    int px = hx, py = hy;

                    int a = ty - hy;
                    int b = -(tx - hx);
                    int c = -((a * hx) + (b * hy));
                    double d = Math.Sqrt((a * a) + (b * b));

                    double width = Math.Sqrt(((tx - hx) * (tx - hx)) + ((ty - hy) * (ty - hy)));

                    int mx = 0, my = 0;
                    double maxH = -1, height = 0;
                    double length = 0;
                    if (width != 0)
                    {
                        for (j = (headIndex[i - 1] + 1); j < headIndex[i]; j++)
                        {
                            int nx = XandY[j][0], ny = XandY[j][1];
                            height = Math.Abs(a * nx + b * ny + c) / d;

                            length += Math.Sqrt(((nx - px) * (nx - px)) + ((ny - py) * (ny - py)));

                            if (height > maxH || maxH == -1)
                            {
                                maxH = height;
                                mx = nx;
                                my = ny;
                            }

                            px = nx;
                            py = ny;
                        }
                    }
                    else
                    {
                        for (j = (headIndex[i - 1] + 1); j < headIndex[i]; j++)
                        {
                            int nx = XandY[j][0], ny = XandY[j][1];
                            height = Math.Sqrt(((nx - hx) * (nx - hx)) + ((ny - hy) * (ny - hy)));

                            length += Math.Sqrt(((nx - px) * (nx - px)) + ((ny - py) * (ny - py)));

                            if (height > maxH || maxH == -1)
                            {
                                maxH = height;
                                mx = nx;
                                my = ny;
                            }

                            px = nx;
                            py = ny;
                        }
                    }

                    HeadTailData.Add(hx);
                    HeadTailData.Add(hy);
                    HeadTailData.Add(tx);
                    HeadTailData.Add(ty);
                    HeadTailData.Add(mx);
                    HeadTailData.Add(my);

                    HeadTailData.Add(width);

                    HeadTailData.Add(maxH);

                    HeadTailData.Add(length);
                }
            }

            return HeadTailData;
        }
    }
}
