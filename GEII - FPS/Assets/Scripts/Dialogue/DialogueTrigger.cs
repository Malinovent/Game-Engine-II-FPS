using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData activeDialogue;

    public void ActivateDialogue()
    {
        DialogueManager.Singleton.StartDialogue(activeDialogue);
    }

    public void Interact()
    {
        ActivateDialogue();
    }
}