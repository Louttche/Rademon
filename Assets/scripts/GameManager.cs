using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public DialogueManager dialoguemanager;
    public Dialogue IntroDialogue;    
    private GameObject player;
    private bool IsIntro;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;        
        IsIntro = true;
    }

    private void Update()
    {
        if (IsIntro == true)
        {
            Debug.Log("IsIntro is true");
            dialoguemanager.StartDialogue(IntroDialogue);
            IsIntro = false;
        }
        else
        {
            if (dialoguemanager.EndofDialogue == true)
            {
                Debug.Log("IsIntro is false");
                player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;
            }
        }
    }
}
