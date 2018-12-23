using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

    private SoundManager.MySound sound;
    private AudioSource source;
    private float volume;

	// Use this for initialization
	void Start () {
        source = this.GetComponent<AudioSource>();
        sound = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().GetSound("lamp_noise");
        volume = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().folleyVolume;
        source.clip = sound.clip;
        source.volume = volume;
        source.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
