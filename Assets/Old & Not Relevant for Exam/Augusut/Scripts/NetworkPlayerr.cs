using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class NetworkPlayerr : NetworkBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private Transform head;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    NetworkVariable<int> score = new NetworkVariable<int>();

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            root.position = VRRigReferencess.singleton.root.position;
            root.rotation = VRRigReferencess.singleton.root.rotation;

            head.position = VRRigReferencess.singleton.head.position;
            head.rotation = VRRigReferencess.singleton.head.rotation;

            leftHand.position = VRRigReferencess.singleton.leftHand.position;
            leftHand.rotation = VRRigReferencess.singleton.leftHand.rotation;

            rightHand.position = VRRigReferencess.singleton.rightHand.position;
            rightHand.rotation = VRRigReferencess.singleton.rightHand.rotation;

        }
    }

    public void IncrementScore()
    {
        IncrementScoreServerRpc();
    }


    [ServerRpc]
    public void IncrementScoreServerRpc()
    {
        score.Value++;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            VRRigReferencess.singleton.setNetworkPlayer(this);
        }
        score.OnValueChanged += scoreChanged;
    }

    void scoreChanged(int oldValue, int currentValue)
    {
        print(currentValue);
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        score.OnValueChanged -= scoreChanged;
    }
}
