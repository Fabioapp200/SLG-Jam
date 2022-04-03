using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int nextScene, prevScene;
    public static GameManager gm;
    public GameObject pressToContinuePrompt;
    string currentScene;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void LoadScene(int prev, int next)
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadSceneAsync(next);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadSceneAsync(prev);
        }
    }
    public void Win()
    {
        Instantiate(pressToContinuePrompt, transform.position, Quaternion.identity);
        pressToContinuePrompt.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void Lose()
    {

    }
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "Start Menu":
                if (Input.anyKeyDown)
                {
                    nextScene = 1;
                    SceneManager.LoadScene(nextScene);
                }
                break;
            case "Lvl 1":
                Win();
                LoadScene(1, 2);
                break;
            case "Lvl 2":
                LoadScene(1, 3);
                break;
            case "Lvl 3":
                LoadScene(2, 4);
                break;
            case "Lvl 4":
                LoadScene(3, 5);
                break;
            case "Lvl 5":
                LoadScene(4, 5);
                break;
            case "VictoryScreen":
                LoadScene(5, 1);
                break;
        }
    }
}