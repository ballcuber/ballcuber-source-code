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

            int steps;

            Motor Motor;

            using (var state = GlobalState.GetState())
            {
                Motor = state.Motors.Motors.FirstOrDefault((x) => x.Axe == Axe && x.Courronne == Couronne);

                if (Motor == null)   throw new Exception("Unable to find motor definition for crown " + Couronne + " axe " + Axe);

                var stepPerQuarter = state.HardwareConfigGlobal.StepsPerCubeQuarter;

                var modPos = Motor.Position % stepPerQuarter;

                switch (KnownPosition)
                {

                    case KnownPosition.NegativeQuarter:
                        steps = -stepPerQuarter;
                        break;

                    case KnownPosition.PositiveQuarter:
                        steps = stepPerQuarter;
                        break;

                    //case KnownPosition.UTurn:
                    //    steps = 2 * stepPerQuarter;
                    //    break;

                    case KnownPosition.MaxStop:
                        steps = Motor.StepsToPositivePosition - modPos;
                        break;

                    case KnownPosition.MinStop:
                        steps = -modPos;
                        break;

                    case KnownPosition.Middle:
                        steps = Motor.StepsToPositivePosition / 2 - modPos;
                        break;

                    default:
                        throw new Exception("Unknow position : " + KnownPosition);
                }

                steps += Motor.Position;

                ctx.SetTargetPosition(Motor.Courronne, Motor.Axe, steps);

                if (state.Simulated) return;
            }

            Runner.BeginMoveAbsolute(Motor, steps);
        }


        public override string ToString()
        {
            return "Move " + Axe + " " + Couronne + " to " + KnownPosition;
        }
    }
}
