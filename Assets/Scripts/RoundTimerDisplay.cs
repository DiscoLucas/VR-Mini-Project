using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundTimerDisplay : MonoBehaviour
{
    public GameObject timerObject;


    public TMP_Text timeText;

    public CostumerOrderManager costumerOrderManager;

    // Start is called before the first frame update
    void Start()
    {
        timerObject.GetComponent<TMP_Text>().text = timeText.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (costumerOrderManager.roundTimer > 0)
        {
            DisplayTime(costumerOrderManager.roundTimer);
        }
        else
        {

            Debug.Log("Time has run out!");
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }



}
