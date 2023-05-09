using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChanger : MonoBehaviour
{
    [SerializeField] Text scoreText;

    public void Display(string text)
    {
        if(scoreText == null) {
            scoreText = GetComponent<Text>();
        }
        scoreText.text = text;
    }
}
