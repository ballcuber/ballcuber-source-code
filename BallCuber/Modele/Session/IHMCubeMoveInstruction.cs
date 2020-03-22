using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballcuber.Modele
{
    public class IHMCubeMoveInstruction : Instruction
    {
        public MotorMove Move { get; private set; }

        public IHMCubeMoveInstruction(MotorMove move)
        {
            this.Move = move;
        }

        public override void Execute(ResolutionSessionRuntimeContext ctx)
        {
            MainForm.Instance.Viewer.ExecuteMachineMove(Move);
        }

        public override string ToString()
        {
            return "Animation IHM";
        }
    }
}
