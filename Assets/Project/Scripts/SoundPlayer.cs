using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip _mainTheme;

        [SerializeField] private AudioClip _crowSound;
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private AudioClip[] _moveSounds;
        [SerializeField] private AudioClip _teleportSound;
        [SerializeField] private AudioClip _winSound;
        [SerializeField] private AudioClip _loseSound;
        [SerializeField] private AudioClip[] _stickerSounds;

        [SerializeField] private AudioSource _main;
        [SerializeField] private AudioSource _second;
        [SerializeField] private AudioSource _crowd;

        private void Awake()
        {
            PlayMainTheme();
        }

        private void PlayMainTheme()
        {
            _crowd.clip = _crowSound;
            _main.clip = _mainTheme;

            _main.Play();
            _crowd.Play();
        }

        public void PlayStickerSound()
        {
            var index = Random.Range(0, _stickerSounds.Length - 1);

            _second.clip = _stickerSounds[index];
            _second.Play();
        }

        public void PlayJumpSound()
        {
            _second.clip = _jumpSound;
            _second.Play();
        }

        public void PlayTeleportSound()
        {
            _second.clip = _teleportSound;
            _second.Play();
        }

        public void PlayMoveSound()
        {
            var index = Random.Range(0, _moveSounds.Length - 1);

            _second.clip = _moveSounds[index];
            _second.Play();
        }

        public void PlayWinSound()
        {
            _second.clip = _winSound;
            _second.Play();
        }

        public void PlayLoseSound()
        {
            _second.clip = _loseSound;
            _second.Play();
        }
    }
}