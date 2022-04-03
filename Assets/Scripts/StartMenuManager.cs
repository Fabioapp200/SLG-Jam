using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public string sceneToLoad;
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
