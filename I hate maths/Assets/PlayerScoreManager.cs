using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    public int score;

    public TextMeshProUGUI scoreText;

    [SerializeField] ProgressBar bar;

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        bar.slider.maxValue = 100;
        bar.SetMinValue(score);
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        bar.SetValue(score);
    }
}
