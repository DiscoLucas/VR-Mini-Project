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

    [SerializeField] int amountsOfChops;

    public GameObject foodPrefab;

    public Ingredients ingredientType;


}
