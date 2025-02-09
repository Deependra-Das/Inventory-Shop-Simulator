using System;
using UnityEngine;

namespace ServiceLocator.Sound
{
    public class SoundService
    {
        private SoundScriptableObject _audioList;
        private AudioSource _audioSourceSFX;
        private AudioSource _audioSourceBGM;

        public SoundService(SoundScriptableObject audioList, AudioSource audioSourceSFX, AudioSource audioSourceBGM)
        {
            this._audioList = audioList;
            _audioSourceSFX = audioSourceSFX;
            _audioSourceBGM = audioSourceBGM;
            PlayBGM(SoundType.BackgroundMusic, true);
        }

        public void PlaySFX(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                _audioSourceSFX.loop = loopSound;
                _audioSourceSFX.clip = clip;
                _audioSourceSFX.PlayOneShot(clip);
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private void PlayBGM(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                _audioSourceBGM.loop = loopSound;
                _audioSourceBGM.clip = clip;
                _audioSourceBGM.Play();
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private AudioClip GetSoundClip(SoundType soundType)
        {
            Sounds sound = Array.Find(_audioList.audioList, item => item.soundType == soundType);
            if (sound.audio != null)
                return sound.audio;
            return null;
        }
    }
}
