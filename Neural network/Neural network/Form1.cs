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
            Flag = 0;
        int[]
            //EEG sensor channels
            AF3 = new int[5], AF4 = new int[5],
            F7 = new int[5], F3 = new int[5], F4 = new int[5], F8 = new int[5],
            FC5 = new int[5], FC6 = new int[5],
            T7 = new int[5], T8 = new int[5],
            P7 = new int[5], P8 = new int[5],
            O1 = new int[5], O2 = new int[5],
            //tracked points x,y,z, co-ordinates (16 joints)
            WristL = new int[3], WristR = new int[3],
            ElbowL = new int[3], ElbowR = new int[3],
            ShoulderL = new int[3], ShoulderR = new int[3],
            Center = new int[3],
            Neck = new int[3],
            Back = new int[3],
            Pelvis = new int[3],
            HipL = new int[3], HipR = new int[3],
            KneeL = new int[3], KneeR = new int[3],
            AnkleL = new int[3], AnkleR = new int[3];
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
            ConvertNeural(500);

            //add contents of kinematic data to array
            Kinematic = KinematicData.Text;
            //ConvertKinematic(500);            
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
                Components = new int[14][],
                NeuralData = new int[DataSize][];

            while (iter < 14)
            {
                
                for (int i = 0; i < NeuralChar.Length; i++)
                {
                    if ((NeuralChar[i] == ','))
                    {
                        iter++;
                        i++;
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
                            F7[i] = F7[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[2][iter] = F7[i];
                            break;
                        case 3:
                            F3[i] = F3[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[3][iter] = F3[i];
                            break;
                        case 4:
                            F4[i] = F4[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[4][iter] = F4[i];
                            break;
                        case 5:
                            F8[i] = F8[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[5][iter] = F8[i];
                            break;
                        case 6:
                            FC5[i] = FC5[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[6][iter] = FC5[i];
                            break;
                        case 7:
                            FC6[i] = FC6[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[7][iter] = FC6[i];
                            break;
                        case 8:
                            T7[i] = T7[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[8][iter] = T7[i];
                            break;
                        case 9:
                            T8[i] = T8[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[9][iter] = T8[i];
                            break;
                        case 10:
                            P7[i] = P7[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[10][iter] = P7[i];
                            break;
                        case 11:
                            P8[i] = P8[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[11][iter] = P8[i];
                            break;
                        case 12:
                            O1[i] = O1[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[12][iter] = O1[i];
                            break;
                        case 13:
                            O2[i] = O2[i] + (int)Char.GetNumericValue(NeuralChar[i]);
                            Components[13][iter] = O2[i];
                            break;
                    }
                }
            }
            return NeuralData;
        }
        private int[][] ConvertKinematic(int datasize)
        {
            int
                iter = 0,
                DataSize = datasize;

            char[] KinematicChar = Kinematic.ToCharArray();

            int[][]
                Components = new int[16][],
                KinData = new int[DataSize][];

            for (int i = 0; i < KinematicChar.Length; i++)
            {
                while (iter < 16)
                {
                    if (KinematicChar[i] == ',')
                    {
                        iter++;
                    }

                    switch (iter)
                    {
                        case 0:
                            WristL[i] = WristL[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[1][iter] = WristL[i];
                            break;
                        case 1:
                            WristR[i] = WristR[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[1][iter] = WristR[i];
                            break;
                        case 2:
                            ElbowL[i] = ElbowL[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            Components[1][iter] = WristR[i];
                            break;
                        case 3:
                            ElbowR[i] = ElbowR[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 4:
                            ShoulderL[i] = ShoulderL[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 5:
                            ShoulderR[i] = ShoulderR[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 6:
                            Center[i] = Center[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 7:
                            Neck[i] = Neck[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 8:
                            Back[i] = Back[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 9:
                            Pelvis[i] = Pelvis[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 10:
                            HipL[i] = HipL[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 11:
                            HipR[i] = HipR[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 12:
                            KneeL[i] = KneeL[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 13:
                            KneeR[i] = KneeR[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 14:
                            AnkleL[i] = AnkleL[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                        case 15:
                            AnkleR[i] = AnkleR[i] + (int)Char.GetNumericValue(KinematicChar[i]);
                            break;
                    }
                }
            }
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
                    i++;
                }
                if (j == 0)
                {
                    NeuralData.Text = NeuralData.Text + DataSet[i];
                }
                else
                {
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
    }
}
