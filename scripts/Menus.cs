using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Menus : MonoBehaviour
{
    [Header("All Menu's")]
    public GameObject pauseMenuUI;
    public GameObject EndGameMenu;
    public GameObject ObjectiveMenu;

    public static bool GameIsStopped=false;
    private bool isMenuOpen = false;  // Flag to track menu state

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  // Use GetKeyDown for one-time trigger
        {
            if (!isMenuOpen)  // Check if menu isn't already open
            {
                if (GameIsStopped)
                {
                    Resume();
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Pause();
                    Cursor.lockState = CursorLockMode.None;
                    isMenuOpen = true;  // Set flag to indicate menu is open
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Escape))  // Reset flag when Escape is released
        {
            isMenuOpen = false;
        }

        else if (Input.GetKeyDown("m"))
            {
                if(GameIsStopped)
                {
                    removeObjectives();
                    Cursor.lockState= CursorLockMode.Locked;
                }
                else 
                {
                    showObjective();
                     Cursor.lockState= CursorLockMode.None;
                }
            }
    }

    public void  showObjective()
    {
        ObjectiveMenu.SetActive(true);
        Time.timeScale=0f;
       
        GameIsStopped=true;
    }

    public void removeObjectives()
    {
        ObjectiveMenu.SetActive(false);
        Time.timeScale=1f;
       Cursor.lockState= CursorLockMode.Locked;
        GameIsStopped=false;
    }


    public void Resume()
     {
        pauseMenuUI.SetActive(false);
        Time.timeScale =1f;
        Cursor.lockState= CursorLockMode.Locked;
        GameIsStopped=false;
     }

    public void Restart()
    {
        SceneManager.LoadScene("timeline_mission");

    }

    public void LoadMenu()
    {
        Time.timeScale=1f;

        SceneManager.LoadScene("Menu");

    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game .........");
        Application.Quit();
    }

    void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale=0f;
            GameIsStopped=true;

        }

}
