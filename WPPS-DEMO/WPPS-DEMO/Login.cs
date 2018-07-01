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
    public partial class Login : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 登录按时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.txtPassword.Text == "")
            {
                MessageBox.Show("请输入密码！");
            }
            else if (this.txtUsername.Text == "123" && this.txtPassword.Text == "123")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Username or Password Error!");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtUsername.Text = "";
            this.txtPassword.Text = "";
        }
    }
}
