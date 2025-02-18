using System;
using System.Collections.Generic;
using System.Linq;
using ColorGame.Common.ColorManagement;
using ColorGame.Common.Services.Interfaces;
using UnityEngine;

namespace ColorGame.Common.Services
{
    public class ColorService : IColorService
    {
        public List<ColorSet> ColorSet { get; private set; }
        public event Action<ColorSet, List<ColorSet>> ColorChangedEvent;
        public event Action<bool> ColorSelectedEvent;
        public ColorName CurrentColor { get; private set; } = ColorName.None;
        private const int ColorSetCount = 3;

        public void Initialize(List<ColorSet> colorSets)
        {
            ColorSet = colorSets;
        }

        public void SetNextColor()
        {
            List<ColorSet> availableColors = ColorSet.Where(cs => cs.colorName != CurrentColor).ToList();

            if (availableColors.Count < ColorSetCount)
            {
                Debug.LogError("Not enough available colors set!");
                return;
            }

            ColorSet newCurrentColor = availableColors.OrderBy(_ => UnityEngine.Random.value).First();

            availableColors = availableColors.Where(cs => cs.colorName != newCurrentColor.colorName).ToList();

            List<ColorSet> randomColors = availableColors
                .OrderBy(_ => UnityEngine.Random.value)
                .Take(ColorSetCount - 1)
                .ToList();
            CurrentColor = newCurrentColor.colorName;
            ColorChangedEvent?.Invoke(newCurrentColor, randomColors);
        }

        public bool ColorSelected(ColorName colorName)
        {
            bool isCorrect = colorName == CurrentColor;
            ColorSelectedEvent?.Invoke(isCorrect);
            return isCorrect;
        }
    }
}