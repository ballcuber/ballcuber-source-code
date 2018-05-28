using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver
{
    public sealed class ImageProcessor
    {
        private static Capture _capture;

        private static object _captureLock = new object();

        public static bool IsCapturing
        {
            get
            {
                lock (_captureLock)
                {
                    return _capture != null;
                }
            }
        }

        public static void StartCapture()
        {
            lock (_captureLock)
            {
                _capture = new Capture();


            }
        }

        public static void StopCapture()
        {
            lock (_captureLock)
            {
               if(_capture != null)
                {
                    _capture.Stop();
                    _capture.Dispose();
                    _capture = null;
                }
           }

        }

        
    }
}
