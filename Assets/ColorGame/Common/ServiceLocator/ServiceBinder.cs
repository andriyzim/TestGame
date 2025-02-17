using ColorGame.Common.Services;
using ColorGame.Common.Services.Interfaces;
using UnityEngine;

namespace ColorGame.Common.ServiceLocator
{
    public static class ServiceBinder
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            Service.Initialize();
            Service.Instance.Register<IColorService>(new ColorService());
        }
    }
}