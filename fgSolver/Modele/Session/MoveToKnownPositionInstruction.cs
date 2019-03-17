using fgSolver.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver.Modele
{
    public enum KnownPosition
    {
        MinStop, // butée min (0)
        MaxStop, // butée max
        Middle, // position intermédiare débrayée
        PositiveQuarter, // un quart de tour positif
        NegativeQuarter, // un quart de tour négatif
       // UTurn // demi tour de la couronne
    }
    public class MoveToKnownPositionInstruction : Instruction
    {
        public Axe Axe { get; set; }

        public Couronne Couronne { get; set; }

        public KnownPosition KnownPosition { get; set; }

        public MoveToKnownPositionInstruction(Axe axe, Couronne couronne, KnownPosition pos)
        {
            this.Couronne = couronne;
            this.Axe = axe;
            this.KnownPosition = pos;
        }

        public override void Execute(ResolutionSessionRuntimeContext ctx)
        {

             Runner.MoveToKnownPosition(Axe, Couronne, KnownPosition, ctx);

        }


        public override string ToString()
        {
            return "Move " + Axe + " " + Couronne + " to " + KnownPosition;
        }
    }
}
