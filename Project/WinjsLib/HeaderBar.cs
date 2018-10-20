using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinjsLib
{
    public partial class HeaderBar : Form
    {
        private WebForm form;
        Point downPoint;
        public HeaderBar(WebForm form)
        {
            InitializeComponent();
            this.form = form;
        }

        public void SetTop(int Value) { this.Location = new Point(this.Location.X, Value); }
        public void SetLeft(int Value) { this.Location = new Point(Value, this.Location.Y); }
        public void SetHeight(int Value) { this.Height = Value; }
        public void SetWidth(int Value) { this.Width = Value; }

        private void HeaderBar_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.01;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Red;
            this.Width = form.Width;
            this.Height = 32;
            this.Location = new Point(form.Location.X, form.Location.Y);

            this.ShowInTaskbar = false;
        }

        private void HeaderBar_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
            form.Focus();
        }

        private void HeaderBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                
                form.Location = new Point(form.Location.X + e.X - downPoint.X,
                    form.Location.Y + e.Y - downPoint.Y);
                for (int i = 0; i < form.bars.Count; i++)
                {
                    form.bars[i].Location = new Point(form.bars[i].Location.X + e.X - downPoint.X,
                    form.bars[i].Location.Y + e.Y - downPoint.Y);
                }
            }
        }
        //            this.TopMost = true;

        public void SetTopMost(Boolean value)
        {
            this.TopMost = value;
        }
    }
}
