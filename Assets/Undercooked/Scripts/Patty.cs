using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patty : MonoBehaviour
{
    public GameObject stoveTop;
    public float timeCooked;
    public float finishTime;
    public float finishWindow; // the window of time where the patty is considered cooked, before it burns
    public Color raw;
    public Color cooked;
    private new Renderer renderer;
    public PattyState currentState;

    // Patty enum states
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
                }
                else
                {
                    SetState(PattyState.CookedState);
                }
            }
            //gameObject.GetComponent<Renderer>().material.color = Color.Lerp(raw, cooked, finishTime); //change the color of the patty

            /*if (timeCooked >= finishTime)
            {
                Debug.Log("Patty is cooked");
                if (timeCooked >= finishTime + finishWindow) //if the patty is cooked
                {
                    Debug.Log("Patty is burnt");
                    gameObject.GetComponent<Renderer>().material.color = Color.Lerp(cooked, Color.black, 5f); //
                }
            }
            */
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Color raw = renderer.material.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
    }
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
                renderer.material.color = Color.Lerp(cooked, Color.black, 5f);
                break;
        }
    }
}
