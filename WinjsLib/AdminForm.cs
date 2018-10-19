using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinjsLib
{
    public partial class AdminForm : Form
    {
        public WebForm webForm { get; set; }
        public AdminForm(WebForm webForm)
        {
            this.webForm = webForm;
            InitializeComponent();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            webForm.browser.Load(textBox1.Text);
            string js = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"base\1.js"));
            webForm.browser.ExecuteScriptAsync(js);
        }

        /// <summary>
        /// 开发者模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            webForm.browser.ShowDevTools();

        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            webForm.Hide();
            this.Hide();
            webForm.Close();
            System.Environment.Exit(0);
        }
    }
}
