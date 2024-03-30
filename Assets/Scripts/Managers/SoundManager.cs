using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    PlayerMovement,
    ButtonClick,
    BG,
    PlayerDeath
}

[Serializable]
public class Sounds
{
    public SoundType type;
    public AudioClip audioClip;
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    public AudioSource soundFXSource;

    public Sounds[] audioCollection;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(SoundType soundType)
    {
        AudioClip clip = GetClip(soundType);
        if(clip != null)
        {
            soundFXSource.PlayOneShot(clip);
        }

        else
        {
            Debug.LogError("Clip not found for given SoundType!");
        }
    }

    private AudioClip GetClip(SoundType type)
    {
        Sounds item = Array.Find(audioCollection, i => i.type == type);
        if(item != null) { return item.audioClip; }
        return null;
    }
}
