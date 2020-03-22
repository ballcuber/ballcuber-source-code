using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fgSolver
{
    public partial class VideoScannerDebugForm : Form
    {
        private const string SEPARATOR = ";";

        private List<Pastille>[,,] _scannedColors;

        public VideoScannerDebugForm()
        {
            InitializeComponent();
        }

        public VideoScannerDebugForm(List<Pastille>[,,] _scannedColors) : this()
        {
            this._scannedColors = _scannedColors;

            txtInfo.Clear();

            txtInfo.AppendText("avgR");
            txtInfo.AppendText(SEPARATOR);
            txtInfo.AppendText("avgG");
            txtInfo.AppendText(SEPARATOR);
            txtInfo.AppendText("avgB");
            txtInfo.AppendText(SEPARATOR);
            txtInfo.AppendText("stdR");
            txtInfo.AppendText(SEPARATOR);
            txtInfo.AppendText("stdG");
            txtInfo.AppendText(SEPARATOR);
            txtInfo.AppendText("stdB");
            txtInfo.AppendText("\r\n");

            try
            {
                foreach (var pastille in _scannedColors)
                {
                    var avgR = pastille.Average((x) => x.MeanColorBGR.Red);
                    var avgG = pastille.Average((x) => x.MeanColorBGR.Green);
                    var avgB = pastille.Average((x) => x.MeanColorBGR.Blue);

                    var stdR = Math.Sqrt(pastille.Average((x) => x.MeanColorBGR.Red * x.MeanColorBGR.Red) - avgR * avgR);
                    var stdG = Math.Sqrt(pastille.Average((x) => x.MeanColorBGR.Green * x.MeanColorBGR.Green) - avgG * avgG);
                    var stdB = Math.Sqrt(pastille.Average((x) => x.MeanColorBGR.Blue * x.MeanColorBGR.Blue) - avgB * avgB);

                    txtInfo.AppendText(avgR.ToString());
                    txtInfo.AppendText(SEPARATOR);

                    txtInfo.AppendText(avgG.ToString());
                    txtInfo.AppendText(SEPARATOR);

                    txtInfo.AppendText(avgB.ToString());
                    txtInfo.AppendText(SEPARATOR);

                    txtInfo.AppendText(stdR.ToString());
                    txtInfo.AppendText(SEPARATOR);

                    txtInfo.AppendText(stdG.ToString());
                    txtInfo.AppendText(SEPARATOR);

                    txtInfo.AppendText(stdB.ToString());


                    txtInfo.AppendText("\r\n");
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }

        }
    }
}
