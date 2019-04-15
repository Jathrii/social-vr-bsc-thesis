using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace SocialVR
{
    public class FlowController : NetworkBehaviour
    {
        public GameObject InfoCanvasPrefab;
        public GameObject InfoController;
        public GameObject SteamVRPlayer;
        public GameObject SpectatorCamera;
        public static Transform startPosition;

        private KeyCode[] alpha;
        private KeyCode[] keypad;
        private InputField[] input;
        private GameObject message;

        // Start is called before the first frame update
        private void Start()
        {
            if (!isServer)
                return;

            alpha = new[] {
                KeyCode.Alpha1,
                KeyCode.Alpha2,
                KeyCode.Alpha3,
                KeyCode.Alpha4,
                KeyCode.Alpha5,
                KeyCode.Alpha6,
                KeyCode.Alpha7,
                KeyCode.Alpha8,
                KeyCode.Alpha9
            };

            keypad = new[] {
                KeyCode.Keypad1,
                KeyCode.Keypad2,
                KeyCode.Keypad3,
                KeyCode.Keypad4,
                KeyCode.Keypad5,
                KeyCode.Keypad6,
                KeyCode.Keypad7,
                KeyCode.Keypad8,
                KeyCode.Keypad9
            };

            input = InfoController.GetComponentsInChildren<InputField>();

            SteamVRPlayer.SetActive(false);
            SpectatorCamera.SetActive(true);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!isServer)
                return;

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                if (message != null)
                {
                    Destroy(message);
                    message = null;
                }

                RpcSpawnInfo("Delete");
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (Input.GetKeyDown(alpha[i]) || Input.GetKeyDown(keypad[i]))
                    {
                        if (message != null)
                            Destroy(message);

                        GameObject InfoCanvas = Instantiate(InfoCanvasPrefab, new Vector3(0f, 3.5f, -4.0f), Quaternion.identity);
                        InfoCanvas.GetComponentInChildren<Text>().text = input[i].text;
                        message = InfoCanvas;

                        RpcSpawnInfo(input[i].text);
                    }
                }

            }
        }

        [ClientRpc]
        private void RpcSpawnInfo(string info)
        {
            if (message != null)
            {
                Destroy(message);
                message = null;
            }

            if (info == "Delete")
                return;

            GameObject InfoCanvas = Instantiate(InfoCanvasPrefab, new Vector3(0f, 1.7f, 0f), startPosition.rotation);
            InfoCanvas.GetComponentInChildren<Text>().text = info;
            message = InfoCanvas;
        }
    }
}