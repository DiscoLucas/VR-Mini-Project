using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour



{


    public InputActionProperty grabAnimation;
    public InputActionProperty fistAnimation;

    public Animator handAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float grabValue = grabAnimation.action.ReadValue<float>();

        handAnimation.SetFloat("Grab", grabValue);

        Debug.Log("Grab percent" + grabValue);

        float fistValue = fistAnimation.action.ReadValue<float>();

        handAnimation.SetFloat("Fist", fistValue);

        Debug.Log("Fisting percent" + fistValue);


    }
}
