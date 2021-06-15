using UnityEngine;
using UnityEngine.UI;

public class ScoreWave : MonoBehaviour
{
    public Slider waveSlider;
    public Text timerText;

    public float gameTime = 120;
    public float waveSpeed;

    private bool isGameTime = true;

    private float currentWaveValue = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameTime)
        {
            if (gameTime > 0.0f)
            {
                gameTime -= Time.deltaTime;
            }
            else
            {
                Debug.Log("TIME OUT");
                gameTime = 0.0f;
                isGameTime = false;
            }
            
            DisplayTime();

            currentWaveValue += waveSpeed * Time.deltaTime;

            if (currentWaveValue >= 100.0f)
            {
                DataManager.MakeItRain<GameManager>(DataKeys.GAMEMANAGER).EnableGameOverScreen();
                Debug.Log("GAME OVER");
                currentWaveValue = 100.0f;
                isGameTime = false;
            }

            waveSlider.value = currentWaveValue;
        }
    }

    public void ReduceWave(float amount)
    {
        currentWaveValue = Mathf.Max(0, currentWaveValue - amount);
    }

    private void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(gameTime / 60);
        float seconds = Mathf.FloorToInt(gameTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
