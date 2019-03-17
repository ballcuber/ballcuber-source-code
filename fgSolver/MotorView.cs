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
            if (_selectedMotor != null)
            {
                txtPosition.Text = _selectedMotor.Position.ToString();
                ledEnabled.On = _selectedMotor.Enabled;
            }
            else
            {
                txtPosition.Text = "---";
                ledEnabled.On = false;
            }

            foreach(var led in this.Controls.OfType<Bulb.LedBulb>())
            {
                var m = GetByTag(currentState, led.Tag);

                if (m != null) led.On = m.Enabled;
            }

        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;

            if (radio == null || !radio.Checked) return;
            
            using(var state = GlobalState.GetState())
            {
                SelectedMotor = GetByTag(state, radio.Tag); ;
            }
        }

        private Motor GetByTag( GlobalState state, object tag)
        {
            var couronneName = tag as string;

            Motor m;

            if (couronneName == null || couronneName.Length != 2) return null;

            Axe a;
            if (!Enum.TryParse<Axe>(couronneName[0].ToString(), out a)) return null;

            int ic;
            if (!int.TryParse(couronneName[1].ToString(), out ic)) return null;

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
                    return null;
            }

            return state.Motors.Motors.FirstOrDefault((x) => x.Courronne == c && x.Axe == a);
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
            Runner.SetSpeedMotor(_selectedMotor, (int)udSpeed.Value);
        }

        private void grid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            using(var state = GlobalState.GetState())
            {
                state.SaveConfiguration();
            }
        }

        private void btnMoveAbsolute_Click(object sender, EventArgs e)
        {
            Runner.BeginMoveAbsolute(_selectedMotor, (int)udAbsolute.Value);
        }

        private void btnSetPosition_Click(object sender, EventArgs e)
        {
            Runner.SetCurrentPosition(_selectedMotor, (int)udPosition.Value);
        }

        private void btnEnableAll_Click(object sender, EventArgs e)
        {
            Runner.EnableMotorAll();
        }

        private void btnDisableAll_Click(object sender, EventArgs e)
        {
            Runner.DisableMotorAll();
        }

        private void btnSetSpeedAll_Click(object sender, EventArgs e)
        {
            Runner.SetSpeedAll((int)udSetSpeedAll.Value);
        }

        private void btnSetPositionAll_Click(object sender, EventArgs e)
        {
            Runner.SetCurrentPositionAll((int)udSetPositionAll.Value);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Runner.Stop(_selectedMotor);
        }

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            Runner.StopAll();
        }

        private void btnMoveMin_Click(object sender, EventArgs e)
        {
            Runner.MoveToKnownPosition(_selectedMotor.Axe, _selectedMotor.Courronne, KnownPosition.MinStop);
        }

        private void btnMoveMiddle_Click(object sender, EventArgs e)
        {
            Runner.MoveToKnownPosition(_selectedMotor.Axe, _selectedMotor.Courronne, KnownPosition.Middle);
        }

        private void btnMoveMaxStop_Click(object sender, EventArgs e)
        {
            Runner.MoveToKnownPosition(_selectedMotor.Axe, _selectedMotor.Courronne, KnownPosition.MaxStop);
        }

        private void btnMoveNegativeQuarter_Click(object sender, EventArgs e)
        {
            Runner.MoveToKnownPosition(_selectedMotor.Axe, _selectedMotor.Courronne, KnownPosition.NegativeQuarter);
        }

        private void btnMovePositiveQuarter_Click(object sender, EventArgs e)
        {
            Runner.MoveToKnownPosition(_selectedMotor.Axe, _selectedMotor.Courronne, KnownPosition.PositiveQuarter);
        }

        private void btnMoveAllMinStop_Click(object sender, EventArgs e)
        {
            Runner.MoveAllToKnownPosition( KnownPosition.MinStop);
        }

        private void btnMoveAllMiddle_Click(object sender, EventArgs e)
        {
            Runner.MoveAllToKnownPosition(KnownPosition.Middle);
        }

        private void btnMoveAllMaxStop_Click(object sender, EventArgs e)
        {
            Runner.MoveAllToKnownPosition(KnownPosition.MaxStop);
        }

        private void btnSetPosMinStop_Click(object sender, EventArgs e)
        {
            Runner.SetCurrentPosition(_selectedMotor, 0);
        }


        private void btnSetPosMiddle_Click(object sender, EventArgs e)
        {

            Runner.SetCurrentPosition(_selectedMotor, _selectedMotor.StepsToPositivePosition/2);
        }

        private void btnSetPosMaxStop_Click(object sender, EventArgs e)
        {
            Runner.SetCurrentPosition(_selectedMotor, _selectedMotor.StepsToPositivePosition );
        }

        private void btnSetAllPosMiddle_Click(object sender, EventArgs e)
        {
            using (var state = GlobalState.GetState())
            {
                foreach (var m in state.Motors.Motors)
                {
                    Runner.SetCurrentPosition(m, m.StepsToPositivePosition/2);
                }
            }
        }

        private void btnSetAllPosMinStop_Click(object sender, EventArgs e)
        {
            using (var state = GlobalState.GetState())
            {
                foreach (var m in state.Motors.Motors)
                {
                    Runner.SetCurrentPosition(m, 0);
                }
            }
        }

        private void btnSetAllPosMaxStop_Click(object sender, EventArgs e)
        {
            using (var state = GlobalState.GetState())
            {
                foreach (var m in state.Motors.Motors)
                {
                    Runner.SetCurrentPosition(m, m.StepsToPositivePosition);
                }
            }
        }

        private void btnMoveAll_Click(object sender, EventArgs e)
        {
            using (var state = GlobalState.GetState())
            {
                foreach (var m in state.Motors.Motors)
                {
                    Runner.BeginMoveStep(m, (int)udMoveAll.Value);
                }
            }
        }

        private void setAccelAll_Click(object sender, EventArgs e)
        {
            Runner.SetAccelerationAll((int)udAccel.Value);
        }
    }
}
