using RevengeCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballcuber.Modele
{
    public class InitializeIHMInstruction : Instruction
    {
        public ColorCube Cube { get; private set; }

        public InitializeIHMInstruction(ColorCube cube)
        {
            this.Cube = cube;
        }

        public override void Execute(ResolutionSessionRuntimeContext ctx)
        {
            MainForm.Instance.Viewer.RefreshCube(Cube);
        }

        public override string ToString()
        {
            return "Initialisation IHM";
        }
    }
}
