using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;


    public void StartDialogue()
    {
        dialogueUI.SetActive(true);
    }

    public bool DialogueActive()
    {
        return dialogueUI.activeInHierarchy;

    }
}
