using UnityEngine;

public class SendScoreToServer : MonoBehaviour
{
    dreamloLeaderBoard leaderboard;

    private void OnEnable()
    {
        GameEvents.current.OnLevelWin += SendData;
    }
    private void OnDisable()
    {
        GameEvents.current.OnLevelWin -= SendData;
    }
    void Start()
    {
     
        leaderboard = dreamloLeaderBoard.current;
    
    }


    public void SendData(int starsAchieved, int starsAccum, TreeTypes currentTree, int score, int totalScore, int currentLevel)
    {

        leaderboard.AddScore(PlayerPrefs.GetString("playername", ""), totalScore, currentLevel);
    }
}
