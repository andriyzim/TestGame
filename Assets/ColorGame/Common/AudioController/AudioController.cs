using ColorGame.Common.ServiceLocator;
using ColorGame.Common.Services.Interfaces;
using UnityEngine;

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
        private IColorService ColorService => Service.Instance.Get<IColorService>();

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            ColorService.ColorSelectedEvent += ColorSelectReaction;
        }

        private void OnDisable()
        {
            ColorService.ColorSelectedEvent += ColorSelectReaction;
        }

        private void ColorSelectReaction(bool isCorrect)
        {
            _audioSource.Stop();
            _audioSource.clip = isCorrect ? _correctAudioClip : _worngAudioClip;
            _audioSource.Play();
        }
    }
}