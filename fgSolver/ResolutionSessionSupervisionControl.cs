using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fgSolver.Modele;
using System.Reflection;

namespace fgSolver
{
    public partial class ResolutionSessionSupervisionControl : UserControl
    {
        public ResolutionSessionSupervisionControl()
        {
            InitializeComponent();

             typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(lst, new object[] { ControlStyles.OptimizedDoubleBuffer, true });
         }

        private int _lastSignature = -1;
        public void PeriodicUpdate(GlobalState currentState)
        {
            var status = currentState.ResolutionSessionStatus;

            if (status.Signature == _lastSignature) return;
            _lastSignature = status.Signature;

            lblState.Text = status.State.ToString();

            ListViewItem itmToTrack = null;

            int selectedId = -1;
            if (lst.SelectedIndices.Count == 1) selectedId = lst.SelectedIndices[0];

            int bottomItemIndex = -1;
            if (lst.Items.Count > 0)
            {
                bottomItemIndex = 0;
                while (bottomItemIndex < lst.Items.Count && lst.Items[bottomItemIndex].Position.Y < lst.ClientSize.Height - 20)
                    bottomItemIndex++;
            }

            try
            {
                lst.BeginUpdate();


                lst.Items.Clear();
                lst.Groups.Clear();

                var curGrp = new ListViewGroup("");
                lst.Groups.Add(curGrp);


                for (int i = 0; i < status.Instructions.Length; i++)
                {
                    var instr = status.Instructions[i];

                    if (instr.GetType() == typeof(BeginGroupeInstruction))
                    {
                        curGrp = lst.Groups.Add(Guid.NewGuid().ToString(), instr.ToString());
                    }
                    else
                    {
                        var itm = lst.Items.Add(instr.ToString());
                        itm.Group = curGrp;
                        itm.Tag = i;
                        if (status.CurrentInstructionID == i)
                        {
                            itm.ImageIndex = instr.Breakpoint ? 2 : 1;
                            if (chkFollow.Checked) itmToTrack = itm;
                        }
                        else
                        {
                            itm.ImageIndex = instr.Breakpoint ? 0 : -1;
                        }

                        if (instr.IsPseudoInstruction)
                        {
                            itm.Font = new Font(itm.Font, FontStyle.Italic);
                        }
                    }
                }

                if (itmToTrack == null && bottomItemIndex < lst.Items.Count && bottomItemIndex >= 0)
                {
                    itmToTrack = lst.Items[bottomItemIndex];
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            finally
            {
                lst.EndUpdate();
               if (selectedId >= 0 && selectedId < lst.Items.Count)
                {
                    lst.Items[selectedId].Selected = true;
                }

                if (itmToTrack != null)
                {
                    itmToTrack.Selected = chkFollow.Checked;
                    itmToTrack.EnsureVisible();
                }


            }
        }



        private void btnTest_Click(object sender, EventArgs e)
        {
            ResolutionSession.Clear();
            ResolutionSession.Add(new CommentInstruction("Coucou"));
            ResolutionSession.Add(new WaitInstruction(2));
            ResolutionSession.Add(new WaitInstruction(3));
            ResolutionSession.Add(new BeginGroupeInstruction("Test1"));
            ResolutionSession.Add(new WaitInstruction(2));
            ResolutionSession.Add(new WaitInstruction(3));
            ResolutionSession.Add(new BeginGroupeInstruction("Test2"));
            ResolutionSession.Add(new WaitInstruction(2));
            ResolutionSession.Add(new CommentInstruction("Blabla u gui g jkfg yi fitf iyt u"));
            ResolutionSession.Add(new WaitInstruction(3));
            for(int i = 0; i < 100; i++)
            {
                ResolutionSession.Add(new WaitInstruction(0.1));
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ResolutionSession.Run();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ResolutionSession.Next();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            ResolutionSession.Abort();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            ResolutionSession.Pause();
        }

        private void btnSetStep_Click(object sender, EventArgs e)
        {
            if (lst.SelectedItems.Count != 1) return;

            ResolutionSession.SetExecutionPointer((int)lst.SelectedItems[0].Tag);
        }

        private void lst_MouseLeave(object sender, EventArgs e)
        {
            lst.Cursor = Cursors.Default;
        }

        private void lst_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 20)
            {
                lst.Cursor = Cursors.No;
            }
            else
            {
                lst.Cursor = Cursors.Default;
            }
        }

        private void lst_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X < 20)
            {
                var itm = lst.GetItemAt(e.X, e.Y);

                if (itm != null)
                {
                    ResolutionSession.SetBreakpoint((int)itm.Tag);
                }
            }
            
        }

        private void chkFollow_CheckedChanged(object sender, EventArgs e)
        {
            _lastSignature = 0;
        }

        // l'édition permet le copier/coller
        private void lst_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            e.CancelEdit = true;
        }
    }
}
