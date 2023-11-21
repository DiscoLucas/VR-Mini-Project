using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigReferencess : MonoBehaviour
{
    public static VRRigReferencess singleton;

    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public NetworkPlayerr localPlayer { private set; get; }
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setNetworkPlayer(NetworkPlayerr myPlayer)
    {
        localPlayer = myPlayer;
    }
}
