using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAttackDialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;

    public void TriggerFirstAttackDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
