using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CorrectDeliveryChecker : MonoBehaviour
{
    public CostumerOrderManager orderManager;

    public List<Ingredients> ingredientsInOrder;
    public List<Ingredients> ingredients;

    public float score;

    private void OnTriggerEnter(Collider collisionTarget)
    {


        if (collisionTarget.CompareTag("Plate"))
        {

            foreach (var order in orderManager.orders)
            {
                foreach (var ingredient in order)
                {
                    // Her sammenlignes maden p� tallerken med en bestilling i orders listen, hvis de er ens (alts� en tallerken er blevet stacked korrekt), fjernes den og vi f�r et signal til en bestilling er blevet lavet korrekt
                    if (order.SequenceEqual(collisionTarget.GetComponent<StackFoodOnPlate>().ingredientsOnPlate))
                    {
                        
                        // Ikke kig p� hvor grimt dene n�ste linje ser ud
                        // Vi skal bruge en int til at pege p� et specifikt level i listen, s� vi konverterer b�de order og ingredient til en int, for at give os pladsen i orderlisten vi er n�et til
                        int orderNumber = (int)order[(int)ingredient];

                        Console.WriteLine("Equal list, order is correct!");

                        // Der tilf�jes til scoren lig resterende tid i bestillingen
                        score += orderManager.orderTimes[orderNumber];

                        // Derefter fjernes bestillingen og dens timer fra listerne
                        orderManager.orderTimes.RemoveAt(orderNumber);
                        orderManager.orders.RemoveAt(orderNumber);
                        Destroy(collisionTarget);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Not Equal list");
                    }
                }
            }



            Destroy(collisionTarget);


        }



    }




}
