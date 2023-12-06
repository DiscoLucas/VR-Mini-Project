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

        // Hvis der rent faktisk er blevet udregnet en score, g�re vi alting, hvis ikke, displayes der bare et r�dt kryds
        if (deliveryScore != 0)
        {
            highScore.text += deliveryScore.ToString();
            // Og beregner skalerings parameteren alt efter hvor t�t scoren er p� den "maxScore" vi har bestemt
            scaleParameter = Mathf.InverseLerp(0, maxScore, deliveryScore);
            //countPrSecond at lige meget hvor h�j highscoren er, tager det kun scoreTime at t�lle det hele op
            countPrSecond = deliveryScore / scoreTime;

        }
        else
        {
            highScore.text = "X";
            highScore.color = Color.red;
        }

        // Vi resetter ogs� med det samme newestDeliveryScore for at v�re helt sikker p� der ikke sker b�vl
        correctDeliveryChecker.newestDeliveryScore = 0;

        Destroy(gameObject, scoreTime + waitTimeBeforeDestroy);
    }

    void Update()
    {
        if (deliveryScore != 0)
        {
            //Debug.Log(Time.timeSinceLevelLoad);
            // Hvis den visuelle score p� sk�rmen, er mindre end den scoren brugeren har f�et
            if (scoreCounter < deliveryScore)
            {
                // ... bliver den visuelle score st�rre med den beregnede countPrSecond factor svarer til i frame-by-frame tidsperspektiv (hence * Time.deltatime)
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

            // N�r hele scoren er talt op visuelt, resetter vi scoren til den normale st�rrelse gennem endnu et lerp
            if (scoreCounter >= deliveryScore)
            {
                //Debug.Log("AAAAAAAAA");
                // Bare for at v�re sikker p� vi ender p� det rigtige tal
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
