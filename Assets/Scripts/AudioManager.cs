using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField, Tooltip("l'audio mixer de musique a mettre")] private AudioMixerGroup m_audioMixer;
    public Sound[] l_sounds;
    void Awake()
    {
        foreach (Sound s in l_sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = m_audioMixer;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(l_sounds, sounds => sounds.objectName == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(l_sounds, sounds => sounds.objectName == name);
        s.source.Stop();
    }
}
