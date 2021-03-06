﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RevengeCube;

namespace Ballcuber
{
    public partial class ParametreControl : UserControl, INavigableForm
    {
        public ParametreControl()
        {
            InitializeComponent();
        }

        public string FormName
        {
            get
            {
                return "Paramètres";
            }
        }

        public Image Image
        {
            get
            {
                return Properties.Resources.Parameters;
            }
        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
        }


        public int Index
        {
            get
            {
                return 100;
            }
        }


        public void NavigueTo() {
            using(var state = GlobalState.GetState())
            {
                pgHard1.SelectedObject = state.HardwareConfig1.Clone();
                pgHard2.SelectedObject = state.HardwareConfig2.Clone();
                pgGlobal.SelectedObject = state.HardwareConfigGlobal.Clone();
            }
        }

         private void Save()
        {
            using (var state = GlobalState.GetState())
            {
                state.HardwareConfig1 = (HardwareConfig)pgHard1.SelectedObject;
                state.HardwareConfig2 = (HardwareConfig)pgHard2.SelectedObject;
                state.HardwareConfigGlobal = (HardwareConfigGlobal)pgGlobal.SelectedObject;
                state.SaveConfiguration();
            }
        }

        public void LeaveFrom() { }

        private void PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Save();
        }
    }
}
