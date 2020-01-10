using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver.Modele
{
    public class SetSpeedInstruction : Instruction
    {
        public int value { get; private set; }

        public SetSpeedInstruction(int value)
        {
            this.value = value;
        }

        public override void Execute(ResolutionSessionRuntimeContext ctx)
        {
            Hardware.Runner.SetSpeedAll(value);
        }

        public override string ToString()
        {
            return "Set speed to : " + value;
        }
    }
}
