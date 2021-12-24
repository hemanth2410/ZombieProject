using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveData : MonoBehaviour
{
    dreamloLeaderBoard leaderboard;
    private void Awake()
    {
        leaderboard = dreamloLeaderBoard.current;
    }
    void Start()
    {
         //leaderboard = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
        //leaderboard = dreamloLeaderBoard.current;
        RecieveScores();
    }

    public void RecieveScores()
    {
        List<dreamloLeaderBoard.Score> Scores = leaderboard.ToListHighToLow();
        int count = 0;
        foreach(dreamloLeaderBoard.Score Currentscore in Scores)
        {
            //Hook this part to required UI
            //Outputs text in below case Player scored score in these many levels
            count++;
            Debug.Log(Currentscore.playerName + " " + Currentscore.score + " " + Currentscore.seconds);
            if (count > 10) break;
        }
    }
}
