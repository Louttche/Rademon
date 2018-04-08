using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    public void TogglePause(bool pause)
    {
        if (pause == false)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
