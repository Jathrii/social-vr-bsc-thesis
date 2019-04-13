using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace SocialVR {
    public class FlowController : NetworkBehaviour {
        public GameObject InfoCanvasPrefab;
        public GameObject InfoController;
        public GameObject SteamVRPlayer;
        public GameObject SpectatorCamera;
        public static Transform startPosition;
        // Start is called before the first frame update
        private void Start () {
            if (!isServer)
                return;

            SteamVRPlayer.SetActive(false);
            SpectatorCamera.SetActive(true);
        }

        // Update is called once per frame
        private void Update () {
            if (!isServer)
                return;
            
            if (Input.GetKeyUp (KeyCode.Alpha1) || Input.GetKeyUp (KeyCode.Keypad1)) {
                InputField[] info = InfoController.GetComponentsInChildren<InputField> ();
                RpcSpawnInfo (info[0].text, 2.0f);
            } else if (Input.GetKeyUp (KeyCode.Alpha2) || Input.GetKeyUp (KeyCode.Keypad2)) {
                InputField[] info = InfoController.GetComponentsInChildren<InputField> ();
                RpcSpawnInfo (info[1].text, 2.0f);
            } else if (Input.GetKeyUp (KeyCode.Alpha3) || Input.GetKeyUp (KeyCode.Keypad3)) {
                InputField[] info = InfoController.GetComponentsInChildren<InputField> ();
                RpcSpawnInfo (info[2].text, 2.0f);
            } else if (Input.GetKeyUp (KeyCode.Alpha4) || Input.GetKeyUp (KeyCode.Keypad4)) {
                InputField[] info = InfoController.GetComponentsInChildren<InputField> ();
                RpcSpawnInfo (info[3].text, 2.0f);
            } else if (Input.GetKeyUp (KeyCode.Alpha5) || Input.GetKeyUp (KeyCode.Keypad5)) {
                InputField[] info = InfoController.GetComponentsInChildren<InputField> ();
                RpcSpawnInfo (info[4].text, 2.0f);
            } else if (Input.GetKeyUp (KeyCode.Alpha6) || Input.GetKeyUp (KeyCode.Keypad6)) {
                InputField[] info = InfoController.GetComponentsInChildren<InputField> ();
                RpcSpawnInfo (info[5].text, 2.0f);
            } else if (Input.GetKeyUp (KeyCode.Alpha7) || Input.GetKeyUp (KeyCode.Keypad7)) {
                InputField[] info = InfoController.GetComponentsInChildren<InputField> ();
                RpcSpawnInfo (info[6].text, 2.0f);
            } else if (Input.GetKeyUp (KeyCode.Alpha8) || Input.GetKeyUp (KeyCode.Keypad8)) {
                InputField[] info = InfoController.GetComponentsInChildren<InputField> ();
                RpcSpawnInfo (info[7].text, 2.0f);
            } else if (Input.GetKeyUp (KeyCode.Alpha9) || Input.GetKeyUp (KeyCode.Keypad9)) {
                RpcSpawnInfo ("Thank you for you participation!", 2.0f);
            }
        }

        [ClientRpc]
        private void RpcSpawnInfo (string info, float delay) {
            GameObject InfoCanvas = Instantiate (NetworkManager.singleton.spawnPrefabs[0], new Vector3 (0f, 1.7f, 0f), startPosition.rotation);
            InfoCanvas.GetComponentInChildren<Text> ().text = info;
            Destroy (InfoCanvas, delay);
        }
    }
}