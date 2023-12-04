using UnityEngine;

public class Patty : MonoBehaviour
{
    [SerializeField] float timeCooked;
    public float finishTime;
    public float finishWindow; // the window of time where the patty is considered cooked, before it burns
    private float burnTimer;
    public Color raw;
    public Color cooked;
    private new Renderer renderer;
    public PattyState currentState;
    [SerializeField] AudioManager audioManager;
    private ParticleSystem ps;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        raw = renderer.material.color;
        currentState = PattyState.RawState;
        //var ps = GetComponent<ParticleSystem>();
        ps = GetComponent<ParticleSystem>();
        if (ps.isPlaying)
        {
            ps.Stop();
        }
    }
    
    public enum PattyState
    {
        RawState,
        CookedState,
        BurntState
    }


    public void OnTriggerStay(Collider other) //when the patty enters the stove
    {
        if (other.CompareTag("Burner")) //if the patty is on the stove
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Burner"))
        {
            // turn on particle system for smoke
            var ps = GetComponent<ParticleSystem>();
            ps.Play();

            // play sizzling sound
            audioManager.PlayClip(0);
            //AudioSource audio = GetComponent<AudioSource>();
            //audio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Burner"))
        {
            var ps = GetComponent<ParticleSystem>();
            ps.Stop();

            // stop sizzling sound
            audioManager.StopAudio(0);
            //AudioSource audio = GetComponent<AudioSource>();
            //audio.Stop();
        }
    }


    /// <summary>
    /// Used to set the state of the patty.
    /// I think it uses the enum PattyState to do this
    /// </summary>
    /// <param name="newState"></param>
    private void SetState(PattyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            var main = ps.main;
            switch (currentState)
            {
                case PattyState.CookedState:
                    Debug.Log("Patty is cooked");
                    gameObject.tag = "FinishedFoodItem";
                    audioManager.PlayClip(1);

                    break;

                case PattyState.BurntState:
                    Debug.Log("Patty is burnt");
                    gameObject.tag = "FoodItem";
                    main.startColor = Color.grey;
                    break;
            }
        }
    }
}
