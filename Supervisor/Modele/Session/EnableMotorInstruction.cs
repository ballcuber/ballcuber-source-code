using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver.Modele
{
    public class EnableMotorInstruction : Instruction
    {
        public bool value { get; private set; }

        public EnableMotorInstruction(bool value)
        {
            this.value = value;
        }

        public override void Execute(ResolutionSessionRuntimeContext ctx)
        {
            if (value) Hardware.Runner.EnableMotorAll();
            else Hardware.Runner.DisableMotorAll();
        }

        public override string ToString()
        {
            return value ? "Enable motors" : "Disable motors";
        }
    }
}
