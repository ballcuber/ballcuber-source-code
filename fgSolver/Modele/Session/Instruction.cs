using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver.Modele
{
    public abstract class Instruction
    {
        public abstract void Execute(ResolutionSessionRuntimeContext ctx);

        public bool Breakpoint = false;


        public virtual bool IsPseudoInstruction
        {
            get { return false; }
        }
    }
}
