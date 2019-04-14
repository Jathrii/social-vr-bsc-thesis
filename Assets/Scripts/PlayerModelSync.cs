using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace SocialVR
{
    public class PlayerModelSync : NetworkBehaviour
    {
        public GameObject Head;
        public GameObject Chest;

        private void Start()
        {
            if (!isLocalPlayer)
                return;

            FlowController.startPosition = transform;

            //InputTracking.disablePositionalTracking = true;

            Transform SteamVRPlayer = GameObject.FindGameObjectWithTag("SteamVRPlayer").transform;
            SteamVRPlayer.SetPositionAndRotation(transform.position, transform.rotation);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!isLocalPlayer)
                return;

            Transform VRCamTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform;
            transform.position = new Vector3(VRCamTransform.position.x, 0, VRCamTransform.position.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, VRCamTransform.eulerAngles.y, transform.eulerAngles.z);
            Head.transform.eulerAngles = new Vector3(-VRCamTransform.eulerAngles.x, Head.transform.eulerAngles.y, VRCamTransform.eulerAngles.z);

            CmdSyncModel(transform.position, VRCamTransform.eulerAngles);
        }

        [Command]
        void CmdSyncModel(Vector3 position, Vector3 eulerAngles)
        {
            RpcSyncModel(position, eulerAngles);
        }

        [ClientRpc]
        void RpcSyncModel(Vector3 position, Vector3 eulerAngles)
        {
            if (isLocalPlayer)
                return;

            transform.position = new Vector3(position.x, 0, position.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, eulerAngles.y, transform.eulerAngles.z);
            Head.transform.eulerAngles = eulerAngles;
        }
    }
}