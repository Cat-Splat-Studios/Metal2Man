using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuParent;

    //need to ref the players input system
    PlayerInput p1_input;
    PlayerInput p2_input;
    PlayerController p1_Controller;
    PlayerController p2_Controller;

    private void Awake()
    {
        pauseMenuParent.SetActive(false);

        //p1_input = DataManager.MakeItRain<Player>(DataKeys.PLAYER1).input;
        //p2_input = DataManager.MakeItRain<Player>(DataKeys.PLAYER2).input;

        //p1_Controller =  PlayerManager.GetController(0);
        //p2_Controller =  PlayerManager.GetController(1);

        p1_input = PlayerManager.GetController(0).PlayerInputs;

        if (!p1_input) Debug.Log("no controller of player input@ER&*()_+");

    }

    private void Update()
    {
        if (p1_input)
            p1_input.actions["Pause"].performed += HandleInputs;
        else
            Debug.Log("no p1 input found");
        //if (p2_input)
        //    p2_input.onActionTriggered += HandleInputs;
    }

    private void HandleInputs(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Pause":
                if (!pauseMenuParent.gameObject.activeInHierarchy)
                {
                    PauseGame();
                }
                else if (pauseMenuParent.gameObject.activeInHierarchy)
                {
                    Resume();
                }
                break;
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
