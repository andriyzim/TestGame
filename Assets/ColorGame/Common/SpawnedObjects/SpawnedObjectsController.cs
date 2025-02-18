using System.Collections.Generic;
using System.Linq;
using ColorGame.Common.ColorManagement;
using ColorGame.Common.InputManagement.Interfaces;
using UnityEngine;
using Zenject;

namespace ColorGame.Common.SpawnedObjects
{
    public class SpawnedObjectsController : MonoBehaviour
    {
        [Inject]
        SpawnedObject.Factory _factory;

        [SerializeField]
        private Transform[] _spawnPoints;

        private List<IInteractable> _interactables;

        public void CreateObjects()
        {
            _interactables = new List<IInteractable>(_spawnPoints.Length);
            foreach (Transform point in _spawnPoints)
            {
                SpawnedObject spawnedObject = _factory.Create();
                spawnedObject.Initialize(point.position);
                _interactables.Add(spawnedObject);
            }
        }

        public void MakeСlickable(bool isClickable)
        {
            foreach (IInteractable interactable in _interactables)
            {
                interactable.MakeClickable(isClickable);
            }
        }
        public void ChangeColors(ColorSet correctColorSet, List<ColorSet> retsOfColorSets)
        {
            List<IInteractable> list = _interactables.OrderBy(_ => Random.value).ToList();
            list[0].ChangeColor(correctColorSet);
            for (int i = 0; i < retsOfColorSets.Count; i++)
            {
                list[i+1].ChangeColor(retsOfColorSets[i]);
            }
        }
    }
}