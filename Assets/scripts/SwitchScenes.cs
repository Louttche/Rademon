using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour {

    private string BattleSceneName = "BattleScene";
    private Color loadToColor = Color.black;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Battle")
        {
            Debug.Log("Entered Battle");
            //SceneManager.LoadScene(BattleSceneName);            
            Initiate.Fade(BattleSceneName, loadToColor, 2.0f);
        }
    }

    /*IEnumerator WaitForAudio()
    {
        yield return new WaitForSeconds(EnterBattleAudio.GetComponent<AudioClip>().length);
    }*/
}
