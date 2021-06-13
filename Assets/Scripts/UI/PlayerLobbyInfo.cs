using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLobbyInfo : MonoBehaviour
{
    [SerializeField] Image playerPortrait;
    [SerializeField] TextMeshProUGUI joinText;

    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite[] selectedSprite;

    [SerializeField] TextMeshProUGUI buttonDisplays;

    [TextArea(3, 5)] [SerializeField] string keyBoardInput;
    [TextArea(3, 5)] [SerializeField] string gamepadInput;
    [TextArea(3, 5)] [SerializeField] string splitKeyboardInput;

    private void Start()
    {
        
    }

    public void UpdateInfo(PlayerController controller, bool isDualKeyboard = false)
    {
        if (isDualKeyboard)
        {
            playerPortrait.sprite = defaultSprite;
            buttonDisplays.text = splitKeyboardInput;
        }

        if (controller == null)
        {
            playerPortrait.sprite = defaultSprite;
            joinText.gameObject.SetActive(true);
            joinText.text = "Press Spacebar or A to join";
        }

        else
        {
            gameObject.SetActive(true);
            if (controller.PlayerInputs.currentControlScheme == "Gamepad")
            {
                buttonDisplays.text = gamepadInput;
            }

            else
            {
                buttonDisplays.text = keyBoardInput;
            }
            //joinText.gameObject.SetActive(false);
        }
    }

    public void UpdateInfo(UnityEngine.InputSystem.PlayerInput inputs)
    {
        gameObject.SetActive(true);
        Debug.Log("PlayerLobbyInfo: player control scheme is: " + inputs.currentControlScheme);
        if (inputs.currentControlScheme == "Gamepad")
        {
            buttonDisplays.text = gamepadInput;
        }
        else if (inputs.currentControlScheme == "Keyboard&Mouse")
        {
            buttonDisplays.text = keyBoardInput;
        }
    }
}
