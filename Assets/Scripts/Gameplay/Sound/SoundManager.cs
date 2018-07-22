using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour 
{
    public static SoundManager instance;

    public AudioClip[] audioClips;
    public Dictionary<string, AudioClip> audioClipDatabase = new Dictionary<string, AudioClip>();

    void Awake()
    {
        instance = this;

        for (int i = 0; i < audioClips.Length; i++)
        {
            audioClipDatabase.Add(audioClips[i].name, audioClips[i]);
        }
    }

    public void PlayOneShot(AudioSource audioSource, string clipName)
    {
        audioSource.PlayOneShot(audioClipDatabase[clipName]);
    }
}
