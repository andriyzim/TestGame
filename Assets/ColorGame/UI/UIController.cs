using System.Collections.Generic;
using ColorGame.Common.ColorManagement;
using ColorGame.Common.ServiceLocator;
using ColorGame.Common.Services.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace ColorGame.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private Image _colorInfoImage;

        private IColorService ColorService => Service.Instance.Get<IColorService>();

        private void OnEnable()
        {
            ColorService.ColorChangedEvent += ColorServiceOnColorSelectedEvent;
        }

        private void OnDisable()
        {
            ColorService.ColorChangedEvent -= ColorServiceOnColorSelectedEvent;
        }

        private void ColorServiceOnColorSelectedEvent(ColorSet correctColorSet, List<ColorSet> restColorSets)
        {
            _colorInfoImage.color = correctColorSet.Color;
        }
    }
}