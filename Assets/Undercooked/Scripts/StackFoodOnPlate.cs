using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackFoodOnPlate : MonoBehaviour
{
    public List<Ingredients> ingredientsOnPlate = new List<Ingredients>();

    public List<GameObject> foodObjects = new List<GameObject>();

    public float snapDistance;

    public int foodCount;

    public int lastFoodCount;

    public List<float> snapDistances = new List<float> ();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FoodItem")
        {
            if (other.GetComponent<FoodItem>() && !foodObjects.Contains(other.gameObject))
            {
                ingredientsOnPlate.Add(other.GetComponent<FoodItem>().ingredientType);

                foodObjects.Add(other.gameObject);

                //snapDistance += other.gameObject.GetComponent<SphereCollider>().radius;

                //other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + snapDistance);
                //other.gameObject.transform.rotation = gameObject.transform.rotation;

                //other.gameObject.GetComponent<Collider>().isTrigger = true;

                Debug.Log(snapDistances.Count);

                if (snapDistances.Count == 0)
                {
                    snapDistances.Add(other.gameObject.GetComponent<SphereCollider>().radius / 2);
                }
                else
                {
                    Debug.Log("Count 1"); 
                    snapDistances.Add(snapDistances[foodCount - 1] + (other.gameObject.GetComponent<SphereCollider>().radius / 2));
                    //lastFoodCount++;
                }

                foodCount++;
            }
            else
            {
                Debug.Log("No FoodItem Script on foodItem :((");
            }



        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ingredientsOnPlate.Count > 0) 
        { 
            for (int i = 0; i < foodObjects.Count; i++)
            {
                foodObjects[i].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + snapDistances[i], gameObject.transform.position.z);
                foodObjects[i].transform.rotation = gameObject.transform.rotation;
            }
        }
    }
}
