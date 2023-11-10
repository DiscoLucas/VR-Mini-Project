using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayerr : NetworkBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private Transform head;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
}
