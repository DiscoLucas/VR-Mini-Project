using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OrderReceiptUI : MonoBehaviour
{

    public CostumerOrderManager costumerOrderManager;

    //public List<TMP_Text> ingredients_Text = new List<TMP_Text>();

    public TMP_Text receiptText_TMP;

    // Start is called before the first frame update
    void Start()
    {
        
        receiptText_TMP = GetComponentInChildren<TMP_Text>();
        costumerOrderManager = GetComponentInParent<CostumerOrderManager>();

        //foreach (var ingredients in costumerOrderManager.orders[costumerOrderManager.orders.Count - 1])
        //{
        //    string ingredientString = ingredients.ToString();
        //    //ingredientString.
        //    ingredients_Text.Add();
        //}

        for (int i = 0; i < costumerOrderManager.orders[costumerOrderManager.orders.Count - 1].Count; i++)
        {
            string ingredientString = costumerOrderManager.orders[costumerOrderManager.orders.Count - 1][i].ToString();

            receiptText_TMP.text += ingredientString + Environment.NewLine;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
