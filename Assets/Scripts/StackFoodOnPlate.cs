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

    public GameObject socketPrefab;

    public List<GameObject> socketObjects = new List<GameObject> ();


    private void Start()
    {
        Instantiate(socketPrefab, this.transform);
    }


    public void AddFoodItemToSnappingTransforms(GameObject gameObject)
    {
        //if (gameObject.GetComponent<FoodItem>() && !foodObjects.Contains(gameObject.gameObject))
        //{
            // Log hvilken ingredienstype der bliver sat på tallerkenen
            ingredientsOnPlate.Add(gameObject.GetComponent<FoodItem>().ingredientType);


            // Log hvilket gameObject der bliver sat på tallerkenen
            foodObjects.Add(gameObject);

            // Hvis det er den første foodItem, tilføj dens halve radius til snapDistances listen, som bruges til at placere alle ingredienserne forskudt fra hinanden
            if (snapDistances.Count == 0)
            {
                if (gameObject.GetComponent<SphereCollider>() && gameObject.GetComponent<SphereCollider>().enabled)
                {
                    snapDistances.Add(gameObject.GetComponent<SphereCollider>().radius);
                }
                if (gameObject.GetComponent<BoxCollider>() && gameObject.GetComponent<BoxCollider>().enabled)
                {
                    snapDistances.Add(gameObject.GetComponent<BoxCollider>().size.y);
                }
                else
                {
                    Debug.Log("Ingen box/sphere collider?");
                }

            }
            else
            {
                // Ellers tilføjes snapdistance, ligmed den forrige ingredients snapdistance + dette objekts snapdistance
                if (gameObject.GetComponent<SphereCollider>() && gameObject.GetComponent<SphereCollider>().enabled)
                {
                    snapDistances.Add(snapDistances[foodCount - 1] + (gameObject.GetComponent<SphereCollider>().radius / 2));
                }
                if (gameObject.GetComponent<BoxCollider>() && gameObject.GetComponent<BoxCollider>().enabled)
                {
                    snapDistances.Add(snapDistances[foodCount - 1] + (gameObject.GetComponent<BoxCollider>().size.y));
                }
            }

            // Foodcout tælles op for at vise hvilken ingrediensnummer vi er nået til
            foodCount++;


            Instantiate(socketPrefab, this.transform);

        //}
        //else
        //{
        //    Debug.Log("No FoodItem Script on foodItem :((");
        //}
    }





    // Bruges til correctdeliverychecker og skraldespanden, når tallerkenen og ingredienser på tallerkenen skal slettes
    public void DestroyPlateAndFood(float waitTime)
    {
        StartCoroutine(WaitThenDestroy(waitTime));
    }


    // Venter x sekunder og derefter sletter alle foodObjects på tallerkenen og derefter tallerkenen selv
    IEnumerator WaitThenDestroy(float time)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(time);
        foreach (var item in foodObjects)
        {
            Destroy(item);
        }
        Destroy(gameObject);
    }


}
