using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Battle")
        {
            Debug.Log("Entered Battle");
            SceneManager.LoadScene("BattleScene");
        }
    }
}
