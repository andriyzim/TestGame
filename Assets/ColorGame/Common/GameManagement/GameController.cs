using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ColorGame.Common.ColorManagement;
using ColorGame.Common.InputManagement.Interfaces;
using ColorGame.Common.ServiceLocator;
using ColorGame.Common.Services.Interfaces;
using ColorGame.Common.SpawnedObjects.Interfaces;
using UnityEngine;

namespace ColorGame.Common.GameManagement
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private ColorSetAsset _colorSetAsset;

        [SerializeField]
        private GameObject [] _spawnedObjs;
        
        private List<IInitializible> _spawnedObjects;
        private List<IClickable> _clickables;

        private const float DelayBeforeNextColor = 1.5f;

        private IColorService ColorService => Service.Instance.Get<IColorService>();

        private void Awake()
        {
            _spawnedObjects = new List<IInitializible>();
            _clickables = new List<IClickable>();
            foreach (GameObject obj in _spawnedObjs)
            {
                _spawnedObjects.Add(obj.GetComponent<IInitializible>());
                _clickables.Add(obj.GetComponent<IClickable>());
            }
        }

        private void OnEnable()
        {
            ColorService.ColorChangedEvent += ColorChangeHandler;
            ColorService.ColorSelectedEvent += ColorSelectionHandler;
            
            ColorService.Initialize(_colorSetAsset.ColorSets);
        }
        private void OnDisable()
        {
            ColorService.ColorChangedEvent -= ColorChangeHandler;
            ColorService.ColorSelectedEvent -= ColorSelectionHandler;
        }

        private void ColorSelectionHandler(bool isCorrect)
        {
            if (isCorrect)
            {
                StartCoroutine(SelectNextColor());
            }
        }

        private IEnumerator SelectNextColor()
        {
            foreach (IClickable clickable in _clickables)
            {
                clickable.MakeClickable(false);
            }
            yield return new WaitForSeconds(DelayBeforeNextColor);
            ColorService.SetNextColor();
        }

        private void ColorChangeHandler(ColorSet correctColorSet, List<ColorSet> retsOfColorSets)
        {
            List<IInitializible> list = _spawnedObjects.OrderBy(_ => Random.value).ToList();
            list[0].Initialize(correctColorSet);
            for (int i = 0; i < retsOfColorSets.Count; i++)
            {
                list[i+1].Initialize(retsOfColorSets[i]);
            }
            foreach (IClickable clickable in _clickables)
            {
                clickable.MakeClickable(true);
            }
        }
    }
}