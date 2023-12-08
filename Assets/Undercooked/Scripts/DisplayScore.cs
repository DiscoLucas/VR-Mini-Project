using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEditor.Build.Content;

public class DisplayScore : MonoBehaviour
{
    public CostumerOrderManager costumerOrderManager;
    public CorrectDeliveryChecker correctDeliveryChecker;
    public GameManager gameManager;
    public GameObject highScoreObject;
    public GameObject nextSceneButton;
    public TMP_Text highScore;
    public float scoreCounter = 0;
    public float maxScore = 5000;
    public float targetScale = 1;
    public float timeToLerp = 0.5f;
    float scaleModifier = 1;
    private bool animationIsRunning = true;
    public float scaleParameter;
    public float scoreTime;
    public float countPrSecond;

    public void Awake()
    {
        //Finder deliverychecker og correctDeliveryChecker scriptet, som har scoren
        GameObject deliveryChecker = GameObject.FindGameObjectWithTag("DeliveryChecker");
        correctDeliveryChecker = deliveryChecker.GetComponent<CorrectDeliveryChecker>();

        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();

        nextSceneButton.SetActive(false);
        highScoreObject.GetComponent<TMP_Text>().text = highScore.text;
    }

    void Start()
    {
        //Vi starter med at tage scoren der lige er blevet udregnet i correctDeliveryChecker
        highScore.text += correctDeliveryChecker.score.ToString();
        // Og beregner skalerings parameteren alt efter hvor tæt scoren er på den "maxScore" vi har bestemt
        scaleParameter = Mathf.InverseLerp(0, maxScore, correctDeliveryChecker.score);
        //countPrSecond at lige meget hvor høj highscoren er, tager det kun scoreTime at tælle det hele op
        countPrSecond = correctDeliveryChecker.score / scoreTime;
    }

    void Update()
    {
        //Debug.Log(Time.timeSinceLevelLoad);
        // Hvis den visuelle score på skærmen, er mindre end den scoren brugeren har fået
        if (scoreCounter < correctDeliveryChecker.score)
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
        if (scoreCounter >= correctDeliveryChecker.score)
        {
            //Debug.Log("AAAAAAAAA");
            // Bare for at være sikker på vi ender på det rigtige tal
            scoreCounter = correctDeliveryChecker.score;
            //if (animationIsRunning)
            //{
            //    StartCoroutine(LerpFunctionReverse(targetScale - scaleParameter, timeToLerp + 4));
            //    animationIsRunning = false;
            //}
            nextSceneButton.SetActive(true);
        }
    }

    public void ReloadSceneClick()
    {
        gameManager.ReloadScene();
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
    IEnumerator LerpFunctionReverse(float endValue, float duration)
    {
        float time = timeToLerp;
        float startValue = scaleModifier;
        Vector3 startScale = highScoreObject.transform.localScale;

        while (time > duration)
        {
            scaleModifier = Mathf.Lerp(startValue, endValue, time / duration);
            highScoreObject.transform.localScale = startScale * scaleModifier;
            time -= Time.deltaTime;
            yield return null;
        }
        highScoreObject.transform.localScale = startScale * endValue;
        scaleModifier = targetScale - scaleParameter;

    }
}
