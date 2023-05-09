using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersistantData : MonoBehaviour
{


    public static PersistantData instance;
    public int score;

    public string difficulity;

    public string username;

    ScoreChanger scoreChanger;

    [SerializeField] InputField nameInput;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }

        
    }

    public void SetDifficulty(string diff)
    {
        instance.score = 0;

        Debug.Log(diff);
        if(nameInput!=null)
        {
            instance.SetName(nameInput.text);
        }
        else
        {
            instance.SetName("aboslute loser");
        }
        instance.difficulity = diff;

        SceneManager.LoadScene(2);
    }

    public void SetName(string name)
    {
        instance.username = name;
    }

    public void AddScore(int scoreToAdd)
    {
        instance.score += scoreToAdd;
        instance.DisplayScore();
    }
    public void DisplayScore()
    {
        if (scoreChanger == null)
        {
                scoreChanger = GameObject.FindGameObjectWithTag("ScoreChange").GetComponent<ScoreChanger>();
        }
            
        Debug.Log(scoreChanger.ToString());

        
        scoreChanger.Display( "Score: "+ instance.score.ToString());
    }
    public void GoToSelectDifficulty()
    {
        SceneManager.LoadScene(1);
    }

    public int GetScore()
    {
        return instance.score;
    }
    public string GetName()
    {
        return instance.username;
    }

}
