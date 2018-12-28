using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace fgSolver.Modele
{
    public class MotorMove
    {
        public int MaxMovesCount = 0;
        public int MidMaxMovesCount = 0;
        public int MidMinMovesCount = 0;

        public Axe Axe;

        public MotorMove(Axe a)
        {
            this.Axe = a;
        }

        public MotorMove(Axe a, Couronne c, Sens s)
        {
            this.Axe = a;

            Add(c, s);
        }

        public void Add(Couronne c, Sens s)
        {
            switch (c)
            {
                case Couronne.Max:
                    increment(ref MaxMovesCount ,s);
                    break;
                case Couronne.MidMax:
                    increment(ref MidMaxMovesCount, s);
                    break;
                case Couronne.MidMin:
                    increment(ref MidMinMovesCount, s);
                    break;
            }
        }

        public int GetMoves(Couronne courrone)
        {
            switch (courrone)
            {
                case Couronne.Max:
                    return MaxMovesCount;
                    break;
                case Couronne.MidMax:
                    return MidMaxMovesCount;
                    break;
                case Couronne.MidMin:
                    return MidMinMovesCount;
                    break;
                default:
                    return 0;
                    break;
            }
        }

        // nombre de quart de tour positif pour ce mouvement
        public int PotentialPositiveQuarters
        {
            get
            {
                return (MaxMovesCount > 0 ? MaxMovesCount : 0) + (MidMaxMovesCount > 0 ? MidMaxMovesCount : 0) + (MidMinMovesCount > 0 ? MidMinMovesCount : 0);
            }
        }

        // nombre de quart de tour négatif pour ce mouvement (un demi tour peut potentiellement se faire dans le sens négatif)
        public int PotentialNegativeQuarters
        {
            get
            {
                return Math.Abs((MaxMovesCount < 0 ? MaxMovesCount : 0) + (MidMaxMovesCount < 0 ? MidMaxMovesCount : 0) + (MidMinMovesCount < 0 ? MidMinMovesCount : 0)) +
                  (MaxMovesCount == 2 ? 2 : 0) + (MidMaxMovesCount == 2 ? 2 : 0) + (MidMinMovesCount == 2 ? 2 : 0);
            }
        }

        private void increment(ref int count, Sens s)
        {
            count += (int)s;

            switch (count)
            {
                case 3:
                    count = -1;
                    break;
                case 4:
                    count = 0;
                    break;
                case -2:
                    count = 2;
                    break;
                case -3:
                    count = 1;
                    break;
                case -4:
                    count = 0;
                    break;
            }
        }

        public override string ToString()
        {
            return Axe.ToString() + " " + MidMinMovesCount + " " + MidMaxMovesCount + " " + MaxMovesCount;
        }

        // nombre de quart de tour de ce mouvement
       public int QuarterNumber
        {
            get
            {
                return Math.Max(Math.Abs(MaxMovesCount), Math.Max(Math.Abs(MidMinMovesCount), Math.Abs(MidMaxMovesCount)));
            }
        }

        public string EquivalentCubeMove()
        {

            var moves = new List<string>();

            for (int i = 0; i < Math.Abs(MaxMovesCount); i++)
            {
                string faceMove = "";
                switch (Axe)
                {
                    case Axe.X:
                        faceMove = "B";
                        break;
                    case Axe.Y:
                        faceMove = "L";
                        break;
                    case Axe.Z:
                        faceMove = "U";
                        break;
                }

                if (MaxMovesCount > 0)
                {
                    faceMove += "'";
                }

                moves.Add(faceMove);
            }

            for (int i = 0; i < Math.Abs(MidMaxMovesCount); i++)
            {
                string letter = "";
                switch (Axe)
                {
                    case Axe.X:
                        letter = "B";
                        break;
                    case Axe.Y:
                        letter = "L";
                        break;
                    case Axe.Z:
                        letter = "U";
                        break;
                }


                string faceMove;

                if (MidMaxMovesCount > 0)
                {
                    faceMove = letter + "w'" + " " + letter;
                }
                else {
                    faceMove = letter + "w" + " " + letter + "'";
                }

                moves.Add(faceMove);
            }


            for (int i = 0; i < Math.Abs(MidMinMovesCount); i++)
            {
                string letter = "";
                switch (Axe)
                {
                    case Axe.X:
                        letter = "F";
                        break;
                    case Axe.Y:
                        letter = "R";
                        break;
                    case Axe.Z:
                        letter = "D";
                        break;
                }


                string faceMove;

                if (MidMinMovesCount > 0)
                {
                    faceMove = letter + "w" + " " + letter + "'";
                }
                else
                {
                    faceMove = letter + "w'" + " " + letter;
                }

                moves.Add(faceMove);
            }



            return string.Join(" ", moves);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() == this.GetType()) return false;

            return this == (MotorMove)obj;
        }

        public static bool operator ==(MotorMove m1, MotorMove m2)
        {
            return m1.Axe == m2.Axe && m1.MaxMovesCount == m2.MaxMovesCount && m1.MidMaxMovesCount == m2.MidMaxMovesCount && m1.MidMinMovesCount == m2.MidMinMovesCount;
        }

        public static bool operator !=(MotorMove m1, MotorMove m2)
        {
            return !(m1 == m2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class MachineMoveList : List<Move>
    {
        public List<MotorMove> MotorMoves  = new List<MotorMove>();

        public void Add(Axe a, Couronne c, Sens s)
        {
            Add(new Move(a, c, s));

            var motorMove = new MotorMove(a,c,s);

            if (MotorMoves.Count > 0 && MotorMoves.Last().Axe==a)
            {
                MotorMoves.Last().Add(c, s);
            }
            else
            {
                MotorMoves.Add(motorMove);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() == this.GetType()) return false;

            return this == (MachineMoveList)obj;
        }

        public static bool operator ==(MachineMoveList m1, MachineMoveList m2)
        {
            if ((object)m1 == null && (object)m2 == null) return true;

            if ((object)m1 == null ^ (object)m2 == null) return false;

            if ((object)m1.MotorMoves == null ^ (object)m2.MotorMoves == null) return false;

            if ((object)m1.MotorMoves != null && (object)m2.MotorMoves != null)
            {
                if (m1.MotorMoves.Count != m2.MotorMoves.Count) return false;

                for (int i = 0; i < m1.MotorMoves.Count; i++)
                {
                    if (m1.MotorMoves[i] != m2.MotorMoves[i]) return false;
                }
            }

            if (m1.Count != m2.Count) return false;

            for (int i= 0;i< m1.Count; i++)
            {
                if(m1[i]!=m2[i]) return false;
            }


            return true;
        }

        public static bool operator !=(MachineMoveList m1, MachineMoveList m2)
        {
            return !(m1 == m2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    class MoveCalculator
    {
        // matrice de passe du repère cube au repère machine
        private Matrix4x4 _matrix = Matrix4x4.Identity;

        private MachineMoveList _machineMoves = new MachineMoveList();
        public MachineMoveList MachineMoves
        {
            get
            {
                return _machineMoves;
            }
        }

        public void AddCubeMove(Move cubeMove)
        {
            var machineMoveVector = Vector3.Transform( cubeMove.Vector, Matrix4x4.Transpose(_matrix));

            var axe = machineMoveVector.X != 0 ? Axe.X : (machineMoveVector.Y != 0 ? Axe.Y : Axe.Z);

            var couronne = (Couronne)(int)Vector3.Dot(machineMoveVector, Vector3.One);

            var oneTransform =  Vector3.Transform(Vector3.One, Matrix4x4.Transpose(_matrix));

            // l'axe de rotation dans le repère machine est orienté en indirect, il faut inverser le sens de rotation
            var sensRepereMachine = axe == Axe.X ? (Sens)oneTransform.X : (axe == Axe.Y ? (Sens)oneTransform.Y : (Sens)oneTransform.Z);
            if(sensRepereMachine == Sens.Negatif)
            {
                cubeMove = cubeMove.Inverse();
            }

            // si la couronne du bas est bougée (fixe par rapport au bati), bouger les 3 autres dans le sens inverse et mettre à jour la matrice
            if (couronne == Couronne.Min)
            {
                var InvertMove=  cubeMove.Inverse();
                _machineMoves.Add(axe, Couronne.Max, InvertMove.Sens);
                _machineMoves.Add(axe, Couronne.MidMax, InvertMove.Sens);
                _machineMoves.Add(axe, Couronne.MidMin, InvertMove.Sens);

                int s = (int)InvertMove.Sens;
                int ux = axe == Axe.X ? 1 : 0;
                int uy = axe == Axe.Y ? 1 : 0;
                int uz = axe == Axe.Z ? 1 : 0;
                var rotationMatrix = new Matrix4x4(ux, -uz * s, uy * s, 0, uz * s, uy, -ux * s, 0, -uy * s, ux * s, uz, 0, 0, 0, 0, 0);

                _matrix = rotationMatrix * _matrix;
            }
            else
            {
                _machineMoves.Add(axe, couronne, cubeMove.Sens);
            }
        }
    }
}
