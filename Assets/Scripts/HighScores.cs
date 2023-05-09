using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    [SerializeField] PersistantData persistantData;


    GameObject namesText;
    GameObject scoresText;



    string nameKey, scoreKey;

    string baseLeaderBoardKey = "LEADERBOARDKEY";

    string baseLeaderBoardScoreKey = "LEADERBOARDSCORE";

    // how this works: 
    //LEADERBOARDKEY1 -> username, LEADERBOARDSCORE1-> score
    //LEADERBOARDKEY2 -> username, LEADERBOARDSCORE2-> score
    //etc 5 total



    public void Start()
    {

        
        AddToLeaderBoards(persistantData.GetScore(), persistantData.GetName());
        DisplayLeaderBoards();
    }

    public void AddToLeaderBoards(int currentScore, string currentName)
    {
        if(currentName == "")
        {
            currentName = "Unamed";
        }

        for (int i = 0; i < 5; i++)
        {
            nameKey = baseLeaderBoardKey + i.ToString();

            scoreKey = baseLeaderBoardScoreKey + i.ToString();

            //if the spot dont have anything, fill it and end function
            if (!PlayerPrefs.HasKey(nameKey))
            {
                PlayerPrefs.SetString(nameKey, currentName);
                PlayerPrefs.SetInt(scoreKey, currentScore);

                Debug.Log("added name and score to Leaderboard"+ currentName+currentScore);
                return;
            }

            if (PlayerPrefs.GetInt(scoreKey) < currentScore)
            {
               
                string tempName = PlayerPrefs.GetString(nameKey);
                int tempScore = PlayerPrefs.GetInt(scoreKey);
                //sets the current place we are at to the new score
                PlayerPrefs.SetString(nameKey, currentName);
                PlayerPrefs.SetInt(scoreKey, currentScore);

                //replace what we are trying to add so it continues the loop until the end, replacing each one that is lower.
                currentScore = tempScore;
                currentName = tempName;
            }
        }

        
    }

    public void DisplayLeaderBoards()
    {
        namesText = GameObject.FindGameObjectWithTag("LeaderboardName");
        scoresText = GameObject.FindGameObjectWithTag("LeaderBoardScore");

        string names = "";
        string score = "";
        for (int i = 0; i < 5; i++)
        {
            nameKey = baseLeaderBoardKey + i.ToString();

            scoreKey = baseLeaderBoardScoreKey + i.ToString();


            

            if (PlayerPrefs.HasKey(nameKey))
            {
                names += PlayerPrefs.GetString(nameKey) + "\n\n";
                score += PlayerPrefs.GetInt(scoreKey).ToString() + "\n\n";
            }
        }

        namesText.gameObject.GetComponent<Text>().text = names;
        scoresText.gameObject.GetComponent<Text>().text = score;
    }

}
