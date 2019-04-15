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

        private void Start()
        {
            if (!isLocalPlayer)
                return;

            FlowController.startPosition = transform;

            //InputTracking.disablePositionalTracking = true;

            Transform VRCamTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
            VRCamTransform.position = Eyes.transform.position;
            VRCamTransform.eulerAngles = Eyes.transform.eulerAngles;

            cameraOffset = Eyes.transform.position - Avatar.transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!isLocalPlayer)
                return;

            Transform VRCamTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform;
            Avatar.transform.eulerAngles = new Vector3(Avatar.transform.eulerAngles.x, invertAngle(VRCamTransform.eulerAngles.y), Avatar.transform.eulerAngles.z);
            Head.transform.eulerAngles = new Vector3(-VRCamTransform.eulerAngles.x, Head.transform.eulerAngles.y, VRCamTransform.eulerAngles.z);

            cameraOffset = Eyes.transform.position - Avatar.transform.position;

            Avatar.transform.position = VRCamTransform.position - cameraOffset;

            CmdSyncModel(Avatar.transform.position, VRCamTransform.eulerAngles);
        }

        [Command]
        private void CmdSyncModel(Vector3 position, Vector3 eulerAngles)
        {
            RpcSyncModel(position, eulerAngles);
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