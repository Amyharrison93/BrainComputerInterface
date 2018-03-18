using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Neural_network
{
    public partial class Form1 : Form
    {
        string
            info,
            FileName,
            Neural,
            Kinematic;
        int //flag sinitialise
            Flag = 0,
            neuralCount=0,
            KinematicCount=0;
        int[]
            //EEG sensor channels
            AF3 = new int[5], AF4 = new int[5],
            F7 = new int[5], F3 = new int[5], F4 = new int[5], F8 = new int[5],
            FC5 = new int[5], FC6 = new int[5],
            T7 = new int[5], T8 = new int[5],
            P7 = new int[5], P8 = new int[5],
            O1 = new int[5], O2 = new int[5],
            //tracked points x,y,z, co-ordinates (16 joints
            WristLX = new int[5], WristLY = new int[5], WristLZ = new int[5],
            WristRX = new int[5], WristRY = new int[5], WristRZ = new int[5],
            ElbowLX = new int[5], ElbowLY = new int[5], ElbowLZ = new int[5],
            ElbowRX = new int[5], ElbowRY = new int[5], ElbowRZ = new int[5],
            ShoulderLX = new int[5], ShoulderLY = new int[5], ShoulderLZ = new int[5],
            ShoulderRX = new int[5], ShoulderRY = new int[5], ShoulderRZ = new int[5],
            CenterX = new int[5], CenterY = new int[5], CenterZ = new int[5],
            NeckX = new int[5], NeckY = new int[5], NeckZ = new int[5],
            BackX = new int[5], BackY = new int[5], BackZ = new int[5],
            PelvisX = new int[5], PelvisY = new int[5], PelvisZ = new int[5],
            HipLX = new int[5], HipLY = new int[5], HipLZ = new int[5],
            HipRX = new int[5], HipRY = new int[5], HipRZ = new int[5],
            KneeLX = new int[5], KneeLY = new int[5], KneeLZ = new int[5],
            KneeRX = new int[5], KneeRY = new int[5], KneeRZ = new int[5],
            AnkleLX = new int[5], AnkleLY = new int[5], AnkleLZ = new int[5],
            AnkleRX = new int[5], AnkleRY = new int[5], AnkleRZ = new int[5];

        private void RawFileData_TextChanged(object sender, EventArgs e)
        {

        }

        int[][]
            neuralData = new int[14][],
            kinematicData = new int[49][];
        int[][][]
            neuralIndex = new int[300000][][], //300000 = 500 minutes of data
            kinematicIndex = new int[300000][][];
        int[][][][]
            SortedData = new int[2][][][];
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Browse();
            Flag++;
        }

        private void Sort_Click(object sender, EventArgs e)
        {
            int
                j = 0;
            char[] DataSet = info.ToCharArray();

            if (FileName == BrowseBox.Text)
            {
                Flag = 0;
            }
            if (Flag == 0)
            {
                FlagWarning(DataSet);
            }
            else
            {
                //sorts data into kinematic and neural
                DataSort(DataSet);
            }
            FileName = BrowseBox.Text;

            //add contents of neural data to array
            Neural = NeuralData.Text;
            ConvertNeural(neuralCount);

            //add contents of kinematic data to array
            Kinematic = KinematicData.Text;
            ConvertKinematic(KinematicCount);            
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("No, Stop that!");
        }
        private int[][] ConvertNeural(int datasize)
        {
            int 
                iter = 0,
                DataSize = datasize,
                j = 0;

            char[] NeuralChar = Neural.ToCharArray();

            int[][] 
                Components = new int[datasize][],
                NeuralData = new int[DataSize][];

            while (iter < 14)
            {
                
                for (int i = 0; i < NeuralChar.Length; i++)
                {
                    if((NeuralChar[i] == ':'))
                    {
                        NeuralChar[i] = ',';
                    }
                    if ((NeuralChar[i] == ','))
                    {
                        iter++;
                        //i++;
                        j = 0;
                    }
                    switch (iter)
                    {
                        case 0:
                            AF3[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[0] = AF3;
                            j++;
                            break;
                        case 1:
                            AF4[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[1] = AF4;
                            j++;
                            break;
                        case 2:
                            F7[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[2] = F7;
                            j++;
                            break;
                        case 3:
                            F3[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[3] = F3;
                            j++;
                            break;
                        case 4:
                            F4[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[4] = F4;
                            j++;
                            break;
                        case 5:
                            F8[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[5] = F8;
                            j++;
                            break;
                        case 6:
                            FC5[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[6] = FC5;
                            j++;
                            break;
                        case 7:
                            FC6[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[7] = FC6;
                            j++;
                            break;
                        case 8:
                            T7[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[8] = T7;
                            j++;
                            break;
                        case 9:
                            T8[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[9] = T8;
                            j++;
                            break;
                        case 10:
                            P7[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[10] = P7;
                            j++;
                            break;
                        case 11:
                            P8[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[11] = P8;
                            j++;
                            break;
                        case 12:
                            O1[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[12] = O1;
                            j++;
                            break;
                        case 13:
                            O2[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[13] = O2;
                            j++;
                            break;
                    }
                    neuralIndex[i] = Components;
                }
            }
            SortedData[0] = neuralIndex;
            return NeuralData;
        }
        private int[][] ConvertKinematic(int datasize)
        {
            int
                iter = 0,
                DataSize = datasize,
                j=0;

            char[] KinematicChar = Kinematic.ToCharArray();

            int[][]
                Components = new int[50][],
                KinData = new int[DataSize][];

            while (iter < 50)
            {

                for (int i = 0; i < KinematicChar.Length; i++)
                {
                    if ((KinematicChar[i] == ':') || ((int)Char.GetNumericValue(KinematicChar[i]) == -1))
                    {
                        KinematicChar[i] = ',';
                    }
                    if ((KinematicChar[i] == ','))
                    {
                        iter++;
                        //i++;
                        j = 0;
                    }
                    switch (iter)
                    {
                        case 0:                            
                            WristLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[0] = WristLX;
                            j++;                                                     
                            break;
                        case 1:
                            WristLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[1] = WristLY;
                            j++;
                            break;
                        case 2:
                            WristLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[2] = WristLZ;
                            j++;
                            break;
                        case 3:
                            WristRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[3] = WristRX;
                            j++;
                            break;
                        case 4:
                            WristRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[4] = WristRY;
                            j++;
                            break;
                        case 5:
                            WristRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[5] = WristRZ;
                            j++;
                            break;
                        case 6:
                            ElbowLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[6] = ElbowLX;
                            j++;
                            break;
                        case 7:
                            ElbowLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[7] = ElbowLY;
                            j++;
                            break;
                        case 8:
                            ElbowLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[8] = ElbowLZ;
                            j++;
                            break;
                        case 9:
                            ElbowRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[9] = ElbowRX;
                            j++;
                            break;
                        case 10:
                            ElbowRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[10] = ElbowRY;
                            j++;
                            break;
                        case 11:
                            ElbowRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[11] = ElbowRZ;
                            j++;
                            break;
                        case 12:
                            ShoulderLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[12] = ShoulderLX;
                            j++;
                            break;
                        case 13:
                            ShoulderLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[13] = ShoulderLY;
                            j++;
                            break;
                        case 14:
                            ShoulderLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[14] = ShoulderLZ;
                            j++;
                            break;
                        case 15:
                            ShoulderRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[15] = ShoulderRX;
                            j++;
                            break;
                        case 16:
                            ShoulderRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[16] = ShoulderRY;
                            j++;
                            break;
                        case 17:
                            ShoulderRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[17] = ShoulderRZ;
                            j++;
                            break;
                        case 18:
                            CenterX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[18] = CenterX;
                            j++;
                            break;
                        case 19:
                            CenterY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[19] = CenterY;
                            j++;
                            break;
                        case 20:
                            CenterZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[20] = CenterZ;
                            j++;
                            break;
                        case 21:
                            NeckX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[21] = NeckX;
                            j++;
                            break;
                        case 22:
                            NeckY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[22] = NeckY;
                            j++;
                            break;
                        case 23:
                            NeckZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[23] = NeckZ;
                            j++;
                            break;
                        case 24:
                            BackX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[24] = BackX;
                            j++;
                            break;
                        case 25:
                            BackY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[25] = BackY;
                            j++;
                            break;
                        case 26:
                            BackZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[26] = BackZ;
                            j++;
                            break;
                        case 27:
                            PelvisX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[27] = PelvisX;
                            j++;
                            break;
                        case 28:
                            PelvisY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[28] = PelvisY;
                            j++;
                            break;
                        case 29:
                            PelvisZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[29] = PelvisZ;
                            j++;
                            break;
                        case 30:
                            HipLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[30] = HipLX;
                            j++;
                            break;
                        case 31:
                            HipLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[31] = HipLY;
                            j++;
                            break;
                        case 32:
                            HipLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[32] = HipLY;
                            j++;
                            break;
                        case 33:
                            HipLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[33] = HipLZ;
                            j++;
                            break;
                        case 34:
                            HipRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[34] = HipRX;
                            j++;
                            break;
                        case 35:
                            HipRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[35] = HipRY;
                            j++;
                            break;
                        case 36:
                            HipRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[36] = HipRZ;
                            j++;
                            break;
                        case 37:
                            KneeLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[37] = KneeLX;
                            j++;
                            break;
                        case 38:
                            KneeLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[38] = KneeLY;
                            j++;
                            break;
                        case 39:
                            KneeLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[39] = KneeLZ;
                            j++;
                            break;
                        case 40:
                            KneeRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[40] = KneeRX;
                            j++;
                            break;
                        case 41:
                            KneeRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[41] = KneeRY;
                            j++;
                            break;
                        case 42:
                            KneeRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[42] = KneeRY;
                            j++;
                            break;
                        case 43:
                            KneeRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[43] = KneeRZ;
                            j++;
                            break;
                        case 44:
                            AnkleLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[44] = AnkleLX;
                            j++;
                            break;
                        case 45:
                            AnkleLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[45] = AnkleLY;
                            j++;
                            break;
                        case 46:
                            AnkleLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[46] = AnkleLZ;
                            j++;
                            break;
                        case 47:
                            AnkleRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[47] = AnkleRX;
                            j++;
                            break;
                        case 48:
                            AnkleRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[48] = AnkleRY;
                            j++;
                            break;
                        case 49:
                            AnkleRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[49] = AnkleRZ;
                            j++;
                            break;
                    }
                    kinematicIndex[i] = Components;
                }
            }
            SortedData[1] = kinematicIndex;        
            return KinData;
        }
        private void DataSort(char[] Dataset)
        {
            char[] DataSet = Dataset;
            int j = 0;
            for (int i = 0; i < DataSet.Length; i++)
            {
                char bit = DataSet[i];

                if (DataSet[i].Equals(':'))
                {
                    j++;
                    j = j % 2;
                    DataSet[i] = ',';
                    i++;
                }
                if (j == 0)
                {
                    if(Dataset[i] == ',')
                    {
                        neuralCount++;
                    }
                    NeuralData.Text = NeuralData.Text + DataSet[i];
                }
                else
                {
                    if (Dataset[i] == ',')
                    {
                        KinematicCount++;
                    }
                    KinematicData.Text = KinematicData.Text + DataSet[i];
                }
            }
        }
        private void Browse()
        {
            OpenFileDialog Data = new OpenFileDialog();
            Data.Title = "Locate unity data";
            Data.InitialDirectory = @"\\DEV-HA\Redirected_Folders\tjharrison1\Desktop\Neural network data";
            Data.Filter = "All files (*.*)|*.*|Text Files(*.txt)|*.txt";
            Data.FilterIndex = 2;
            if (Data.ShowDialog() == DialogResult.OK)
            {
                BrowseBox.Text = Data.FileName;
                RawFileData.Text = File.ReadAllText(Data.FileName);
                info = File.ReadAllText(Data.FileName);
            }
        }
        private void FlagWarning(char[] data)
        {
            char[] DataSet = data; 

            DialogResult alert = MessageBox.Show("The Data File is the same as previous" +
                    "\n" + "are you sure you want to reload?" +
                    "\n" + "This could effect your results", "Warning", MessageBoxButtons.YesNo);
            if (alert == DialogResult.Yes)
            {
                //sorts data into kinematic and neural
                DataSort(DataSet);
            }
            else if (alert == DialogResult.No)
            {
                Browse();

                Flag++;
                if (FileName == BrowseBox.Text)
                {
                    Flag = 0;
                }
                //sorts data into kinematic and neural
                DataSort(DataSet);
            }
        }       
        private void frameCapture(int[][][][] Sorteddata, int frameSize)
        {            
            if(frameSize == 0)
            {
                frameSize = 30;
            }

            int
                i = 0, j = 0, k = 0, l = 0;
            double
                L, wholenumber = 0;
            double[]
                AvNeural = new double[14];
       
            //captures # of frames from sorteddata for processing
            for(i=0; i < SortedData[1].Length;i=i+frameSize)
            {
                for(j = 0; j < frameSize; j++)
                {
                    //average neural data
                    for (k = 0; k < 14; k++)
                    {
                        for (l = 5; l > 0; l--)
                        {
                            L = Math.Pow(10, l);
                            wholenumber += SortedData[0][i][k][l] * L;
                        }
                        //prevents exceptions when k == 0
                        if (k == 1)
                        {
                            AvNeural[k] = (AvNeural[k - 1] * wholenumber) / k;
                        }
                        else
                        {
                            AvNeural[k] = wholenumber;
                        }
                    }     
                }
                //find data spike/s using average value and % diference
                for(k=0;k<14;k++)
                {
                    for (l = 5; l > 0; l--)
                    {
                        L = Math.Pow(10, l);
                        wholenumber += SortedData[0][i][k][l] * L;
                    }            
                    //compare the value with the average
                    if(wholenumber > (AvNeural[k]+(AvNeural[k]*0.1)))
                    {
                        //if the number is bigger than 10% the average replace the average
                        AvNeural[k] = wholenumber;
                    }        
                }
                //map kinematic path
                for (j = 0; j < frameSize; j++)
                {
                    //for each joint
                    for (k = 0; k < 49; k++)
                    {
                        //for each component
                        for (l = 5; l > 0; l--)
                        {
                            L = Math.Pow(10, l);
                            wholenumber += SortedData[0][i][k][l] * L;
                        }
                        //vector 
                    }
                }
            }
        }
        private void NeuralNet(double[] NeuFrame, double[] QuertFrame)
        {
            //gaussian distribution function for initial weightings
            Random 
                GaussDist = new Random(),
                LayGen = new Random();
            int
                i, j, k, 
                LayNum,
                NodeNum;

            //max of 125000 nodes total (including inp and out)
            double[]
                //node values
                NodeValue = new double[50],
                //weightings for activation functions
                WeightVal = new double[50];

            double[][]
                //number of nodes per layer
                NodePerLay = new double[50][],
                NodeWeight = new double[50][];
            double[][][]
                //number of layers to contain nodes
                Layers = new double[50][][],
                WeightLayer = new double[50][][];

            //add data as first layer
            Layers[0][0] = NeuFrame;
            //set initial layer bias
            Layers[0][0][14] = 1;

            LayNum = LayGen.Next(25, 51);
            for(i=0; i>LayNum; i++)
            {
                NodeNum = LayGen.Next(20, 45);
                for (j=0; j> NodeNum;j++)
                {
                    WeightLayer[ ][][] = 
                }
            }
        }
    }
}
