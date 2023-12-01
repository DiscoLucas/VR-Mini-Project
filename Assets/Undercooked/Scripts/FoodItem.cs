using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum Ingredients
{
    Tomato,
    Onion,
    Lettuce,
    Meat,
    Bun
}

//[CustomEditor(typeof(FoodItem))]
//public class FoodItemEditor : Editor
//{
    //public override void OnInspectorGUI()
    //{
    //    // If we call base the default inspector will get drawn too.
    //    // Remove this line if you don't want that to happen.
    //    base.OnInspectorGUI();

    //    FoodItem foodItemClass = target as FoodItem;

    //    target.isChoppable = EditorGUILayout.Toggle("isChoppable", target.isChoppable);

    //    if (target.isChoppable)
    //    {
    //        target.amountsOfChops = EditorGUILayout.FloatField("amountsOfChops:", target.amountsOfChops);

    //    }
    //}

public class FoodItem : MonoBehaviour
{

    [SerializeField] bool isChoppable;
    [SerializeField] bool isGrillable;

    //[SerializeField] int amountsOfChops;

    public GameObject foodPrefab;

    public Ingredients ingredientType;

    public bool isGrabbed = false;

    AudioManager audioManager;


    // ONLY FOR CHOPPABLE OBJEKTER
    public GameObject[] choppingObjects;
    int chopCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        //gameObject.tag = "FoodItem";

        audioManager = GetComponent<AudioManager>();


        foreach (GameObject item in choppingObjects)
        {
            item.GetComponent<MeshRenderer>().enabled = false;
        }
        if (isChoppable)
        {
           choppingObjects[0].GetComponent<MeshRenderer>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Knife" && isChoppable)
        {
            if (chopCounter < choppingObjects.Length - 1)
            {

                choppingObjects[chopCounter].GetComponent<MeshRenderer>().enabled = false;
                chopCounter++;
                choppingObjects[chopCounter].GetComponent<MeshRenderer>().enabled = true;
                audioManager.PlayClip(0);

            }
            else
            {
                if (ingredientType == Ingredients.Tomato || ingredientType == Ingredients.Onion)
                {
                    gameObject.tag = "FinishedFoodItem";
                }
                audioManager.PlayClip(1);
                Debug.Log("færdigt chopped");
            }

        }
    }



    public void isGrabbedControl(bool Grabbed)
    {
        isGrabbed = Grabbed;
    }

}
