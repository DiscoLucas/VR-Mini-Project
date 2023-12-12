using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour



{
    //The Animationcontroller used for the hands of the player, being able to clench their fist and pinch their fingers. 

    public InputActionProperty grabAnimation;
    public InputActionProperty fistAnimation;

    public Animator handAnimation;
  

    // Update is called once per frame
    void Update()
    {
        float grabValue = grabAnimation.action.ReadValue<float>();

        handAnimation.SetFloat("Grab", grabValue);


        float fistValue = fistAnimation.action.ReadValue<float>();

        handAnimation.SetFloat("Fist", fistValue);


    }
}
