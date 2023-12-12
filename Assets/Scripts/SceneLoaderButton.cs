using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderButton : MonoBehaviour
{

    /// <summary>
    /// The scenemanager of the project. Which is activated with buttons found in the UI Canvas
    /// </summary>

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
