using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private float typewriterSpeedInSeconds;
     
    [Header("References")]
    [SerializeField] private GameObject container;
    [SerializeField] private TMP_Text actorNameText;
    [SerializeField] private TMP_Text dialogueText;
    
    public static DialogueManager Singleton;
    private DialogueData activeDialogue;
    private int currentDialogueIndex;
    
    

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            
            Destroy(gameObject);
        }
    }

    public void StartDialogue(DialogueData data)
    {
        activeDialogue = data;
        dialogueText.text = "";
        currentDialogueIndex = 0;
        container.SetActive(true);
        StartCoroutine(TypewriteDialogue());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public IEnumerator TypewriteDialogue()
    {
        DialogueInfo current = activeDialogue.dialogues[currentDialogueIndex];
    
        actorNameText.text = current.actorName;
        dialogueText.text = "";

        foreach (char c in current.dialogueText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typewriterSpeedInSeconds);
        }
    }

    public void NextDialogue()
    {
        StopAllCoroutines();
        currentDialogueIndex++;

        if (currentDialogueIndex < activeDialogue.dialogues.Length)
        {
            StartCoroutine(TypewriteDialogue());
        }
        else
        {
            CloseDialogue();
        }
    }
    
    private void CloseDialogue()
    {
        activeDialogue = null;
        container.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
}