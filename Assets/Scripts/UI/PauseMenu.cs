using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuParent;

    private void Start()
    {
        pauseMenuParent.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        pauseMenuParent.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        pauseMenuParent.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
