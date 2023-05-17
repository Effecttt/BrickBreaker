using System;
using UnityEngine;

namespace AudioSystem.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        public Sound[] sounds;

        private void Awake()
        {
            Instance = this;
            
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        public void Play(string soundName)
        {
            Sound s = Array.Find(sounds, sound => sound.soundName == soundName);
            s?.source.Play();
        }
    }
}