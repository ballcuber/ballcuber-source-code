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
        private const char SEPARATOR = ';';

        private List<Pastille>[,,] _scannedColors;

        public VideoScannerDebugForm()
        {
            InitializeComponent();
        }

        public VideoScannerDebugForm(List<Pastille>[,,] _scannedColors) : this()
        {
            this._scannedColors = _scannedColors;



            try
            {
                var sbAvgH = new StringBuilder();
                var sbAvgS = new StringBuilder();
                var sbAvgV = new StringBuilder();

                var sbAvgR = new StringBuilder();
                var sbAvgG = new StringBuilder();
                var sbAvgB = new StringBuilder();

                foreach (var pastille in _scannedColors)
                {
                    sbAvgH.Append(pastille.Average((x) => x.MeanColorHSV.Hue));
                    sbAvgH.Append(SEPARATOR);

                    sbAvgS.Append(pastille.Average((x) => x.MeanColorHSV.Satuation));
                    sbAvgS.Append(SEPARATOR);

                    sbAvgV.Append(pastille.Average((x) => x.MeanColorHSV.Value));
                    sbAvgV.Append(SEPARATOR);

                    sbAvgR.Append(pastille.Average((x) => x.MeanColorBGR.Red));
                    sbAvgR.Append(SEPARATOR);

                    sbAvgG.Append(pastille.Average((x) => x.MeanColorBGR.Green));
                    sbAvgG.Append(SEPARATOR);

                    sbAvgB.Append(pastille.Average((x) => x.MeanColorBGR.Blue));
                    sbAvgB.Append(SEPARATOR);

                }

                txtInfo.AppendText(sbAvgH.ToString());
                txtInfo.AppendText("\r\n");
                txtInfo.AppendText(sbAvgS.ToString());
                txtInfo.AppendText("\r\n");
                txtInfo.AppendText(sbAvgV.ToString());
                txtInfo.AppendText("\r\n");
                txtInfo.AppendText(sbAvgR.ToString());
                txtInfo.AppendText("\r\n");
                txtInfo.AppendText(sbAvgG.ToString());
                txtInfo.AppendText("\r\n");
                txtInfo.AppendText(sbAvgB.ToString());
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }

        }
    }
}
