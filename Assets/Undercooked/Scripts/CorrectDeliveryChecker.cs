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

        // Hvis vi stiller en tallerken p� checker-collider'en
        if (collisionTarget.CompareTag("Plate"))
        {
            // K�rer vi igennem listen af bestillinger, som orderManager har genereret
            foreach (var order in orderManager.orders)
            {
                // Vi logger hvilken bestilling vi er p� gennem en super jank m�de, nemlig at tilf�je
                // bestillingen til en anden liste, der kun er til for at checke hvilken order der er korrekt
                ingredientsList.Add(order);


                // For hver bestilling i orders, k�rer vi igennem ingredienslisten for at se om det matcher den p� tallerkenen
                // Her sammenlignes maden p� tallerken med en bestilling i orders listen, hvis de er ens (alts� en tallerken er blevet stacked korrekt), fjernes den og vi f�r et signal til en bestilling er blevet lavet korrekt
                if (order.SequenceEqual(collisionTarget.GetComponent<StackFoodOnPlate>().ingredientsOnPlate))
                {

                    // Virkelig jank m�de at se hvilken bestilling, tallerken matcher. Den t�ller op med en hver gang vi g�r en bestilling ned af listen, men count starter p� en og list-index starter p� nul, s� vi er n�dt til at minus'e med en for at de skal matche.
                    int orderNumber = ingredientsList.Count - 1;

                    //Debug.Log(orderNumber);

                    Debug.Log("Equal list, order is correct!");

                    // Der tilf�jes til scoren lig resterende tid i bestillingen
                    score += orderManager.orderTimes[orderNumber];

                    // Derefter fjernes bestillingen og dens timer fra listerne
                    orderManager.orderTimes.RemoveAt(orderNumber);
                    orderManager.orders.RemoveAt(orderNumber);

                    // Vi resetter ingredientsListen
                    ingredientsList.Clear();

                    // Tallerken-objektet bliver derefter slettet efter en bestemt tid (NOTE: SKAL OGS� KUNNE SLETTE ALLE INGREDIENSER P� OBJEKTET)
                    collisionTarget.gameObject.GetComponent<StackFoodOnPlate>().DestroyPlateAndFood(waitTimeBeforeDeletion);

                    // Derefter returnere vi, s� vi ikke itererer gennem resten af listerne, da vi allerede har f�et en match og dermed ikke risikerer at f� to matches med den samme tallerken, hvis der er to bestillinger der har samme ingredientsliste
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
