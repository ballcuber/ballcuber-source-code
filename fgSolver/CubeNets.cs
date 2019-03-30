using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RevengeCube;
using fgSolver.Modele;

namespace fgSolver
{
    public partial class CubeNets : UserControl
    {
        private List<CubePartControl> _parts = new List<CubePartControl>();

        private IEnumerable<Button> _ctrlButtons;
        private IEnumerable<Label> _ctrlLabels;


        public CubeNets()
        {
            InitializeComponent();

            _ctrlButtons = tableCube.Controls.OfType<Button>().ToList();
            _ctrlLabels = tableCube.Controls.OfType<Label>().ToList();


            CreateFacePart(4, 0);
            CreateFacePart(0, 4);
            CreateFacePart(4, 4);
            CreateFacePart(8, 4);
            CreateFacePart(12, 4);
            CreateFacePart(4, 8);

            Clickable = true;

        }

        public delegate void MoveClickEventHandler(object o, MoveClickEventArgs e);

        public event MoveClickEventHandler MoveClick;

        private void moveClick(object sender, EventArgs e)
        {
            var txt = (sender as Button)?.Tag as string;

            if (txt?.Length != 3) return; // le tag du bouton doit être de la forme X2+ (X:axe, 2:nieme courronne, +:sens)


            Axe axe;

            if (!Enum.TryParse<Axe>(txt[0].ToString(), out axe)) return;

            var mv = new MotorMove(axe);

            int sens;

            switch (txt[2])
            {
                case '+':
                    sens = 1;
                    break;
                case '-':
                    sens = -1;
                    break;
                default:
                    return;
            }

            switch (txt[1])
            {
                case '1':
                    mv.MaxMovesCount = sens;
                    break;
                case '2':
                    mv.MidMaxMovesCount = sens;
                    break;
                case '3':
                    mv.MidMinMovesCount = sens;
                    break;
                default:
                    return;
            }

            System.Threading.Tasks.Task.Factory.StartNew(new Action(() =>
            {
                if (MoveClick != null) MoveClick(sender, new MoveClickEventArgs(mv));
            }));
        }



        private bool _Clickable;
        [DefaultValue(true)]
        public bool Clickable
        {
            get
            {
                return _Clickable;
            }
            set
            {
                foreach (var part in _parts)
                {
                    part.Clickable = value;
                }
                _Clickable = value;
            }
        }

        private bool _showControlButtons = true;
        [DefaultValue(true)]
        public bool ShowControlButtons
        {
            get
            {
                return _showControlButtons;
            }
            set
            {
                foreach (var btn in _ctrlButtons)
                {
                    btn.Visible = value;
                }
                foreach (var lbl in _ctrlLabels)
                {
                    lbl.Visible = value;
                }
                _showControlButtons = value;
            }
        }



    private void CreateFacePart(int column, int row)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var part = new CubePartControl(_parts.Count);
                tableCube.Controls.Add(part, column+j, row+i);
                part.Dock = DockStyle.Fill;
                part.Margin = new Padding(0);
                _parts.Add(part);
            }
        }
    }

        public void PeriodicUpdate(ColorCube cube)
        {
            foreach (var part in _parts)
            {
                part.PeriodicUpdate(cube);
            }
        }
    }

    public class MoveClickEventArgs : EventArgs
    {
        private MotorMove _motorMove;
        public MotorMove MotorMove
        {
            get
            {
                return _motorMove;
            }
        }

        public MoveClickEventArgs(MotorMove mv)
        {
            _motorMove = mv;
        }
    }
}
