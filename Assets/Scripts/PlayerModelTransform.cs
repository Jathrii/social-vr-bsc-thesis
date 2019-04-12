using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Valve.VR;

public class PlayerModelTransform : NetworkBehaviour
{
    public GameObject Head;

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
            return;

        Transform VRCamTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform;

        transform.position = VRCamTransform.position;
        transform.forward = new Vector3(VRCamTransform.forward.x, 0, VRCamTransform.forward.z);
        transform.Translate(new Vector3(0, -VRCamTransform.position.y, 0));
        Head.transform.position = VRCamTransform.position;
        Head.transform.forward = VRCamTransform.forward;

        CmdSyncModel(VRCamTransform.position, VRCamTransform.forward);
    }

    [Command]
    void CmdSyncModel(Vector3 position, Vector3 forward)
    {
        RpcSyncModel(position, forward);
    }

    [ClientRpc]
    void RpcSyncModel(Vector3 position, Vector3 forward)
    {
        transform.position = position;
        transform.forward = new Vector3(forward.x, 0, forward.z);
        transform.Translate(new Vector3(0, -position.y, 0));
        Head.transform.position = position;
        Head.transform.forward = forward;
    }
}
