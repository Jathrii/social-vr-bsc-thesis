﻿using GSdkNet.Board;
using GSdkNet.Bridge;
using GSdkNet.Peripheral;
using Mirror;
using UnityEngine;

namespace SocialVR
{
    public class CaptoManager : NetworkBehaviour
    {
        public GameObject lHand;
        public GameObject rHand;

        public static bool mainPlayer = false;

        private IPeripheralCentral Central;
        private IBoardPeripheral lPeripheral;
        private IBoardPeripheral rPeripheral;
        private float[] lSensors;
        private float[] rSensors;

        private float[] lLast;
        private float[] rLast;

        private Finger[] lFingers;
        private Finger[] rFingers;

        private Animator lHandAnimator;

        private Animator rHandAnimator;

        // Use this for initialization
        void Start()
        {

            lFingers = new Finger[5];
            rFingers = new Finger[5];

            lFingers[4] = new Finger(250, 3000, "Thumb");
            lFingers[3] = new Finger(150, 1000, "Index");
            lFingers[2] = new Finger(150, 1000, "Mid");
            lFingers[1] = new Finger(200, 1000, "Ring");
            lFingers[0] = new Finger(250, 1000, "Pinky");

            rFingers[0] = new Finger(500, 2800, "Thumb");
            rFingers[1] = new Finger(250, 1000, "Index");
            rFingers[2] = new Finger(150, 1000, "Mid");
            rFingers[3] = new Finger(100, 1000, "Ring");
            rFingers[4] = new Finger(100, 1000, "Pinky");

            if (!isLocalPlayer)
                return;

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
        }

        private void Central_PeripheralsChanged(object sender, PeripheralsEventArgs e)
        {
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
            if (isLocalPlayer)
            {
                mainPlayer = lHand.activeSelf || rHand.activeSelf;

                if (lSensors != null)
                {
                    if (lLast == null)
                        lLast = new float[10];

                    // Animate left hand fingers
                    for (int i = 0; i < 5; i++)
                    {
                        if (Mathf.Abs(lSensors[i * 2 + 1] - lLast[i * 2 + 1]) > 100)
                            lLast[i * 2 + 1] = lSensors[i * 2 + 1];
                    }
                }

                if (rSensors != null)
                {
                    if (rLast == null)
                        rLast = new float[10];
                    
                    // Animate right hand fingers
                    for (int i = 0; i < 5; i++)
                    {
                        if (Mathf.Abs(rSensors[i * 2] - rLast[i * 2]) > 100)
                            rLast[i * 2] = rSensors[i * 2];
                    }
                }

                if (lSensors != null || lSensors != null)
                    CmdAnimateHands((lSensors != null), (rSensors != null), lLast, rLast);
            }

            if (lLast != null)
            {
                if (!lHand.activeSelf)
                {
                    lHand.SetActive(true);

                    if (isLocalPlayer)
                    {
                        TrackedObject lTracked = lHand.AddComponent<TrackedObject>();
                        lTracked.hand = TrackedObject.Hand.Left;
                    }
                    
                    lHandAnimator = lHand.GetComponent<Animator>();
                }

                for (int i = 0; i < 5; i++)
                    lHandAnimator.Play(lFingers[i].getAnimationState(), -1, lFingers[i].evaluate(lLast[i * 2 + 1]));
            }

            if (rLast != null)
            {
                if (!rHand.activeSelf)
                {
                    rHand.SetActive(true);

                    if (isLocalPlayer)
                    {
                        TrackedObject rTracked = rHand.AddComponent<TrackedObject>();
                        rTracked.hand = TrackedObject.Hand.Right;
                    }

                    rHandAnimator = rHand.GetComponent<Animator>();
                }

                for (int i = 0; i < 5; i++)
                    rHandAnimator.Play(rFingers[i].getAnimationState(), -1, rFingers[i].evaluate(rLast[i * 2]));
            }

        }

        [Command]
        private void CmdAnimateHands(bool left, bool right, float[] lValues, float[] rValues)
        {
            lLast = lValues;
            rLast = rValues;
            RpcAnimateHands(left, right, lValues, rValues);
        }

        [ClientRpc]
        private void RpcAnimateHands(bool left, bool right, float[] lValues, float[] rValues)
        {
            if (isLocalPlayer)
                return;

            lLast = lValues;
            rLast = rValues;
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
            Stop();
        }
    }
}