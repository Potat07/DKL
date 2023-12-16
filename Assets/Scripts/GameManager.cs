using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    [SerializeField ] private GameObject PauseScreen;
    private int PauseCounter = 0;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }


    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void OutsideCourt()
    {

        SceneManager.LoadScene("Outside_Court");

    }
    public void restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void UseMap()
    {
        SceneManager.LoadScene("Map");
    }
    public void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            SceneManager.LoadScene("Map");
        }
        if (Input.GetKeyDown("escape"))
        {
            PauseCounter++;
            PauseScreen.SetActive(true);
            if (PauseCounter == 2)
            {
                PauseCounter = 0;
                PauseScreen.SetActive(false);
            }


        }
    }
}


