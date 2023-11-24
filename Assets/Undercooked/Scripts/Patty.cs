using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patty : MonoBehaviour
{
    public GameObject stoveTop;
    public float timeCooked;
    public float finishTime;
    public float finishWindow; // the window of time where the patty is considered cooked, before it burns
    private float burnTimer;
    public Color raw;
    public Color cooked;
    private new Renderer renderer;
    public PattyState currentState;
    private static bool isOnStove;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        raw = renderer.material.color;
        currentState = PattyState.RawState;
    }
    
    public enum PattyState
    {
        RawState,
        CookedState,
        BurntState
    }


    public void OnTriggerStay(Collider other) //when the patty enters the stove
    {
        if (other.gameObject == stoveTop) //if the patty is on the stove
        {
            timeCooked += Time.deltaTime; //add time to the timer
            float cookingProgress = Mathf.Clamp01(timeCooked / finishTime); //clamp the time to the finish time

            Color lerpedColor = Color.Lerp(raw, cooked, cookingProgress); //lerp the color of the patty
            renderer.material.color = lerpedColor;

            if (timeCooked >= finishTime)
            {
                if (timeCooked >= finishTime + finishWindow) //if the patty is cooked
                {
                    SetState(PattyState.BurntState);
                    burnTimer += Time.deltaTime;
                    float burnProgress = Mathf.Clamp01(burnTimer / finishWindow);
                    renderer.material.color = Color.Lerp(cooked, Color.black, burnProgress);
                }
                else
                {
                    SetState(PattyState.CookedState);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }


    /// <summary>
    /// Used to set the state of the patty.
    /// I think it uses the enum PattyState to do this
    /// </summary>
    /// <param name="newState"></param>
    private void SetState(PattyState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case PattyState.CookedState:
                Debug.Log("Patty is cooked");
                break;

            case PattyState.BurntState:
                Debug.Log("Patty is burnt");
                break;
        }
    }


}
