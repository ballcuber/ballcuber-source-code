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
using Ballcuber.Hardware;

namespace Ballcuber
{
    public partial class AlarmControl : UserControl, INavigableForm
    {
        public AlarmControl()
        {
            InitializeComponent();
        }

        public string FormName
        {
            get
            {
                return "Alarmes actives";
            }
        }

        public Image Image
        {
            get
            {
                return Properties.Resources.Warning;
            }
        }

        public int Index
        {
            get
            {
                return 200;
            }
        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            if(lstAlarms.Items.Count != currentState.Alarms.Count)
            {
                lstAlarms.Items.Clear();

                var itms = currentState.Alarms.Reverse<Alarm>().Select((x) => new ListViewItem(new string[] { x.Message, x.AdditionnalInfo })).ToArray();

                lstAlarms.Items.AddRange(itms);
                
            }
        }


        public void NavigueTo() { }
        public void LeaveFrom() { }

        private void btnAcq_Click(object sender, EventArgs e)
        {
            using(var state = GlobalState.GetState())
            {
                state.Alarms.Clear();
            }

            FormManager.Previous();
        }
    }
}
