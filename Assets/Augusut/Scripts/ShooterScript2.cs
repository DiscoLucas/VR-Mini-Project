using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;

public class ShooterScript2 : NetworkBehaviour
{

    public GameObject bullet;
    public Transform spawnPosition;


    void Start()
    {
        if (NetworkManager.Singleton == null) Debug.LogError("No network manager yet");
        NetworkManager.Singleton.AddNetworkPrefab(bullet);
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        XRGrabInteractable theGunGrabable = GetComponent<XRGrabInteractable>();
        theGunGrabable.activated.AddListener(bangBang); 
    }

    public void bangBang(ActivateEventArgs arg)
    {
        spawnBullet_ServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void spawnBullet_ServerRpc()
    {
        GameObject newBullet = Instantiate(bullet,spawnPosition.position, spawnPosition.rotation);

        NetworkObject netBullet = newBullet.GetComponent<NetworkObject>();

        netBullet.Spawn();
    }


}
