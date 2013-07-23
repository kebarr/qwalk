using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Text.RegularExpressions;
using QWalk;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  when the user clicks, want to take the data from the two boxes, check its the right format, a+bi, 
        ///  convert string to complex number, verify normalisatiion (to be precise and get around lack of square root button,
        ///  going to have to be able to handle input like "sqrt(a)" too), then send to runWalk
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void click_Click(object sender, EventArgs e)
        {
            string lcoinState = leftCoinState.Text;
            /// now need to check these are either: a + bi, a, bi, sqrt(a), sqrt(b)i, sqrt(a) + sqrt(b)i
            if (inputs.IsValid(lcoinState) == false)
            {
                MessageBox.Show("Left coin state value must be of form a, bi, a+bi, sqrt(a), sqrt(b)i or sqrt(a) + sqrt(b)i, default value sqrt(0.5) used for simulation");
            }

            Complex leftInitial = inputs.convert(lcoinState);
              
            string rcoinState = rightCoinState.Text;
            if (inputs.IsValid(rcoinState) == false)
            {
                MessageBox.Show("Right coin state value must be of form a, bi, a+bi, sqrt(a), sqrt(b)i or sqrt(a) + sqrt(b)i, default value sqrt(0.5) used for simulation");
            }

            /// check initial state is normalised. if not, just reassign to something that is for time being. eventually
            /// want to make it so only runs if user puts in valid initial condition, but need it to work for all such conditions first.
            
            Complex rightInitial = inputs.convert(rcoinState);
            if (qWalker.isNormalised(rightInitial,leftInitial) == false)
            {
                MessageBox.Show("Initial state not normalised, modulus squared of the amplitudes did not equal one, default value sqrt(0.5) used for each coin state");
                rightInitial = new Complex(0,Math.Sqrt(0.5));
                leftInitial = new Complex(0, Math.Sqrt(0.5));
            }
            double[] probabilities = qWalker.runWalk(rightInitial,leftInitial);
            chart1.Series.Clear(); 
            chart1.Series.Add("probabilities");
            for (int i = 0; i < 100; i++)
            {
                
                this.chart1.Series["probabilities"].Points.AddXY(i-49, probabilities[i]);
                ///A chart element with the name 'probabilities' could not be found in the 'SeriesCollection'.
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
