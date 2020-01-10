using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver
{
    public sealed class FormManager
    {
        public static Dictionary<Type, INavigableForm> Forms = new Dictionary<Type, INavigableForm>();

        static FormManager()
        {
            // scan forms
            foreach(var type in typeof(FormManager).Assembly.GetTypes())
            {
                if (!type.IsAbstract && typeof(INavigableForm).IsAssignableFrom(type))
                {
                    var form = (INavigableForm)Activator.CreateInstance(type);
                    Forms[type] = form;
                }
            }
        }

        public static void Init()
        {

        }

        public static void Navigate<T>() where T: INavigableForm
        {
            INavigableForm form;
            if(Forms.TryGetValue(typeof(T), out form))
            {
                Navigate(form);
            }
            else
            {
                Logger.Log("Impossible de naviguer vers la fenêtre de type " + typeof(T).Name);
            }
        }

        public static void Navigate(INavigableForm form)
        {
            internalNavigate(form);

            lock (_history)
            {
                _history[_indexNext] = form;
                _indexNext++;

                for(int i= _indexNext;i< _history.Count; i++)
                {
                    _history.Remove(_indexNext);
                }
            }

        }


        private static void internalNavigate(INavigableForm form)
        { 
            MainForm.Instance.Navigate(form);
        }


        private static Dictionary<int, INavigableForm> _history = new Dictionary<int, INavigableForm>();

        private static int _indexNext = 0;


        public static bool Previous()
        {
            lock (_history)
            {
                if (_indexNext <= 1 || !_history.ContainsKey(_indexNext-2)) return false;

                _indexNext--;
                internalNavigate(_history[_indexNext-1]);

                return true;
            }
        }

        public static bool Next()
        {
            lock (_history)
            {
            if (_indexNext >= _history.Count || !_history.ContainsKey(_indexNext)) return false;

            _indexNext++;
            internalNavigate(_history[_indexNext-1]);

            return true;
            }
        }
    }
}
