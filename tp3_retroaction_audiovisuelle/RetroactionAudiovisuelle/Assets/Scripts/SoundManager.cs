using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [Serializable]
    public struct MySound
    {
        public string name;
        public AudioClip clip;
        public bool ambient;
    }
    public MySound[] sounds;

    public float ambientVolume;
    public float effectsVolume;

    public AudioSource sourceAmbient;
    public AudioSource sourceAmbient2;
    public AudioSource sourceEffect;

    public float fadeSpeed = 0.01f;

    private bool transition = false;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
	}

    public MySound GetSound(string name)
    {
        foreach (MySound sound in sounds)
        {
            if (sound.name == name)
            {
                return sound;
            }
        }
        MySound emptySound;
        emptySound.ambient = false;
        emptySound.clip = null;
        emptySound.name = "undefined";
        return emptySound;
    }

    public void Play(string name)
    {
        MySound sound = this.GetSound(name);
        if (sound.ambient)
            this.PlayFadeInOut(sound.clip, ambientVolume);
        else
            sourceEffect.PlayOneShot(sound.clip, effectsVolume);
    }

    public void PlayFadeInOut(AudioClip clip, float volume)
    {
        this.StartCoroutine(this.PlayFadeInOutEnum(clip, ambientVolume));
    }

    public IEnumerator PlayFadeInOutEnum(AudioClip clip, float volume)
    {
        while (this.transition)
            yield return new WaitForSeconds(this.fadeSpeed);

        this.transition = true;
        bool first = true;

        if (sourceAmbient.isPlaying)
        {
            sourceAmbient2.volume = 0f;
            sourceAmbient2.PlayOneShot(clip, 1f);
            first = false;
        }
        else
        {
            sourceAmbient.volume = 0f;
            sourceAmbient.PlayOneShot(clip, 1f);
        }

        while ((first && (sourceAmbient.volume < volume || sourceAmbient2.volume > 0)) || (!first && (sourceAmbient2.volume < volume || sourceAmbient.volume > 0)))
        {
            if (first)
            {
                sourceAmbient.volume += 0.01f;
                sourceAmbient2.volume -= 0.01f;
            }
            else
            {
                sourceAmbient.volume -= 0.01f;
                sourceAmbient2.volume += 0.01f;
            }
            yield return new WaitForSeconds(this.fadeSpeed);
        }

        if (first)
            sourceAmbient2.Stop();
        else
            sourceAmbient.Stop();

        this.transition = false;
    }

    public void StopAmbient()
    {
        sourceAmbient.Stop();
    }

    public void StopEffect()
    {
        sourceEffect.Stop();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
