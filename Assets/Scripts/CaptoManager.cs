using GSdkNet.Board;
using GSdkNet.Bridge;
using GSdkNet.Peripheral;
using Mirror;
using UnityEngine;

namespace SocialVR
{
    public class CaptoManager : NetworkBehaviour
    {
        private IPeripheralCentral Central;
        private IBoardPeripheral lPeripheral;
        private IBoardPeripheral rPeripheral;
        private float[] lSensors;
        private float[] rSensors;

        private float[] lLast;
        private float[] rLast;

        private Finger[] lHand;
        private Finger[] rHand;

        private Animator lHandAnimator;

        private Animator rHandAnimator;

        // Use this for initialization
        void Start()
        {
            lHandAnimator = GameObject.FindGameObjectWithTag("lHand").GetComponent<Animator>();
            rHandAnimator = GameObject.FindGameObjectWithTag("rHand").GetComponent<Animator>();

            lHand = new Finger[5];
            rHand = new Finger[5];

            lHand[4] = new Finger(250, 3000, "Thumb");
            lHand[3] = new Finger(150, 2300, "Index");
            lHand[2] = new Finger(150, 2800, "Mid");
            lHand[1] = new Finger(200, 2200, "Ring");
            lHand[0] = new Finger(250, 2200, "Pinky");

            rHand[0] = new Finger(500, 2800, "Thumb");
            rHand[1] = new Finger(600, 3600, "Index");
            rHand[2] = new Finger(150, 4000, "Mid");
            rHand[3] = new Finger(100, 3600, "Ring");
            rHand[4] = new Finger(100, 4000, "Pinky");

            Debug.Log("Start ");
            var logEntry = new CallbackLogEntry(LogLevel.Debug);
            logEntry.LogReceived += (obj, arguments) =>
            {
                Debug.Log(arguments.Message);
            };
            GSdkNet.Bridge.Logger.Shared.AddEntry(logEntry);
            Debug.Log("Looking for peripheral");
            var boardFactory = new BoardFactory();
            Central = boardFactory.MakeBoardCentral();
            Central.PeripheralsChanged += Central_PeripheralsChanged;
            Central.StartScan(new ScanOptions() { PreferredInterval = 5 });

            lLast = new float[10];
            rLast = new float[10];
        }

        private void Central_PeripheralsChanged(object sender, PeripheralsEventArgs e)
        {
            Debug.Log(e.Inserted.Length);

            foreach (var peripheral in e.Inserted)
            {
                try
                {
                    Debug.Log("Trying to connect peripheral");
                    Debug.Log("- ID: " + peripheral.Id);
                    Debug.Log("- Name: " + peripheral.Name);
                    peripheral.Start();

                    if (peripheral.Name == "CaptoGlove1502")
                    {
                        rPeripheral = peripheral as IBoardPeripheral;
                        rPeripheral.StreamTimeslotsWrite(new StreamTimeslots()
                        {
                            SensorsState = 6
                        });
                        rPeripheral.StreamReceived += Peripheral_StreamReceived;

                        Debug.Log(peripheral.Name + " - Status: " + rPeripheral.Status.ToString());
                    }
                    else if (peripheral.Name == "CaptoGlove1401")
                    {
                        lPeripheral = peripheral as IBoardPeripheral;
                        lPeripheral.StreamTimeslotsWrite(new StreamTimeslots()
                        {
                            SensorsState = 6
                        });
                        lPeripheral.StreamReceived += Peripheral_StreamReceived;

                        Debug.Log(peripheral.Name + " - Status: " + lPeripheral.Status.ToString());
                    }
                }
                catch
                {
                    Debug.Log(peripheral.Name + " - Status: " + peripheral.Status.ToString());
                }
            }
        }

        private static string FloatsToString(float[] value)
        {
            string result = "";
            var index = 0;
            foreach (var element in value)
            {
                if (index != 0)
                {
                    result += ", ";
                }
                result += element.ToString();
                index += 1;
            }
            return result;
        }

        private void Peripheral_StreamReceived(object sender, BoardStreamEventArgs e)
        {
            if (e.StreamType == BoardStreamType.SensorsState)
            {
                var args = e as BoardFloatSequenceEventArgs;
                var value = FloatsToString(args.Value);

                IBoardPeripheral peripheral = sender as IBoardPeripheral;

                if (peripheral.Name == "CaptoGlove1502")
                    rSensors = args.Value;
                else if (peripheral.Name == "CaptoGlove1401")
                    lSensors = args.Value;

                Debug.Log(peripheral.Name + " received sensors state: " + value);
            }
        }

        public void Stop()
        {
            if (Central != null)
            {
                Central.PeripheralsChanged -= Central_PeripheralsChanged;
                Central = null;
            }
            if (rPeripheral != null)
            {
                rPeripheral.StreamReceived -= Peripheral_StreamReceived;
                if (rPeripheral.Status != PeripheralStatus.Disconnected)
                {
                    rPeripheral.Stop();
                }
                rPeripheral = null;
            }
            if (lPeripheral != null)
            {
                lPeripheral.StreamReceived -= Peripheral_StreamReceived;
                if (lPeripheral.Status != PeripheralStatus.Disconnected)
                {
                    lPeripheral.Stop();
                }
                lPeripheral = null;
            }
        }

        void OnDisable()
        {
            Debug.Log("OnDisable");
        }

        void OnEnable()
        {
            Debug.Log("OnEnable");
        }

        // Update is called once per frame
        void Update()
        {
            if (lSensors != null)
            {
                // Animate left hand fingers
                for (int i = 0; i < 5; i++)
                {
                    if (Mathf.Abs(lSensors[i * 2 + 1] - lLast[i * 2 + 1]) > 100)
                        lLast[i * 2 + 1] = lSensors[i * 2 + 1];
                    lHandAnimator.Play(lHand[i].getAnimationState(), -1, lHand[i].evaluate(lLast[i * 2 + 1]));
                }
            }

            if (rSensors != null)
            {
                // Animate right hand fingers
                for (int i = 0; i < 5; i++)
                {
                    if (Mathf.Abs(rSensors[i * 2] - rLast[i * 2]) > 100)
                        rLast[i * 2] = rSensors[i * 2];
                    rHandAnimator.Play(rHand[i].getAnimationState(), -1, rHand[i].evaluate(rLast[i * 2]));
                }
            }

            if (lSensors != null || lSensors != null)
                CmdAnimateHands((lSensors != null), (rSensors != null), lLast, rLast);
        }

        [Command]
        private void CmdAnimateHands(bool left, bool right, float[] lValues, float[] rValues)
        {
            RpcAnimateHands(left, right, lValues, rValues);
        }

        [ClientRpc]
        private void RpcAnimateHands(bool left, bool right, float[] lValues, float[] rValues)
        {
            if (isLocalPlayer)
                return;

            for (int i = 0; i < 5 && left; i++)
            {
                lHandAnimator.Play(lHand[i].getAnimationState(), -1, lHand[i].evaluate(lValues[i * 2 + 1]));
            }

            for (int i = 0; i < 5 && right; i++)
            {
                rHandAnimator.Play(rHand[i].getAnimationState(), -1, rHand[i].evaluate(rValues[i * 2]));
            }
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
            Stop();
        }
    }
}