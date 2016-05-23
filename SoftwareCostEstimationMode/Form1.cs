using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareCostEstimationMode.DataMining;
using System.Windows.Forms;
using SoftwareCostEstimationMode.Utility.Generators;

namespace SoftwareCostEstimationMode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            /*here we should open an open file dialog*/
            OpenFileDialog openFileDial = new OpenFileDialog();
            openFileDial.ShowDialog();
            /*get the chosen file*/
            string filePath = openFileDial.FileName;
            DataSetMap dsMap = new DataSetMap();
            dsMap.ReadFile(filePath);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ParameterTestGenerators ps = new ParameterTestGenerators(1, 20, "D:\\School\\Doctorat\\Related Work\\TestProject\\TestFile.txt");
            ps.ParameterTestGenerators_Start();
        }
    }
}
