using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField] Transform spawnPoint;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        audio.Play();
        SpawnPrefab();
        
    }
    public void SpawnPrefab()
    {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }

}
