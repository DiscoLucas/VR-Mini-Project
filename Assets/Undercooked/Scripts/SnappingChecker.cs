using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnappingChecker : MonoBehaviour
{
    public GameObject target = null;
    public float snapDistance = 1f;

    // Når en ingrediens rammer skærebrættet
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        // Hvis det er mad og den ikke er grabbed længere
        if (other.tag == "FoodItem")
        {
            // Sæt target til at være at være det objekt og gøre skærebrættet til dens parent
            target = other.gameObject;
            target.transform.SetParent(gameObject.transform, true);
            //target.transform.localRotation = Quaternion.identity;
            //target.transform.localPosition = Vector3.zero;
            Debug.Log("LÅST");
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
        // Reset target og frigør ingrediensen, når ingrediensen bliver grabbed af spilleren
        else if(target.GetComponent<ChoppingAndSnapControl>().isGrabbed == true)
        {
            //gameObject.transform.DetachChildren();
            target.transform.parent = null;
            target = null;
        }
    }

}
