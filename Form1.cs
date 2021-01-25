using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Job4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //全域
        string _nameStr;
        DirectoryMethod _dm = new DirectoryMethod();
        GetData _gd = new GetData();
        ReadData _rd = new ReadData();
        CalculateF _cf = new CalculateF();
        ConvertBIG5 _cbig5 = new ConvertBIG5();

        List<List<string>> _sampleNameData = null;
        List<List<double[]>> _sampleFeature1Data;
        List<List<double[]>> _sampleFeature4Data;

        List<int[]> _strokeRange = null;
        List<int> _strokes = new List<int>();
        List<int[]> _strokeTI = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            mainFolderPath.Text = @"Z:\run_datas\base\";
            getData_dataSourceForder.Text = "pointdata(No_noise)";
            other_combine_sourceFolderName.Text = "pointdata";
            other_combine_saveFolderName.Text = "Pointdata(Combine)";
            other_noNoise_saveFolderName.Text = "pointdata(No_noise)";
            other_noNoise_sourceFolderName.Text = "Pointdata(Combine)";
            getData_saveFolderName.Text = "Feature";
            other_converSampleTxt_saveName.Text = "sampleName(Combine).txt";
            other_converSampleTxt_sourceName.Text = "sampleName.txt";
            getData_sampleName.Text = "sampleName(Combine).txt";
            identification_dataSaveFolderName.Text = "Result";
            identification_dataSourceFolderName.Text = "Feature";
            identification_limitativeRange.Text = "1";
            identification_feature1Magnification.Text = "1";
            identification_feature2Magnification.Text = "1";
            identification_feature3Magnification.Text = "1";
            identification_feature4Magnification.Text = "1";
            identification_feature5Magnification.Text = "1";
            identification_strokeToleranceIntervalFileName.Text = "strokeToleranceInterval.txt";
            identification_strokeRangeFileName.Text = "strokeRange.txt";
            other_getStroke_dataSaveFileName.Text = "strokes.txt";
            other_getStroke_dataSourceFolderName.Text = "pointdata(No_noise)";
            identification_strokesFileName.Text = "strokes.txt";
            identification_feature4_5_magnification.Text = "1";
            identification_feature4_6_magnification.Text = "1";
            textBox1.Text = "pointdata(No_noise)";
            textBox2.Text = "Image";
            textBox3.Text = "Pointdata(Combine)";
            textBox4.Text = "Image";
            _nameStr = "b16.1";
        }

        //瀏覽按鈕
        private void mainFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            mainFolderPath.Text = path.SelectedPath + @"\";
        }

        private void getData_dataSourceForderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.SelectedPath = mainFolderPath.Text + getData_dataSourceForder.Text;
            path.ShowDialog();
            getData_dataSourceForder.Text = Path.GetFileNameWithoutExtension(path.SelectedPath);
        }

        private void other_combine_sourceFolderNameButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.SelectedPath = other_combine_sourceFolderName.Text + other_noNoise_sourceFolderName.Text;
            path.ShowDialog();
            other_combine_sourceFolderName.Text = Path.GetFileNameWithoutExtension(path.SelectedPath);
        }

        private void other_noNoise_sourceFolderNameButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.SelectedPath = mainFolderPath.Text + other_noNoise_sourceFolderName.Text;
            path.ShowDialog();
            other_noNoise_sourceFolderName.Text = Path.GetFileNameWithoutExtension(path.SelectedPath);
        }

        private void other_combineButton_Click(object sender, EventArgs e)
        {
            int i;
            string sourcePath, savePath, str;

            //預設值
            sourcePath = mainFolderPath.Text + other_combine_sourceFolderName.Text;
            savePath = mainFolderPath.Text + other_combine_saveFolderName.Text;

            //確保儲存位置
            if (Directory.Exists(savePath)) //創立-刪除 save資料夾
                Directory.Delete(savePath, true);
            Directory.CreateDirectory(savePath);

            //開始
            string[] dirName = Directory.GetDirectories(sourcePath);
            foreach (string filePath11 in Directory.GetFileSystemEntries(dirName[0], "*.txt"))
            {
                string filePath12 = dirName[1] + @"\" + Path.GetFileName(filePath11);
                string fileSavePath = savePath + @"\" + Path.GetFileName(filePath11).Remove(0, 4);

                List<string> strData = new List<string>();

                StreamReader file = new StreamReader(filePath11);
                while ((str = file.ReadLine()) != null)
                {
                    strData.Add(str);
                }
                file = new StreamReader(filePath12);
                while ((str = file.ReadLine()) != null)
                {
                    if (str.Length == 3)
                    {
                        int number = int.Parse(str.Substring(1, 2));
                        number += 11;
                        str = str.Substring(0, 1) + number;
                    }
                    strData.Add(str);
                }

                using (StreamWriter sw = File.CreateText(fileSavePath))
                {
                    for (i = 0; i < strData.Count; i++)
                    {
                        sw.WriteLine(strData[i]);
                    }
                }
            }
        }

        private void other_noNoiseButton_Click(object sender, EventArgs e)
        {
            int i, j;
            string sourcePath, savePath, str;

            //預設值
            sourcePath = mainFolderPath.Text + other_noNoise_sourceFolderName.Text;
            savePath = mainFolderPath.Text + other_noNoise_saveFolderName.Text;

            //確保儲存位置
            if (Directory.Exists(savePath)) //創立-刪除 save資料夾
                Directory.Delete(savePath, true);
            Directory.CreateDirectory(savePath);

            //開始
            foreach (string filePath in Directory.GetFileSystemEntries(sourcePath, "*.txt")) //依編號各個執行
            {
                string fileSavePath = savePath + @"\" + Path.GetFileName(filePath);

                List<List<string>> strData = new List<List<string>>();

                i = -1;
                StreamReader file = new StreamReader(filePath);
                while ((str = file.ReadLine()) != null)
                {
                    if (str.Length == 3)
                    {
                        i++;
                        strData.Add(new List<string>());
                        strData[i].Add(str);
                    }
                    else
                        strData[i].Add(str);
                }

                for (i = 0; i < strData.Count; i++)
                {
                    //過濾開始 >>>
                    for (j = 2; j < (strData[i].Count - 1); j++)
                    {
                        if ((strData[i][j].Substring(6, 2) != strData[i][j - 1].Substring(6, 2)) && (strData[i][j].Substring(6, 2) != strData[i][j + 1].Substring(6, 2)))
                        {
                            strData[i].RemoveAt(j);
                            j--;
                        }
                    }
                    if (strData[i][j].Substring(6, 2) != strData[i][j - 1].Substring(6, 2))
                        strData[i].RemoveAt(j);
                    if (strData[i][1].Substring(6, 2) != strData[i][2].Substring(6, 2))
                        strData[i].RemoveAt(1);
                    //過濾結束 <<<
                }

                using (StreamWriter sw = File.CreateText(fileSavePath))
                {
                    for (i = 0; i < strData.Count; i++)
                    {
                        for (j = 0; j < strData[i].Count; j++)
                        {
                            sw.WriteLine(strData[i][j]);
                        }
                    }
                }
            }
        }

        private void getData_saveFolderNameButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            getData_saveFolderName.Text = Path.GetFileNameWithoutExtension(path.SelectedPath) + @"\";
        }

        private void other_converSampleTxt_sourceNameButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = mainFolderPath.Text;
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            other_converSampleTxt_sourceName.Text = Path.GetFileName(openFileDialog.FileName);
        }

        //轉換
        private void other_converSampleTxt_Button_Click(object sender, EventArgs e)
        {
            int i;
            string str, sourcePath, savePath;
            List<string> strData = new List<string>();

            sourcePath = mainFolderPath.Text + other_converSampleTxt_sourceName.Text;
            savePath = mainFolderPath.Text + other_converSampleTxt_saveName.Text;

            StreamReader file = new StreamReader(sourcePath);
            while ((str = file.ReadLine()) != null)
            {
                string[] strArr = str.Split('_');
                i = int.Parse(strArr[1]);
                if (strArr[2] == "12")
                    i += 11;
                strData.Add(strArr[0] + "_" + i);
            }

            using (StreamWriter sw = File.CreateText(savePath))
            {
                for (i = 0; i < strData.Count; i++)
                {
                    sw.WriteLine(strData[i]);
                }
            }
        }

        //提取
        private void getData_getData_Click(object sender, EventArgs e)
        {
            if (getNameCheckBox.Checked ||
                getFeature1CheckBox.Checked ||
                getFeature2CheckBox.Checked ||
                getFeature3CheckBox.Checked ||
                getFeature4CheckBox.Checked ||
                getFeature4_1CheckBox.Checked ||
                getFeature4_4CheckBox.Checked ||
                getFeature4_5CheckBox.Checked ||
                getFeature4_6CheckBox.Checked ||
                getFeature4_7CheckBox.Checked ||
                getFeature5CheckBox.Checked ||
                getFourDirectionCheckBox.Checked)
            {
                int i;
                string str, sourcePath, savePath, sampleNamePath;
                List<List<List<string>>> sourceData = new List<List<List<string>>>();

                //預設值
                sourcePath = mainFolderPath.Text + getData_dataSourceForder.Text;
                savePath = mainFolderPath.Text + getData_saveFolderName.Text;
                sampleNamePath = mainFolderPath.Text + getData_sampleName.Text;

                //取數據開始 >>>
                foreach (string filePath in Directory.GetFileSystemEntries(sourcePath, "*.txt"))
                {
                    int needCapacityOne = int.Parse(Path.GetFileNameWithoutExtension(filePath));
                    if (sourceData.Count < needCapacityOne)
                    {
                        int difference = needCapacityOne - sourceData.Count;
                        for (i = 0; i < difference; i++)
                            sourceData.Add(new List<List<string>>());
                    }

                    StreamReader file = new StreamReader(filePath);
                    int needCapacityTwo = 0;
                    while ((str = file.ReadLine()) != null)
                    {
                        if (str.Length == 3)
                        {
                            needCapacityTwo = int.Parse(str.Substring(1, 2));
                            if (sourceData[needCapacityOne - 1].Count < needCapacityTwo)
                            {
                                int difference = needCapacityTwo - sourceData[needCapacityOne - 1].Count;
                                for (i = 0; i < difference; i++)
                                    sourceData[needCapacityOne - 1].Add(new List<string>());
                            }
                            sourceData[needCapacityOne - 1][needCapacityTwo - 1].Add(str);
                        }
                        else
                            sourceData[needCapacityOne - 1][needCapacityTwo - 1].Add(str);
                    }
                }
                //取數據結束 <<<

                if (getNameCheckBox.Checked)
                {
                    string path = savePath + @"\Name\";
                    _gd.Name(sourceData, path, sampleNamePath);
                }

                if (getFourDirectionCheckBox.Checked)
                {
                    string path = savePath + @"\FourDirection\";

                    //確保儲存位置

                    _dm.DeleteAndCreate(path); //創立-刪除 Feature-FourDirection資料夾
                    string namePath = savePath + @"\name\";
                    _gd.FourDirection(sourceData, path, namePath);
                }

                if (getFeature1CheckBox.Checked)
                {
                    string path = savePath + @"\Feature1\";

                    //確保儲存位置
                    _dm.DeleteAndCreate(path); //創立-刪除 Feature-Feature1資料夾

                    string namePath = savePath + @"\name\";
                    _gd.Feature1(sourceData, path, namePath);
                }

                if (getFeature2CheckBox.Checked)
                {
                    string path = savePath + @"\Feature2\";

                    //確保儲存位置
                    _dm.DeleteAndCreate(path); //創立-刪除 Feature-Feature2資料夾

                    string namePath = savePath + @"\name\";
                    _gd.Feature2(sourceData, path, namePath);
                }

                if (getFeature3CheckBox.Checked)
                {
                    string path = savePath + @"\Feature3\";

                    //確保儲存位置
                    _dm.DeleteAndCreate(path); //創立-刪除 Feature-Feature3資料夾

                    string namePath = savePath + @"\name\";
                    _gd.Feature3(sourceData, path, namePath);
                }

                if (getFeature4CheckBox.Checked)
                {
                    string path = savePath + @"\Feature4\";

                    //確保儲存位置
                    _dm.DeleteAndCreate(path); //創立-刪除 Feature-Feature4資料夾

                    string namePath = savePath + @"\name\";
                    _gd.Feature4(sourceData, path, namePath);
                }

                if (getFeature4_1CheckBox.Checked)
                {
                    string path = savePath + @"\Feature4_1\";

                    //確保儲存位置
                    _dm.DeleteAndCreate(path); //創立-刪除 Feature-Feature4_1資料夾

                    string namePath = savePath + @"\name\";
                    _gd.Feature4_1(sourceData, path, namePath);
                }

                if (getFeature4_4CheckBox.Checked)
                {
                    string path = savePath + @"\Feature4_4\";

                    //確保儲存位置
                    _dm.DeleteAndCreate(path); //創立-刪除 Feature-Feature4_4資料夾

                    string namePath = savePath + @"\name\";
                    _gd.Feature4_4(sourceData, path, namePath);
                }

                if (getFeature4_5CheckBox.Checked)
                {
                    string path = savePath + @"\Feature4_5\";

                    //確保儲存位置
                    _dm.DeleteAndCreate(path); //創立-刪除 Feature-Feature4_5資料夾

                    string namePath = savePath + @"\name\";
                    _gd.Feature4_5(sourceData, path, namePath);
                }

                if (getFeature4_6CheckBox.Checked)
                {
                    string path = savePath + @"\Feature4_6\";

                    //確保儲存位置
                    _dm.DeleteAndCreate(path); //創立-刪除 Feature-Feature4_6資料夾

                    string namePath = savePath + @"\name\";
                    _gd.Feature4_6(sourceData, path, namePath);
                }

                if (getFeature4_7CheckBox.Checked)
                {
                    string path = savePath + @"\Feature4_7\";

                    //確保儲存位置
                    _dm.DeleteAndCreate(path); //創立-刪除 Feature-Feature4_7資料夾

                    string namePath = savePath + @"\name\";
                    _gd.Feature4_7(sourceData, path, namePath);
                }

                if (getFeature5CheckBox.Checked)
                {
                    string path = savePath + @"\Feature5\";

                    //確保儲存位置
                    _dm.DeleteAndCreate(path); //創立-刪除 Feature-Feature5資料夾

                    string namePath = savePath + @"\name\";
                    _gd.Feature5(sourceData, path, namePath);
                }
            }
            else
                MessageBox.Show("未選取要取得之特徵");
        }

        //辨識
        private void identification_identificationButton_Click(object sender, EventArgs e)
        {
            if (identificationFeature1CheckBox.Checked ||
                identificationFeature2CheckBox.Checked ||
                identificationFeature3CheckBox.Checked ||
                identificationFeature4CheckBox.Checked ||
                identificationFeature4_1CheckBox.Checked ||
                identificationFeature4_2CheckBox.Checked ||
                identificationFeature4_3CheckBox.Checked ||
                identificationFeature4_4CheckBox.Checked ||
                identificationFeature4_5CheckBox.Checked ||
                identificationFeature4_6CheckBox.Checked ||
                identificationFeature4_6maCheckBox.Checked ||
                identificationFeature4_7CheckBox.Checked ||
                identificationFeature4_7maCheckBox.Checked ||
                identificationFeature4_8CheckBox.Checked ||
                identificationFeature5CheckBox.Checked ||
                identificationFeature5SqrtCheckBox.Checked)
            {
                int i, j, k, l, m, haveF, n, total;
                double f1m = 0, f2m = 0, f3m = 0, f4m = 0, f5m = 0;
                string sourcePath, resultPath, resultDirPath;

                //預設值
                sourcePath = mainFolderPath.Text + identification_dataSourceFolderName.Text;
                resultPath = mainFolderPath.Text + identification_dataSaveFolderName.Text + @"\";

                //讀入name
                List<List<string>> sampleNameData = _rd.Rstring(sourcePath + @"\name\sample\");
                List<List<string>> testNameData = _rd.Rstring(sourcePath + @"\name\test\");

                //讀入四面
                List<List<List<int[]>>> sampleFourDirection = null;
                List<List<List<int[]>>> testFourDirection = null;
                if (checkBox9.Checked)
                {
                    haveF = 4;
                    _nameStr += "fd";
                    sampleFourDirection = _rd.RfourDirection(sourcePath + @"\FourDirection\sample\");
                    testFourDirection = _rd.RfourDirection(sourcePath + @"\FourDirection\test\");
                }
                else
                {
                    haveF = 1;
                    sampleFourDirection = _rd.RnoFourDirection(sourcePath + @"\FourDirection\sample\");
                    testFourDirection = _rd.RnoFourDirection(sourcePath + @"\FourDirection\test\");
                }

                //讀入筆劃數
                List<int[]> strokeData = _rd.RintArr(mainFolderPath.Text + identification_strokesFileName.Text);

                //筆劃區間
                List<int[]> strokeRange = _rd.RintArr(mainFolderPath.Text + identification_strokeRangeFileName.Text);
                List<int> strokes = new List<int>();
                for (i = 0; i < strokeRange.Count; i++)
                {
                    for (j = strokeRange[i][1]; j <= strokeRange[i][2]; j++)
                    {
                        strokes.Add(strokeRange[i][0] - 1);
                    }
                }

                List<int[]> strokeTI;
                //範圍
                if (checkBox14.Checked)
                {
                    _nameStr += "r";
                    strokeTI = _rd.RintArr(mainFolderPath.Text + identification_strokeToleranceIntervalFileName.Text);
                    for (i = 0; i < strokeTI.Count; i++)
                    {
                        strokeTI[i][1]--;
                        strokeTI[i][2]--;
                    }
                }
                else
                {
                    int range = int.Parse(identification_limitativeRange.Text);
                    strokeTI = _rd.RintArr_noTI((mainFolderPath.Text + identification_strokeToleranceIntervalFileName.Text), range);
                    for (i = 0; i < strokeTI.Count; i++)
                    {
                        strokeTI[i][1]--;
                        strokeTI[i][2]--;
                    }
                }


                //讀入特徵1
                List<List<double[]>> sampleFeature1Data = null;
                List<List<double[]>> testFeature1Data = null;
                if (identificationFeature1CheckBox.Checked ||
                    identificationFeature4_3CheckBox.Checked ||
                    identificationFeature4_4CheckBox.Checked ||
                    identificationFeature4_5CheckBox.Checked ||
                    identificationFeature4_6CheckBox.Checked ||
                    identificationFeature4_6maCheckBox.Checked ||
                    identificationFeature4_7CheckBox.Checked ||
                    identificationFeature4_7maCheckBox.Checked ||
                    identificationFeature4_8CheckBox.Checked)
                {
                    if (identificationFeature1CheckBox.Checked)
                    {
                        if (_nameStr != null) _nameStr += "+";
                        _nameStr += "f1x" + identification_feature1Magnification.Text;
                    }

                    f1m = int.Parse(identification_feature1Magnification.Text);
                    if (Directory.Exists(sourcePath + @"\Feature1"))
                    {
                        sampleFeature1Data = _rd.Rdouble(sourcePath + @"\Feature1\sample\");
                        testFeature1Data = _rd.Rdouble(sourcePath + @"\Feature1\test\");
                    }
                    else
                    {
                        MessageBox.Show("特徵1不存在");
                    }
                }

                //讀入特徵2
                List<List<double[]>> sampleFeature2Data = null;
                List<List<double[]>> testFeature2Data = null;
                if (identificationFeature2CheckBox.Checked)
                {
                    if (_nameStr != null) _nameStr += "+";
                    _nameStr += "f2x" + identification_feature2Magnification.Text;

                    f2m = int.Parse(identification_feature2Magnification.Text);
                    if (Directory.Exists(sourcePath + @"\Feature2"))
                    {
                        sampleFeature2Data = _rd.Rdouble(sourcePath + @"\Feature2\sample\");
                        testFeature2Data = _rd.Rdouble(sourcePath + @"\Feature2\test\");
                    }
                    else
                    {
                        MessageBox.Show("特徵2不存在");
                    }
                }

                //讀入特徵3
                List<List<double[]>> sampleFeature3Data = null;
                List<List<double[]>> testFeature3Data = null;
                if (identificationFeature3CheckBox.Checked)
                {
                    if (_nameStr != null) _nameStr += "+";
                    _nameStr += "f3x" + identification_feature3Magnification.Text;

                    f3m = int.Parse(identification_feature3Magnification.Text);
                    if (Directory.Exists(sourcePath + @"\Feature3"))
                    {
                        sampleFeature3Data = _rd.Rdouble(sourcePath + @"\Feature3\sample\");
                        testFeature3Data = _rd.Rdouble(sourcePath + @"\Feature3\test\");
                    }
                    else
                    {
                        MessageBox.Show("特徵3不存在");
                    }

                }

                //讀入特徵4
                List<List<int[]>> sampleFeature4Data = null;
                List<List<int[]>> testFeature4Data = null;
                if (identificationFeature4CheckBox.Checked || identificationFeature4_2CheckBox.Checked || identificationFeature4_3CheckBox.Checked)
                {
                    if (_nameStr != null) _nameStr += "+";
                    if (identificationFeature4CheckBox.Checked)
                        _nameStr += "f4x" + identification_feature4Magnification.Text;
                    else if (identificationFeature4_2CheckBox.Checked)
                        _nameStr += "f4_2x" + identification_feature4Magnification.Text;
                    else if (identificationFeature4_3CheckBox.Checked)
                        _nameStr += "f4_3x" + identification_feature4Magnification.Text;
                    f4m = int.Parse(identification_feature4Magnification.Text);
                    if (Directory.Exists(sourcePath + @"\Feature4"))
                    {
                        sampleFeature4Data = _rd.Rint(sourcePath + @"\Feature4\sample\");
                        testFeature4Data = _rd.Rint(sourcePath + @"\Feature4\test\");
                    }
                    else
                    {
                        MessageBox.Show("特徵4不存在");
                    }
                }

                //讀入特徵4-1
                List<List<double[]>> sampleFeature4_1Data = null;
                List<List<double[]>> testFeature4_1Data = null;
                if (identificationFeature4_1CheckBox.Checked)
                {
                    if (_nameStr != null) _nameStr += "+";
                    _nameStr += "f4_1x" + identification_feature4Magnification.Text;

                    f4m = int.Parse(identification_feature4Magnification.Text);
                    if (Directory.Exists(sourcePath + @"\Feature4_1"))
                    {
                        sampleFeature4_1Data = _rd.Rdouble(sourcePath + @"\Feature4_1\sample\");
                        testFeature4_1Data = _rd.Rdouble(sourcePath + @"\Feature4_1\test\");
                    }
                    else
                    {
                        MessageBox.Show("特徵4-1不存在");
                    }
                }

                //讀入特徵4-4
                List<List<int[]>> sampleFeature4_4Data = null;
                List<List<int[]>> testFeature4_4Data = null;
                if (identificationFeature4_4CheckBox.Checked)
                {
                    if (_nameStr != null) _nameStr += "+";
                    _nameStr += "f4_4x" + identification_feature4Magnification.Text;

                    f4m = int.Parse(identification_feature4Magnification.Text);
                    if (Directory.Exists(sourcePath + @"\Feature4"))
                    {
                        sampleFeature4_4Data = _rd.Rint(sourcePath + @"\Feature4_4\sample\");
                        testFeature4_4Data = _rd.Rint(sourcePath + @"\Feature4_4\test\");
                    }
                    else
                    {
                        MessageBox.Show("特徵4-4不存在");
                    }
                }

                //讀入特徵4-5
                List<List<double[]>> sampleFeature4_5Data = null;
                List<List<double[]>> testFeature4_5Data = null;
                if (identificationFeature4_5CheckBox.Checked)
                {
                    if (_nameStr != null) _nameStr += "+";
                    _nameStr += "f4_5_" + identification_feature4_5_magnification.Text + "x" + identification_feature4Magnification.Text;

                    f4m = int.Parse(identification_feature4Magnification.Text);
                    if (Directory.Exists(sourcePath + @"\Feature4_5"))
                    {
                        sampleFeature4_5Data = _rd.Rdouble(sourcePath + @"\Feature4_5\sample\");
                        testFeature4_5Data = _rd.Rdouble(sourcePath + @"\Feature4_5\test\");
                    }
                    else
                    {
                        MessageBox.Show("特徵4-5不存在");
                    }
                }

                //讀入特徵4-6
                List<List<double[]>> sampleFeature4_6Data = null;
                List<List<double[]>> testFeature4_6Data = null;
                if (identificationFeature4_6CheckBox.Checked || identificationFeature4_6maCheckBox.Checked ||
                    identificationFeature4_8CheckBox.Checked ||
                    identificationFeature4_7CheckBox.Checked || identificationFeature4_7maCheckBox.Checked)
                {
                    if (_nameStr != null) _nameStr += "+";
                    if (identificationFeature4_6CheckBox.Checked || identificationFeature4_7CheckBox.Checked)
                    {
                        if (identificationFeature4_6CheckBox.Checked)
                            _nameStr += "f4_6f_" + identification_feature4_6_magnification.Text + "x" + identification_feature4Magnification.Text;
                        else
                            _nameStr += "f4_6m_" + identification_feature4_6_magnification.Text + "x" + identification_feature4Magnification.Text;
                    }
                    else if (identificationFeature4_8CheckBox.Checked)
                        _nameStr += "f4_8_b" + identification_feature4_8_magnification.Text + "x" + identification_feature4Magnification.Text;
                    else
                    {
                        if (identificationFeature4_6maCheckBox.Checked)
                            _nameStr += "f4_6fma_" + identification_feature4_6_magnification.Text + "x" + identification_feature4Magnification.Text;
                        else
                            _nameStr += "f4_6mma_" + identification_feature4_6_magnification.Text + "x" + identification_feature4Magnification.Text;
                    }

                    f4m = int.Parse(identification_feature4Magnification.Text);
                    if (Directory.Exists(sourcePath + @"\Feature4_6") &&
                        (identificationFeature4_6CheckBox.Checked || identificationFeature4_6maCheckBox.Checked || identificationFeature4_8CheckBox.Checked))
                    {
                        sampleFeature4_6Data = _rd.Rdouble(sourcePath + @"\Feature4_6\sample\");
                        testFeature4_6Data = _rd.Rdouble(sourcePath + @"\Feature4_6\test\");
                    }
                    else if (Directory.Exists(sourcePath + @"\Feature4_7") &&
                            (identificationFeature4_7CheckBox.Checked || identificationFeature4_7maCheckBox.Checked))
                    {
                        sampleFeature4_6Data = _rd.Rdouble(sourcePath + @"\Feature4_7\sample\");
                        testFeature4_6Data = _rd.Rdouble(sourcePath + @"\Feature4_7\test\");
                    }
                    else
                    {
                        MessageBox.Show("特徵4-6不存在");
                    }
                }

                //讀入特徵5
                List<List<double[]>> sampleFeature5Data = null;
                List<List<double[]>> testFeature5Data = null;
                if (identificationFeature5CheckBox.Checked || identificationFeature5SqrtCheckBox.Checked)
                {
                    if (_nameStr != null) _nameStr += "+";
                    if (identificationFeature5CheckBox.Checked)
                        _nameStr += "f5x" + identification_feature5Magnification.Text;
                    else if (identificationFeature5SqrtCheckBox.Checked)
                        _nameStr += "f5sx" + identification_feature5Magnification.Text;

                    f5m = int.Parse(identification_feature5Magnification.Text);
                    if (Directory.Exists(sourcePath + @"\Feature5"))
                    {
                        sampleFeature5Data = _rd.Rdouble(sourcePath + @"\Feature5\sample\");
                        testFeature5Data = _rd.Rdouble(sourcePath + @"\Feature5\test\");
                    }
                    else
                    {
                        MessageBox.Show("特徵5不存在");
                    }

                }

                resultDirPath = resultPath + "(" + _nameStr + ")result";
                _dm.DeleteAndCreate(resultDirPath);


                //辨識開始
                total = n = 0;
                int testSum = 0, strokeSum = 0;
                for (i = 0; i < testNameData.Count; i++)
                {
                    using (StreamWriter sw = File.CreateText(resultDirPath + @"\" + (i + 1) + ".txt"))
                    {
                        for (j = 0; j < testNameData[i].Count; j++)
                        {
                            total++;

                            if (identificationFeature4_4CheckBox.Checked)
                            {
                                int thisStrokes = Calculate_For_4_4_FarPoint(testFeature4_4Data[i][j], testFourDirection[i][j][0]);
                                if (thisStrokes > 0)
                                {
                                    testSum++;
                                    strokeSum += thisStrokes;
                                }
                            }

                            List<double> FeatureD = new List<double>();
                            List<int[]> FeatureN = new List<int[]>();
                            for (k = 0; k < sampleNameData.Count; k++)
                            {
                                for (l = 0; l < sampleNameData[k].Count; l++)
                                {
                                    int stemp = int.Parse(sampleNameData[k][l].Split('_')[0]) - 1;
                                    if ((i >= strokeTI[strokes[stemp]][1]) && (i <= strokeTI[strokes[stemp]][2]))
                                    {
                                        double feature = 0;

                                        if (identificationFeature1CheckBox.Checked)
                                            feature += _cf.Feature1(testFeature1Data[i][j], testFourDirection[i][j][0], sampleFeature1Data[k][l], sampleFourDirection[k][l][0]);
                                        if (identificationFeature2CheckBox.Checked)
                                            feature += _cf.Feature2(testFeature2Data[i][j], testFourDirection[i][j][0], sampleFeature2Data[k][l], sampleFourDirection[k][l][0]);
                                        if (identificationFeature3CheckBox.Checked)
                                            feature += _cf.Feature3(testFeature3Data[i][j], testFourDirection[i][j][0], sampleFeature3Data[k][l], sampleFourDirection[k][l][0]);
                                        if (identificationFeature4CheckBox.Checked)
                                            feature += _cf.Feature4(testFeature4Data[i][j], testFourDirection[i][j][0], sampleFeature4Data[k][l], sampleFourDirection[k][l][0]);
                                        if (identificationFeature4_1CheckBox.Checked)
                                            feature += _cf.Feature4_1(testFeature4_1Data[i][j], testFourDirection[i][j][0], sampleFeature4_1Data[k][l], sampleFourDirection[k][l][0]);
                                        if (identificationFeature4_2CheckBox.Checked)
                                            feature += _cf.Feature4_2(testFeature4Data[i][j], testFourDirection[i][j][0], sampleFeature4Data[k][l], sampleFourDirection[k][l][0]);
                                        if (identificationFeature4_3CheckBox.Checked)
                                            feature += _cf.Feature4_3(testFeature4Data[i][j], testFeature1Data[i][j], testFourDirection[i][j][0], sampleFeature4Data[k][l], sampleFeature1Data[k][l], sampleFourDirection[k][l][0]);
                                        if (identificationFeature4_4CheckBox.Checked)
                                            feature = _cf.Feature4_4(testFeature4_4Data[i][j], testFeature1Data[i][j], testFourDirection[i][j][0], sampleFeature4_4Data[k][l], sampleFeature1Data[k][l], sampleFourDirection[k][l][0]);
                                        if (identificationFeature4_5CheckBox.Checked)
                                            feature = _cf.Feature4_5(testFeature4_5Data[i][j], testFeature1Data[i][j], testFourDirection[i][j][0], sampleFeature4_5Data[k][l], sampleFeature1Data[k][l], sampleFourDirection[k][l][0], int.Parse(identification_feature4_5_magnification.Text));
                                        if (identificationFeature4_6CheckBox.Checked || identificationFeature4_7CheckBox.Checked)
                                            feature = _cf.Feature4_6(testFeature4_6Data[i][j], testFeature1Data[i][j], testFourDirection[i][j][0], sampleFeature4_6Data[k][l], sampleFeature1Data[k][l], sampleFourDirection[k][l][0], int.Parse(identification_feature4_6_magnification.Text));
                                        if (identificationFeature4_6maCheckBox.Checked || identificationFeature4_7maCheckBox.Checked)
                                            feature = _cf.Feature4_6ma(testFeature4_6Data[i][j], testFeature1Data[i][j], testFourDirection[i][j][0], sampleFeature4_6Data[k][l], sampleFeature1Data[k][l], sampleFourDirection[k][l][0], int.Parse(identification_feature4_6_magnification.Text));
                                        if (identificationFeature4_8CheckBox.Checked)
                                            feature = _cf.Feature4_8(testFeature4_6Data[i][j], testFeature1Data[i][j], testFourDirection[i][j][0], sampleFeature4_6Data[k][l], sampleFeature1Data[k][l], sampleFourDirection[k][l][0], double.Parse(identification_feature4_8_magnification.Text));
                                        if (identificationFeature5CheckBox.Checked)
                                            feature += _cf.Feature5(testFeature5Data[i][j], testFourDirection[i][j][0], sampleFeature5Data[k][l], sampleFourDirection[k][l][0]);
                                        if (identificationFeature5SqrtCheckBox.Checked)
                                            feature += _cf.Feature5s(testFeature5Data[i][j], testFourDirection[i][j][0], sampleFeature5Data[k][l], sampleFourDirection[k][l][0]);

                                        FeatureD.Add(feature);
                                        FeatureN.Add(new int[] { k, l });
                                    }//for(l)
                                }//if(range)
                            }//for(k)

                            if (FeatureN.Count > 0)
                            {
                                double[] featureArr = FeatureD.ToArray();
                                int minIndex = Array.IndexOf(featureArr, featureArr.Min());

                                sw.WriteLine("{0} = {1} : {2}", testNameData[i][j], sampleNameData[FeatureN[minIndex][0]][FeatureN[minIndex][1]], FeatureD[minIndex]);

                                if (testNameData[i][j].Split('_')[0] == sampleNameData[FeatureN[minIndex][0]][FeatureN[minIndex][1]].Split('_')[0])
                                {
                                    n++;
                                }

                            }
                        }//for(j)
                    }
                }//for(i)

                double idRate = (n * 100 / (double)total);

                if (identificationFeature4_4CheckBox.Checked)
                {
                    using (StreamWriter sw = File.CreateText(resultDirPath + @"\0result.txt"))
                    {
                        sw.WriteLine("辨識率：{0} %, 配對成功數量：{1}, 測試檔數量：{2}, 有達最遠點標準的測試檔數量：{3}, 其中有達標的筆劃數量：{4}",
                            idRate.ToString("0.0000"), n, total, testSum, strokeSum);
                    }
                    MessageBox.Show("辨識率為 " + idRate.ToString("0.0000") + "%");
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(resultDirPath + @"\0result.txt"))
                    {
                        sw.WriteLine("辨識率：{0} %, 配對成功數量：{1}, 測試檔數量：{2}", idRate.ToString("0.0000"), n, total);
                    }
                    MessageBox.Show("辨識率為 " + idRate.ToString("0.0000") + "%");
                }



            }
            else
                MessageBox.Show("未選取要辨識之特徵");
        }

        private void identification_dataSourceFolderNameButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.SelectedPath = mainFolderPath.Text + identification_dataSourceFolderName.Text;
            path.ShowDialog();
            other_combine_sourceFolderName.Text = Path.GetFileNameWithoutExtension(path.SelectedPath);
        }

        public int Calculate_For_4_4_FarPoint(int[] a, int[] aidx)
        {
            int i, sum = 0;
            for (i = 0; i < aidx.Length; i++)
            {
                if (a[(i * 7) + 6] > 0)
                    sum++;
            }
            return sum;
        }
        public double Feature_Add(double[] featureArr, int num)
        {
            int i, value = num, sum = 0;
            double feature = 0;

            for (i = 0; i < num; i++)
            {
                feature += featureArr[i] * value;
                sum += value;
                value--;
            }

            return feature / sum;
        }

        //功能鈕 h
        private void button15_Click(object sender, EventArgs e)
        {
            int i, j, k, l;
            string str, sourcePath, savePath;
            List<List<List<string>>> sourceData = new List<List<List<string>>>();

            sourcePath = mainFolderPath.Text + other_getStroke_dataSourceFolderName.Text;
            savePath = mainFolderPath.Text + other_getStroke_dataSaveFileName.Text;

            //取數據開始 >>>
            foreach (string filePath in Directory.GetFileSystemEntries(sourcePath, "*.txt"))
            {
                int needCapacityOne = int.Parse(Path.GetFileNameWithoutExtension(filePath));
                if (sourceData.Count < needCapacityOne)
                {
                    int difference = needCapacityOne - sourceData.Count;
                    for (i = 0; i < difference; i++)
                        sourceData.Add(new List<List<string>>());
                }

                StreamReader file = new StreamReader(filePath);
                int needCapacityTwo = 0;
                while ((str = file.ReadLine()) != null)
                {
                    if (str.Length == 3)
                    {
                        needCapacityTwo = int.Parse(str.Substring(1, 2));
                        if (sourceData[needCapacityOne - 1].Count < needCapacityTwo)
                        {
                            int difference = needCapacityTwo - sourceData[needCapacityOne - 1].Count;
                            for (i = 0; i < difference; i++)
                                sourceData[needCapacityOne - 1].Add(new List<string>());
                        }
                        sourceData[needCapacityOne - 1][needCapacityTwo - 1].Add(str);
                    }
                    else
                        sourceData[needCapacityOne - 1][needCapacityTwo - 1].Add(str);
                }
            }
            //取數據結束 <<<

            using (StreamWriter sw = File.CreateText(savePath))
            {
                for (i = 0; i < sourceData.Count; i++)
                {
                    str = null;
                    for (j = 0; j < sourceData[i].Count; j++)
                    {
                        if (j != 0) str += ",";
                        if (sourceData[i][j].Count != 0)
                        {
                            l = 1;
                            for (k = 2; k < sourceData[i][j].Count; k++)
                            {
                                if (sourceData[i][j][k].Substring(6, 2) != sourceData[i][j][k - 1].Substring(6, 2))
                                    l++;
                            }
                            str += l;
                        }
                        else
                            str += "0";
                    }

                    if (str.Split(',').Length != 23)
                    {
                        int needCapacity = 23 - str.Split(',').Length;
                        for (j = 0; j < needCapacity; j++)
                            str += ",0";
                    }

                    sw.WriteLine(str);
                }
            }



        }

        private void button14_Click(object sender, EventArgs e)
        {
            int i, j, k, l;
            double sum;
            string str;

            List<int[]> strokeRage = new List<int[]>();
            List<int[]> strokes = new List<int[]>();

            StreamReader file = new StreamReader(@"D:\Folder\C#\WorkingArea\Job4\strokeRage.txt");
            while ((str = file.ReadLine()) != null)
            {
                strokeRage.Add(Array.ConvertAll(str.Split(','), int.Parse));
            }

            file = new StreamReader(@"D:\Folder\C#\WorkingArea\Job4\strokes.txt");
            while ((str = file.ReadLine()) != null)
            {
                strokes.Add(Array.ConvertAll(str.Split(','), int.Parse));
            }

            int[] n = new int[41];
            using (StreamWriter sw = File.CreateText(@"D:\Folder\C#\WorkingArea\Job4\data111.txt"))
            {
                for (i = 0; i < strokeRage.Count; i++)
                {
                    sw.Write("筆劃{0}: 範圍：( {1} - {2} )", strokeRage[i][0], strokeRage[i][1], strokeRage[i][2]);
                    Array.Clear(n, 0, n.Length);
                    sum = 0;
                    for (j = 0; j < n.Length; j++)
                    {
                        for (k = strokeRage[i][1]; k <= strokeRage[i][2]; k++)
                        {
                            for (l = 0; l < strokes[k].Length; l++)
                            {
                                if (strokes[k][l] == j) n[j]++;
                            }
                        }
                    }

                    for (j = 1; j < n.Length; j++)
                    {
                        sum += n[j];
                    }

                    sw.Write("  總共：{0}個", sum);
                    if (n[0] != 0) sw.WriteLine(" , 漏字數：{0}個", n[0]);
                    else sw.WriteLine();

                    for (j = 0; j < n.Length; j++)
                    {
                        if (n[j] != 0)
                        {
                            sw.WriteLine("  {0}: {1}個 -- {2}%", j, n[j], (n[j] * 100 / sum).ToString("0.00"));
                        }
                    }
                    sw.WriteLine();
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        //功能鈕 t

        //手寫板 h
        List<string> _mouseDrawData = new List<String>();
        List<string> _mouseDrawAllData = new List<String>();
        List<double[]> _wbData1 = new List<double[]>();
        double[] _wbD1;
        List<double[]> _wbData4_6 = new List<double[]>();
        double[] _wbD4_6;

        private void writingBoard_ReadDataBut_Click(object sender, EventArgs e)
        {
            int i, j;

            comboBox5.Items.Clear();
            comboBox4.Items.Clear();
            for (i = 1; i <= 5399; i++)
            {
                comboBox5.Items.Add(i);
                byte[] t = { (byte)(_cbig5.SEQtoBIG5(i) / 256), (byte)(_cbig5.SEQtoBIG5(i) % 256) };
                comboBox4.Items.Add(Encoding.GetEncoding("Big5").GetString(t));
            }

            string sourcePath = mainFolderPath.Text + identification_dataSourceFolderName.Text;



            //同筆畫範圍
            _strokeRange = _rd.RintArr(mainFolderPath.Text + identification_strokeRangeFileName.Text);

            for (i = 0; i < _strokeRange.Count; i++)
            {
                for (j = _strokeRange[i][1]; j <= _strokeRange[i][2]; j++)
                {
                    _strokes.Add(_strokeRange[i][0] - 1);
                }
            }

            //筆畫區間
            _strokeTI = _rd.RintArr(mainFolderPath.Text + identification_strokeToleranceIntervalFileName.Text);
            for (i = 0; i < _strokeTI.Count; i++)
            {
                _strokeTI[i][1]--;
                _strokeTI[i][2]--;
            }

            //讀入特徵1
            if (Directory.Exists(sourcePath))
            {
                _sampleNameData = _rd.Rstring(sourcePath + @"\name\sample\");
                _sampleFeature1Data = _rd.Rdouble(sourcePath + @"\Feature1\sample\");
                _sampleFeature4Data = _rd.Rdouble(sourcePath + @"\Feature4_6\sample\");
            }
            else
            {
                MessageBox.Show("特徵不存在");
            }
        }

        Point _lastPoint = Point.Empty;
        bool _isMouseDown = new Boolean();
        int _strokeTimes = 0;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (_sampleNameData == null || _strokeRange == null || _strokes == null || _strokeTI == null)
            if (comboBox5.Items.Count == 0)
            {
                MessageBox.Show("未讀入資料");
            }
            else
            {
                _lastPoint = e.Location;

                _isMouseDown = true;
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown == true)

            {

                if (_lastPoint != null)

                {
                    _mouseDrawData.Add(_lastPoint.X.ToString("000") + _lastPoint.Y.ToString("000") + _strokeTimes.ToString("00"));

                    if (pictureBox1.Image == null)

                    {
                        Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

                        pictureBox1.Image = bmp;

                    }

                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        g.DrawLine(new Pen(Color.Black, 4), _lastPoint, e.Location);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                    }

                    pictureBox1.Invalidate();

                    _lastPoint = e.Location;

                }

            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int i, j, k, l = 1;

            _isMouseDown = false;
            _mouseDrawData.Add(_lastPoint.X.ToString("000") + _lastPoint.Y.ToString("000") + _strokeTimes.ToString("00"));

            if (_mouseDrawData.Count > 1)
            {
                foreach (string s in _mouseDrawData)
                    _mouseDrawAllData.Add(s);

                _strokeTimes++;
                _lastPoint = Point.Empty;

                listBox1.Items.Clear();

                double[] mouseDrawDataFeature1 = _gd.GetFeature1(_mouseDrawAllData).ToArray();
                double[] mouseDrawDataFeature2 = _gd.GetFeature2(_mouseDrawAllData).ToArray();

                // < 辨識
                List<double> FeatureD = new List<double>();
                List<int[]> FeatureN = new List<int[]>();
                if (comboBox5.SelectedIndex == -1)
                {
                    for (i = 0; i < _sampleNameData.Count; i++)
                    {
                        for (j = 0; j < _sampleNameData[i].Count; j++)
                        {
                            int stemp = int.Parse(_sampleNameData[i][j].Split('_')[0]) - 1;
                            if (((mouseDrawDataFeature1.Length - 1) >= _strokeTI[_strokes[stemp]][1]) && ((mouseDrawDataFeature1.Length - 1) <= _strokeTI[_strokes[stemp]][2]))
                            {
                                double feature = 0;
                                feature += _cf.Calculate_Feature4_6(mouseDrawDataFeature2, mouseDrawDataFeature1, _sampleFeature4Data[i][j], _sampleFeature1Data[i][j]);


                                FeatureD.Add(feature);
                                FeatureN.Add(new int[] { i, j });
                            }//for(j)
                        }//if(range)
                    }//for(i)

                    var min_three = (from s in FeatureD
                                     orderby s
                                     select s).Take(20);

                    foreach (double d in min_three)
                    {
                        for (i = 0; i < FeatureD.Count; i++)
                        {
                            if (FeatureD[i] == d)
                            {
                                k = int.Parse(_sampleNameData[FeatureN[i][0]][FeatureN[i][1]].Split('_')[0]);
                                byte[] t = { (byte)(_cbig5.SEQtoBIG5(k) / 256), (byte)(_cbig5.SEQtoBIG5(k) % 256) };// k改成數字
                                listBox1.Items.Add((l++).ToString("00") + "." + Encoding.GetEncoding("Big5").GetString(t) + d.ToString("\t(0.00)"));
                            }
                        }

                    }
                }
                else
                {
                    double feature = 0;
                    feature += _cf.Calculate_Feature4_6(mouseDrawDataFeature2, mouseDrawDataFeature1, _wbD4_6, _wbD1);


                    k = comboBox5.SelectedIndex + 1;
                    byte[] t = { (byte)(_cbig5.SEQtoBIG5(k) / 256), (byte)(_cbig5.SEQtoBIG5(k) % 256) };// k改成數字
                    listBox1.Items.Add((l++).ToString("00") + "." + Encoding.GetEncoding("Big5").GetString(t) + feature.ToString("\t(0.00)"));
                }
                // > 辨識
            }
            _mouseDrawData.Clear();
        }

        private void writingBoard_clearBut_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                _mouseDrawAllData = new List<String>();
                pictureBox1.Image = null;
                _strokeTimes = 0;
                Invalidate();
                listBox1.Items.Clear();
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int i, j;

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            for (i = 1; i <= 5399; i++)
            {
                comboBox1.Items.Add(i);
                byte[] t = { (byte)(_cbig5.SEQtoBIG5(i) / 256), (byte)(_cbig5.SEQtoBIG5(i) % 256) };
                comboBox2.Items.Add(Encoding.GetEncoding("Big5").GetString(t));
            }
            string sourcePath = mainFolderPath.Text + identification_dataSourceFolderName.Text;

            ReadData rd = new ReadData();

            //同筆畫範圍
            _strokeRange = rd.RintArr(mainFolderPath.Text + identification_strokeRangeFileName.Text);

            for (i = 0; i < _strokeRange.Count; i++)
            {
                for (j = _strokeRange[i][1]; j <= _strokeRange[i][2]; j++)
                {
                    _strokes.Add(_strokeRange[i][0] - 1);
                }
            }

            //筆畫區間
            _strokeTI = rd.RintArr(mainFolderPath.Text + identification_strokeToleranceIntervalFileName.Text);
            for (i = 0; i < _strokeTI.Count; i++)
            {
                _strokeTI[i][1]--;
                _strokeTI[i][2]--;
            }

            //讀入特徵1
            if (Directory.Exists(sourcePath))
            {
                _sampleNameData = rd.Rstring(sourcePath + @"\name\sample\");
                _sampleFeature1Data = rd.Rdouble(sourcePath + @"\Feature1\sample\");
                _sampleFeature4Data = rd.Rdouble(sourcePath + @"\Feature4_6\sample\");
            }
            else
            {
                MessageBox.Show("特徵不存在");
            }
        }
        //手寫板 t


        //資料庫 h
        List<List<string>> _Data = new List<List<string>>();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            comboBox2.SelectedIndex = comboBox1.SelectedIndex;
            comboBox3.SelectedIndex = -1;


            int i = -1;
            string str, path;


            path = mainFolderPath.Text + textBox4.Text + @"\" + int.Parse(comboBox1.Text).ToString("0000");

            comboBox3.Items.Clear();
            foreach (string s in Directory.GetFileSystemEntries(path, "*.bmp"))
            {
                comboBox3.Items.Add(Path.GetFileNameWithoutExtension(s));
            }

            path = mainFolderPath.Text + textBox1.Text + @"\" + int.Parse(comboBox1.Text).ToString("0000") + ".txt";
            StreamReader file = new StreamReader(path);
            _Data = new List<List<string>>();
            i = -1;
            while ((str = file.ReadLine()) != null)
            {
                if (str.Length == 3)
                {
                    i++;
                    _Data.Add(new List<string>());
                    _Data[i].Add(str);
                }
                else
                    _Data[i].Add(str);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboBox2.SelectedIndex;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex != -1)
            {
                int i;
                string path;
                comboBox4.SelectedIndex = comboBox5.SelectedIndex;

                path = mainFolderPath.Text + @"Feature\Feature1\forWB\" + comboBox5.Text + ".txt";
                _wbData1 = _rd.R2double(path);

                path = mainFolderPath.Text + @"Feature\Feature4_6\forWB\" + comboBox5.Text + ".txt";
                _wbData4_6 = _rd.R2double(path);

                comboBox6.Items.Clear();
                for (i = 0; i < _wbData4_6.Count; i++)
                {
                    comboBox6.Items.Add(i + 1);
                }

                if (comboBox6.SelectedIndex == -1)
                {
                    comboBox6.SelectedIndex = 0;
                }
            }

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex != -1)
            {
                comboBox5.SelectedIndex = comboBox4.SelectedIndex;
            }

        }

        private void button21_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.SelectedPath = mainFolderPath.Text + textBox1;
            path.ShowDialog();
            textBox1.Text = Path.GetFileNameWithoutExtension(path.SelectedPath);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            int i, j;
            string str, sourcePath, savePath;

            sourcePath = mainFolderPath.Text + textBox3.Text;
            savePath = mainFolderPath.Text + textBox2.Text;

            _dm.DeleteAndCreate(savePath);

            foreach (string path in Directory.GetFileSystemEntries(sourcePath, "*.txt"))
            {
                string dirPath = savePath + @"\" + Path.GetFileNameWithoutExtension(path);
                _dm.DeleteAndCreate(dirPath);

                StreamReader file = new StreamReader(path);
                List<List<string>> sourceData = new List<List<string>>();

                i = -1;
                while ((str = file.ReadLine()) != null)
                {
                    if (str.Length == 3)
                    {
                        i++;
                        sourceData.Add(new List<string>());
                        sourceData[i].Add(str);
                    }
                    else
                        sourceData[i].Add(str);
                }

                foreach (List<string> data in sourceData)
                {
                    if (data.Count != 0)
                    {
                        int x, y, maxX, maxY;
                        List<int> xp = new List<int>();
                        List<int> yp = new List<int>();
                        List<int> headIndex = new List<int>();

                        headIndex.Add(0);
                        maxX = int.Parse(data[1].Substring(0, 3));
                        maxY = int.Parse(data[1].Substring(3, 3));
                        xp.Add(maxX);
                        yp.Add(maxY);

                        for (i = 2; i < data.Count; i++)
                        {
                            x = int.Parse(data[i].Substring(0, 3));
                            y = int.Parse(data[i].Substring(3, 3));
                            xp.Add(x);
                            yp.Add(y);

                            if (x > maxX)
                                maxX = x;
                            if (y > maxY)
                                maxY = y;

                            if (data[i].Substring(6, 2) != data[i - 1].Substring(6, 2))
                            {
                                headIndex.Add(i - 1);
                            }
                        }
                        headIndex.Add(i - 1);
                        Bitmap bmp = new Bitmap(maxX + 1, maxY + 1);
                        Graphics g = Graphics.FromImage(bmp);
                        for (i = 1; i < headIndex.Count; i++)
                        {
                            for (j = headIndex[i - 1] + 1; j < headIndex[i]; j++)
                            {
                                g.DrawLine(new Pen(Color.Black, 2), xp[j - 1], yp[j - 1], xp[j], yp[j]);
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                            }
                        }
                        bmp.Save(dirPath + @"\" + data[0].Substring(1, 2) + ".bmp");
                    }
                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if(comboBox1.Items.Count == 0)
            {
                MessageBox.Show("未產生資料");
            }
            else if(comboBox1.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("未選取字型");
            }
            else
            {
                int i, j, sum = 0;
                foreach (List<string> data in _Data)
                {
                    double[] Feature1Arr = _gd.GetFeature1(data).ToArray();
                    double[] Feature2Arr = _gd.GetFeature2(data).ToArray();

                    // < 辨識
                    List<double> FeatureD = new List<double>();
                    List<int[]> FeatureN = new List<int[]>();
                    for (i = 0; i < _sampleNameData.Count; i++)
                    {
                        for (j = 0; j < _sampleNameData[i].Count; j++)
                        {
                            int stemp = int.Parse(_sampleNameData[i][j].Split('_')[0]) - 1;
                            if (((Feature1Arr.Length - 1) >= _strokeTI[_strokes[stemp]][1]) && ((Feature1Arr.Length - 1) <= _strokeTI[_strokes[stemp]][2]))
                            {
                                double feature = 0;
                                feature += _cf.Calculate_Feature4_6(Feature2Arr, Feature1Arr, _sampleFeature4Data[i][j], _sampleFeature1Data[i][j]);


                                FeatureD.Add(feature);
                                FeatureN.Add(new int[] { i, j });
                            }//for(j)
                        }//if(range)
                    }//for(i)

                    // > 辨識

                    var min_three = (from s in FeatureD
                                     orderby s
                                     select s).Take(1);

                    foreach (double d in min_three)
                    {
                        for (i = 0; i < FeatureD.Count; i++)
                        {
                            if (FeatureD[i] == d)
                            {

                                if ((int.Parse(comboBox1.Text)).ToString() == _sampleNameData[FeatureN[i][0]][FeatureN[i][1]].Split('_')[0])
                                    sum++;
                            }
                        }

                    }
                }
                label23.Text = "( " + comboBox1.Text + "_" + comboBox3.Text + " - " + comboBox2.Text + " )辨識率： " + sum + " / " + comboBox3.Items.Count + "\t" + (sum / (double)comboBox3.Items.Count).ToString("(0.00% )");
            }                
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label23.Text = "辨識率：";
            if (comboBox3.SelectedIndex != -1)
            {
                string path = mainFolderPath.Text + textBox4.Text + @"\" + int.Parse(comboBox1.Text).ToString("0000") + @"\" + comboBox3.Text + ".bmp";
                Bitmap bmp = new Bitmap(path);
                pictureBox2.Image = bmp;
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex != -1)
            {
                string path = mainFolderPath.Text + textBox4.Text + @"\" + int.Parse(comboBox5.Text).ToString("0000") + @"\" + int.Parse(comboBox6.Text).ToString("00") + ".bmp";
                Bitmap bmp = new Bitmap(path);
                pictureBox1.Image = bmp;

                //int k, l = 1;

                //_wbD1 = _wbData1[comboBox6.SelectedIndex];
                //_wbD4_6 = _wbData4_6[comboBox6.SelectedIndex];

                //double[] mouseDrawDataFeature1 = _gd.GetFeature1(_mouseDrawAllData).ToArray();
                //double[] mouseDrawDataFeature2 = _gd.GetFeature2(_mouseDrawAllData).ToArray();

                //double feature = 0;
                //feature += _cf.Calculate_Feature4_6(mouseDrawDataFeature2, mouseDrawDataFeature1, _wbD4_6, _wbD1);

                //listBox1.Items.Clear();

                //k = comboBox5.SelectedIndex + 1;
                //byte[] t = { (byte)(_cbig5.SEQtoBIG5(k) / 256), (byte)(_cbig5.SEQtoBIG5(k) % 256) };// k改成數字
                //listBox1.Items.Add((l++).ToString("00") + "." + Encoding.GetEncoding("Big5").GetString(t) + feature.ToString("\t(0.00)"));
            }
        
        }

        private void button26_Click(object sender, EventArgs e)
        {
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            int i, j, k, l = 1, n1 = comboBox5.SelectedIndex + 1, n2 = comboBox5.SelectedIndex + 1;

            _wbD1 = _wbData1[comboBox6.SelectedIndex];
            _wbD4_6 = _wbData4_6[comboBox6.SelectedIndex];


            List<double> FeatureD = new List<double>();
            List<int[]> FeatureN = new List<int[]>();

            for (i = 0; i < _sampleNameData.Count; i++)
            {
                for (j = 0; j < _sampleNameData[i].Count; j++)
                {
                    int stemp = int.Parse(_sampleNameData[i][j].Split('_')[0]) - 1;
                    if (((_wbD1.Length - 1) >= _strokeTI[_strokes[stemp]][1]) && ((_wbD1.Length - 1) <= _strokeTI[_strokes[stemp]][2]))
                    {
                        double feature = 0;
                        feature += _cf.Calculate_Feature4_6(_wbD4_6, _wbD1, _sampleFeature4Data[i][j], _sampleFeature1Data[i][j]);


                        FeatureD.Add(feature);
                        FeatureN.Add(new int[] { i, j });
                    }//for(j)
                }//if(range)
            }//for(i)

            var min_three = (from s in FeatureD
                             orderby s
                             select s).Take(20);

            listBox1.Items.Clear();

            foreach (double d in min_three)
            {
                for (i = 0; i < FeatureD.Count; i++)
                {
                    if (FeatureD[i] == d)
                    {
                        k = int.Parse(_sampleNameData[FeatureN[i][0]][FeatureN[i][1]].Split('_')[0]);
                        byte[] t = { (byte)(_cbig5.SEQtoBIG5(k) / 256), (byte)(_cbig5.SEQtoBIG5(k) % 256) };// k改成數字
                        listBox1.Items.Add((l++).ToString("00") + "." + Encoding.GetEncoding("Big5").GetString(t) + d.ToString("\t(0.00)"));
                    }
                }

            }
        }
        //資料庫 t
    }

}