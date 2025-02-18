using System.Collections.Generic;

namespace ColorGame.Common.ColorManagement
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "ColorsAsset", menuName = "Color Game/Create New Color Asset")]
    public class ColorSetAsset : ScriptableObject
    {
        [field: SerializeField]
        public float DelayBeforeNextColor { get; private set; } = 1.5f;

        [field: SerializeField]
        public List<ColorSet> ColorSets { get; private set; }
    }
}