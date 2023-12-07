using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForDelivery : MonoBehaviour
{
    public BoxCollider boxCollider;
    public Material material;
    public Color activeColor;
    public Color inactiveColor;
    public CorrectDeliveryChecker correctDeliveryChecker;
    public AudioManager audioManager;

    bool buttonPressCooldown;

    public float cooldownTime;

    private void Start()
    {
        //Finder deliverychecker og correctDeliveryChecker scriptet, som har scoren
        GameObject deliveryChecker = GameObject.FindGameObjectWithTag("DeliveryChecker");
        correctDeliveryChecker = deliveryChecker.GetComponent<CorrectDeliveryChecker>();

        // Resetter boxcollider og farve, samt finder audiomanager
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        //material = GetComponent<Material>();
        material.color = inactiveColor;

        audioManager = GetComponent<AudioManager>();

        buttonPressCooldown = true;
    }

    // Når en tallerken er blevet placeret, enables knappen
    public void FoodOnDelivery()
    {
        boxCollider.enabled = true;
        material.color = activeColor;
    }

    // Når tallerken fjernes igen, disables knappen
    public void FoodOffDelivery()
    {
        boxCollider.enabled = false;
        material.color = inactiveColor;
    }

    // Når en hånd rør spawner, aktiveres knappen og den fortæller
    // deliverychecker at den må checker delivery nu
    public void ActivateDeliveryChecker()
    {
        if (buttonPressCooldown)
        {
            Debug.Log("Click");
            StartCoroutine(ButtonPressCooldown());
        }
        audioManager.PlayClip(0);
    }

    // Enumeratoren forhindrer checkdelivery at blive spammet flere gange, hvilket vil give bugs i scoring og scoringDisplay
    IEnumerator ButtonPressCooldown()
    {
        correctDeliveryChecker.CheckDelivery();
        buttonPressCooldown = false;

        yield return new WaitForSeconds(cooldownTime);  

        buttonPressCooldown = true;

    }
}
