using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;


public class NetworkGrabbablee : NetworkBehaviour
{



    NetworkObject netObject;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        netObject = GetComponent<NetworkObject>();

        XRGrabInteractable theGunGrabable = GetComponent<XRGrabInteractable>();
        theGunGrabable.hoverEntered.AddListener(touched);
    }

    public void touched(HoverEnterEventArgs arg)
    {
        var interactorXrRig = arg.interactorObject.transform.gameObject.GetComponentInParent<XROrigin>();

        if (interactorXrRig.gameObject == VRRigReferencess.singleton.gameObject)
        {
            var player = VRRigReferencess.singleton.localPlayer;
            player.IncrementScore();
        }

    }


    public void requestOwnership()
    {
        requestOwnership_ServerRpc(NetworkManager.Singleton.LocalClient.ClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void requestOwnership_ServerRpc(ulong clientID)
    {
        netObject.ChangeOwnership(clientID);
        Debug.Log("changing ownership");
    }


}
