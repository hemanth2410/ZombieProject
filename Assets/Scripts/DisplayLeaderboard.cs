using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DisplayLeaderboard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] nameText,levelText,scoreText;
    [SerializeField] TextMeshProUGUI playerNameText,playerLevelText,playerScoreText;

    dreamloLeaderBoard leaderboard;
    List<dreamloLeaderBoard.Score> highscoreList;

    private void OnEnable()
    {
       // leaderboard = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
         leaderboard = dreamloLeaderBoard.current;
        
         RefreshLeaderboard();
         RefreshMyScore();
       // RecieveScores();
    }
    private void Start()
    {
      //  leaderboard = dreamloLeaderBoard.current;
    }
    public void RefreshLeaderboard()
    {
        leaderboard = dreamloLeaderBoard.current;
        highscoreList = leaderboard.ToListHighToLow();
        
        for (int i = 0; i < nameText.Length; i++)
        {
            nameText[i].text = "";
            levelText[i].text = "";
            scoreText[i].text = "";
            if (highscoreList.Count > i)
            {
                nameText[i].text = highscoreList[i].playerName;
                levelText[i].text = "Level - " + highscoreList[i].seconds.ToString();
                scoreText[i].text =  highscoreList[i].score.ToString();

            }
        }
    }

    public void RefreshMyScore()
    {

        playerNameText.text = RegisterPanel.playerName; 
        playerLevelText.text = "Level - " + GameManager.currentLevel.ToString();
        playerScoreText.text = GameManager.totalScore.ToString();

    }

    public void RecieveScores()
    {
        List<dreamloLeaderBoard.Score> Scores = leaderboard.ToListHighToLow();
        int count = 0;
        foreach (dreamloLeaderBoard.Score Currentscore in Scores)
        {
            //Hook this part to required UI
            //Outputs text in below case Player scored score in these many levels
            count++;
            Debug.Log(Currentscore.playerName + " " + Currentscore.score + " " + Currentscore.seconds);
            if (count > 10) break;
        }
        Debug.Log("zero scores");
    }
}
