using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnappingChecker : MonoBehaviour
{
    public GameObject target = null;
    public Transform snapPos;
    public float snapDistance = 1f;

    // Når en ingrediens rammer skærebrættet
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (target == null)
        {
            // Hvis det er mad og den ikke er grabbed længere
            if (other.tag == "FoodItem")
            {
                // Sæt target til at være at være det objekt og gøre skærebrættet til dens parent
                target = other.gameObject;
                //target.transform.SetParent(gameObject.transform, true);
                
                target.transform.position = snapPos.position;
                target.transform.rotation = snapPos.rotation;
                //target.GetComponent<Rigidbody>().freezeRotation = true;

                //target.transform.localRotation = Quaternion.identity;
                //target.transform.localPosition = Vector3.up;
                Debug.Log("LÅST");
            }
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        if (target.GetComponent<ChoppingAndSnapControl>().isGrabbed == false)//Vector3.Distance(transform.position, target.transform.position) <= snapDistance) )  
        {

            target.transform.position = snapPos.position;
            target.transform.rotation = snapPos.rotation;
        }
        // Reset target og frigør ingrediensen, når ingrediensen bliver grabbed af spilleren
        else if(target.GetComponent<ChoppingAndSnapControl>().isGrabbed == true)
        {
            //target.GetComponent<Rigidbody>().freezeRotation = false;
            target = null;
        }
    }

}
