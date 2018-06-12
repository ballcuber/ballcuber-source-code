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

namespace fgSolver
{
    public partial class BoardsControl : UserControl, INavigableForm
    {
        public BoardsControl()
        {
            InitializeComponent();
        }

        public string FormName
        {
            get
            {
                return "Cartes de pilotage";
            }
        }

        public Image Image
        {
            get
            {
                return Properties.Resources.Board;
            }
        }

        public int Index
        {
            get
            {
                return 101;
            }
        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            boardControl1.PeriodicUpdate(formerState, currentState);
            boardControl2.PeriodicUpdate(formerState, currentState);
        }

        public void NavigueTo() { }

        public void LeaveFrom() { }

    }
}
