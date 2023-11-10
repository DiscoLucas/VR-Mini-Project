using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patty : MonoBehaviour
{
    public GameObject stoveTop;
    public float timeCooked;


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggerd");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
