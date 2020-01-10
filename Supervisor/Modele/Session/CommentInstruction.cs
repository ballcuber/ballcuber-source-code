using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver.Modele
{
    class CommentInstruction : Instruction
    {
        public string Comment { get; private set; }

        public CommentInstruction(string comment)
        {
            this.Comment = comment;
        }

        public override void Execute(ResolutionSessionRuntimeContext ctx)
        {
            
        }

        public override bool IsPseudoInstruction => true;

        public override string ToString()
        {
            return Comment;
        }
    }
}
