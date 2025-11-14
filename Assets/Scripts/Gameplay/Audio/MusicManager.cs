using System;
using UnityEngine;

namespace TBT.Gameplay.Audio
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioClip ambiantMusic;
        [SerializeField] private AudioClip fightMusic;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = ambiantMusic;
            audioSource.Play();
        }

        public void SetFightMusic()
        {
            if (audioSource.clip != fightMusic)
            {
                audioSource.clip = fightMusic;
                audioSource.Play();
            }
        }

        public void SetAmbiantMusic()
        {
            if (audioSource.clip != ambiantMusic)
            {
                audioSource.clip = ambiantMusic;
                audioSource.Play();
            }
        }
    }
}
