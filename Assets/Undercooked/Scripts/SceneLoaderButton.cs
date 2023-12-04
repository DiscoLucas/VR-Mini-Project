using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderButton : MonoBehaviour
{

    public GameManager gameManager;

    private void Awake()
    {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    public void LoadNextSceneClick()
    {
        gameManager.LoadNextScene();
    }
}
