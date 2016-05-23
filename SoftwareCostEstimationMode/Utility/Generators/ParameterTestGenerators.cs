using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftwareCostEstimationMode.Utility.Generators
{
    class ParameterTestGenerators
    {
        private const double HighPercentage = 100.0;
        private double Step;
        private int NbOfParams;
        public delegate void ParameterGenerations_EndNotificationEvent();
        public event ParameterGenerations_EndNotificationEvent ParameterGenerators_EndNotificationEvent;
        public delegate void ParameterGenerations_StopNotificationEvent();
        public event ParameterGenerations_StopNotificationEvent ParameterGenerators_StoppedNotificationEvent;
        private object thisLock = new object();
        private ManualResetEvent[] mre;
        private int items = 0;
        private double ParameterSum;
        private int ParamsNumber;
        private double[] Solution;
        private string filename;
        IO.IO_Operation OutFile;
        public ParameterTestGenerators(double _step, int _nbOfParams,string _filename)
        {
            this.Step = _step;
            this.NbOfParams = _nbOfParams;
            ParameterSum = 0.0;
            ParamsNumber = 0;
            Solution = new double[1000];
            this.items = 0;
            filename = _filename;
            OutFile = new IO.IO_Operation();
        }
        public void ParameterTestGenerators_Start()
        {
            //this.init();
            OutFile.SetFileToBeWrite(this.filename);
            RecursiveFunction(0);
            OutFile.CloseOutputFile(this.filename);
            MessageBox.Show("Am ajuns la o solutie");
        }
        public void ParameterTestGenerators_Stop()
        {
            if (ParameterGenerators_StoppedNotificationEvent != null)
            {
                ParameterGenerators_StoppedNotificationEvent();
            }
        }
        public bool IsSolution(int Position)
        {
            double Sum = 0;

            for (int i = 0; i < Position; i++)
            {
                Sum += Solution[i];
            }
            Sum += Solution[Position];
            return (((Position == NbOfParams - 1u)) && ((Sum == HighPercentage)));
        }
        public bool IsValid(int Position)
        {
            return (Position < NbOfParams) && (Solution[Position] != 0.0);
        }
        public void tipareste(int Position)
        {
            string line = "";
            for (int i = 0; i <= Position; i++)
            {
                line += Solution[i].ToString() + " ";
            }

            line += "\n";
            OutFile.WriteLine(line);
            //MessageBox.Show("Am ajuns la o solutie");
        }
        void RecursiveFunction(int Position)
        {
            try
            {
                for (double i = Step; i < HighPercentage; i = i + Step)
                {
                    Solution[Position] = i;
                    if (IsValid(Position))
                    {
                        if (IsSolution(Position))
                        {
                            tipareste(Position);
                        }
                        else
                        {
                            RecursiveFunction(Position + 1);
                        }
                    }
                }
                if (ParameterGenerators_EndNotificationEvent != null)
                {
                    ParameterGenerators_EndNotificationEvent();
                }
            }
            catch (Exception ex)
            {
                if (ParameterGenerators_StoppedNotificationEvent != null)
                {
                    ParameterGenerators_StoppedNotificationEvent();
                }
            }
            
        }
    }
}