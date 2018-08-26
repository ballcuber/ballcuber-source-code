using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevengeCube;
using System.ComponentModel;

namespace fgSolver.Modele
{
    public class Solution : ApplicationState
    {
        public DateTime Date { get; set; }

        public string MovesString { get; set; }

        public ColorCube OriginalCube { get; set; }

        public string SolverOutput { get; set; }
        public  MachineMoveList MachineMoves { get; set; }
        public  List<Move> Moves { get; set; }


        public bool IsRunning
        {
            get
            {
                return MachineMoves != null && MachineMoves.MotorMoves!=null && LastExecutedMotorMove >= 0 && LastExecutedMotorMove+1 < MachineMoves.MotorMoves.Count;
            }
        }

        public int LastExecutedMotorMove { get; set; } = -1;


        // deux solution sont égaled si elles ont les mêmes MachineMoves et Moves
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Solution)) return false;

            var sol = (Solution)obj;

            if (sol.Moves == null && Moves != null || Moves == null && sol.Moves != null) return false;
            if (sol.MachineMoves == null && MachineMoves != null || MachineMoves == null && sol.MachineMoves != null) return false;

            if(sol.Moves!=null && Moves != null)
            {
                if (sol.Moves.Count() != Moves.Count()) return false;

                for(int i = 0; i < Moves.Count(); i++)
                {
                    if (Moves[i] != sol.Moves[i]) return false;      
                }
            }

            if (sol.MachineMoves != null && MachineMoves != null)
            {
                if (sol.MachineMoves != MachineMoves) return false;
            }

            return LastExecutedMotorMove == sol.LastExecutedMotorMove && Date == sol.Date;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override object Clone()
        {
            var newSolution = new Solution();

            newSolution.MovesString = MovesString;

            if (OriginalCube != null) newSolution.OriginalCube = OriginalCube.CloneCube();

            newSolution.SolverOutput = SolverOutput;

            if (MachineMoves != null)
            {
                newSolution.MachineMoves = new MachineMoveList();

                foreach (var mv in MachineMoves)
                {
                    newSolution.MachineMoves.Add(mv.Axe, mv.Couronne, mv.Sens);
                }
            }

            if (Moves != null)
            {
                newSolution.Moves = new List<Move>();

                foreach (var mv in Moves)
                {
                    newSolution.Moves.Add(new Move(mv.Axe, mv.Couronne, mv.Sens));
                }
            }

            newSolution.LastExecutedMotorMove = LastExecutedMotorMove;
            newSolution.Date = Date;

            return newSolution;
        }

        public static bool operator ==(Solution s1, Solution s2)
        {
            if ((object)s1 == null && (object)s2 != null) return false;

            if ((object)s1 == null && (object)s2 == null) return true;

            return s1.Equals(s2);
        }

        public static bool operator !=(Solution s1, Solution s2)
        {
            return !(s1 == s2);
        }

    }
}
