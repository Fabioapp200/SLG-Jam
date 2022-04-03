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
    public bool winBadgeUp;
    private void Awake()
    {   
        //Destroys any aditional GameManagers that may be instantiated
        GameObject[] go = GameObject.FindGameObjectsWithTag("GameManager");
        if(go.Length > 1)
        {
            for (int i = 0; i < go.Length - 1; i++)
            {
                Destroy(go[i]);
            }
        }

        //Makes the gameManager Object last for the whole game
        DontDestroyOnLoad(gameObject);
    }
    void LoadScene()
    {
        //Controls level browsing and restarts the game
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadSceneAsync(nextScene);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadSceneAsync(prevScene);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(currentScene);
        }
    }
    public IEnumerator Win()
    {   //Shows the "You Won" text on screen an inhibts the player and minotaur to move.
        //After the player presses Space, loads the next scene
        winBadgeUp = true;
        GameObject winBadge = Instantiate(pressToContinuePrompt, transform.position, Quaternion.identity);
        winBadge.transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Canvas").SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        SceneManager.LoadSceneAsync(nextScene);
        winBadgeUp = false;
    }
    public IEnumerator Lose()
    {
        //Shows the "You Lose" text on screen an inhibts the player and minotaur to move.
        //After the player presses Space, reloads the level
        winBadgeUp = true;
        GameObject winBadge = Instantiate(pressToContinuePrompt, transform.position, Quaternion.identity);
        winBadge.transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Canvas").SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        SceneManager.LoadSceneAsync(currentScene);
        winBadgeUp = false;
    }
    void Update()
    {
        //Gets the current level name and changes the next and previous scene to be loaded
        currentScene = SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "Start Menu":
                nextScene = 1;
                prevScene = 1;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(nextScene);
                }
                break;
            case "Lvl 1":
                prevScene = 1;
                nextScene = 2;
                LoadScene();
                break;
            case "Lvl 2":
                prevScene = 1;
                nextScene = 3;
                LoadScene();
                break;
            case "Lvl 3":
                prevScene = 2;
                nextScene = 4;
                LoadScene();
                break;
            case "Lvl 4":
                prevScene = 3;
                nextScene = 5;
                LoadScene();
                break;
            case "Lvl 5":
                prevScene = 4;
                nextScene = 6;
                LoadScene();
                break;
            case "VictoryScreen":
                prevScene = 5;
                nextScene = 0;
                LoadScene();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(nextScene);
                }
                break;
        }
    }
}