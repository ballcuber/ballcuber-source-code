using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharer;
using fgSolver.Modele;
using Sharer.Command;
using System.Threading;

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

        public SharerConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public List<Sharer.FunctionCall.SharerFunction> Functions
        {
            get
            {
                return _connection?.Functions;
            }
        }

        private HardwareConfigBoard _index;
        public HardwareConfigBoard Index
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


        public Board(HardwareConfigBoard index)
        {
            _index = index;
        }

        public void Connect()
        {
            if (_connection != null && _connection.Connected) return;

            var config = GetHardwareConfig();

            if (config.Simulated) return;
            _connection = new SharerConnection(config.COMPort, config.BaudRate);

            _connection.Connect();

            _tmr= new Timer((state) => PeriodicGetInfo(), null, 5000, 100);

        }

        const int NB_MOTOR = 5;
        string[] _variablesToRead = Enumerable.Range(0, NB_MOTOR).SelectMany((x) =>new string[] { "position[" + x + "]", "enabled[" + x + "]"}).ToArray();

        private void PeriodicGetInfo()
        {
            try
            {
                if (!Connected) return;

                if(_connection.Variables.Count == 0 || _connection.Functions.Count == 0)
                {
                    _connection.RefreshFunctions();
                    _connection.RefreshVariables();
                }

                var values = _connection.ReadVariables(_variablesToRead);

                using (var state = GlobalState.GetState())
                {
                    for (int i = 0; i < NB_MOTOR; i++)
                    {
                        var position = (int)values[2 * i].Value;
                        var enabled = (bool)values[2 * i + 1].Value;

                        var m = state.Motors.Motors.FirstOrDefault((x) => x.Board == _index && x.Index == i);

                        if (m != null)
                        {
                            m.SetState(enabled, position);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Log(ex);
            }


        }

        private Timer _tmr;



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
            _tmr.Dispose();
        }

        public void AssertConnectedAndConfigured()
        {
            AssertConnectedAndConfigured(GetHardwareConfig());
        }

        public void AssertConnectedAndConfigured(HardwareConfig config)
        {
            if (config.Simulated) throw new Exception("Carte simulée");

            if (!Connected) throw new Exception("Carte non connectée");
        }


        public void BlockingMove(MotorMove mv, TimeSpan timeout)
        {
            var config = GetHardwareConfig();

            if (config.Simulated)
            {
                System.Threading.Thread.Sleep(mv.QuarterNumber * config.SimulatedTime);
                return;
            }

            byte[] m = new byte[2] { 0xFF, 0xFF };
            int[] r = new int[2] { 0, 0 };

            Motor[] motors;
            using (var state = GlobalState.GetState())
            {
                motors = state.Motors.Motors.Where((x) => x.Axe == mv.Axe && x.Board == this.Index).Select((x) => (Motor)x.Clone()).ToArray();
            }

            if (motors.Count() == 0) return;
            else if (motors.Count() > 2) throw new Exception("Il ne peut pas y avoir plus de deux moteurs pas à pas pilotés en même temps pour des questions de performance");

            using (var state = GlobalState.GetState())
            {
                for(int i=0;i<motors.Length; i++)
                {
                    m[i] = motors[i].Index;
                                 
                    //r[i] = state.HardwareConfigGlobal.ConvertMoveToSteps(mv.GetMoves(motors[i].Courronne), motors[i].Inverted);
                }
            }

            // pas de mouvement, rien ne sert d'envoyer 0
            if (r[0] == 0 && r[1] == 0) return;

            AssertConnectedAndConfigured(config);
            var ret = BlockingMove(timeout, m[0], r[0], m[1], r[1]);

            if (!ret.Value) throw new Exception("Impossible d'exécuter le mouvement " + mv.ToString());
        }

        public SharerFunctionReturn<bool> BlockingMove(TimeSpan timeout, byte m1, long r1, byte m2, long r2)
        {
            AssertConnectedAndConfigured();
            return _connection.Call<bool>("blockingMove", timeout, m1, r1, m2, r2);
        }

        public void Stop(int mask)
        {
            AssertConnectedAndConfigured();
            _connection.Call("stop", mask);
        }

        public SharerFunctionReturn<bool> DisableOutputs(int mask)
        {
            AssertConnectedAndConfigured();
            return _connection.Call<bool>("disableOutputs", mask);
        }

        public SharerFunctionReturn<bool> SetSpeed(int mask, int value)
        {
            AssertConnectedAndConfigured();
            return _connection.Call<bool>("setMaxSpeed", mask, value);
        }

        public void BeginMoveStep(int mask, int value)
        {
            AssertConnectedAndConfigured();
            Task.Factory.StartNew(() => Move(mask, value));
        }

        public SharerFunctionReturn<bool> Move(int mask, int value)
        {
            AssertConnectedAndConfigured();
            return _connection.Call<bool>("move", mask, value);
        }
        public SharerFunctionReturn<bool> MoveTo(int mask, int value)
        {

            AssertConnectedAndConfigured();
            return _connection.Call<bool>("moveTo", mask, value);
        }

        public SharerFunctionReturn<bool> SetCurrentPosition(int mask, int value)
        {
            AssertConnectedAndConfigured();
            return _connection.Call<bool>("setCurrentPosition", mask, value);
        }

        public SharerFunctionReturn<bool> EnableOutputs(int mask)
        {
            AssertConnectedAndConfigured();
            return _connection.Call<bool>("enableOutputs", mask);
        }
    }
}
