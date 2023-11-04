using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGhostform : MonoBehaviour
{
    public bool grabbingObject;
    
    float ghostformColliderRadius;

    public GameObject ghostformObject;


    // Start is called before the first frame update
    void Start()
    {
        ghostformColliderRadius = gameObject.GetComponent<SphereCollider>().radius;

        if (ghostformObject.GetComponent<MeshRenderer>().enabled == true)
        {
            ghostformObject.GetComponent<MeshRenderer>().enabled = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (grabbingObject)
        {
            if (tag == "Cuttingboard")
            {
                Vector3 handPosition = gameObject.transform.position;

                RaycastHit[] hits = Physics.SphereCastAll(handPosition, ghostformColliderRadius, transform.forward, 0);

                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.gameObject.tag == "Cuttingboard")
                    {
                        if (ghostformObject.GetComponent<MeshRenderer>().enabled == false)
                        {
                            ghostformObject.GetComponent<MeshRenderer>().enabled = true;
                        }
                        ghostformObject.transform.position = hit.transform.position;
                    }
                }

            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (tag == "Cuttingboard")
        {
            ghostformObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        



    }
}
