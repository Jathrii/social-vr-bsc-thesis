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
        public GameObject LeftHand;
        public GameObject RightHand;

        private Vector3 cameraOffset;

        [SyncVar]
        private Vector3 VRCamPosition;

        [SyncVar]
        private Vector3 VRCamEulerAngles;

        [SyncVar]
        private Vector3 LeftHandPosition;

        [SyncVar]
        private Vector3 LeftHandEulerAngles;
        
        [SyncVar]
        private Vector3 RightHandPosition;
        
        [SyncVar]
        private Vector3 RightHandEulerAngles;

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
                SyncLeftHand();
                SyncRightHand();
            }

            Avatar.transform.eulerAngles = new Vector3(Avatar.transform.eulerAngles.x, invertAngle(VRCamEulerAngles.y), Avatar.transform.eulerAngles.z);
            Head.transform.eulerAngles = new Vector3(-VRCamEulerAngles.x, Head.transform.eulerAngles.y, VRCamEulerAngles.z);

            cameraOffset = Eyes.transform.position - Avatar.transform.position;

            Avatar.transform.position = VRCamPosition - cameraOffset;

            LeftHand.transform.position = LeftHandPosition;
            LeftHand.transform.eulerAngles = LeftHandEulerAngles;
            RightHand.transform.position = RightHandPosition;
            RightHand.transform.eulerAngles = RightHandEulerAngles;
        }

        private void SyncModel(Vector3 position, Vector3 eulerAngles)
        {
            CmdSyncModel(position, eulerAngles);
        }

        private void SyncLeftHand()
        {
            Vector3 position = LeftHand.transform.position;
            Vector3 eulerAngles = LeftHand.transform.eulerAngles;
            CmdSyncLeftHand(position, eulerAngles);
        }

        private void SyncRightHand()
        {
            Vector3 position = RightHand.transform.position;
            Vector3 eulerAngles = RightHand.transform.eulerAngles;
            CmdSyncRightHand(position, eulerAngles);
        }

        [Command]
        void CmdSyncModel(Vector3 position, Vector3 eulerAngles)
        {
            VRCamPosition = position;
            VRCamEulerAngles = eulerAngles;
        }

        [Command]
        void CmdSyncLeftHand(Vector3 position, Vector3 eulerAngles)
        {
            LeftHandPosition = position;
            LeftHandEulerAngles = eulerAngles;
            //RpcSyncLeftHand(position, eulerAngles);
        }

        [Command]
        void CmdSyncRightHand(Vector3 position, Vector3 eulerAngles)
        {
            RightHandPosition = position;
            RightHandEulerAngles = eulerAngles;
            //RpcSyncRightHand(position, eulerAngles);
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

        [ClientRpc]
        private void RpcSyncLeftHand(Vector3 position, Vector3 eulerAngles)
        {
            if (isLocalPlayer)
                return;

            LeftHand.transform.position = position;
            LeftHand.transform.eulerAngles = eulerAngles;
        }

        [ClientRpc]
        private void RpcSyncRightHand(Vector3 position, Vector3 eulerAngles)
        {
            if (isLocalPlayer)
                return;

            RightHand.transform.position = position;
            RightHand.transform.eulerAngles = eulerAngles;
        }

        private static float invertAngle(float angle)
        {
            return angle > 180f ? angle - 180f : angle + 180f;
        }
    }
}