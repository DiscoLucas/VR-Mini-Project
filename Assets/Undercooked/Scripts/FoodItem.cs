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

        // disable all chopping objects
        foreach (GameObject item in choppingObjects)
        {
            item.GetComponent<MeshRenderer>().enabled = false;
            item.transform.localPosition = Vector3.zero;
        }
        // enable only first chopping object
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
            // if the item is chopped less than the amount of objects in the array
            if (chopCounter < choppingObjects.Length - 1)
            {
                
                choppingObjects[chopCounter].GetComponent<MeshRenderer>().enabled = false; // disable current chopping object
                chopCounter++;
                choppingObjects[chopCounter].GetComponent<MeshRenderer>().enabled = true; // enable next chopping object
                audioManager.PlayClip(0);

            }
            else
            {
                if (ingredientType == Ingredients.Tomato || ingredientType == Ingredients.Onion)
                {
                    gameObject.tag = "FinishedFoodItem";
                }
                if (ingredientType == Ingredients.Meat) 
                {
                    Debug.Log("spawn burger patty");
                    //Destroy(GetComponent<Collider>()); // remove physics components to avoid collision with burger patty
                    //Destroy(GetComponent<Rigidbody>());
                    Instantiate(choppingObjects[choppingObjects.Length - 1], transform.position, transform.rotation); // spawn raw burger patty
                    audioManager.PlayClip(1);
                    Destroy(gameObject); // game-end self
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
