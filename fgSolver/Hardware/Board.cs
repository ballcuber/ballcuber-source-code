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
            if (_connection!=null && _connection.Connected) return;

            var config = GetHardwareConfig();

            if (config.Simulated) return;
            _connection = new SharerConnection(config.COMPort, config.BaudRate);

            _connection.Connect();

            // Armer un timer pour rafraichir la liste des fonctions, ,il faut le temps que le arduino reboot
            Timer tmr = null;
            tmr  = new Timer((state) => {
                try
                {
                    _connection.RefreshFunctions(); tmr.Dispose();
                }
                catch (Exception ex){
                    Logger.Log(ex);
                }
            }, null, 2000, 0);
           
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
                                 
                    r[i] = state.HardwareConfigGlobal.ConvertMoveToSteps(mv.GetMoves(motors[i].Courronne), motors[i].Inverted);
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
            return _connection.Call<bool>("blockingMove", timeout, m1, r1, m2, r2);
        }

        public SharerFunctionReturn<bool> DisableOutputs(int mask)
        {
            return _connection.Call<bool>("disableOutputs", mask);
        }

        public SharerFunctionReturn<bool> SetSpeed(int mask, int value)
        {
            return _connection.Call<bool>("setMaxSpeed", mask);
        }

        public void BeginMoveStep(int mask, int value)
        {
            Task.Factory.StartNew(() => Move(mask, value));
        }

        public SharerFunctionReturn<bool> Move(int mask, int value)
        {
            return _connection.Call<bool>("move", mask);
        }

        public SharerFunctionReturn<bool> EnableOutputs(int mask)
        {
            return _connection.Call<bool>("enableOutputs", mask);
        }

        public void LockServos()
        {
            using (var state = GlobalState.GetState())
            {
                _connection.Call("ServoWrite", state.ServoConfig.LockedPosition0, state.ServoConfig.LockedPosition1, state.ServoConfig.LockedPosition2, state.ServoConfig.LockedPosition3);
            }
        }

        public void OpenServos()
        {
            using (var state = GlobalState.GetState())
            {
                _connection.Call("ServoWrite", state.ServoConfig.OpenedPosition0, state.ServoConfig.OpenedPosition1, state.ServoConfig.OpenedPosition2, state.ServoConfig.OpenedPosition3);
            }
        }

        public void InitSteppers()
        {
            using (var state = GlobalState.GetState())
            {
                _connection.Call("setMaxSpeed", 0xff, state.HardwareConfigGlobal.Speed);
                _connection.Call("setAcceleration", 0xff, state.HardwareConfigGlobal.Acceleration);
            }
        }
    }
}
