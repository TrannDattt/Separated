using System.Collections.Generic;
using Separated.Enums;
using Separated.Helpers;
using UnityEngine;

namespace Separated.GameManager
{
    public class SoundControl : Singleton<SoundControl>
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _backgroundAudioSource;
        [SerializeField] private AudioSource _sfxAudioSource;

        [Header("Background Musics")]
        [SerializeField] private AudioClip _mainMenuMusic;
        [SerializeField] private AudioClip _gameplayMusic;

        [Header("Sound Effects")]
        [SerializeField] private AudioClip _buttonClickSFX;
        [SerializeField] private AudioClip _itemPickupSFX;

        private Dictionary<EBackgroundMusicType, AudioClip> _backgroundDict = new();
        private Dictionary<ESoundEffectType, AudioClip> _sfxDict = new();

        private void Init()
        {
            
        }

        private AudioClip GetSFX(ESoundEffectType key)
        {
            _sfxDict.TryGetValue(key, out var clip);
            return clip;
        }

        private AudioClip GetBackgroundMusic(EBackgroundMusicType key)
        {
            _backgroundDict.TryGetValue(key, out var clip);
            return clip;
        }

        public void PlayBackgroundMusic(EBackgroundMusicType type)
        {
            var clip = GetBackgroundMusic(type);
            if (clip != null)
            {
                _backgroundAudioSource.clip = clip;
                _backgroundAudioSource.Play();
            }
        }

        public void PlaySFX(ESoundEffectType type)
        {
            var clip = GetSFX(type);
            if (clip != null)
            {
                _sfxAudioSource.PlayOneShot(clip);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            Init();
        }
    }
}