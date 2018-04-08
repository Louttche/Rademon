using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour {

    //float fadeTime;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Battle")
        {
            Debug.Log("Entered Battle");
            //fadeTime = GameObject.Find("GameManager").GetComponent<Fading>().BeginFade(1);
            //StopAllCoroutines();
            //StartCoroutine(WaitTime());
            SceneManager.LoadScene("BattleScene");
        }
    }

    /*IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(fadeTime);
    }*/
}
