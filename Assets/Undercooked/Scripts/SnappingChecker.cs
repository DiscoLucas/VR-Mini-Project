using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnappingChecker : MonoBehaviour
{
    public GameObject target = null;
    public float snapDistance = 1f;

    // N�r en ingrediens rammer sk�rebr�ttet
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        // Hvis det er mad og den ikke er grabbed l�ngere
        if (other.tag == "FoodItem")
        {
            // S�t target til at v�re at v�re det objekt og g�re sk�rebr�ttet til dens parent
            target = other.gameObject;
            target.transform.SetParent(gameObject.transform, true);
            //target.transform.localRotation = Quaternion.identity;
            //target.transform.localPosition = Vector3.zero;
            Debug.Log("L�ST");
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        if (target.GetComponent<ChoppingAndSnapControl>().isGrabbed == false)//Vector3.Distance(transform.position, target.transform.position) <= snapDistance) )  
        {


            //target.transform.parent = transform;
            //target.transform.localRotation = Quaternion.identity;
            //target.transform.localPosition = Vector3.zero;
        }
        // Reset target og frig�r ingrediensen, n�r ingrediensen bliver grabbed af spilleren
        else if(target.GetComponent<ChoppingAndSnapControl>().isGrabbed == true)
        {
            //gameObject.transform.DetachChildren();
            target.transform.parent = null;
            target = null;
        }
    }

}
