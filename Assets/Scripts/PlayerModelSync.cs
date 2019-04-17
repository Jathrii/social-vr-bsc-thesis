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
        public GameObject Avatar;
        public GameObject Head;
        public GameObject Eyes;
        private Vector3 cameraOffset;

        [SyncVar]
        private Vector3 VRCamPosition;

        [SyncVar]
        private Vector3 VRCamEulerAngles;

        private void Start()
        {
            if (!isLocalPlayer)
                return;

            FlowController.startPosition = transform;

            //InputTracking.disablePositionalTracking = true;

            Transform VRPlayerTransform = GameObject.FindGameObjectWithTag("SteamVRPlayer").transform;
            VRPlayerTransform.position = new Vector3(transform.position.x, VRPlayerTransform.position.y, transform.position.z);
            VRPlayerTransform.eulerAngles = transform.eulerAngles;

            Transform VRCamTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
            VRCamTransform.position = Eyes.transform.position;
            VRCamTransform.eulerAngles = Eyes.transform.eulerAngles;
        }

        // Update is called once per frame
        private void Update()
        {
            if (isLocalPlayer)
            {
                Transform VRCamTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform;
                SyncModel(VRCamTransform.position, VRCamTransform.eulerAngles);
            }

            Avatar.transform.eulerAngles = new Vector3(Avatar.transform.eulerAngles.x, invertAngle(VRCamEulerAngles.y), Avatar.transform.eulerAngles.z);
            Head.transform.eulerAngles = new Vector3(-VRCamEulerAngles.x, Head.transform.eulerAngles.y, VRCamEulerAngles.z);

            cameraOffset = Eyes.transform.position - Avatar.transform.position;

            Avatar.transform.position = VRCamPosition - cameraOffset;
        }

        private void SyncModel(Vector3 position, Vector3 eulerAngles)
        {
            CmdSyncModel(position, eulerAngles);
        }

        [Command]
        void CmdSyncModel(Vector3 position, Vector3 eulerAngles)
        {
            VRCamPosition = position;
            VRCamEulerAngles = eulerAngles;
        }

        [ClientRpc]
        private void RpcSyncModel(Vector3 position, Vector3 eulerAngles)
        {
            if (isLocalPlayer)
                return;

            Avatar.transform.position = position;
            Avatar.transform.eulerAngles = new Vector3(Avatar.transform.eulerAngles.x, eulerAngles.y, Avatar.transform.eulerAngles.z);
            Head.transform.eulerAngles = new Vector3(-eulerAngles.x, Head.transform.eulerAngles.y, eulerAngles.z);
        }

        private static float invertAngle(float angle)
        {
            return angle > 180f ? angle - 180f : angle + 180f;
        }
    }
}