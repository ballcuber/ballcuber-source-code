using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace fgSolver
{

    public class Alarm
    {
        public string Message { get; set; }
        public string AdditionnalInfo { get; set; }
    }

    public sealed class Logger
    {
        private static TraceSource _source = new TraceSource("Log");

        public static void Log(Exception ex)
        {
            Log(ex.Message, TraceEventType.Error, ex.ToString());
        }

        private static int id = 0;
        public static void Log(string message, TraceEventType level = TraceEventType.Information, string AdditionnalInfo = "")
        {
            _source.TraceEvent(level, id, message );

            if(!string.IsNullOrEmpty(AdditionnalInfo)) _source.TraceEvent(level, id, AdditionnalInfo);

            if (level == TraceEventType.Error || level == TraceEventType.Warning || level == TraceEventType.Critical)
            {
                using(var state = GlobalState.GetState())
                {
                    var al = new Alarm();
                    al.Message = message;
                    al.AdditionnalInfo = AdditionnalInfo;
                    state.Alarms.Add(al);
                }
            }

            id++;

        }
    }
}
