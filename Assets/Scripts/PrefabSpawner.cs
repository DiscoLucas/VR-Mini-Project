using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PrefabSpawner : MonoBehaviour
{

    /// <summary>
    /// Script used to spawn the Food Items and other objects in the scene by instantiating provided prefabs
    /// </summary>

    public GameObject prefab;
    [SerializeField] Transform spawnPoint;

   
    private void OnTriggerEnter(Collider other)
    {
        SpawnPrefab();
    }
    public void SpawnPrefab()
    {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }

}
