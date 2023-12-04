using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketPlateInteractor : XRSocketInteractor
{
    public string targetTag;

    public GameObject socketedGameObject;

    StackFoodOnPlate stackFoodOnPlate;

    protected override void Start()
    {
        base.Start();
        stackFoodOnPlate = GetComponentInParent<StackFoodOnPlate>();
        stackFoodOnPlate.socketObjects.Add(gameObject);
        if (stackFoodOnPlate.snapDistances.Count != 0)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + stackFoodOnPlate.snapDistances[stackFoodOnPlate.foodCount - 1], gameObject.transform.position.z);
        }
        
    }

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

        socketedGameObject = args.interactableObject.transform.gameObject;
        

        //Debug.Log(socketedGameObject.GetComponent<FoodItem>().ingredientType);

        stackFoodOnPlate.AddFoodItemToSnappingTransforms(socketedGameObject);

        //Debug.Log("FinishedFoodEnter");

        if (socketedGameObject.GetComponent<SphereCollider>())
        {
            //stackFoodOnPlate.snapDistances.Add(socketedGameObject.GetComponent<SphereCollider>().radius / 2);
            socketedGameObject.GetComponent<SphereCollider>().enabled = false;
        }
        if (socketedGameObject.GetComponent<BoxCollider>())
        {
            //stackFoodOnPlate.snapDistances.Add(socketedGameObject.GetComponent<BoxCollider>().size.y / 2);
            socketedGameObject.GetComponent<BoxCollider>().enabled = false;
        }

        gameObject.GetComponent<BoxCollider>().enabled = false;

    }

}
