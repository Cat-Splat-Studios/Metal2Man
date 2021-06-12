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

    public void UpdateInfo(PlayerController controller)
    {
        if (controller == null)
        {
            playerPortrait.sprite = defaultSprite;
            joinText.gameObject.SetActive(true);
            joinText.text = "Press Spacebar or A to join";
        }

        else
        {
            playerPortrait.sprite = selectedSprite[0];
            joinText.gameObject.SetActive(false);
        }
    }
}
