using System.Collections.Generic;
using ColorGame.Common.ColorManagement;
using ColorGame.Common.Services.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ColorGame.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private Image _colorInfoImage;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        private const float ColorFadeDuration = 0.5f;

        [Inject]
        private IColorService _colorService;

        private void Awake()
        {
            _colorService.ColorChangedEvent += ColorServiceOnColorSelectedEvent;
            _canvasGroup.alpha = 0f;
        }

        private void Start()
        {
            _canvasGroup.DOFade(1f, ColorFadeDuration*2f);
        }

        private void OnDestroy()
        {
            _colorService.ColorChangedEvent -= ColorServiceOnColorSelectedEvent;
        }

        private void ColorServiceOnColorSelectedEvent(ColorSet correctColorSet, List<ColorSet> restColorSets)
        {
            _colorInfoImage.CrossFadeColor(correctColorSet.Color, ColorFadeDuration, false, true);
        }
    }
}