using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [HideInInspector]
    public AudioSource HitSound;
    [HideInInspector]
    public AudioSource RadiationSound;
    [HideInInspector]
    public AudioSource VictorySound;
    [HideInInspector]
    public AudioSource BackgroundMusic;

    private void Start()
    {
        RadiationSound = this.transform.Find("Radiation").GetComponent<AudioSource>();
        HitSound = this.transform.Find("Hit").GetComponent<AudioSource>();
        VictorySound = this.transform.Find("Victory").GetComponent<AudioSource>();
        BackgroundMusic = this.transform.Find("Background_Music").GetComponent<AudioSource>();
    }

}
