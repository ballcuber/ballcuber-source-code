using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharer;
using fgSolver.Modele;
using Sharer.Command;

namespace fgSolver.Hardware
{
    public enum RampsSteppers
    {
        E0 = 0,
        E1,
        X,
        Y,
        Z
    }


    public class Board
    {
        private SharerConnection _connection;

        private int _index;
        public int Index
        {
            get
            {
                return _index;
            }
        } 

        public HardwareConfig GetHardwareConfig()
        {
            using (var state = GlobalState.GetState())
            {
                return state.HardwareConfig(_index).CloneHardwareConfig();
            }
        }


        public Board(int index)
        {
            _index = index;
        }

        public void Connect()
        {
            if (_connection!=null && _connection.Connected) return;

            var config = GetHardwareConfig();

            if (config.Simulated) return;
            _connection = new SharerConnection(config.COMPort, config.BaudRate);

            _connection.Connect();

            _connection.RefreshFunctions();
        }

        public bool Connected
        {
            get
            {
                return _connection != null && _connection.Connected;
            }
        }

        public void Disconnect()
        {
            _connection?.Disconnect();
        }

        public void AssertConnectedAndConfigured()
        {
            AssertConnectedAndConfigured(GetHardwareConfig());
        }

        public void AssertConnectedAndConfigured(HardwareConfig config)
        {
            if (config.Simulated) throw new Exception("Carte simulée");

            if (!Connected) throw new Exception("Carte non connectée");

            if (config.Motors == null || config.Motors.Count == 0) throw new Exception("Aucun moteur déclaré");
        }


        public void BlockingMove(MotorMove mv, TimeSpan timeout)
        {
            var config = GetHardwareConfig();

            if (config.Simulated)
            {
                System.Threading.Thread.Sleep(mv.QuarterNumber * GetHardwareConfig().SimulatedTime);
                return;
            }

            byte m1 = 0xFF;
            byte m2 = 0xFF;
            byte m3 = 0xFF;

            int r1 = 0;
            int r2 = 0;
            int r3 = 0;

            foreach(var motor in config.Motors.Where((x) => x.Axe == mv.Axe))
            {
                if (motor.Courronne == Couronne.MidMin && mv.MidMinMovesCount != 0)
                {
                    m1 = motor.Index;
                    r1 = motor.ConvertMoveToSteps(mv.MidMinMovesCount);
                }
                else if (motor.Courronne == Couronne.MidMax && mv.MidMaxMovesCount != 0)
                {
                    m2 = motor.Index;
                    r2 = motor.ConvertMoveToSteps(mv.MidMaxMovesCount);
                }
                else if (motor.Courronne == Couronne.Max && mv.MaxMovesCount != 0)
                {
                    m3 = motor.Index;
                    r3 = motor.ConvertMoveToSteps(mv.MaxMovesCount);
                }
            }

            // pas de mouvement, rien ne sert d'envoyer 0
            if (r1 == 0 && r2 == 0 && r3 == 0) return;

            AssertConnectedAndConfigured(config);
            var ret = BlockingMove(timeout, m1, r1, m2, r2, m3, r3);

            if (!ret.Value) throw new Exception("Impossible d'exécuter le mouvement " + mv.ToString());
        }

        public SharerFunctionReturn<bool> BlockingMove(TimeSpan timeout, byte m1, long r1, byte m2, long r2, byte m3, long r3)
        {
            return _connection.Call<bool>("blockingMove", timeout, m1, r1, m2, r2, m3, r3);
        }

        public SharerFunctionReturn<bool> DisableOutputs(int mask)
        {
            return _connection.Call<bool>("disableOutputs", mask);
        }

        public SharerFunctionReturn<bool> EnableOutputs(int mask)
        {
            return _connection.Call<bool>("enableOutputs", mask);
        }
    }
}
