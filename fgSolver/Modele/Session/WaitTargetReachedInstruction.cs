using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver.Modele
{
    public class WaitTargetReachedInstruction : Instruction
    {
        public override void Execute(ResolutionSessionRuntimeContext ctx)
        {
            try
            {
                var sw = Stopwatch.StartNew();

                var allReached = false;

                while (true)
                {
                    allReached = true;

                    int timeout;

                    using (var state = GlobalState.GetState())
                    {
                        if (state.Simulated)
                        {
                            System.Threading.Thread.Sleep(200);
                            return;
                        }
                        foreach (var m in state.Motors.Motors)
                        {
                            var targetPos = ctx.GetTargetPosition(m.Courronne, m.Axe);
                            if (targetPos != null && targetPos != m.Position) allReached = false;
                        }

                        timeout = state.HardwareConfigGlobal.WaitTargetReachedInstructionTimeoutMilliseconds;
                    }

                    if (allReached || ctx.CancelAsked) return;
                    else if (sw.ElapsedMilliseconds > timeout) throw new Exception("Tous les moteurs n'ont pas atteint leur position dans le temps imparti");
                    else System.Threading.Thread.Sleep(10);
                }
            }
            finally
            {
                ctx.ResetTargetPositions();
            }
            
        }

        public override string ToString()
        {
            return "Wait target reached";
        }
    }
}
