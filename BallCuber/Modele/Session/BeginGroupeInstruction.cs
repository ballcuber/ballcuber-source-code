using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballcuber.Modele
{
    class BeginGroupeInstruction : Instruction
    {
        public string Name { get; private set; }

        public BeginGroupeInstruction(string name)
        {
            Name = name;
        }

        public override void Execute(ResolutionSessionRuntimeContext ctx)
        {
            
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool IsPseudoInstruction => true;
    }
}
