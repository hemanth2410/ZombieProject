using UnityEngine;
using TMPro;
public class PlayerProfile : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    private void OnEnable()
    {
        GameEvents.current.OnNameChange += ChangeName;

        nameText.text = RegisterPanel.playerName;
        totalScoreText.text = GameManager.totalScore.ToString();
    }
    private void OnDisable()
    {
        GameEvents.current.OnNameChange -= ChangeName;
    }
    public void ChangeName(string newName)
    {
        nameText.text = newName;
    }
}
