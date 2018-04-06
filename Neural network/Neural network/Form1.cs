using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Neural_network
{
    public partial class Form1 : Form
    {
        //set background workers
        BackgroundWorker
            RunKinConv = new BackgroundWorker(),
            RunNeuConv = new BackgroundWorker(),
            frameCapN = new BackgroundWorker(),
            frameCapK = new BackgroundWorker(),
            NeuralThread = new BackgroundWorker();

        string
            info,
            FileName,
            Neural,
            Kinematic;
        int //flag initialise
            Flag = 0,
            neuralCount=10,
            KinematicCount=10;
        double[]
            //EEG sensor channels
            AF3 = new double[10], AF4 = new double[10],
            F7 = new double[10], F3 = new double[10], F4 = new double[10], F8 = new double[10],
            FC5 = new double[10], FC6 = new double[10],
            T7 = new double[10], T8 = new double[10],
            P7 = new double[10], P8 = new double[10],
            O1 = new double[10], O2 = new double[10],
            //tracked podoubles x,y,z, co-ordinates (16 jodoubles
            WristLX = new double[10], WristLY = new double[10], WristLZ = new double[10],
            WristRX = new double[10], WristRY = new double[10], WristRZ = new double[10],
            ElbowLX = new double[10], ElbowLY = new double[10], ElbowLZ = new double[10],
            ElbowRX = new double[10], ElbowRY = new double[10], ElbowRZ = new double[10],
            ShoulderLX = new double[10], ShoulderLY = new double[10], ShoulderLZ = new double[10],
            ShoulderRX = new double[10], ShoulderRY = new double[10], ShoulderRZ = new double[10],
            CenterX = new double[10], CenterY = new double[10], CenterZ = new double[10],
            NeckX = new double[10], NeckY = new double[10], NeckZ = new double[10],
            BackX = new double[10], BackY = new double[10], BackZ = new double[10],
            PelvisX = new double[10], PelvisY = new double[10], PelvisZ = new double[10],
            HipLX = new double[10], HipLY = new double[10], HipLZ = new double[10],
            HipRX = new double[10], HipRY = new double[10], HipRZ = new double[10],
            KneeLX = new double[10], KneeLY = new double[10], KneeLZ = new double[10],
            KneeRX = new double[10], KneeRY = new double[10], KneeRZ = new double[10],
            AnkleLX = new double[10], AnkleLY = new double[10], AnkleLZ = new double[10],
            AnkleRX = new double[10], AnkleRY = new double[10], AnkleRZ = new double[10];
        double[,]
            neuralData = new double[14,10],
            kinematicData = new double[49,10];
        double[,,,]
            SortedData = new double[2,300000,14,10];
        


        private void Process_Click(object sender, EventArgs e)
        {
            NeuralThread.RunWorkerAsync();

            MessageBox.Show("Data Processing Complete");
        }

        private void RawFileData_TextChanged(object sender, EventArgs e)
        {

        }
        public Form1()
        {
            InitializeComponent();
        }

        //browse button
        private void button1_Click(object sender, EventArgs e)
        {
            Browse();
            Flag++;
        }

        private void Sort_Click(object sender, EventArgs e)
        {

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

            //add contents of data to array
            Neural = NeuralData.Text;
            Kinematic = KinematicData.Text;

            //run the functions
            RunKinConv.RunWorkerAsync();
            RunNeuConv.RunWorkerAsync();

            //Capture Frame Data
            frameCapK.RunWorkerAsync();
            frameCapN.RunWorkerAsync();

            MessageBox.Show("Sorting Complete");
                        
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("No, Stop that!");
        }
        private double[,] ConvertNeural(double datasize)
        {
            int
            iter = 0,
            i = 0,
            j = 0,
            k = 0,
            l = 0,
            Gen = 0;

            char[] 
                NeuralChar = Neural.ToCharArray();

            double
                DataSize = datasize;

            double[,] 
                Components = new double[14,10],
                NeuralData = new double[Convert.ToInt32(datasize),10];

            while (iter < 14 && Gen < 300000)
            {
                for (i = 0; i < NeuralChar.Length; i++)
                {
                    if ((NeuralChar[i] == ':'))
                    {
                        NeuralChar[i] = ',';
                    }
                    if(iter == 13)
                    {
                        Gen++;
                        iter = 0;
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
                            Components[0, j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            j++;
                            break;
                        case 1:
                            Components[1, j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            j++;
                            break;
                        case 2:
                            Components[2, j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            j++;
                            break;
                        case 3:
                            F3[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[3, j] = F3[j];
                            j++;
                            break;
                        case 4:
                            F4[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[4, j] = F4[j];
                            j++;
                            break;
                        case 5:
                            F8[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[5, j] = F8[j];
                            j++;
                            break;
                        case 6:
                            FC5[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[6, j] = FC5[j];
                            j++;
                            break;
                        case 7:
                            FC6[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[7, j] = FC6[j];
                            j++;
                            break;
                        case 8:
                            T7[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[8, j] = T7[j];
                            j++;
                            break;
                        case 9:
                            T8[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[9, j] = T8[j];
                            j++;
                            break;
                        case 10:
                            P7[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[10, j] = P7[j];
                            j++;
                            break;
                        case 11:
                            P8[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[11, j] = P8[j];
                            j++;
                            break;
                        case 12:
                            O1[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[12, j] = O1[j];
                            j++;
                            break;
                        case 13:
                            O2[j] = (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[13, j] = O2[j];
                            j++;
                            break;
                    }
                    if (iter == 13)
                    {
                        l = j;
                        for (j = 0; ((j <= 13) && (Gen < 300000)); j++)
                        {
                            for (k = 0; (Components[j,k] != 0) || (k < 7); k++)
                            {
                                SortedData[0, Gen, j, k] = Components[j, k];
                            }
                        }
                        j = l;
                    }
                }
            }
            return NeuralData;
        }
        private void ConvertKinematic(int datasize)
        {
            int
                iter = 0,
                DataSize = datasize,
                i=0,
                j=0,
                k=0,
                l=0,
                Gen = 0;

            char[] KinematicChar = Kinematic.ToCharArray();

            double[,]
                Components = new double[DataSize,6],
                KinData = new double[DataSize,6];

            while (iter < 50)
            {

                for (i = 0; i < KinematicChar.Length; i++)
                {
                    if ((KinematicChar[i] == ':') || ((int)Char.GetNumericValue(KinematicChar[i]) == -1))
                    {
                        KinematicChar[i] = ',';
                    }
                    
                    switch (iter)
                    {
                        case 0:                            
                            WristLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[0,j] = WristLX[j];
                            j++;                                                     
                            break;
                        case 1:
                            WristLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[1,j] = WristLY[j];
                            j++;
                            break;
                        case 2:
                            WristLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[2,j] = WristLZ[j];
                            j++;
                            break;
                        case 3:
                            WristRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[3,j] = WristRX[j];
                            j++;
                            break;
                        case 4:
                            WristRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[4,j] = WristRY[j];
                            j++;
                            break;
                        case 5:
                            WristRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[5,j] = WristRZ[j];
                            j++;
                            break;
                        case 6:
                            ElbowLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[6,j] = ElbowLX[j];
                            j++;
                            break;
                        case 7:
                            ElbowLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[7,j] = ElbowLY[j];
                            j++;
                            break;
                        case 8:
                            ElbowLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[8,j] = ElbowLZ[j];
                            j++;
                            break;
                        case 9:
                            ElbowRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[9,j] = ElbowRX[j];
                            j++;
                            break;
                        case 10:
                            ElbowRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[10,j] = ElbowRY[j];
                            j++;
                            break;
                        case 11:
                            ElbowRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[11,j] = ElbowRZ[j];
                            j++;
                            break;
                        case 12:
                            ShoulderLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[12,j] = ShoulderLX[j];
                            j++;
                            break;
                        case 13:
                            ShoulderLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[13,j] = ShoulderLY[j];
                            j++;
                            break;
                        case 14:
                            ShoulderLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[14,j] = ShoulderLZ[j];
                            j++;
                            break;
                        case 15:
                            ShoulderRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[15,j] = ShoulderRX[j];
                            j++;
                            break;
                        case 16:
                            ShoulderRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[16,j] = ShoulderRY[j];
                            j++;
                            break;
                        case 17:
                            ShoulderRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[17,j] = ShoulderRZ[j];
                            j++;
                            break;
                        case 18:
                            CenterX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[18,j] = CenterX[j];
                            j++;
                            break;
                        case 19:
                            CenterY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[19,j] = CenterY[j];
                            j++;
                            break;
                        case 20:
                            CenterZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[20,j] = CenterZ[j];
                            j++;
                            break;
                        case 21:
                            NeckX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[21,j] = NeckX[j];
                            j++;
                            break;
                        case 22:
                            NeckY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[22,j] = NeckY[j];
                            j++;
                            break;
                        case 23:
                            NeckZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[23,j] = NeckZ[j];
                            j++;
                            break;
                        case 24:
                            BackX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[24,j] = BackX[j];
                            j++;
                            break;
                        case 25:
                            BackY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[25,j] = BackY[j];
                            j++;
                            break;
                        case 26:
                            BackZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[26,j] = BackZ[j];
                            j++;
                            break;
                        case 27:
                            PelvisX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[27,j] = PelvisX[j];
                            j++;
                            break;
                        case 28:
                            PelvisY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[28,j] = PelvisY[j];
                            j++;
                            break;
                        case 29:
                            PelvisZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[29,j] = PelvisZ[j];
                            j++;
                            break;
                        case 30:
                            HipLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[30,j] = HipLX[j];
                            j++;
                            break;
                        case 31:
                            HipLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[31,j] = HipLY[j];
                            j++;
                            break;
                        case 32:
                            HipLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[32,j] = HipLY[j];
                            j++;
                            break;
                        case 33:
                            HipLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[33,j] = HipLZ[j];
                            j++;
                            break;
                        case 34:
                            HipRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[34,j] = HipRX[j];
                            j++;
                            break;
                        case 35:
                            HipRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[35,j] = HipRY[j];
                            j++;
                            break;
                        case 36:
                            HipRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[36,j] = HipRZ[j];
                            j++;
                            break;
                        case 37:
                            KneeLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[37,j] = KneeLX[j];
                            j++;
                            break;
                        case 38:
                            KneeLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[38,j] = KneeLY[j];
                            j++;
                            break;
                        case 39:
                            KneeLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[39,j] = KneeLZ[j];
                            j++;
                            break;
                        case 40:
                            KneeRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[40,j] = KneeRX[j];
                            j++;
                            break;
                        case 41:
                            KneeRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[41,j] = KneeRY[j];
                            j++;
                            break;
                        case 42:
                            KneeRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[42,j] = KneeRY[j];
                            j++;
                            break;
                        case 43:
                            KneeRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[43,j] = KneeRZ[j];
                            j++;
                            break;
                        case 44:
                            AnkleLX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[44,j] = AnkleLX[j];
                            j++;
                            break;
                        case 45:
                            AnkleLY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[45,j] = AnkleLY[j];
                            j++;
                            break;
                        case 46:
                            AnkleLZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[46,j] = AnkleLZ[j];
                            j++;
                            break;
                        case 47:
                            AnkleRX[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[47,j] = AnkleRX[j];
                            j++;
                            break;
                        case 48:
                            AnkleRY[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[48,j] = AnkleRY[j];
                            j++;
                            break;
                        case 49:
                            AnkleRZ[j] = (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[49,j] = AnkleRZ[j];
                            j++;
                            break;
                    }

                    if ((KinematicChar[i] == ','))
                    {
                        iter++;
                        //i++;
                        j = 0;
                    }
                    if (iter == 13)
                    {
                        l = j;
                        for (j = 0; j < 14; j++)
                        {
                            for (k = 0; Components[j, k] != 0; k++)
                            {
                                SortedData[0, Gen, j, k] = Components[j, k];
                            }
                        }
                        j = l;
                    }
                }
            }
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
        private void frameCapKin(int frameSize)
        {            
            if(frameSize == 0)
            {
                frameSize = 30;
            }

            Random 
                seed = new Random();

            int
                i = 0, 
                j = 0, 
                k = 0, 
                l = 0,
                frame;
            double
                L, 
                wholenumber = 0;
            double[]
                VectorFr = new double[49];
            //frame select
            for (i = 0; i > 30; i+=6)
            {
                //seed random frame
                frame = seed.Next(0, frameSize);
                //for each joint
                for (j = 0; j < 49; j++)
                {
                    //for each point
                    for (k = 3; k > 0; k--)
                    {
                        //each data point
                        for (l = 1; l < 5; l++)
                        {
                            //magnitude of each point
                            L = Math.Pow(10, l);
                            //compile into one number
                            wholenumber += SortedData[0,frame,k,l] * L;
                        }
                        //0th element becomes the whole number
                        SortedData[0,j,k,l] = wholenumber;
                    }
                }

                //Vector calculations
                for (j = 0; j < 49; j++)
                {
                    for (k = 1; k < frameSize; k += 2)
                    {
                        //sum vectors
                        VectorFr[j] +=
                        Math.Sqrt
                        (Math.Pow(2, SortedData[1,j,k,0] - SortedData[1,j,k,0]) +
                        (Math.Pow(2, SortedData[1,j,k + 1,0] - SortedData[1,j,k + 1,0])) +
                        (Math.Pow(2, SortedData[1,j,k + 2,0] - SortedData[1,j,k + 2,0])));
                    }
                    SortedData[1,i,j,0] = VectorFr[j];
                }
            }
        }
        private void frameCapNeur(int frameSize)
        {
            if (frameSize == 0)
            {
                frameSize = 30;
            }

            Random 
                seed = new Random();

            int
                i = 0,
                j = 0,
                k = 0,
                l = 0,
                frame;
            double
                L,
                wholenumber = 0;
            double[]
                AvNeural = new double[14];

            //captures # of frames from sorteData for processing
            for (i = 0; (!SortedData[0,i,0,0].Equals(null)) && i < 299950; i+=6)
            {
                //seed random frame
                frame = seed.Next(0, frameSize);
                for (j = 0; j < 5; j++)
                {
                    for (k = 0; k < 14; k++)
                    {
                        //initialise complation array
                        AvNeural[k] = 0;
                    }
                    //average neural data
                    for (k = 0; k < 14; k++)
                    {
                        //each data point
                        for (l = 1; l < 5; l++)
                        {
                            //magnitude of each point
                            L = Math.Pow(10, l);
                            //compile into one number
                            wholenumber += SortedData[0,i,k,l] * L;
                        }
                        //add number to channel array
                        AvNeural[k] = wholenumber / 10;
                        //zero complation var
                        wholenumber = 0;
                        //end data complation

                        //if all channels have been compiled
                        if (k == 13)
                        {
                            //replace previous data points with compiled data
                            for (l = 0; l < 14; l++)
                            {
                                //replace 0th point with channel av
                                SortedData[0,i,l,0] = AvNeural[l];
                            }
                        }

                        //prevents exceptions when k == 0
                        if (j > 1 && k > 0)
                        {
                            //average the frame of data
                            AvNeural[k] = (AvNeural[k] + (AvNeural[k - 1])) / j;
                        }
                        else
                        {
                            //do nothing
                        }
                    }
                }
            }
        }
        private void NeuralNet()
        {
            //gaussian distribution function for initial weightings
            Random
                GaussDist = new Random(),
                LayGen = new Random();
            int
                i, j, k, l,
                iter,
                LayNum = 0,
                NodeNum = 0,
                initFlag = 0;
            double
                sig,
                sigPrime,
                z = 0,
                Value = 0,
                AvErr = 0;
            double[]
                Error = new double[49];
            double[,]
                ActFunc = new double[50, 15],           //activation function store
                SigPrime = new double[50, 15],          //sigmoid prime storage
                ValLayer = new double[50, 15],          //processed value for each node
                DeltaCh = new double[50, 15];
            //max of 125000 nodes total (including inp and out layers)
            double[,,]
                Layers = new double[50, 50, 50],
                WeightLayer = new double[50, 50, 50];   //Weight storage

            chart1.Series.Clear();
            chart1.Series.Add("Average error");
            chart1.Series["Average error"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            //start neural network for 100 iterations
            for (iter = 0; (iter < 10); iter++)
            {
                //add data as first layer
                for (i = 0; i < 14; i++)
                {
                    ActFunc[0, i] = SortedData[0, 0, i, 0];
                }
                //set initial layer bias
                Layers[0, 0, 14] = 1;

                //set number of layers
                LayNum = LayGen.Next(25, 50);

                /**************************
                 * set initial weightings *
                 **************************/
                //step through layers
                if (initFlag == 0)
                {
                    for (i = 0; i < LayNum; i++)
                    {
                        //set number of nodes
                        NodeNum = LayGen.Next(5, 14);
                        //step thorugh nodes
                        for (j = 0; j < NodeNum; j++)
                        {
                            for (k = 0; k < NodeNum; k++)
                            {
                                //set intial weights based on gaussian distribution
                                WeightLayer[i, j, k] = GaussDist.Next(0, 100);
                                WeightLayer[i, j, k] = WeightLayer[i, j, k] / 100;
                            }
                        }
                    }
                    //Prevent reinitialisation of weights
                    initFlag = 1;
                }
                /******************
                 * weightings end *
                 ******************/

                /**********************************
                 * calculate activation functions *
                 **********************************/
                //for each layer
                for (i = 0; i < LayNum; i++)
                {
                    //for each node value
                    for (j = 0; j < NodeNum; j++)
                    {
                        //for each connection
                        for (k = 0; k < NodeNum; k++)
                        {
                            //for each weight
                            for (l = 0; l < NodeNum; l++)
                            {
                                //determine the 
                                z += Layers[i, j, k] * (WeightLayer[j, k, l]);

                            }
                            //sigmoid function 1/1+e^z
                            sig = 1 / (1 + Math.Pow(Math.E, -z));
                            //sigmoid prime
                            sigPrime = sig - Math.Pow(sig, 2);
                            //activation function
                            ActFunc[i, j] = sig;
                            SigPrime[i, j] = sigPrime;

                            //clear values to prevent errors
                            z = 0;
                            sig = 0;
                            sigPrime = 0;
                        }
                    }
                }
                /****************************
                 * activation functions end *
                 ****************************/

                /************************************
                 * output value for current weights *
                 ************************************/
                //for each layer
                for (i = 1; i < LayNum; i++)
                {
                    //for each node value
                    for (j = 0; j < NodeNum; j++)
                    {
                        //for each connection
                        for (k = 0; k < NodeNum; k++)
                        {
                            //find the input value for the node
                            Value += ActFunc[i, j] * WeightLayer[i, j, j];
                        }
                        ValLayer[i, j] = Value;
                        //determine if node has been activated
                        if (ValLayer[j, k] < ActFunc[j + 1, k])
                        {
                            ValLayer[j, k] = 0;
                        }
                        Value = 0;
                    }
                }
                /****************************************
                 * output value for current weights end *
                 ****************************************/

                /********************
                 * Back Propegation *
                 ********************/
                for (i = 0; i < LayNum; i++)
                {
                    for (j = 1; j < NodeNum; j++)
                    {
                        //for each connection
                        for (k = 0; k < NodeNum; k++)
                        {
                            //find error margin
                            Error[k] = SortedData[1, 0, j, 0] - ValLayer[NodeNum, j];
                            AvErr =+ Error[k];

                            if(k == NodeNum-1)
                            {
                                AvErr = AvErr / k;
                                //add to chart
                                chart1.Series["Average error"].Points.AddXY(iter, AvErr);
                            }
                            
                            //Rate of change calculation
                            DeltaCh[j, k] = SigPrime[j, k] * Error[j];
                            //replace old weights
                            WeightLayer[i, j, k] = WeightLayer[i, j, k] / DeltaCh[j, k];
                        }
                    }
                }
                AvErr = 0;
                /************************
                 * Back propegation end *
                 ************************/
            }
        }
        private void RunNeuConv_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            int arg = (int)e.Argument;

            // Start the time-consuming operation.
            e.Result = ConvertNeural(neuralCount);

            // If the operation was canceled by the user, 
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }
        private void RunNeuConv_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                // The user canceled the operation.
                MessageBox.Show("Operation was canceled");
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(msg);
            }
            else
            {
                // The operation completed normally.
                string msg = String.Format("Result = {0}", e.Result);
                MessageBox.Show(msg);
            }
        }
        private void RunKinConv_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            int arg = (int)e.Argument;

            // Start the time-consuming operation.
            ConvertKinematic(KinematicCount);

            // If the operation was canceled by the user, 
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }
        private void RunKinConv_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                // The user canceled the operation.
                MessageBox.Show("Operation was canceled");
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(msg);
            }
            else
            {
                // The operation completed normally.
                string msg = String.Format("Result = {0}", e.Result);
                MessageBox.Show(msg);
            }
        }
        private void NeuralThread_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            int arg = (int)e.Argument;

            // Start the time-consuming operation.
            NeuralNet();

            // If the operation was canceled by the user, 
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }
        private void FeameCapN_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            int arg = (int)e.Argument;

            // Start the time-consuming operation.
            frameCapNeur(30);

            // If the operation was canceled by the user, 
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }
        private void FeameCapK_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            int arg = (int)e.Argument;

            // Start the time-consuming operation.
            frameCapKin(30);

            // If the operation was canceled by the user, 
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }
    }
}
