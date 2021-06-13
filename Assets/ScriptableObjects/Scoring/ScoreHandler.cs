using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private TMP_Text _scoreText;
    private float _currentScore;
    private const string _displayText = "Score: ";
    public ScoringRoster ScoreInfo;
    private void Awake()
    {
        DataManager.ToTheCloud(DataKeys.SCORE, this);
        _scoreText = GetComponent<TMP_Text>();
    }

    public void ResetScore()
    {
        _currentScore = 0;
        _scoreText.text = _displayText + _currentScore;
    }

    public void AddToScore(EOrderTypes orderType)
    {
        float amount=0;
        for(int i = 0; i < ScoreInfo.Roster.Count; i++)
        {
            if(ScoreInfo.Roster[i].OrderType == orderType)
            {
                amount = ScoreInfo.Roster[i].ScoreAmount;
                break;
            }
        }
        _currentScore += amount;
        _scoreText.text = _displayText + _currentScore;
    }
}
