using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    public int score;

    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }
}
