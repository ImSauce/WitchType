using UnityEngine;
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/DialogueNode")]
public class DialogueSO : ScriptableObject
{
   public DialogueLine[] lines;
   public DialogueOption[] options;
}

[System.Serializable]
public class DialogueLine
{ 
    public ActorSO speaker;
    [TextArea(3, 5)] public string text;

}


[System.Serializable]
public class DialogueOption
{
    public string optionText;
    public DialogueSO nextDialogue;
}