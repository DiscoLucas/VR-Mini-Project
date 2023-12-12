using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnappingChecker : MonoBehaviour
{
    public GameObject target = null;
    public Transform snapPos;
    public float snapDistance = 1f;

    // N�r en ingrediens rammer sk�rebr�ttet
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (target == null)
        {
            // Hvis det er mad og den ikke er grabbed l�ngere
            if (other.tag == "FoodItem")
            {
                // S�t target til at v�re at v�re det objekt og g�re sk�rebr�ttet til dens parent
                target = other.gameObject;
                //target.transform.SetParent(gameObject.transform, true);
                
                snapDistance = target.GetComponent<SphereCollider>().radius;


                // S�t ingrediens objektet til at v�re i samme position i sk�rebr�ttet, men l�ftet
                // over sk�rebr�ttet lig med ingredienses colliders radius
                target.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + snapDistance);
                target.transform.rotation = gameObject.transform.rotation;

                //target.GetComponent<Collider>().isTrigger = true;

                //target.transform.position = snapPos.position;
                //target.transform.rotation = snapPos.rotation;
                //target.GetComponent<Rigidbody>().freezeRotation = true;

                //target.transform.localRotation = Quaternion.identity;
                //target.transform.localPosition = Vector3.up;
                Debug.Log("L�ST");
            }
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        if (target.GetComponent<ChoppingAndSnapControl>().isGrabbed == false)//Vector3.Distance(transform.position, target.transform.position) <= snapDistance) )  
        {

            target.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + snapDistance);
            target.transform.rotation = gameObject.transform.rotation;

            //target.transform.position = snapPos.position;
            //target.transform.rotation = snapPos.rotation;
        }
        // Reset target og frig�r ingrediensen, n�r ingrediensen bliver grabbed af spilleren
        else if(target.GetComponent<ChoppingAndSnapControl>().isGrabbed == true)
        {
            //target.GetComponent<Rigidbody>().freezeRotation = false;
            target.GetComponent<Collider>().isTrigger = true;
            target = null;
        }
    }

}
