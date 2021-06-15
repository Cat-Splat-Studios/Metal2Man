using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Transform[] PlayerSpawnPoints;
    public Player Player1Prefab;
    public Player Player2Prefab;
    public TMP_Text GameOverScoreText;
    public GameObject GameOverScreen;
    public GameObject BottomUI;
    public GameObject Components;
    public GameObject MasterCanvas;
    private const string ScorePreText = "Score ";
    // Start is called before the first frame update
    void Awake()
    {
        DataManager.ToTheCloud(DataKeys.GAMEMANAGER,this);
        Instantiate(Player1Prefab, PlayerSpawnPoints[0].position, PlayerSpawnPoints[0].rotation);
        if(DataManager.MakeItRain<AudioHandler>(DataKeys.AUDIO))
        {
            bool playerActive = DataManager.MakeItRain<AudioHandler>(DataKeys.AUDIO).Player2Joined;
            if(playerActive)
            {
                Instantiate(Player2Prefab, PlayerSpawnPoints[1].position, PlayerSpawnPoints[1].rotation);
            }
            DataManager.MakeItRain<AudioHandler>(DataKeys.AUDIO).PlayAudio(EAudioEvents.Music);    
        }
        GameOverScreen.SetActive(false);
    }

    public void EnableGameOverScreen()
    {
        Components.SetActive(false);
        BottomUI.SetActive(false);
        MasterCanvas.SetActive(false);
        var score = DataManager.MakeItRain<ScoreHandler>(DataKeys.SCORE).CurrentScore;
        DataManager.MakeItRain<ScoreHandler>(DataKeys.SCORE).ResetScore();
        GameOverScoreText.text = ScorePreText + score;
        GameOverScreen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
