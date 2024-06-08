using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ИМ_Лаб12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Freq = new double[3];
        }

        int state, days, hour;
        double[,] inf_matrix = { { 0.3, 0.5, 0.2 }, { 0.3, 0.6, 0.1 }, { 0.1, 0.4, 0.5 } };
        double[] Freq = { 0, 0, 0 };

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            state = 0;
            hour = 0;
            days = (int)numericUpDown1.Value;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            state = nextState(state);
            Freq[state] += 1;

            if (hour % 24 == 0)
            {
                chart1.Series[0].Points.AddXY(hour / 24, state + 1);
            }

            if (hour == days * 24)
            {
                Freq[0] /= days * 24;
                Freq[1] /= days * 24;
                Freq[2] /= days * 24;
                label1.Text = Math.Round(Freq[0], 3).ToString();
                label2.Text = Math.Round(Freq[1], 3).ToString();
                label3.Text = Math.Round(Freq[2], 3).ToString();
                timer1.Stop();
            }
            hour += 1;
        }

        int nextState(int state)
        {
            Random random = new Random();

            double a = random.NextDouble();

            for (int i = 0; i < 3; i++)
            {
                if (i == state) continue;

                a -= inf_matrix[state, i];
                if (a < 0) return i;
            }
            return state;
        }

    }
}
