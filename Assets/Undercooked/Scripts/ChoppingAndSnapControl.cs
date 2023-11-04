using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingAndSnapControl : MonoBehaviour
{
    public bool isGrabbed = false;

    public Color[] colors;
    public Material material;


    public GameObject[] choppingObjects;



    int chopCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        material.color = colors[chopCounter];

        foreach (GameObject item in choppingObjects)
        {
            item.GetComponent<MeshRenderer>().enabled = false;
        }
        choppingObjects[0].GetComponent<MeshRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Knife")
        {
            if (chopCounter < choppingObjects.Length)
            {
                //gameObject.GetComponent<Material>().color = colors[chopCounter];
                //chopCounter++;
                //material.color = colors[chopCounter];

                choppingObjects[chopCounter].GetComponent<MeshRenderer>().enabled = false;
                chopCounter++;
                choppingObjects[chopCounter].GetComponent<MeshRenderer>().enabled = true;

            }
            else
            {
                Debug.Log("færdigt chopped");
            }

        }
    }



    public void isGrabbedControl(bool Grabbed)
    {
        isGrabbed = Grabbed;
    }


}
