using ColorGame.Common.Services;
using ColorGame.Common.Services.Interfaces;
using ColorGame.Common.SpawnedObjects;
using UnityEngine;
using Zenject;

namespace ColorGame.Common.Zenject
{
    public class Binder : MonoInstaller
    {
        [SerializeField]
        private GameObject _objectsPrefab;
        public override void InstallBindings()
        {
            Container.Bind<IColorService>().To<ColorService>().AsSingle();
            Container.BindFactory<SpawnedObject, SpawnedObject.Factory>().FromComponentInNewPrefab(_objectsPrefab);
        }
    }
}