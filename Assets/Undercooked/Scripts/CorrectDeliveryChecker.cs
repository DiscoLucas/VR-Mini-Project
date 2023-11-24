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

    public float waitTimeBeforeDeletion;

    public List<List<Ingredients>> ingredientsList = new List<List<Ingredients>>();

    private void OnTriggerEnter(Collider collisionTarget)
    {

        // Hvis vi stiller en tallerken på checker-collider'en
        if (collisionTarget.CompareTag("Plate"))
        {
            // Kører vi igennem listen af bestillinger, som orderManager har genereret
            foreach (var order in orderManager.orders)
            {
                // Vi logger hvilken bestilling vi er på gennem en super jank måde, nemlig at tilføje
                // bestillingen til en anden liste, der kun er til for at checke hvilken order der er korrekt
                ingredientsList.Add(order);


                // For hver bestilling i orders, kører vi igennem ingredienslisten for at se om det matcher den på tallerkenen
                // Her sammenlignes maden på tallerken med en bestilling i orders listen, hvis de er ens (altså en tallerken er blevet stacked korrekt), fjernes den og vi får et signal til en bestilling er blevet lavet korrekt
                if (order.SequenceEqual(collisionTarget.GetComponent<StackFoodOnPlate>().ingredientsOnPlate))
                {

                    // Virkelig jank måde at se hvilken bestilling, tallerken matcher. Den tæller op med en hver gang vi går en bestilling ned af listen, men count starter på en og list-index starter på nul, så vi er nødt til at minus'e med en for at de skal matche.
                    int orderNumber = ingredientsList.Count - 1;

                    //Debug.Log(orderNumber);

                    Debug.Log("Equal list, order is correct!");

                    // Der tilføjes til scoren lig resterende tid i bestillingen
                    score += orderManager.orderTimes[orderNumber];

                    // Derefter fjernes bestillingen og dens timer fra listerne
                    orderManager.orderTimes.RemoveAt(orderNumber);
                    orderManager.orders.RemoveAt(orderNumber);

                    // Vi resetter ingredientsListen
                    ingredientsList.Clear();

                    // Tallerken-objektet bliver derefter slettet efter en bestemt tid (NOTE: SKAL OGSÅ KUNNE SLETTE ALLE INGREDIENSER PÅ OBJEKTET)
                    collisionTarget.gameObject.GetComponent<StackFoodOnPlate>().DestroyPlateAndFood(waitTimeBeforeDeletion);

                    // Derefter returnere vi, så vi ikke itererer gennem resten af listerne, da vi allerede har fået en match og dermed ikke risikerer at få to matches med den samme tallerken, hvis der er to bestillinger der har samme ingredientsliste
                    return;
                }
                else
                {
                    Debug.Log("Not Equal list");
                }
            }

            // Hvis den ikke matcher listen, reset ingredientsListen og slet tallerken objektet
            ingredientsList.Clear();
            collisionTarget.gameObject.GetComponent<StackFoodOnPlate>().DestroyPlateAndFood(waitTimeBeforeDeletion);


        }



    }




}
