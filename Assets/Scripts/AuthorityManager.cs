using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AuthorityManager : NetworkBehaviour
{
    public static GameObject ToBeAssigned;
    public static GameObject Attached;
    public static GameObject Detached;

    public void SetToBeAssigned(GameObject obj)
    {
        ToBeAssigned = obj;
    }

    public void Attach(GameObject obj)
    {
        Attached = obj;
    }
    
    public void Detach(GameObject obj)
    {
        Detached = obj;
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (ToBeAssigned != null)
        {
            NetworkIdentity objectNI = ToBeAssigned.GetComponent<NetworkIdentity>();
            NetworkIdentity playerNI = GetComponent<NetworkIdentity>();

            CmdAssignLocalAuthority(objectNI, playerNI);

            ToBeAssigned = null;
        }

        if (Attached != null)
        {
            CmdDisableGravity(Attached);
            Attached = null;
        }

        if (Detached != null)
        {
            CmdEnableGravity(Detached);
            Detached = null;
        }
    }

    [Command]
    private void CmdAssignLocalAuthority(NetworkIdentity objectNI, NetworkIdentity playerNI)
    {
        NetworkConnection prevOwner = objectNI.clientAuthorityOwner;
        if (prevOwner != null)
            objectNI.RemoveClientAuthority(prevOwner);
        objectNI.AssignClientAuthority(playerNI.connectionToClient);
    }

    [Command]
    private void CmdEnableGravity(GameObject obj)
    {
        RpcEnableGravity(obj);
    }

    [ClientRpc]
    private void RpcEnableGravity(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().useGravity = true;
    }

    [Command]
    private void CmdDisableGravity(GameObject obj)
    {
       RpcDisableGravity(obj);
    }

    [ClientRpc]
    private void RpcDisableGravity(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().useGravity = false;
    }
}
