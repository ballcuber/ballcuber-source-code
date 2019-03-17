using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fgSolver.Modele
{
    public enum ResolutionSessionState
    {
        Stopped,
        Paused,
        Running,
    }

    public class ResolutionSessionStatus {
        public Instruction[] Instructions;
        public int CurrentInstructionID;

        public ResolutionSessionState State;

        public int Signature;
    }

    public class ResolutionSessionRuntimeContext
    {
        private class ResolutionSessionRuntimeContextItems
        {
            public Couronne Couronne;
            public Axe Axe;
            public int? TargetPosition; // null si ce moteur n'a pas de target position
        }

        public ResolutionSessionRuntimeContext()
        {
            foreach(var c in new Couronne[] { Couronne.MidMin, Couronne.MidMax, Couronne.Max })
            {
                foreach(var a in new Axe[] { Axe.X, Axe.Y, Axe.Z })
                {
                    _lst.Add(new ResolutionSessionRuntimeContextItems { Couronne = c, Axe = a });
                }
            }
        }

        public bool CancelAsked;
        
        private List<ResolutionSessionRuntimeContextItems>  _lst = new List<ResolutionSessionRuntimeContextItems>();

        public void SetTargetPosition(Couronne c, Axe a, int value)
        {
            _lst.First((x) => x.Couronne == c && x.Axe == a).TargetPosition = value;
        }

        public int? GetTargetPosition(Couronne c, Axe a)
        {
           return _lst.First((x) => x.Couronne == c && x.Axe == a).TargetPosition;
        }

        public void Reset()
        {
            ResetTargetPositions();
            CancelAsked = true;
        }

        public void ResetTargetPositions()
        {
            foreach(var itm in _lst)
            {
                itm.TargetPosition = null;
            }
        }
    }

    public class ResolutionSessionDesignContext
    {
        private class ResolutionSessionDesignContextItems
        {
            public Couronne Couronne;
            public Axe Axe;
            public KnownPosition Position = KnownPosition.MinStop;
        }

        public ResolutionSessionDesignContext()
        {
            foreach (var c in new Couronne[] { Couronne.MidMin, Couronne.MidMax, Couronne.Max })
            {
                foreach (var a in new Axe[] { Axe.X, Axe.Y, Axe.Z })
                {
                    _lst.Add(new ResolutionSessionDesignContextItems { Couronne = c, Axe = a });
                }
            }
        }


        private List<ResolutionSessionDesignContextItems> _lst = new List<ResolutionSessionDesignContextItems>();

        public void SetPosition(Couronne c, Axe a, KnownPosition value)
        {
            _lst.First((x) => x.Couronne == c && x.Axe == a).Position = value;
        }

        public KnownPosition GetPosition(Couronne c, Axe a)
        {
            return _lst.First((x) => x.Couronne == c && x.Axe == a).Position;
        }

        public void Reset()
        {
            ResetPositions();
        }

        public void ResetPositions()
        {
            foreach (var itm in _lst)
            {
                itm.Position =  KnownPosition.MinStop;
            }
        }
    }


    public static class ResolutionSession
    {
        private static Thread _backgroundThread;

        private static int Signature;

        // objet passé en paramètre des instructions pour maintenir un état
        private static readonly ResolutionSessionRuntimeContext _runtimeContext = new ResolutionSessionRuntimeContext();

        // objet qui permet de maintenir un état lors de la création de la suite d'instruction
        private static readonly ResolutionSessionDesignContext _designContext = new ResolutionSessionDesignContext();

        public static ResolutionSessionStatus GetStatus()
        {
            lock (_lck)
            {
                var st = new ResolutionSessionStatus();
                st.Instructions = _instructions.ToArray();
                st.CurrentInstructionID = _currentInstructionID;
                st.State = State;
                st.Signature = Signature;
                return st;
            }
        }

        static ResolutionSession()
        {
            _backgroundThread = new Thread(Loop);
            _backgroundThread.IsBackground = true;
            _backgroundThread.Start();
        }

        private static void NotifyChange()
        {
            Signature++;
        }

        private static List<Instruction> _instructions = new List<Instruction>();
        private static int _currentInstructionID;

        private static ResolutionSessionState State;

        private static bool RunningToNext;

        private static object _lck = new object();

        public static void Clear()
        {
            lock (_lck)
            {
                Abort();
                _instructions.Clear();
                NotifyChange();
                _designContext.Reset();
            }
        }

        public static void Run()
        {
            lock (_lck)
            {
                State = ResolutionSessionState.Running;
                NotifyChange();
            }
        }

        public static void Pause()
        {
            lock (_lck)
            {
                State = ResolutionSessionState.Paused;
                NotifyChange();
            }
        }

        public static void Next()
        {
            lock (_lck)
            {
                RunningToNext = true;
                Run();
                NotifyChange();
            }
        }

        public static void Abort()
        {
            lock (_lck)
            {
                _currentInstructionID = 0;
                State = ResolutionSessionState.Stopped;
                NotifyChange();
                _runtimeContext.Reset();
            }
        }

        public static void Add(Instruction i)
        {
            lock (_lck)
            {
                Abort();
                _instructions.Add(i);
                NotifyChange();
            }
        }

        public static void SetExecutionPointer(int i)
        {
            lock (_lck)
            {
                if (State == ResolutionSessionState.Running) return;
                _currentInstructionID = i;
                NotifyChange();
            }
        }

        public static void SetBreakpoint(int i)
        {
            lock (_lck)
            {
                if (i >= _instructions.Count || i < 0) return;
                _instructions[i].Breakpoint = !_instructions[i].Breakpoint;
                NotifyChange();
            }
        }

        private static void Loop()
        {
            while (true)
            {
                if (State != ResolutionSessionState.Running)
                {
                    Thread.Sleep(10);
                    continue;
                }

                try
                {
                        Instruction instruction = null;
                        lock (_lck)
                        {
                            // pas en run
                            if (State != ResolutionSessionState.Running)   continue;

                            // fin de la session
                            if (_currentInstructionID >= _instructions.Count || _currentInstructionID < 0)
                            {
                                Pause();
                                continue;
                            }

                            instruction = _instructions[_currentInstructionID];

                            _runtimeContext.CancelAsked = false;
                        }

                        instruction.Execute(_runtimeContext);

                        lock (_lck)
                        {
                            if (State != ResolutionSessionState.Running) continue;

                            while (true)
                            {

                                    _currentInstructionID++;
                                NotifyChange();

                                if (_currentInstructionID < _instructions.Count && _currentInstructionID >= 0)
                                {
                                    // breakpoint
                                    if (_instructions[_currentInstructionID].Breakpoint)
                                    {
                                        Pause();
                                        break;
                                    }
                                    else if (!_instructions[_currentInstructionID].IsPseudoInstruction)
                                    {
                                        // ne pas s'arrêter sur pseudo instrustion sauf si elle a un breakpoint
                                        break;
                                    }
                                }
                                else break;
                            }

                            if (RunningToNext)
                            {
                                RunningToNext = false;
                                Pause();
                            }
                        }

                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    Abort();
                }
            }
        }


        public static void FromSolution()
        {
            Clear();

            using (var state = GlobalState.GetState())
            {
                if (state.Solution?.MachineMoves?.MotorMoves == null) return;

                Add(new CommentInstruction(state.Solution.Date.ToString()));
                Add(new CommentInstruction(state.Solution.MovesString));
                Add(new CommentInstruction("Moves : " + state.Solution.MachineMoves.MotorMoves.Count.ToString()));
                Add(new CommentInstruction("Quarters : " + state.Solution.MachineMoves.MotorMoves.Sum((x) => x.QuarterNumber).ToString()));
                Add(new InitializeIHMInstruction(state.Solution.OriginalCube));

                AddInitBlock();

                foreach (var mv in state.Solution.MachineMoves.MotorMoves)
                {
                    AddAlignment();
                    Add(mv);
                }
            }

        }

        public static void Add(MotorMove mv)
        {
            Add(new BeginGroupeInstruction(mv.ToString()));
            Add(new IHMCubeMoveInstruction(mv));

            using (var state = GlobalState.GetState())
            {
                var remainQuarters = new int[] { mv.MidMinMovesCount, mv.MidMaxMovesCount, mv.MaxMovesCount };

                // cas particulier de l'axe Z Max qui pour fonctionner correctement doit être après un mouvement de son voisin qui remet de l'ordre
                if(mv.Axe== Axe.Z && mv.MaxMovesCount != 0)
                {
                    // Ajouter un quart de tour de ZMidMax dans un sens, puis dans l'autre en finissant par le même sens que Z mAx pour limiter le nombre de mouvement
                    var sensZMax = Math.Sign(mv.MaxMovesCount);
                    AddWithAnticollision(state, new int[] { 0, -sensZMax, 0 }, -sensZMax, Axe.Z);
                    AddWithAnticollision(state, new int[] { 0, sensZMax, 0 }, sensZMax, Axe.Z);
                }

                if (mv.PotentialNegativeQuarters > mv.PotentialPositiveQuarters)
                { // commencer par tourner dans le sens négatif car c'est ce qu'il y a de plus à faire

                    // dans le cas d'un sens négatif, faire les demi tour dans l'autre sens
                    for (int i = 0; i < remainQuarters.Length; i++)
                    {
                        if (remainQuarters[i] == 2) remainQuarters[i] = -2;
                    }

                    AddWithAnticollision(state, remainQuarters, -1, mv.Axe);
                    AddWithAnticollision(state, remainQuarters, -1, mv.Axe);// il y a au plus 2 quarts de tour à faire pour chanque sens
                    AddWithAnticollision(state, remainQuarters, 1, mv.Axe);
                    AddWithAnticollision(state, remainQuarters, 1, mv.Axe);
                }
                else
                {
                    AddWithAnticollision(state, remainQuarters, 1, mv.Axe);
                    AddWithAnticollision(state, remainQuarters, 1, mv.Axe);
                    AddWithAnticollision(state, remainQuarters, -1, mv.Axe);
                    AddWithAnticollision(state, remainQuarters, -1, mv.Axe);
                }

                AddInitBlock();

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="remainQuarters"></param>
        /// <param name="sens">1 ou -1</param>
        /// <param name="axe"></param>
        private static void AddWithAnticollision(GlobalState state,int[] remainQuarters, int sens, Axe axe)
        {
            var couronnesToMove = new List<Couronne>();
            var couronnesToFix = new List<Couronne>();

            if (Math.Sign(remainQuarters[0]) == sens)
            {
                couronnesToMove.Add(Couronne.MidMin);
                remainQuarters[0] -= sens ;
            }
            else couronnesToFix.Add(Couronne.MidMin);

            if (Math.Sign(remainQuarters[1]) == sens)
            {
                couronnesToMove.Add(Couronne.MidMax);
            remainQuarters[1] -= sens;
            }
            else couronnesToFix.Add(Couronne.MidMax);

            if (Math.Sign(remainQuarters[2]) == sens)
            {
                couronnesToMove.Add(Couronne.Max);
                remainQuarters[2]-= sens;
            }
            else couronnesToFix.Add(Couronne.Max);


            if (couronnesToMove.Count == 0) return;

            Add(new SetAccelerationInstruction(state.HardwareConfigGlobal.DisengagedAcceleration));
            Add(new SetSpeedInstruction(state.HardwareConfigGlobal.DisengagedSpeed));

            // mise en position des couronnes parallèles
            foreach (var c in couronnesToFix)
            {
                AddMoveToKnowPosition(axe, c, sens > 0 ? KnownPosition.MinStop : KnownPosition.MaxStop);
            }

            // mise en position des couronnes motrices
            foreach (var c in couronnesToMove)
            {
                AddMoveToKnowPosition(axe, c, sens > 0 ? KnownPosition.MaxStop : KnownPosition.MinStop);
            }


            //Déplacement des autres couronnes perpendiculaires
            foreach (var motor in state.Motors.Motors)
            {
                if (motor.Axe == axe) continue;
                /*
                var negCollision = couronnesToMove.Any((c) => motor.NegativeCollision.HasCollision(axe, c));
                var posCollision = couronnesToMove.Any((c) => motor.PositiveCollision.HasCollision(axe, c));

                KnownPosition pos;

                if (negCollision && posCollision) pos = KnownPosition.Middle;
                else if (negCollision) pos = KnownPosition.MaxStop;
                else if (posCollision) pos = KnownPosition.MinStop;
                else
                {
                    // si pas de collision, ne pas toucher au moteur sauf s'il est en position middle non engreiné
                    if (_designContext.GetPosition(motor.Courronne, motor.Axe) == KnownPosition.Middle) pos = KnownPosition.MinStop;
                    else continue;
                }

                AddMoveToKnowPosition(motor.Axe, motor.Courronne, pos);
                */
                AddMoveToKnowPosition(motor.Axe, motor.Courronne, KnownPosition.Middle);

            }

            Add(new WaitTargetReachedInstruction());

            Add(new SetAccelerationInstruction(state.HardwareConfigGlobal.EngagedAcceleration));
            Add(new SetSpeedInstruction(state.HardwareConfigGlobal.EngagedSpeed));

            // faire un quart de tour
            foreach(var c in couronnesToMove)
            {
                AddMoveToKnowPosition(axe, c, sens > 0 ? KnownPosition.PositiveQuarter : KnownPosition.NegativeQuarter);
            }

            Add(new WaitTargetReachedInstruction());
        }

        private static void AddMoveToKnowPosition(Axe axe, Couronne couronne, KnownPosition pos)
        {
            Add(new MoveToKnownPositionInstruction(axe, couronne, pos));

            /*
            if (_designContext.GetPosition(couronne, axe) != pos)
            {
                Add(new MoveToKnownPositionInstruction(axe, couronne, pos));

                // un quart de tour ne modifie pas la position actuelle, ne mettre à jour que si changement
                if (pos != KnownPosition.NegativeQuarter && pos != KnownPosition.PositiveQuarter)
                {
                    _designContext.SetPosition(couronne, axe, pos);
                }
            }
            */
        }

        public static void AddInitBlock()
        {
            Add(new BeginGroupeInstruction("Position initiale"));
            using (var state = GlobalState.GetState())
            {
                Add(new SetAccelerationInstruction(state.HardwareConfigGlobal.DisengagedAcceleration));
                Add(new SetSpeedInstruction(state.HardwareConfigGlobal.DisengagedSpeed));
            }
            foreach (var c in new Couronne[] { Couronne.MidMin, Couronne.MidMax, Couronne.Max })
            {
                foreach (var a in new Axe[] { Axe.X, Axe.Y, Axe.Z })
                {
                    AddMoveToKnowPosition(a, c, KnownPosition.Middle);
                }
            }
            Add(new WaitTargetReachedInstruction());
        }

        public static void AddAlignment()
        {
            Add(new BeginGroupeInstruction("Alignement"));
            using (var state = GlobalState.GetState())
            {
                Add(new SetAccelerationInstruction(state.HardwareConfigGlobal.DisengagedAcceleration));
                Add(new SetSpeedInstruction(state.HardwareConfigGlobal.DisengagedSpeed));
            }
            foreach (var c in new Couronne[] { Couronne.MidMin, Couronne.MidMax, Couronne.Max })
            {
                foreach (var a in new Axe[] { Axe.X, Axe.Y, Axe.Z })
                {
                    AddMoveToKnowPosition(a, c, KnownPosition.MaxStop);
                }
            }
            Add(new WaitTargetReachedInstruction());
            foreach (var c in new Couronne[] { Couronne.MidMin, Couronne.MidMax, Couronne.Max })
            {
                foreach (var a in new Axe[] { Axe.X, Axe.Y, Axe.Z })
                {
                    AddMoveToKnowPosition(a, c, KnownPosition.MinStop);
                }
            }
            Add(new WaitTargetReachedInstruction());
        }

    }
}
