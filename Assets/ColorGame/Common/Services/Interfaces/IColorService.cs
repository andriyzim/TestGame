using System;
using System.Collections.Generic;
using ColorGame.Common.ColorManagement;
using ColorGame.Common.ServiceLocator;

namespace ColorGame.Common.Services.Interfaces
{
    public interface IColorService : IService
    {
        ColorName CurrentColor { get; }
        List<ColorSet> ColorSet { get; }

        event Action<ColorSet,List<ColorSet>> ColorChangedEvent;
        event Action<bool> ColorSelectedEvent;
        void Initialize(List<ColorSet> colorSets);
        void SetNextColor();
        bool ColorSelected(ColorName colorName);
    }
}