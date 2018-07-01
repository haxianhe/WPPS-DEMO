using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPPS_DEMO
{
    public partial class Control : Form
    {
        public Control()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ControlModel controlModel = ControlModel.getControlModel();
            controlModel.ScadaRealTimeData = 1;
            controlModel.ScadaWeatherData = 1;
            controlModel.PredictRealTimeData = 1;
            controlModel.PredictWeatherData = 1;
            controlModel.QueryData = 1;
            textBox1.Text += "开始调度 ：" + DateTime.Now.ToString() + "\r\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ControlModel controlModel = ControlModel.getControlModel();
            controlModel.ScadaRealTimeData = 0;
            controlModel.ScadaWeatherData = 0;
            controlModel.PredictRealTimeData = 0;
            controlModel.PredictWeatherData = 0;
            controlModel.QueryData = 0;
            textBox1.Text += "停止调度 ：" + DateTime.Now.ToString() + "\r\n";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
            this.textBox1.SelectionLength = 0;
            this.textBox1.ScrollToCaret();
        }
    }
}
