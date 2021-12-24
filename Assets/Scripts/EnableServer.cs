using UnityEngine;

public class EnableServer : MonoBehaviour
{
    dreamloLeaderBoard leaderboard;
    void Start()
    {
        leaderboard = dreamloLeaderBoard.current;
        leaderboard.AddScore(PlayerPrefs.GetString("playername", ""), GameManager.totalScore, GameManager.currentLevel);
    }

}
