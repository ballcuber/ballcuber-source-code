using fgSolver.Hardware;
using fgSolver.Modele;
using RevengeCube;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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

                    lst.Clear();

                    foreach (var elt in (IList)propValue)
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

    public class ColorAssociation : ApplicationState
    {
        [XmlIgnore()]
        public Color TheoreticalColor { get; set; }

        [XmlIgnore()]
        public Color RealColor { get; set; }

        public string TheoreticalColorName
        {
            get
            {
                return TheoreticalColor.R + ";" + TheoreticalColor.G + ";" + TheoreticalColor.B;
            }
            set
            {
                var strs = value.Split(new char[] { ';' });
                TheoreticalColor= Color.FromArgb(int.Parse(strs[0]), int.Parse(strs[1]), int.Parse(strs[2]));
            }
        }

        public string RealColorName
        {
            get
            {
                return RealColor.Name;
            }
            set
            {
                RealColor = Color.FromName(value);
            }
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


        public HardwareConfig HardwareConfig(HardwareConfigBoard index)
        {
            return index == HardwareConfigBoard.Board1 ? HardwareConfig1 : HardwareConfig2;
        }

        public HardwareConfigGlobal HardwareConfigGlobal { get; set; } = new HardwareConfigGlobal();
        public HardwareConfig HardwareConfig1 { get; set; } = new HardwareConfig(HardwareConfigBoard.Board1);
        public HardwareConfig HardwareConfig2 { get; set; } = new HardwareConfig(HardwareConfigBoard.Board2);
        public ServoConfig ServoConfig { get; set; } = new ServoConfig();

        public VideoParameters VideoParameters { get; set; } = new VideoParameters();

        private Solution _solution;


        public MotorList Motors { get; set; } = new MotorList();


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

        public List<ColorAssociation> ColorsAssociation { get; set; } = new List<ColorAssociation>();

        [XmlIgnore()]
        public bool SolutionInCalculation { get; set; } = false;

        // public ResolutionSession ResolutionSession { get; set; }
        [XmlIgnore()]
        public Scanner Scanner { get; set; } = new Scanner();

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
                this.ColorsAssociation = p.ColorsAssociation;
                this.HardwareConfigGlobal = p.HardwareConfigGlobal;
                this.ServoConfig = p.ServoConfig;
                this.VideoParameters = p.VideoParameters;
                if (p.Motors?.Motors == null || p.Motors.Motors.Count == 0 )
                {
                    this.Motors.Reset();
                }
                else { 
                    this.Motors = p.Motors;
                }
            }
        }
    }

    public class VideoParameters : ApplicationState
    {
        public enum SelectedImage
        {
            InImage,
            //MedianBlurIn,
            InImageB,
            InImageG,
            InImageR,
            InImageSum,
            threshold,
            Canny,
            approxContour,

            [Browsable(false)]
            Count,
            [Browsable(false)]
            Last = Count - 1,
        }


            public int CaptureID { get; set; } = 0;

            public Point[] Points { get; set; } = new Point[6];

            public Point Center { get; set; }

            public int PerpectiveSize { get; set; } = 100;

            public int SquareDistance { get; set; } = 5;

            public SelectedImage DebugImage { get; set; } = SelectedImage.Last;

            public int CannyThreshold1 { get; set; } = 20;
            public int CannyThreshold2 { get; set; } = 20;

            public int MedianBlurSize { get; set; } = 11;
            public double ContourEpsilon { get; set; } = 50;
            public double Threshold { get; set; } = 120;
 
    }

    public class HardwareConfigGlobal : ApplicationState
    {
        [Category("Paramètre")]
        public int MotorTeeth { get; set; } // nombre de dents sur le pignon moteur

        [Category("Paramètre")]
        public int BallTeeth { get; set; } // nombre de dent sur le pignon de la sphère

        [Category("Paramètre")]
        public int StepPersRevolution { get; set; } // nombre de pas par tour du moteur

        [Category("Paramètre")]
        public int Speed { get; set;}

        [Category("Paramètre")]
        public int Acceleration { get; set; }

        // retourne le nombre de pas nécessaire pour faire quarters quart de tour de sphère
        public int ConvertMoveToSteps(int quarters, bool inverted)
        {
            if (MotorTeeth == 0) return 0;

            if (inverted) quarters = -quarters;

            return quarters * StepPersRevolution / 4 * BallTeeth / MotorTeeth;
        }

    }

    public class ServoConfig : ApplicationState
    {
        public int OpenedPosition0 { get; set; }
        public int LockedPosition0 { get; set; }

        public int OpenedPosition1 { get; set; }
        public int LockedPosition1 { get; set; }

        public int OpenedPosition2 { get; set; }
        public int LockedPosition2 { get; set; }

        public int OpenedPosition3 { get; set; }
        public int LockedPosition3 { get; set; }


    }

    public class Motor : ApplicationState
    {
        [ReadOnly(true)]
        public Axe Axe { get; set; }

        [ReadOnly(true)]
        public Couronne Courronne { get; set; }

        public RampsSteppers Stepper { get; set; }


        public bool Inverted { get; set; } // sens inverse

        // index dans le tableau interne au code arduino
        public byte Index
        {
            get
            {
                return (byte)Stepper;
            }
        }

        // mask ne représentant que ce moteur
        public int Mask
        {
            get
            {
                return 1<< Index;
            }
        }

        public HardwareConfigBoard Board { get; set; }

        public override string ToString()
        {
            return Axe + " " + Courronne + " " + Stepper;
        }
    }

    public enum HardwareConfigBoard
    {
        Board1,
        Board2
    }

    public class HardwareConfig : ApplicationState
    {
        public HardwareConfig() { }

       public HardwareConfig(HardwareConfigBoard index) { this.Index = index; }

        public HardwareConfigBoard Index { get; set; }

        public string COMPort { get; set; }

        public int BaudRate { get; set; }

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

        public bool HasServo { get;set; }

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


    public class Scanner : ApplicationState
    {
        public static Faces FirstScannedFace = Faces.L;

        // face en cours de scan
        public Faces CurrentScannedFace { get; set; } = FirstScannedFace;

        // la face CurrentScannedFace est en attente d'apparition devant la caméra
        public bool WaitingForFace { get; set; } = true;


        public bool Starting
        {
            get
            {
                return CurrentScannedFace == FirstScannedFace && WaitingForFace;
            }
        }

        public void Reset()
        {
            CurrentScannedFace = FirstScannedFace;
            WaitingForFace = true;
        }
        /*
        private const string WAIT_TEXT = "En attente du côté ";

        public string GetStepText()
        {
            switch (Step)
            {
                case ScanStep.WaitFace:
                    return WAIT_TEXT + "Face";
                    break;

                default:
                    return "aa";
            }
        }*/
    }

    public class MotorList : ApplicationState
    {
        public List<Motor> Motors { get; set; } = new List<Motor>();

        public MotorList()
        {

        }

        public void Reset()
        {
            Motors.Clear();
            int i = 0;
            for (Axe a = Axe.X; a < Axe.NUM; a++)
            {
                foreach (var c in Enum.GetValues(typeof(Couronne)).OfType<Couronne>().Except(new Couronne[] { Couronne.Min, Couronne.UNKNOWN }))
                {
                    var m = new Motor();
                    m.Axe = a;
                    m.Courronne = c;
                    Motors.Add(m);
                    i++;
                }
            }
        }
    }
}