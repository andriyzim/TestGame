using ColorGame.Common.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace ColorGame.Common.AudioController
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _correctAudioClip;

        [SerializeField]
        private AudioClip _worngAudioClip;

        private AudioSource _audioSource;
        
        [Inject]
        private IColorService _сolorService;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _сolorService.ColorSelectedEvent += СolorSelectReaction;
        }

        private void OnDisable()
        {
            _сolorService.ColorSelectedEvent += СolorSelectReaction;
        }

        private void СolorSelectReaction(bool isCorrect)
        {
            _audioSource.Stop();
            _audioSource.clip = isCorrect ? _correctAudioClip : _worngAudioClip;
            _audioSource.Play();
        }
    }
}