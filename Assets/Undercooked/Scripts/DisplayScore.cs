using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public CostumerOrderManager costumerOrderManager;
    public CorrectDeliveryChecker correctDeliveryChecker;
    public GameObject highScoreObject;
    //public GameObject nextSceneButton;
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
        //Finder Minigame Manageren og scriptet til minigame scene management
        //GameObject minigameManager = GameObject.FindGameObjectWithTag("MinigameManager");
        //minigameOrderSelect = minigameManager.GetComponent<MinigameOrderSelect>();

        //costumerOrderManager = GetComponent<CostumerOrderManager>();
        //correctDeliveryChecker = GetComponent<CorrectDeliveryChecker>();

        //nextSceneButton.SetActive(false);
        highScoreObject.GetComponent<TMP_Text>().text = highScore.text;
    }

    void Start()
    {
        //Vi starter med at tage scoren der lige er blevet udregnet i correctDeliveryChecker
        highScore.text += correctDeliveryChecker.score.ToString();
        // Og beregner skalerings parameteren alt efter hvor t�t scoren er p� den "maxScore" vi har bestemt
        scaleParameter = Mathf.InverseLerp(0, maxScore, correctDeliveryChecker.score);
        //countPrSecond at lige meget hvor h�j highscoren er, tager det kun scoreTime at t�lle det hele op
        countPrSecond = correctDeliveryChecker.score / scoreTime;
    }

    void Update()
    {
        Debug.Log(Time.timeSinceLevelLoad);
        // Hvis den visuelle score p� sk�rmen, er mindre end den scoren brugeren har f�et
        if (scoreCounter < correctDeliveryChecker.score)
        {
            // ... bliver den visuelle score st�rre med den beregnede countPrSecond factor svarer til i frame-by-frame tidsperspektiv (hence * Time.deltatime)
            scoreCounter += countPrSecond * Time.deltaTime;
            // Og den visuelle score skaleres gennem en Lerp coroutine
            StartCoroutine(LerpFunction(targetScale, timeToLerp));
        }
        // Herefter afrunder vi scoren og den visuelle score text bliver opdateret
        double b = System.Math.Round(scoreCounter, 0);
        highScore.text = b.ToString();

        // N�r hele scoren er talt op visuelt, resetter vi scoren til den normale st�rrelse gennem endnu et lerp
        if (scoreCounter >= correctDeliveryChecker.score)
        {
            // Bare for at v�re sikker p� vi ender p� det rigtige tal
            scoreCounter = correctDeliveryChecker.score;
            if (animationIsRunning)
            {
                StartCoroutine(LerpFunctionReverse(targetScale - scaleParameter, timeToLerp + 4));
                animationIsRunning = false;
            }
            //nextSceneButton.SetActive(true);
        }
    }

    //public void HighScoreRead()
    //{
    //    if (StateManager.recipeNumber + 1 < 3)
    //    {
    //        StateManager.recipeNumber++;
    //    }
    //    else
    //    {
    //        minigameOrderSelect.doneWithAllMinigames = true;
    //    }
    //    minigameOrderSelect.highScoreRead = true;
    //}

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
