using System.Collections.Generic;
using UnityEngine;

namespace TBT.Gameplay.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [SerializeField] private AudioClip clickSound;
        private List<AudioSource> sources = new List<AudioSource>();
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
        }

        public void PlaySound(AudioName name)
        {
            if (sources.Count == 0)
            {
                GameObject newSource = new GameObject();
                newSource.name = "newSource";
                newSource.AddComponent<AudioSource>();
                newSource.transform.SetParent(transform);
                newSource.transform.position = transform.position;
                sources.Add(newSource.AddComponent<AudioSource>());
            }
            bool sourceFound = false;
            foreach (AudioSource source in sources)
            {
                if (!source.isPlaying)
                {
                    sourceFound = true;
                    source.clip = FindClipWithAudioName(name);
                    source.Play();
                    break;
                }
            }
            if (!sourceFound)
            {
                GameObject newSource = new GameObject();
                newSource.name = "newSource";
                newSource.AddComponent<AudioSource>();
                newSource.transform.SetParent(transform);
                newSource.transform.position = transform.position;
                sources.Add(newSource.AddComponent<AudioSource>());
                newSource.GetComponent<AudioSource>().clip = FindClipWithAudioName(name);
                newSource.GetComponent<AudioSource>().Play();
            }
        }

        private AudioClip FindClipWithAudioName(AudioName name)
        {
            if (name == AudioName.clicSound)
            {
                return clickSound;
            }
            return null;
        }
    }

    public enum AudioName
    {
        clicSound,
        
    }
}
