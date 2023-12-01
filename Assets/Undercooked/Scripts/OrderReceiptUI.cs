using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class OrderReceiptUI : MonoBehaviour
{

    public CostumerOrderManager costumerOrderManager;

    //public List<TMP_Text> ingredients_Text = new List<TMP_Text>();

    public TMP_Text receiptText_TMP;

    public int orderNumber;

    public float orderTimer;

    public Image timerImage;

    public List<Color> colorList = new List<Color>();

    // Start is called before the first frame update
    void Start()
    {

        receiptText_TMP = GetComponentInChildren<TMP_Text>();
        costumerOrderManager = GetComponentInParent<CostumerOrderManager>();

        orderTimer = costumerOrderManager.orderTimeTimer;

        // Vi tilføjer dette gameObject til listen over orderPrefabs i costumerOrderManager, skal bruges til når den slettes igen
        costumerOrderManager.orderPrefabs.Add(gameObject);

        // finder ud af hvilken bestilling vi har med at gøre når denne receipt bliver instantieret, så vi ved hvilken en vi skal kalde når orderen er complete
        //orderNumber = costumerOrderManager.orders.Count - 1;
        orderNumber = costumerOrderManager.orderPrefabs.IndexOf(gameObject);

        for (int i = 0; i < costumerOrderManager.orders[costumerOrderManager.orders.Count - 1].Count; i++)
        {
            string ingredientString = (i+1) + ". " + costumerOrderManager.orders[costumerOrderManager.orders.Count - 1][i].ToString();

            receiptText_TMP.text += ingredientString + Environment.NewLine;
        }
    }

    // Update is called once per frame
    void Update()
    {
        orderTimer -= Time.deltaTime;

        //orderNumber = costumerOrderManager.orderPrefabs.IndexOf(gameObject);

        timerImage.fillAmount = Mathf.InverseLerp(0, costumerOrderManager.orderTimeTimer, orderTimer);
        if (timerImage.fillAmount <= 0.5f && timerImage.fillAmount > 0.2f)
        {
            timerImage.color = colorList[1];
        }
        else if(timerImage.fillAmount <= 0.2f)
        {
            timerImage.color = colorList[2];
        }
        else
        {
            timerImage.color = colorList[0];
        }
    }
}
