using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ballcuber
{
    public interface INavigableForm: IContainerControl
    {
        string FormName { get; }

        Image Image { get; }

        void PeriodicUpdate(GlobalState formerState, GlobalState currentState);

        // se produit à l'affichage de la page
        void NavigueTo();

        // se produit lorsque la page est quitée
        void LeaveFrom();

        // Index dans la liste en haut
        int Index { get; }
    }
}
