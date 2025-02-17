using System.Collections.Generic;

namespace ColorGame.Common.ColorManagement
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "ColorsAsset", menuName = "Color Game/Create New Color Asset")]
    public class ColorSetAsset : ScriptableObject
    {
        [field: SerializeField]
        public List<ColorSet> ColorSets { get; private set; }

    }
}