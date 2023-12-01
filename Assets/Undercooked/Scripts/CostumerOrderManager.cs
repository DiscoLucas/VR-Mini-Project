using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CostumerOrderManager : MonoBehaviour
{
    public static CostumerOrderManager instance { get; private set; }
    //public Ingredients ingredient;
    //public List<Ingredients> ingredientsInOrder;

    public List<List<Ingredients>> orders = new List<List<Ingredients>>();

    public int minNumberOfRandomIngredients = 0;
    public int maxNumberOfRandomIngredients = 2;

    public float orderTimeTimer = 60f;

    public List<float> orderTimes = new List<float>();

    public float waitTimeBetweenOrders;

    bool timeForNewOrder = true;


    // For order receipt spawn
    public GameObject orderPrefab;

    public Transform nextReceiptSpawnpoint;

    public List<GameObject> orderPrefabs = new List<GameObject>();


    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timeForNewOrder = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Som det første, start med at lave en ny bestilling, eller tik timeren til næste bestilling ned en smule
        if (timeForNewOrder)
        {
            StartCoroutine(CountDownToNextOrder());
        }



        // Hvis der er bestillinger tilbage
        if (orders.Count != 0)
        {
            // Tæl ned på timeren for hver aktiv bestilling
            for (int i = 0; i < orders.Count; i++)
            {
                orderTimes[i] -= Time.deltaTime;
                // Hvis en bestillingstimer er nede på 0, fjern den. (Måske også straf med minuspoint? SENERE IMPLEMENTATION)
                if (orderTimes[i] <= 0f)
                {
                    // Her fjernes den korrensponderende orderReceipt fra scenen, samt bestillingen og dens timer
                    RemoveAt(i);
                    //for (int orderNumber = 0; orderNumber < orderPrefabs.Count; orderNumber++)
                    //{
                    //    if (orderPrefabs[orderNumber].gameObject.GetComponent<OrderReceiptUI>().orderNumber == i)
                    //    {
                    //        Destroy(orderPrefabs[orderNumber].gameObject);
                    //        return;
                    //    }
                    //}
                    //orderTimes.RemoveAt(i);
                    //orders.RemoveAt(i);

                }
            }
        }

    }
    /// <summary>
    /// Kaldes når der skal skabes en ny bestilling i systemet
    /// </summary>
    void CreateNewOrder()
    {
        List<Ingredients> ingredientsInOrder = new List<Ingredients>();

        ingredientsInOrder.Add(Ingredients.Bun);
        ingredientsInOrder.Add(Ingredients.Meat);

        for (int i = 0; i < Random.Range(minNumberOfRandomIngredients,maxNumberOfRandomIngredients); i++)
        {
            // Vi gør dette da de tre første Enums er Tomato, Onion og Lettuce
            ingredientsInOrder.Add((Ingredients)Random.Range(0, 2));
            //ingredient = (Ingredients)Random.Range(0, 2);
            //ingredientsInOrder.Add(ingredient);

            //int ingredientChoice = Random.Range(0, 2);
            //if (ingredientChoice == 0)
            //{
            //    ingredientsInOrder.Add(Ingredients.Lettuce);
            //}
            //else if (ingredientChoice == 1)
            //{
            //    ingredientsInOrder.Add(Ingredients.Onion);
            //}
            //else
            //{
            //    ingredientsInOrder.Add(Ingredients.Tomato);
            //}
        }

        ingredientsInOrder.Add(Ingredients.Bun);

        for (int i = 0; i < ingredientsInOrder.Count; i++)
        {
            Debug.Log(ingredientsInOrder[i]);
        }

        // Efter alle ingredienserne er tilføjet til bestillingen, tilføjes den til listen over bestillinger
        orders.Add(ingredientsInOrder);

        Debug.Log("Orders: " + orders.Count);

        // Og en korresponderende timer er tilføjet til timer listen
        orderTimes.Add(orderTimeTimer);

        // KØR IMPLEMENTATION TIL AT SPAWNE EN SEDDEL DER VISER RÆKKEFØLGEN HER
        Instantiate(orderPrefab, nextReceiptSpawnpoint);

    }


    IEnumerator CountDownToNextOrder()
    {
        CreateNewOrder();
        timeForNewOrder = false;
        yield return new WaitForSeconds(waitTimeBetweenOrders);
        timeForNewOrder = true;
    }

    public void RemoveAt(int listIndex)
    {
        // Her fjernes den korrensponderende orderReceipt fra scenen
        for (int i = 0; i < orderPrefabs.Count; i++)
        {
            if (orderPrefabs[i].gameObject.GetComponent<OrderReceiptUI>().orderNumber == listIndex)
            {
                Destroy(orderPrefabs[i].gameObject);
                return;
            }
        }
        orderTimes.RemoveAt(listIndex);
        orders.RemoveAt(listIndex);
    }

}
