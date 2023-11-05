using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScreen : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "Press the button!";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }


}
