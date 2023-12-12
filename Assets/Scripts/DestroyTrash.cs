using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrash : MonoBehaviour
{

    // Used to destroy object in the "Trashcan" and as a "Deathplane" for all items leaving the kitchen


    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
