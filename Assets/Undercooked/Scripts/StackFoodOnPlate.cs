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
        if (other.tag == "FinishedFoodItem")
        {
            // Hvis de er en FoodItem (har scriptet) og ikke er på et gameobject som allerede er i listen af gameobjects på tallerken
            if (other.GetComponent<FoodItem>() && !foodObjects.Contains(other.gameObject))
            {
                // Log hvilken ingredienstype der bliver sat på tallerkenen
                ingredientsOnPlate.Add(other.GetComponent<FoodItem>().ingredientType);


                // Log hvilket gameObject der bliver sat på tallerkenen
                foodObjects.Add(other.gameObject);

                //snapDistance += other.gameObject.GetComponent<SphereCollider>().radius;

                //other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + snapDistance);
                //other.gameObject.transform.rotation = gameObject.transform.rotation;

                //other.gameObject.GetComponent<Collider>().isTrigger = true;

                Debug.Log(snapDistances.Count);

                // Hvis det er den første foodItem, tilføj dens halve radius til snapDistances listen, som bruges til at placere alle ingredienserne forskudt fra hinanden
                if (snapDistances.Count == 0)
                {
                    snapDistances.Add(other.gameObject.GetComponent<SphereCollider>().radius / 2);
                }
                else
                {
                    // Ellers tilføjes snapdistance, ligmed den forrige ingredients snapdistance + dette objekts snapdistance
                    snapDistances.Add(snapDistances[foodCount - 1] + (other.gameObject.GetComponent<SphereCollider>().radius / 2));
                }

                // Foodcout tælles op for at vise hvilken ingrediensnummer vi er nået til
                foodCount++;
            }
            else
            {
                Debug.Log("No FoodItem Script on foodItem :((");
            }



        }
    }

    // Bruges til correctdeliverychecker og skraldespanden, når tallerkenen og ingredienser på tallerkenen skal slettes
    public void DestroyPlateAndFood(float waitTime)
    {
        StartCoroutine(WaitThenDestroy(waitTime));
    }

    // Update is called once per frame
    void Update()
    {
        // Her kører vi igennem listen af foodObjects på tallerkenen og låser dem fast til tallerkenen, forskudt fra hinanden via snapDistance listen
        if (ingredientsOnPlate.Count > 0) 
        { 
            for (int i = 0; i < foodObjects.Count; i++)
            {
                foodObjects[i].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + snapDistances[i], gameObject.transform.position.z);
                foodObjects[i].transform.rotation = gameObject.transform.rotation;
            }
        }
    }

    // Venter x sekunder og derefter sletter alle foodObjects på tallerkenen og derefter tallerkenen selv
    IEnumerator WaitThenDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (var item in foodObjects)
        {
            Destroy(item);
        }
        Destroy(gameObject);
    }


}
