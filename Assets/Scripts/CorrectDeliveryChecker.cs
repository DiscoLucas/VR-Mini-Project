using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

public class CorrectDeliveryChecker : XRSocketInteractor
{

    /// <summary>
    /// Using Sockets, this scripts checks the order and content of FoodItems located on the plate compared to the orders from the CustomerOrderManager.
    /// And then on a succesful order calculates the score of the delivery from how much time is left of the order. 
    /// </summary>

    public CostumerOrderManager orderManager;
    public AudioManager audioManager;

    public List<Ingredients> ingredientsInOrder;
    public List<Ingredients> ingredients;

    public float score;
    public float newestDeliveryScore;

    public float waitTimeBeforeDeletion;

    public List<List<Ingredients>> ingredientsList = new List<List<Ingredients>>();

    // DISPLAY SPAWN
    public GameObject displayObject;
    public Transform displaySpawn;


    // BUTTON PRESS ACTIVATION ATTEMPT

    public ButtonForDelivery buttonForDelivery;

    public GameObject plateObject;


    public string targetTag;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.tag == targetTag;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.tag == targetTag;
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (args.interactableObject.transform.gameObject.GetComponent<StackFoodOnPlate>())
        {
            // Siger til knappen at der er en tallerken p� delivery
            buttonForDelivery.FoodOnDelivery();

            plateObject = args.interactableObject.transform.gameObject;

        }

    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (args.interactableObject.transform.gameObject.GetComponent<StackFoodOnPlate>())
        {
            // Siger til knappen at der er fjernet en tallerken p� delivery
            buttonForDelivery.FoodOffDelivery();

            plateObject = null;
        }

    }




    public void CheckDelivery()
    {
        List<List<Ingredients>> orderManagerOrders = CostumerOrderManager.instance.orders;

        // Hvis vi stiller en tallerken p� checker-collider'en
        if (plateObject.CompareTag("Plate"))
        {
            // K�rer vi igennem listen af bestillinger, som orderManager har genereret
            foreach (var order in orderManagerOrders)
            {
                // Vi logger hvilken bestilling vi er p� gennem en super jank m�de, nemlig at tilf�je
                // bestillingen til en anden liste, der kun er til for at checke hvilken order der er korrekt
                ingredientsList.Add(order);

                Debug.Log("Order " + order);
                Debug.Log("Plate " + plateObject.GetComponent<StackFoodOnPlate>().ingredientsOnPlate);

                // For hver bestilling i orders, k�rer vi igennem ingredienslisten for at se om det matcher den p� tallerkenen
                // Her sammenlignes maden p� tallerken med en bestilling i orders listen, hvis de er ens (alts� en tallerken er blevet stacked korrekt), fjernes den og vi f�r et signal til en bestilling er blevet lavet korrekt
                if (order.SequenceEqual(plateObject.GetComponent<StackFoodOnPlate>().ingredientsOnPlate))
                {

                    // Virkelig jank m�de at se hvilken bestilling, tallerken matcher. Den t�ller op med en hver gang vi g�r en bestilling ned af listen, men count starter p� en og list-index starter p� nul, s� vi er n�dt til at minus'e med en for at de skal matche.
                    int orderNumber = ingredientsList.Count - 1;

                    //Debug.Log(orderNumber);

                    Debug.Log("Equal list, order is correct!");

                    // Der tilf�jes til scoren lig resterende tid i bestillingen
                    //score += orderManager.orderTimes[orderNumber];
                    score += CostumerOrderManager.instance.orderTimes[orderNumber];

                    // S� g�r vi klar til at display'e den individuelle score for ordren der blev afleveret
                    newestDeliveryScore = CostumerOrderManager.instance.orderTimes[orderNumber];
                    Instantiate(displayObject, displaySpawn);

                    // Her fjernes den korrensponderende orderReceipt fra scenen
                    // og derefter fjernes bestillingen og dens timer fra listerne
                    //orderManager.RemoveAt(orderNumber);
                    CostumerOrderManager.instance.RemoveAt(orderNumber);

                    //for (int i = 0; i < orderManager.orderPrefabs.Count; i++)
                    //{
                    //    if (orderManager.orderPrefabs[i].gameObject.GetComponent<OrderReceiptUI>().orderNumber == orderNumber)
                    //    {
                    //        Destroy(orderManager.orderPrefabs[i].gameObject);
                    //        return;
                    //    }
                    //}

                    //// Derefter fjernes bestillingen og dens timer fra listerne
                    //orderManager.orderTimes.RemoveAt(orderNumber);
                    //orderManager.orders.RemoveAt(orderNumber);
                    audioManager.PlayClip(1);


                    // Vi resetter ingredientsListen
                    ingredientsList.Clear();

                    // Tallerken-objektet bliver derefter slettet efter en bestemt tid (NOTE: SKAL OGS� KUNNE SLETTE ALLE INGREDIENSER P� OBJEKTET)
                    plateObject.GetComponent<StackFoodOnPlate>().DestroyPlateAndFood(waitTimeBeforeDeletion);

                    // Derefter returnere vi, s� vi ikke itererer gennem resten af listerne, da vi allerede har f�et en match og dermed ikke risikerer at f� to matches med den samme tallerken, hvis der er to bestillinger der har samme ingredientsliste
                    return;
                }
                else
                {
                    Debug.Log("Not Equal list");
                }
            }
            audioManager.PlayClip(0);
            Instantiate(displayObject, displaySpawn);

            // Hvis den ikke matcher listen, reset ingredientsListen og slet tallerken objektet
            ingredientsList.Clear();
            plateObject.GetComponent<StackFoodOnPlate>().DestroyPlateAndFood(waitTimeBeforeDeletion);

            plateObject = null;

            // Siger til knappen at der er fjernet en tallerken p� delivery og den derfor ikke skal v�re aktiv
            buttonForDelivery.FoodOffDelivery();

        }
    }



   

}
