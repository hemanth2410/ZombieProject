using UnityEngine;
[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObject/Dialogues")]
public class Dialogues : ScriptableObject
{

    [TextArea(3, 10)]
    public string[] sentences;
    public Vector3 dialoguePos;
}
