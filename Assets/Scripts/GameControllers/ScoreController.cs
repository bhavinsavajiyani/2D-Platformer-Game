using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    private int score = 0;

    private void Awake()
    {
        _textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void IncreaseScore(int _score)
    {
        score += _score;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        _textMeshPro.text = "Score: " + score;
    }
}
