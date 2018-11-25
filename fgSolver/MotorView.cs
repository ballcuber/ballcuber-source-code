using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fgSolver.Hardware;
using fgSolver.Modele;

namespace fgSolver
{
    public partial class MotorView : UserControl, INavigableForm
    {
        public MotorView()
        {
            InitializeComponent();

            radioButton3.Checked = true;
        }

        public string FormName
        {
            get
            {
                return "Moteurs";
            }
        }

        public Image Image
        {
            get
            {
                return global::fgSolver.Properties.Resources.stepper;
            }
        }

        public int Index
        {
            get
            {
                return 104;
            }
        }

        public void LeaveFrom()
        {
            

        }

        public void NavigueTo()
        {
           

        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            

        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;

            if (radio == null || !radio.Checked) return;

            var couronneName = radio.Tag as string;

            if (couronneName == null || couronneName.Length != 2) return;

            Axe a;
            if (!Enum.TryParse<Axe>(couronneName[0].ToString(), out a)) return;

            int ic;
            if (!int.TryParse(couronneName[1].ToString(),out ic)) return;

            Couronne c;

            switch (ic)
            {
                case 1:
                    c = Couronne.MidMin;
                    break;
                case 2:
                    c = Couronne.MidMax;
                    break;
                case 3:
                    c = Couronne.Max;
                    break;
                default:
                    return;
            }

            Motor m;
            using(var state = GlobalState.GetState())
            {
                m = state.Motors.Motors.FirstOrDefault((x) => x.Courronne == c && x.Axe == a);
            }
            SelectedMotor = m;
        }

        private Motor _selectedMotor;
        private Motor SelectedMotor
        {
            get
            {
                return _selectedMotor;
            }
            set
            {
                _selectedMotor = value;
                grid.SelectedObject = value;

                if (_selectedMotor == null)
                {
                    lblSelected.Text = "??";
                }
                else
                {
                    lblSelected.Text = _selectedMotor.ToString();
                }
            }
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            Runner.DisableMotor(_selectedMotor);
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            Runner.EnableMotor(_selectedMotor);
        }

        private void btnMoveSteps_Click(object sender, EventArgs e)
        {
            Runner.BeginMoveStep(_selectedMotor, (int)udSteps.Value);

        }

        private void btnSetSpeed_Click(object sender, EventArgs e)
        {
            Runner.SetSpeedMotor(_selectedMotor, (int)udSteps.Value);
        }

        private void grid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            using(var state = GlobalState.GetState())
            {
                state.SaveConfiguration();
            }
        }
    }
}
