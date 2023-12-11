using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textScore;
    int score;
    int maxScore;

    private void Start()
    {
        UpdateUI();
    }

    public void SetMaxScore(int amount)
    {
        maxScore = amount;

        UpdateUI();
    }

    public void AddScore(int amount = 1)
    {
        score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        textScore.text = $"Score: {score}/{maxScore}"; ;
    }
}
