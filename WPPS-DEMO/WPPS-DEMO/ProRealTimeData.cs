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
    public partial class ProRealTimeData : Form
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProRealTimeData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开始模拟按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            CreateRealTimeData create = CreateRealTimeData.getCreateRealTimeData();
            create.refreshtxt = (arg) =>
            {
                this.Invoke(new Action(() =>
                {
                    textBox1.Text += arg + "\r\n";
                }));
            };
            create.Start();
            textBox1.Text = "start\r\n";
        }

        /// <summary>
        /// 停止模拟事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            CreateRealTimeData create = CreateRealTimeData.getCreateRealTimeData();
            create.Stop();
            textBox1.Text += "stop\r\n";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
            this.textBox1.SelectionLength = 0;
            this.textBox1.ScrollToCaret();
        }
    }
}
