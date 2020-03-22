using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestImageProcessing
{
    public partial class Form4 : Form
    {
        private CancellationTokenSource canceller = new CancellationTokenSource();

        public Form4()
        {
            InitializeComponent();

            var task = new Task(() =>
            {
                canceller.Token.Register(Thread.CurrentThread.Abort);
                while (true)
                {
                    chk.Invoke(new Action(() =>
                    {
                        chk.Checked = !chk.Checked;
                    }));
                    Thread.Sleep(100);
                }
            }, canceller.Token);
            task.Start();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            canceller.Cancel();
        }
    }
}
