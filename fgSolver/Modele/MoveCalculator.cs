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
    }

    class MachineMoveList : List<Move>
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
