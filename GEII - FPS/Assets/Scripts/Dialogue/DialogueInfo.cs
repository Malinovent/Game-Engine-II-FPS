using UnityEngine;

[System.Serializable]
public struct DialogueInfo
{
    public string actorName;
    
    [TextArea(1,10)] public string dialogueText;

}