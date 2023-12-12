using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class XRAlyxGrabInteractable : XRGrabInteractable
{
    /*
    This Script is copied directly from the video created by Valem Tutorials 

     https://www.youtube.com/watch?v=WU23Uj1oeh8&list=PLrIb7eNPK270sL7U7174k7xNBLKV70Rlo&index=6

     The comments have later been created to explain the script.

    This script inherits from the XRGrabInteractable class.
    */

    public float velocityThreshold = 2;
    public float jumpAngleInDegree = 60;

    private XRRayInteractor rayInteractor;
    private Vector3 previousPos;
    private Rigidbody interactableRigidbody;
    private bool canJump = true;

    // Here the XRGrabInteractable class's awake is overriden to include getting the Rigidbody. 
    protected override void Awake()
    {
        base.Awake();
        interactableRigidbody = GetComponent<Rigidbody>();
    }


    
    private void Update()
    {
        //Checks to see the if the obejct is selected by the Ray interactor and that it can jump
        if (isSelected && firstInteractorSelecting is XRRayInteractor && canJump)
        {

            // Gets the movement of the hand with the Ray interactor to calculate the "flick" movement
            Vector3 velocity = (rayInteractor.transform.position - previousPos) / Time.deltaTime;
            previousPos = rayInteractor.transform.position;

            // Checks to see if the "flick" movement of the hand is fast enough to release the object and then apply the vector that moves the object to the hand
            if (velocity.magnitude > velocityThreshold)
            {
                Drop();
                interactableRigidbody.velocity = ComputeVelocity();
                canJump = false;
            }
        }
    }


    // This is the formula responsible for calculating the velocity giving to an object when "flicking" the controller. It is based on the difference in position between the grabbed object
    // and the players hand. 
    public Vector3 ComputeVelocity()
    {

        Vector3 diff = rayInteractor.transform.position - transform.position;
        Vector3 diffXZ = new Vector3(diff.x, 0, diff.z);
        float diffXZLength = diffXZ.magnitude;
        float diffYLength = diff.y;

        // The Predefined angle of 60 degress is converted to radians
        float angleInRadian = jumpAngleInDegree * Mathf.Deg2Rad;


        //The jumpspeed of the obejct is calculated by deviding the the length between the object and hand by the angle. While also accounting for the gravity on the rigidbody
        float jumpSpeed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(diffXZLength, 2)
            / (2 * Mathf.Cos(angleInRadian) * Mathf.Cos(angleInRadian) * (diffXZ.magnitude * Mathf.Tan(angleInRadian) - diffYLength)));

        // A vector is then created 
        Vector3 jumpVelocityVector = diffXZ.normalized * Mathf.Cos(angleInRadian) * jumpSpeed + Vector3.up * Mathf.Sin(angleInRadian) * jumpSpeed;

        // And finally returned
        return jumpVelocityVector;
    }


    // Here the OnSelectEntered function is changed as to allow the RayInteractor to flick objects to you, this part also freezes the object in place while being grabbed. 
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRRayInteractor)
        {
            trackPosition = false;
            trackRotation = false;
            throwOnDetach = false;

            rayInteractor = (XRRayInteractor)args.interactorObject;
            previousPos = rayInteractor.transform.position;
            canJump = true;
        }
        else
        {
            trackPosition = true;
            trackRotation = true;
            throwOnDetach = true;
        }

        base.OnSelectEntered(args);
    }
}

