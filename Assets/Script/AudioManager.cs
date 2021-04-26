using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField, Range(0, 1)] private float volume = 1; //ÉÅÉCÉìâπó 
    [SerializeField, Range(0, 1)] private float bgmVolume = 1; //BGMÇÃâπó 
    [SerializeField, Range(0, 1)] private float seVolume = 1; //SEÇÃâπó 

    Dictionary<string, AudioClip> bgmClips = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> seClips = new Dictionary<string, AudioClip>();

    AudioSource bgmAudioSource;
    AudioSource seAudioSource;

    public float Volume
    {
        set
        {
            volume = Mathf.Clamp01(value);
            bgmAudioSource.volume = bgmVolume * volume;
            seAudioSource.volume = seVolume * volume;
        }
        get { return volume; }
    }

    public float BgmVolume
    {
        set
        {
            bgmVolume = Mathf.Clamp01(value);
            bgmAudioSource.volume = bgmVolume * volume;
        }
        get { return bgmVolume; }
    }

    public float SeVolume
    {
        set
        {
            seVolume = Mathf.Clamp01(value);
            seAudioSource.volume = seVolume * volume;
        }
        get { return seVolume; }
    }

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        seAudioSource = gameObject.AddComponent<AudioSource>();

        var bgm = Resources.LoadAll<AudioClip>("Audio/BGM");
        var se = Resources.LoadAll<AudioClip>("Audio/SE");

        foreach (var clip in bgm)
        {
            bgmClips.Add(clip.name, clip);
        }

        foreach (var clip in se)
        {
            seClips.Add(clip.name, clip);
        }
    }

    public void PlayBGM(string name)
    {
        if (!bgmClips.ContainsKey(name))
        {
            throw new System.Exception("Sound" + name + "is not defined");
        }

        bgmAudioSource.clip = bgmClips[name];
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = BgmVolume * Volume;
        bgmAudioSource.Play();
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = null;
    }

    public void PlaySE(string name)
    {
        if (!seClips.ContainsKey(name))
        {
            throw new System.Exception("Sound" + name + "is not defined");
        }

        if (name == "WalkSound2")
        {
            seAudioSource.loop = true;
        }
        seAudioSource.PlayOneShot(seClips[name], seVolume * Volume);
    }

    public void StopSE()
    {
        seAudioSource.Stop();
        seAudioSource.clip = null;
    }
}
