using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    private static SoundManager instanceRef;

    [Serializable]
    public struct MySound
    {
        public string name;
        public AudioClip clip;
        public bool ambient;
    }
    public MySound[] sounds;

    public float ambientVolume = 1f;
    public float effectsVolume = 1f;
    public float folleyVolume = 1f;

    public AudioSource sourceAmbient;
    public AudioSource sourceAmbient2;
    public AudioSource sourceEffect;
    public AudioSource sourceFolley;

    public float fadeSpeed = 0.01f;

    private bool transition = false;

    private Slider ambientSlider;
    private Slider effectSlider;
    private Slider folleySlider;

    void Awake()
    {
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public void SetSliderValues()
    {
        ambientSlider = GameObject.FindGameObjectWithTag("AmbientSlider").GetComponent<Slider>();
        effectSlider = GameObject.FindGameObjectWithTag("EffectSlider").GetComponent<Slider>();
        folleySlider = GameObject.FindGameObjectWithTag("FolleySlider").GetComponent<Slider>();

        ambientSlider.onValueChanged.AddListener(delegate { this.OnAmbientVolumeChanged();  });
        effectSlider.onValueChanged.AddListener(delegate { this.OnEffectVolumeChanged(); });
        folleySlider.onValueChanged.AddListener(delegate { this.OnFolleyVolumeChanged(); });

        ambientSlider.value = ambientVolume;
        effectSlider.value = effectsVolume;
        folleySlider.value = folleyVolume;
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

        while (((first && (sourceAmbient.volume < volume || sourceAmbient2.volume > 0)) || (!first && (sourceAmbient2.volume < volume || sourceAmbient.volume > 0)))
            && this.transition)
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
    public void OnAmbientVolumeChanged()
    {
        this.SetAmbientVolume(ambientSlider.value);
    }
    public void OnEffectVolumeChanged()
    {
        this.SetEffectVolume(effectSlider.value);
    }
    public void OnFolleyVolumeChanged()
    {
        this.SetFolleyVolume(folleySlider.value);
    }
    public void SetAmbientVolume(float value)
    {
        ambientVolume = value;
        sourceAmbient.volume = value;
        this.transition = false;
    }
    public void SetEffectVolume(float value)
    {
        effectsVolume = value;
        sourceEffect.volume = value;
    }
    public void SetFolleyVolume(float value)
    {
        folleyVolume = value;
        sourceFolley.volume = value;
    }
    public void StopAmbient()
    {
        sourceAmbient.Stop();
    }
    public void StopEffect()
    {
        sourceEffect.Stop();
    }
    public void StopFolley()
    {
        sourceFolley.Stop();
    }
}
