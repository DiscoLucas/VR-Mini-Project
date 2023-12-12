using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayDeliveryScore : MonoBehaviour
{
    public CorrectDeliveryChecker correctDeliveryChecker;
    public GameObject highScoreObject;
    public TMP_Text highScore;
    public float deliveryScore;
    public float scoreCounter = 0;
    public float maxScore = 5000;
    public float targetScale = 1;
    public float timeToLerp = 0.5f;
    float scaleModifier = 1;
    private bool animationIsRunning = true;
    public float scaleParameter;
    public float scoreTime;
    public float countPrSecond;
    public float waitTimeBeforeDestroy;

    public void Awake()
    {
        //Finder deliverychecker og correctDeliveryChecker scriptet, som har scoren
        GameObject deliveryChecker = GameObject.FindGameObjectWithTag("DeliveryChecker");
        correctDeliveryChecker = deliveryChecker.GetComponent<CorrectDeliveryChecker>();

        highScoreObject.GetComponent<TMP_Text>().text = highScore.text;
    }

    void Start()
    {
        //Vi starter med at tage scoren der lige er blevet udregnet i correctDeliveryChecker
        deliveryScore = correctDeliveryChecker.newestDeliveryScore;

        // Hvis der rent faktisk er blevet udregnet en score, gøre vi alting, hvis ikke, displayes der bare et rødt kryds
        if (deliveryScore != 0)
        {
            highScore.text += deliveryScore.ToString();
            // Og beregner skalerings parameteren alt efter hvor tæt scoren er på den "maxScore" vi har bestemt
            scaleParameter = Mathf.InverseLerp(0, maxScore, deliveryScore);
            //countPrSecond at lige meget hvor høj highscoren er, tager det kun scoreTime at tælle det hele op
            countPrSecond = deliveryScore / scoreTime;

        }
        else
        {
            highScore.text = "X";
            highScore.color = Color.red;
        }

        // Vi resetter også med det samme newestDeliveryScore for at være helt sikker på der ikke sker bøvl
        correctDeliveryChecker.newestDeliveryScore = 0;

        Destroy(gameObject, scoreTime + waitTimeBeforeDestroy);
    }

    void Update()
    {
        if (deliveryScore != 0)
        {
            //Debug.Log(Time.timeSinceLevelLoad);
            // Hvis den visuelle score på skærmen, er mindre end den scoren brugeren har fået
            if (scoreCounter < deliveryScore)
            {
                // ... bliver den visuelle score større med den beregnede countPrSecond factor svarer til i frame-by-frame tidsperspektiv (hence * Time.deltatime)
                scoreCounter += countPrSecond * Time.deltaTime;
                // Og den visuelle score skaleres gennem en Lerp coroutine
                if (animationIsRunning)
                {
                    StartCoroutine(LerpFunction(targetScale, timeToLerp));
                    animationIsRunning = false;
                }
            }
            // Herefter afrunder vi scoren og den visuelle score text bliver opdateret
            double b = System.Math.Round(scoreCounter, 0);
            highScore.text = b.ToString();

            // Når hele scoren er talt op visuelt, resetter vi scoren til den normale størrelse gennem endnu et lerp
            if (scoreCounter >= deliveryScore)
            {
                //Debug.Log("AAAAAAAAA");
                // Bare for at være sikker på vi ender på det rigtige tal
                scoreCounter = deliveryScore;
            }
        }
    }


    //Use of lerp https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/
    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = scaleModifier;
        Vector3 startScale = highScoreObject.transform.localScale;
        while (time < duration)
        {
            scaleModifier = Mathf.Lerp(startValue, endValue, time / duration);
            highScoreObject.transform.localScale = startScale * scaleModifier;
            time += Time.deltaTime;
            yield return null;
        }
        highScoreObject.transform.localScale = startScale * endValue;
        scaleModifier = targetScale;
    }
}
