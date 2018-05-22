﻿using fgSolver.Hardware;
using fgSolver.Modele;
using RevengeCube;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace fgSolver
{


    public abstract class ApplicationState : ICloneable
    {
        public virtual object Clone()
        {
            return Clone(this);
        }

        public static object Clone(object obj)
        {
            if (obj == null) return null;

            var newObj = Activator.CreateInstance(obj.GetType());

            foreach (var prop in newObj.GetType().GetProperties().Where((x) => x.CanWrite && x.CanRead))
            {
                var propValue = prop.GetValue(obj);

                if (propValue == null) continue;

                if (typeof(ICloneable).IsAssignableFrom(prop.PropertyType))
                {
                    prop.SetValue(newObj, ((ICloneable)propValue).Clone());
                }
                else if (prop.PropertyType.IsValueType)
                {
                    prop.SetValue(newObj, propValue);
                }
                else if (typeof(IList).IsAssignableFrom(prop.PropertyType))
                {
                    var lst = (IList)Activator.CreateInstance(prop.PropertyType);

                    foreach(var elt in (IList)propValue)
                    {
                        lst.Add(Clone(elt));
                    }

                    prop.SetValue(newObj, lst);
                }
                else
                {
                    throw new Exception("La propriété " + prop.Name + " de la classe " + obj.GetType().Name + " n'est pas clonable. Toutes les propriétés de l'objet état doivent être clonable afin d'en avoir une copie");
                }
            }

            return newObj;
        }

        // par défaut il y a égalité si toutes les propriétés sont égales
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != this.GetType()) return false;

            foreach (var prop in obj.GetType().GetProperties().Where((x) => x.CanRead))
            {
                var objValue = prop.GetValue(obj);
                var value = prop.GetValue(this);

                if (objValue == null && value == null)
                {
                    // ok equals
                }
                else if (objValue == null && value != null || objValue != null && value == null || !objValue.Equals(value))
                {
                    return false;
                }

            }

            return true;
        }

        // avoid warning
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


    public class GlobalState : ApplicationState,IDisposable
    {
        private static GlobalState _state = new GlobalState();

        private static object _lock = new object();
            public static GlobalState GetState()
        {
            Monitor.Enter(_lock);
            return _state;
        }
        
        public void Dispose()
        {
            Monitor.Exit(_lock);
        }

        public GlobalState CloneState()
        {
            return (GlobalState)Clone();
        }

        private ColorCube _initialCube = new ColorCube();
        [XmlIgnore()]
        public ColorCube InitialCube
        {
            get
            {
                return _initialCube;
            }
            set
            {
                _initialCube = value;
                Solution = null;
            }
        }


        public HardwareConfig HardwareConfig(int index)
        {
            return index < 2 ? HardwareConfig1 : HardwareConfig2;
        }

        public HardwareConfig HardwareConfig1 { get; set; } = new HardwareConfig(1);
        public HardwareConfig HardwareConfig2 { get; set; } = new HardwareConfig(2);

        private Solution _solution;

        [XmlIgnore()]
        public Solution Solution
        {
            get
            {
                return _solution;
            }
            set
            {
                _solution = value;
                //ResolutionSession = null;
            }
        }

        [XmlIgnore()]
        public List<Alarm> Alarms { get; set; } = new List<Alarm>();



        [XmlIgnore()]
        public bool SolutionInCalculation { get; set; } = false;

       // public ResolutionSession ResolutionSession { get; set; }


        public void SaveConfiguration()
        {
            XmlSerializer xs = new XmlSerializer(typeof(GlobalState));
            using (StreamWriter wr = new StreamWriter("GlobalState.xml"))
            {
                xs.Serialize(wr, this);
            }
        }

        public void ImportConfiguration()
        {
            XmlSerializer xs = new XmlSerializer(typeof(GlobalState));

            using (StreamReader rd = new StreamReader("GlobalState.xml"))
            {
                GlobalState p = xs.Deserialize(rd) as GlobalState;
                this.HardwareConfig1 = p.HardwareConfig1;
                this.HardwareConfig2 = p.HardwareConfig2;
            }
        }
    }

    public class Motor : ApplicationState
    {
        [Category("Identification")]
        public Axe Axe { get; set; }

        [Category("Identification")]
        public Couronne Courronne { get; set; }

        [Category("Identification")]
        public RampsSteppers Stepper { get; set; }

        [Category("Paramètre")]
        public int MotorTeeth { get; set; } // nombre de dents sur le pignon moteur

        [Category("Paramètre")]
        public int BallTeeth { get; set; } // nombre de dent sur le pignon de la sphère

        [Category("Paramètre")]
        public int StepPersRevolution { get; set; } // nombre de pas par tour du moteur

        [Category("Paramètre")]
        public bool Inverted { get; set; } // sens inverse

        [Category("Paramètre")]
        // index dans le tableau interne au code arduino
        public byte Index
        {
            get
            {
                return (byte)Stepper;
            }
        }

        // retourne le nombre de pas nécessaire pour faire quarters quart de tour de sphère
        public int ConvertMoveToSteps(int quarters)
        {
            if (MotorTeeth == 0) return 0;

            if (Inverted) quarters = -quarters;

            return quarters * StepPersRevolution / 4 * BallTeeth / MotorTeeth;
        }

        public override string ToString()
        {
            return Axe + " " + Courronne + " " + Stepper;
        }
    }

    public class HardwareConfig : ApplicationState
    {
        public HardwareConfig() { }

       public HardwareConfig(int index) { this.Index = index; }

        public int Index { get; set; }

        public string COMPort { get; set; }

        public int BaudRate { get; set; }

        public List<Motor> Motors { get; set; } = new List<Motor>();

        public bool Simulated { get; set; }

        [Description("Temps en simulation pour 1/4 tour en ms")]
        public int SimulatedTime { get; set; }

        public bool Connected
        {
            get
            {
                return Runner.GetBoard(Index).Connected;
            }
        }

        public HardwareConfig CloneHardwareConfig()
        {
            return (HardwareConfig)Clone();
        }
    }

    public class ResolutionSession : ApplicationState
    {
        private IReadOnlyList<MotorMove> _motorMoves;
        public IReadOnlyList<MotorMove> MotorMoves
        {
            get
            {
                return _motorMoves;
            }
            set
            {
                _motorMoves = value;
                LastExecutedStep = -1;
            }
        }

        public bool IsRunning
        {
            get
            {
                return LastExecutedStep < 0;
            }
        }

        public int LastExecutedStep { get; set; }


        public override object Clone()
        {
            var res = new ResolutionSession();
            var lst = new List<MotorMove>();

            foreach(var mv in this.MotorMoves)
            {
                var newMv = new MotorMove(mv.Axe);
                newMv.MaxMovesCount = mv.MaxMovesCount;
                newMv.MidMaxMovesCount = mv.MidMaxMovesCount;
                newMv.MidMinMovesCount = mv.MidMinMovesCount;
                lst.Add(newMv);
            }

            res.MotorMoves = lst;
            res.LastExecutedStep = LastExecutedStep;

            return res;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (!(obj is ResolutionSession)) return false;

            var res = (ResolutionSession)obj;

            if (res.MotorMoves?.Count != MotorMoves?.Count) return false;

            for(int i = 0; i < MotorMoves.Count; i++)
            {
                if (MotorMoves[i] != res.MotorMoves[i]) return false;
            }

            return res.LastExecutedStep == LastExecutedStep;
        }

        
    }

}