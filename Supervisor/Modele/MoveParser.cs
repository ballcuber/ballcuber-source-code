using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver.Modele
{
    class MoveParser
    {
        public static List<Move> Parse(string expr)
        {
            var moves = new List<Move>();

            if (expr == "OK") return moves; // cube déjà résolu

           var exploadedExpr = expr.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);


            foreach(string moveExpr in exploadedExpr)
            {
                var isUTurn = moveExpr.Contains("2");
                var isDoubleCrown = moveExpr.Contains("w");
                var isInverse = moveExpr.Contains("'");

                Move move;

                switch (moveExpr[0])
                {
                    case 'U':
                        move = new Move(Axe.Z, Couronne.Max, Sens.Negatif);
                        break;
                    case 'D':
                        move = new Move(Axe.Z, Couronne.Min, Sens.Positif);
                        break;
                    case 'L':
                        move = new Move(Axe.Y, Couronne.Max, Sens.Negatif);
                        break;
                    case 'R':
                        move = new Move(Axe.Y, Couronne.Min, Sens.Positif);
                        break;
                    case 'F':
                        move = new Move(Axe.X, Couronne.Min, Sens.Positif);
                        break;
                    case 'B':
                        move = new Move(Axe.X, Couronne.Max, Sens.Negatif);
                        break;
                    case 'x':
                    case 'y':
                    case 'z':
                        continue;
                    default:
                        throw new Exception("Movement " + moveExpr + " inconnu");
                }

                if (isInverse)
                {
                    move = move.Inverse();
                }

                moves.Add(move);

                if (isUTurn)
                {
                    moves.Add(move);
                }

                if (isDoubleCrown)
                {
                    var secondMove = new Move(move.Axe,(Couronne)((int)move.Couronne / 2), move.Sens);
                    moves.Add(secondMove);

                    if (isUTurn)
                    {
                        moves.Add(secondMove);
                    }
                }

            }

            return moves;
        }
    }
}
