using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CostumerOrderManager : MonoBehaviour
{

    //public Ingredients ingredient;
    //public List<Ingredients> ingredientsInOrder;

    public List<List<Ingredients>> orders = new List<List<Ingredients>>();

    public int minNumberOfRandomIngredients = 0;
    public int maxNumberOfRandomIngredients = 2;

    public float orderTimeTimer = 60f;

    public List<float> orderTimes = new List<float>();

    public float waitTimeBetweenOrders;

    bool timeForNewOrder = true;

    // Start is called before the first frame update
    void Start()
    {
        timeForNewOrder = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Som det f�rste, start med at lave en ny bestilling, eller tik timeren til n�ste bestilling ned en smule
        if (timeForNewOrder)
        {
            StartCoroutine(CountDownToNextOrder());
        }



        // Hvis der er bestillinger tilbage
        if (orders.Count != 0)
        {
            // T�l ned p� timeren for hver aktiv bestilling
            for (int i = 0; i < orders.Count; i++)
            {
                orderTimes[i] -= Time.deltaTime;
                // Hvis en bestillingstimer er nede p� 0, fjern den. (M�ske ogs� straf med minuspoint? SENERE IMPLEMENTATION)
                if (orderTimes[i] <= 0f)
                {
                    orderTimes.RemoveAt(i);
                    orders.RemoveAt(i);
                }
            }
        }

    }
    /// <summary>
    /// Kaldes n�r der skal skabes en ny bestilling i systemet
    /// </summary>
    void CreateNewOrder()
    {
        List<Ingredients> ingredientsInOrder = new List<Ingredients>();

        ingredientsInOrder.Add(Ingredients.Bun);
        ingredientsInOrder.Add(Ingredients.Meat);

        for (int i = 0; i < Random.Range(minNumberOfRandomIngredients,maxNumberOfRandomIngredients); i++)
        {
            // Vi g�r dette da de tre f�rste Enums er Tomato, Onion og Lettuce
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

        // Efter alle ingredienserne er tilf�jet til bestillingen, tilf�jes den til listen over bestillinger
        orders.Add(ingredientsInOrder);

        Debug.Log("Orders: " + orders.Count);

        // Og en korresponderende timer er tilf�jet til timer listen
        orderTimes.Add(orderTimeTimer);

        // K�R IMPLEMENTATION TIL AT SPAWNE EN SEDDEL DER VISER R�KKEF�LGEN HER

    }


    IEnumerator CountDownToNextOrder()
    {
        CreateNewOrder();
        timeForNewOrder = false;
        yield return new WaitForSeconds(waitTimeBetweenOrders);
        timeForNewOrder = true;
    }



}