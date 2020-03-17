using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    
    [SerializeField]
    public List<Sound> Sounds = new List<Sound>();

    private Dictionary<string, AudioClip> _dictionary;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        
        _dictionary = new Dictionary<string, AudioClip>();
        foreach (var sound in Sounds)
        {
            _dictionary.Add(sound.Name, sound.AudioClip);
        }
    }

    public void PlayAudio(string clipName)
    {
        _audioSource.clip = _dictionary[clipName];
        _audioSource.Play();
    }
}

[Serializable]
public class Sound
{
    public string Name;
    public AudioClip AudioClip;
}
