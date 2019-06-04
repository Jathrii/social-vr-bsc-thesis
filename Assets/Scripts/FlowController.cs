using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace SocialVR
{
    public class FlowController : NetworkBehaviour
    {
        public InputField Generated;
        public GameObject InfoCanvasPrefab;
        public GameObject InfoController;
        public GameObject SteamVRPlayer;
        public GameObject SpectatorCamera;
        public static Transform startPosition;

        private KeyCode[] alpha;
        private KeyCode[] keypad;
        private InputField[] input;
        private GameObject message;
        private bool charades = false;

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
            };

            keypad = new[] {
                KeyCode.Keypad1,
                KeyCode.Keypad2,
                KeyCode.Keypad3,
                KeyCode.Keypad4,
                KeyCode.Keypad5,
            };

            input = InfoController.GetComponentsInChildren<InputField>();

            Generated.text = WordGenerator.Generate();

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

                RpcSpawnInfo("Delete", charades);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
            {
                Generated.text = WordGenerator.Generate();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
            {
                if (message != null)
                    Destroy(message);

                GameObject InfoCanvas = Instantiate(InfoCanvasPrefab, new Vector3(0f, 2.5f, -2.0f), Quaternion.identity);
                InfoCanvas.GetComponentInChildren<Text>().text = Generated.text;
                message = InfoCanvas;

                RpcSpawnInfo(Generated.text, charades);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
            {
                charades = !charades;
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (Input.GetKeyDown(alpha[i]) || Input.GetKeyDown(keypad[i]))
                    {
                        if (message != null)
                            Destroy(message);

                        GameObject InfoCanvas = Instantiate(InfoCanvasPrefab, new Vector3(0f, 2.5f, -2.0f), Quaternion.identity);
                        InfoCanvas.GetComponentInChildren<Text>().text = input[i+1].text;
                        message = InfoCanvas;

                        RpcSpawnInfo(input[i+1].text, charades);
                    }
                }

            }
        }
        public void GenerateInfo() {

        }

        public void SpawnInfo(InputField field)
        {
            if (message != null)
                Destroy(message);

            GameObject InfoCanvas = Instantiate(InfoCanvasPrefab, new Vector3(0f, 2.5f, -2.0f), Quaternion.identity);
            InfoCanvas.GetComponentInChildren<Text>().text = field.text;
            message = InfoCanvas;

            RpcSpawnInfo(field.text, charades);
        }

        [ClientRpc]
        private void RpcSpawnInfo(string info, bool charades)
        {
            if (charades && !CaptoManager.mainPlayer)
                return;

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