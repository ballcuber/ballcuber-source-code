using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver.Modele
{
    public class WaitInstruction : Instruction
    {
        public double Seconds { get; private set; }

        public WaitInstruction(double seconds)
        {
            this.Seconds = seconds;
        }

        public override void Execute(ResolutionSessionRuntimeContext ctx)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(Seconds));
        }

        public override string ToString()
        {
            return "Wait : " + Seconds + "s";
        }
    }
}
