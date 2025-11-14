using System.Collections.Generic;
using TBT.Core.Data.AudioData;
using UnityEngine;

namespace TBT.Gameplay.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [SerializeField] private AudioClip clickSound, bigRobotStep, explosion, machineGun, robotDeath, robotHit, throwProjectile, moveCarriage, doors, pulling, startButton, biteSkill, coralSpikesSkill,
        finalExterminationSkill, furtiveShotSkill, guitarRiff, guitarSlash, heal, hitmanSkill, molotovSkill, forceHit, ricochet, strategicChoiceskill, reload;
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
            if (name == AudioName.bigRobotStep)
            {
                return bigRobotStep;
            }
            if (name == AudioName.explosion)
            {
                return explosion;
            }

            if (name == AudioName.robotDeath)
            {
                return robotDeath;
            }
            if (name == AudioName.robotHit)
            {
                return robotHit;
            }

            if (name == AudioName.machineGun)
            {
                return machineGun;
            }
            if (name == AudioName.throwProjectile)
            {
                return throwProjectile;
            }

            if (name == AudioName.moveCarriage)
            {
                return moveCarriage;
            }

            if (name == AudioName.doors)
            {
                return doors;
            }

            if (name == AudioName.pulling)
            {
                return pulling;
            }

            if (name == AudioName.startButton)
            {
                return startButton;
            }

            if (name == AudioName.biteSkill)
            {
                return biteSkill;
            }

            if (name == AudioName.finalExterminationSkill)
            {
                return finalExterminationSkill;
            }

            if (name == AudioName.furtiveShotSkill)
            {
                return furtiveShotSkill;
            }

            if (name == AudioName.guitarRiff)
            {
                return guitarRiff;
            }

            if (name == AudioName.guitarSlash)
            {
                return guitarSlash;
            }

            if (name == AudioName.heal)
            {
                return heal;
            }

            if (name == AudioName.hitmanSkill)
            {
                return hitmanSkill;
            }

            if (name == AudioName.forceHit)
            {
                return forceHit;
            }

            if (name == AudioName.ricochet)
            {
                return ricochet;
            }
            if (name == AudioName.strategicChoiceskill)
            {
                return strategicChoiceskill;
            }
            if (name == AudioName.coralSpikesSkill)
            {
                return coralSpikesSkill;
            }
            if (name == AudioName.molotovSkill)
            {
                return molotovSkill;
            }

            if (name == AudioName.reload)
            {
                return reload;
            }
            return null;
            
            
        }
    }
}
