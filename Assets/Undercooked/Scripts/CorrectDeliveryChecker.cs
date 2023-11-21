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
                    // Her sammenlignes maden på tallerken med en bestilling i orders listen, hvis de er ens (altså en tallerken er blevet stacked korrekt), fjernes den og vi får et signal til en bestilling er blevet lavet korrekt
                    if (order.SequenceEqual(collisionTarget.GetComponent<StackFoodOnPlate>().ingredientsOnPlate))
                    {
                        
                        // Ikke kig på hvor grimt dene næste linje ser ud
                        // Vi skal bruge en int til at pege på et specifikt level i listen, så vi konverterer både order og ingredient til en int, for at give os pladsen i orderlisten vi er nået til
                        int orderNumber = (int)order[(int)ingredient];

                        Console.WriteLine("Equal list, order is correct!");

                        // Der tilføjes til scoren lig resterende tid i bestillingen
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
