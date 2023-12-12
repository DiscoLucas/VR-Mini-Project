using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class BulletBehaviorr : NetworkBehaviour
{



    public float bulletSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.velocity = transform.forward * bulletSpeed;

        var netObject = GetComponent<NetworkObject>();

        StartCoroutine(Despawn());
    }

    [ServerRpc(RequireOwnership = false)]
    public void despawnServerRpc()
    {
        var netObject = GetComponent<NetworkObject>();
        netObject.Despawn();
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(10);
        despawnServerRpc();
    }

}
