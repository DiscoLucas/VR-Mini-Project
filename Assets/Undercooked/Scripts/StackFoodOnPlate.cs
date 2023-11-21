using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackFoodOnPlate : MonoBehaviour
{
    public List<Ingredients> ingredientsOnPlate = new List<Ingredients>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FoodItem")
        {
            if (other.GetComponent<FoodItem>())
            {
                ingredientsOnPlate.Add(other.GetComponent<FoodItem>().ingredientType);
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
        
    }
}
