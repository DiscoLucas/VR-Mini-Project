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

    public int orderNumber;

    // Start is called before the first frame update
    void Start()
    {
        
        receiptText_TMP = GetComponentInChildren<TMP_Text>();
        costumerOrderManager = GetComponentInParent<CostumerOrderManager>();

        // Vi tilf�jer dette gameObject til listen over orderPrefabs i costumerOrderManager, skal bruges til n�r den slettes igen
        costumerOrderManager.orderPrefabs.Add(gameObject);

        // finder ud af hvilken bestilling vi har med at g�re n�r denne receipt bliver instantieret, s� vi ved hvilken en vi skal kalde n�r orderen er complete
        orderNumber = costumerOrderManager.orders.Count - 1;

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
