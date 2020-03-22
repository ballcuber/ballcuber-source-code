using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharer;
using Ballcuber.Modele;

namespace Ballcuber.Hardware
{
    public sealed class Runner
    {
        private static Board _board1 = new Board(HardwareConfigBoard.Board1);
        private static Board _board2 = new Board(HardwareConfigBoard.Board2);

        public static TimeSpan BlockingMoveTimeout { get; set; } = new TimeSpan(0, 0, 2);

        public static Board GetBoard(HardwareConfigBoard index)
        {
            return index == HardwareConfigBoard.Board1 ? _board1 : _board2;
        }

        public static Board Board1
        {
            get
            {
                return _board1;
            }
        }

        public static Board Board2
        {
            get
            {
                return _board2;
            }
        }

        public static void Connect()
        {
            _board1.Connect();
            _board2.Connect();
        }

        public static bool Connected
        {
            get
            {
                return _board1.Connected && _board2.Connected;
            }
        }

        public static void Disconnect()
        {
            _board1.Disconnect();
            _board2.Disconnect();
        }

        //public static void BlockingMove(MotorMove mv)
        //{
        //    var task1 = System.Threading.Tasks.Task.Factory.StartNew(new Action(() => _board1.BlockingMove(mv, BlockingMoveTimeout)));
        //    var task2 = System.Threading.Tasks.Task.Factory.StartNew(new Action(() => _board2.BlockingMove(mv, BlockingMoveTimeout)));

        //    MainForm.Instance.Viewer.ExecuteMachineMove(mv);

        //    task1.Wait(BlockingMoveTimeout + BlockingMoveTimeout);
        //    task2.Wait(BlockingMoveTimeout + BlockingMoveTimeout);

        //    if (task1.Exception != null)
        //    {
        //        throw task1.Exception.InnerException;
        //    }
        //    if (task2.Exception != null)
        //    {
        //        throw task2.Exception.InnerException;
        //    }
        //}

        //public static void AsyncRun()
        //{
        //    System.Threading.Tasks.Task.Factory.StartNew(new Action(() => Run()));
        //}

        //public static void Run()
        //{
        //    try
        //    {
        //        MotorMove currentMove;

        //        MainForm.Instance.Viewer.RefreshCube();

        //        DateTime startDate = DateTime.Now;

        //        using (var state = GlobalState.GetState())
        //        {
        //            if (state.Solution?.MachineMoves != null)
        //            {
        //                state.Solution.LastExecutedMotorMove = -1;
        //                startDate = state.Solution.Date;
        //            }
        //        }

        //        ResolutionSessionControl.Instance.StartTimer();

        //        do
        //        {
        //            currentMove = null;

        //            using (var state = GlobalState.GetState())
        //            {
        //                if (state.Solution?.MachineMoves?.MotorMoves != null && startDate== state.Solution.Date)
        //                {
        //                    int nextStep;
        //                    if (state.Solution.LastExecutedMotorMove < 0) nextStep = 0;
        //                    else nextStep = state.Solution.LastExecutedMotorMove + 1;

        //                    if (nextStep < state.Solution.MachineMoves.MotorMoves.Count)
        //                    {
        //                        state.Solution.LastExecutedMotorMove = nextStep;
        //                        currentMove = state.Solution.MachineMoves.MotorMoves[nextStep];
        //                    }


        //                }
        //            }

        //            if ((object)currentMove != null)
        //            {
        //                BlockingMove(currentMove);
        //            }

        //        }
        //        while ((object)currentMove != null);

        //    }
        //    catch(Exception ex)
        //    {
        //        Logger.Log(ex);
        //        using (var state = GlobalState.GetState())
        //        {
        //           if (state.Solution!=null) state.Solution.LastExecutedMotorMove = -1;
        //        }

        //    }
        //    finally
        //    {
        //        ResolutionSessionControl.Instance.StopTImer();
        //    }
        //}

        public static void SetSpeedMotor(Motor m, int value)
        {
            if (m == null) return;

            GetBoard(m.Board).SetSpeed(m.Mask, value);
        }

        public static void BeginMoveStep(Motor m, int value)
        {
          if (m == null) return;
            AssertMotorEnabled(m);

            GetBoard(m.Board).Move(m.Mask,m.Inverted ? -value : value);
        }

        public static void BeginMoveAbsolute(Motor m, int value)
        {
            if (m == null) return;
            AssertMotorEnabled(m);

            GetBoard(m.Board).MoveTo(m.Mask, m.Inverted ? -value : value);
        }

        private static void AssertMotorEnabled(Motor m)
        {
           // if (!m.Enabled) throw new Exception("Can't move, motor disabled.");
        }


        public static void SetCurrentPosition(Motor m, int value)
        {
            if (m == null) return;

            GetBoard(m.Board).SetCurrentPosition(m.Mask,m.Inverted ? -value : value);
        }


        public static void EnableMotor(Motor m)
        {
            if (m == null) return;

            GetBoard(m.Board).EnableOutputs(m.Mask);
        }

        public static void Stop(Motor m)
        {
            if (m == null) return;

            GetBoard(m.Board).Stop(m.Mask);
        }

        public static void StopAll()
        {
            if (ResolutionSession.GetStatus().State==ResolutionSessionState.Running)
            {
                ResolutionSession.Pause();
            }
            _board1.SetAcceleration(0xFF, 140000);
            _board2.SetAcceleration(0xFF, 140000);
            _board1.Stop(0xff);
            _board2.Stop(0xff);
        }

        public static void DisableMotor(Motor m)
        {
            if (m == null) return;

            GetBoard(m.Board).DisableOutputs(m.Mask);
        }


        public static void SetCurrentPositionAll(int value)
        {
            using (var state = GlobalState.GetState())
            {
                foreach (var m in state.Motors.Motors) {
                    SetCurrentPosition(m, value);
                }
            }
        }

        public static void SetSpeedAll(int value)
        {
            _board1.SetSpeed(0xff, value);
            _board2.SetSpeed(0xff, value);
        }


        public static void EnableMotorAll()
        {
            _board1.EnableOutputs(0xff);
            _board2.EnableOutputs(0xff);
        }

        public static void DisableMotorAll()
        {
            _board1.DisableOutputs(0xff);
            _board2.DisableOutputs(0xff);
        }

        public static void SetAccelerationAll(int value)
        {
            _board1.SetAcceleration(0xff, value);
            _board2.SetAcceleration(0xff, value);
        }

        public static void MoveToKnownPosition(Axe Axe, Couronne Couronne, KnownPosition KnownPosition, ResolutionSessionRuntimeContext ctx = null)
        {
            int steps;
            using (var state = GlobalState.GetState())
            {
              var  Motor = state.Motors.Motors.FirstOrDefault((x) => x.Axe == Axe && x.Courronne == Couronne);

                if (Motor == null) throw new Exception("Unable to find motor definition for crown " + Couronne + " axe " + Axe);

                var stepPerQuarter = state.HardwareConfigGlobal.StepsPerCubeQuarter;

                var modPos = Motor.Position % stepPerQuarter;
                if (modPos < 0) modPos += stepPerQuarter;

                switch (KnownPosition)
                {

                    case KnownPosition.NegativeQuarter:
                        steps = -stepPerQuarter;
                        break;
                    case KnownPosition.PositiveQuarter:
                        steps = stepPerQuarter;
                        break;

                    case KnownPosition.NegativeQuarterFromMinStop:
                        steps = -stepPerQuarter+ Motor.StepsToPositivePosition / 2;
                        break;

                    case KnownPosition.PositiveQuarterFromMaxStop:
                        steps = stepPerQuarter - Motor.StepsToPositivePosition / 2;
                        break;

                    case KnownPosition.UTurnFromMaxStop:
                        steps = 2 * stepPerQuarter - Motor.StepsToPositivePosition / 2; ;
                        break;

                    case KnownPosition.UTurnPositive:
                        steps = 2 * stepPerQuarter;
                        break;

                    case KnownPosition.UTurnNegative:
                        steps = - 2 * stepPerQuarter;
                        break;

                    case KnownPosition.MaxStop:
                        steps = Motor.StepsToPositivePosition - modPos;
                        break;
                    case KnownPosition.MinStop:
                        steps = -modPos;
                        break;
                    case KnownPosition.Middle:
                        steps = Motor.StepsToPositivePosition / 2 - modPos;
                        break;
                    case KnownPosition.PositiveSmallAmount:
                        steps = stepPerQuarter / 6;
                        break;
                    case KnownPosition.NegativeSmallAmount:
                        steps = -stepPerQuarter / 6;
                        break;

                    case KnownPosition.MaxStopIntermediaire:
                        steps = Motor.StepsToPositivePosition - modPos- stepPerQuarter / 10;
                        break;
                    case KnownPosition.MinStopIntermediaire:
                        steps = -modPos+ stepPerQuarter/10;
                        break;
                    default:
                        throw new Exception("Unknow position : " + KnownPosition);
                }


                steps += Motor.Position;

               if(ctx!=null) ctx.SetTargetPosition(Motor.Courronne, Motor.Axe, steps);


               if (state.Simulated) return;


                Runner.BeginMoveAbsolute(Motor,steps);
            }
        }

        public static void MoveAllToKnownPosition(KnownPosition knownPosition)
        {
            foreach(var axe in new Axe[] { Axe.X, Axe.Y, Axe.Z })
            {
                foreach(var courronne in new Couronne[] { Couronne.MidMin, Couronne.MidMax, Couronne.Max })
                {
                    MoveToKnownPosition(axe, courronne, knownPosition);
                }
            }
           
        }

    }
}
