using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Valve.VR;

namespace SocialVR {
    public class PlayerModelSync : NetworkBehaviour {
        public GameObject Head;

        private void Start () {
            if (!isLocalPlayer)
                return;

            FlowController.startPosition = transform;
            
            Transform SteamVRPlayer = GameObject.FindGameObjectWithTag ("SteamVRPlayer").transform;
            SteamVRPlayer.SetPositionAndRotation (transform.position, transform.rotation);
        }

        // Update is called once per frame
        private void Update () {
            if (!isLocalPlayer)
                return;

            Transform VRCamTransform = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().transform;
            transform.position = VRCamTransform.position - new Vector3 (0, VRCamTransform.position.y, 0);
            transform.forward = new Vector3 (VRCamTransform.forward.x, 0, VRCamTransform.forward.z);

            Head.transform.forward = VRCamTransform.forward;

            CmdSyncModel (transform.position, transform.forward, VRCamTransform.forward);
        }

        [Command]
        void CmdSyncModel (Vector3 position, Vector3 forward, Vector3 headForward) {
            RpcSyncModel (position, forward, headForward);
        }

        [ClientRpc]
        void RpcSyncModel (Vector3 position, Vector3 forward, Vector3 headForward) {
            if (isLocalPlayer)
                return;

            transform.position = position;
            transform.forward = forward;
            Head.transform.forward = headForward;
        }
    }
}