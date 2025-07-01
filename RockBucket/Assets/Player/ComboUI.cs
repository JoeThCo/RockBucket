using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
    [SerializeField] private Color successColor;
    [SerializeField] private Color missedColor;
    [Space(10)]
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    private void Start()
    {
        ComboController.OnComboUpdated += ComboController_OnComboUpdated;
        ComboController.OnHighScore += ComboController_OnHighScore;

        ComboController_OnComboUpdated(string.Empty, 0);
        ComboController_OnHighScore(0);
    }

    private void OnDestroy()
    {
        ComboController.OnComboUpdated -= ComboController_OnComboUpdated;
        ComboController.OnHighScore -= ComboController_OnHighScore;
    }

    private void ComboController_OnComboUpdated(string combo, int points)
    {
        comboText.SetText(combo);

        scoreText.gameObject.SetActive(points != 0);
        scoreText.SetText(points.ToString("F0"));
    }

    private void ComboController_OnHighScore(int score)
    {
        highScoreText.SetText(score.ToString("F0"));
    }
}