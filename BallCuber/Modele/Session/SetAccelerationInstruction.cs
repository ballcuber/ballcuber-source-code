using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballcuber.Modele
{
    public class SetAccelerationInstruction : Instruction
    {
        public int value { get; private set; }

        public SetAccelerationInstruction(int value)
        {
            this.value = value;
        }

        public override void Execute(ResolutionSessionRuntimeContext ctx)
        {
            Hardware.Runner.SetAccelerationAll(value);
        }

        public override string ToString()
        {
            return "Set acceleration to : " + value;
        }
    }
}
