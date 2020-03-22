using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.ComponentModel;

namespace Ballcuber.Modele
{
    public enum Axe : int
    {
        X=0,
        Y=1,
        Z=2,

        [Browsable(false)]
        NUM
    }

    public enum Couronne : int
    {
        [Browsable(false)]
        UNKNOWN = 0, // valeur par défaut pour la serialization

        Max = 2,
        MidMax = Max/2,
        MidMin = -MidMax,
        Min = -Max,
    }

    public enum Sens : int
    {
        Positif = 1,
        Negatif = -1
    }

    public struct Move
    {
        private Axe _axe;
        public Axe Axe
        {
            get
            {
                return _axe;
            }
        }

        private Couronne _couronne;
        public Couronne Couronne
        {
            get
            {
                return _couronne;
            }
        }

        private Sens _sens;
        public Sens Sens
        {
            get
            {
                return _sens;
            }
        }

        private Vector3 _vector;
        public Vector3 Vector
        {
            get
            {
                return _vector;
            }
        }

        public Move(Axe a, Couronne c, Sens s)
        {
            _axe = a;
            _couronne = c;
            _sens = s;

            _vector = Vector3.Zero;

            switch (a)
            {
                case Axe.X:
                    _vector.X = (int)c;
                    break;
                case Axe.Y:
                    _vector.Y = (int)c;
                    break;
                case Axe.Z:
                    _vector.Z = (int)c;
                    break;
            }


        }

        public Move Inverse()
        {
            return new Move(_axe,_couronne, (Sens)(-(int)_sens));
        }

        public override string ToString()
        {
            return (int)_couronne + _axe.ToString() + " " + (_sens == Sens.Positif ? "+" : "-");
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() == this.GetType()) return false;

            return this == (Move)obj;
        }

        public static bool operator ==(Move m1, Move m2)
        {
            return m1.Axe == m2.Axe && m1.Couronne == m2.Couronne && m1.Sens == m2.Sens;
        }

        public static bool operator !=(Move m1, Move m2)
        {
            return !(m1 == m2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
