using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharer;
using fgSolver.Modele;

namespace fgSolver.Hardware
{
    public sealed class Runner
    {
        private static Board _board1 = new Board(1);
        private static Board _board2 = new Board(2);

        public static TimeSpan BlockingMoveTimeout { get; set; } = new TimeSpan(0, 0, 2);

        public static Board GetBoard(int index)
        {
            return index < 2 ? _board1 : _board2;
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

        public static void BlockingMove(MotorMove mv)
        {
            var task1 = System.Threading.Tasks.Task.Factory.StartNew(new Action(() => _board1.BlockingMove(mv, BlockingMoveTimeout)));
            var task2 = System.Threading.Tasks.Task.Factory.StartNew(new Action(() => _board2.BlockingMove(mv, BlockingMoveTimeout)));

            MainForm.Instance.Viewer.ExecuteMachineMove(mv);

            task1.Wait(BlockingMoveTimeout + BlockingMoveTimeout);
            task2.Wait(BlockingMoveTimeout + BlockingMoveTimeout);

            if (task1.Exception != null)
            {
                throw task1.Exception;
            }
            if (task2.Exception != null)
            {
                throw task2.Exception;
            }
        }

        public static void AsyncRun()
        {
            System.Threading.Tasks.Task.Factory.StartNew(new Action(() => Run()));
        }

        public static void Run()
        {
            try
            {
                MotorMove currentMove;

                MainForm.Instance.Viewer.RefreshCube();

                using (var state = GlobalState.GetState())
                {
                    if (state.Solution?.MachineMoves != null)
                    {
                        state.Solution.LastExecutedMotorMove = -1;
                    }
                }

                ResolutionSessionControl.Instance.StartTimer();

                do
                {
                    currentMove = null;

                    using (var state = GlobalState.GetState())
                    {
                        if (state.Solution?.MachineMoves?.MotorMoves != null)
                        {
                            int nextStep;
                            if (state.Solution.LastExecutedMotorMove < 0) nextStep = 0;
                            else nextStep = state.Solution.LastExecutedMotorMove + 1;

                            if (nextStep < state.Solution.MachineMoves.MotorMoves.Count)
                            {
                                state.Solution.LastExecutedMotorMove = nextStep;
                                currentMove = state.Solution.MachineMoves.MotorMoves[nextStep];
                            }


                        }
                    }

                    if ((object)currentMove != null)
                    {
                        BlockingMove(currentMove);
                    }

                }
                while ((object)currentMove != null);

            }
            catch(Exception ex)
            {
                Logger.Log(ex);
                using (var state = GlobalState.GetState())
                {
                   if (state.Solution!=null) state.Solution.LastExecutedMotorMove = -1;
                }

            }
            finally
            {
                ResolutionSessionControl.Instance.StopTImer();
            }
        }
    }
}
