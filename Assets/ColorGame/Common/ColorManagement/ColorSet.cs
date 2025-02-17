using System;
using UnityEngine;

namespace ColorGame.Common.ColorManagement
{
    [Serializable]
    public class ColorSet
    {
        [field: SerializeField]
        public ColorName colorName { get; private set; }

        [field: SerializeField]
        public Color Color { get; set; }
    }
}