using System.Collections;
using System.Collections.Generic;
using ColorGame.Common.ColorManagement;
using ColorGame.Common.Services.Interfaces;
using ColorGame.Common.SpawnedObjects;
using UnityEngine;
using Zenject;

namespace ColorGame.Common.GameManagement
{
    [RequireComponent(typeof(SpawnedObjectsController))]
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private ColorSetAsset _colorSetAsset;

        private SpawnedObjectsController _spawnedObjectsController;
       
        [Inject]
        private IColorService _colorService;
        
        private void Awake()
        {
            _spawnedObjectsController = GetComponent<SpawnedObjectsController>();
            _colorService.Initialize(_colorSetAsset.ColorSets);
        }

        private IEnumerator Start()
        {
            yield return  new WaitForEndOfFrame();
            _spawnedObjectsController.CreateObjects();
            _colorService.SetNextColor();            
        }

        private void OnEnable()
        {
            _colorService.ColorChangedEvent += ColorChangeHandler;
            _colorService.ColorSelectedEvent += ColorSelectionHandler;
            
        }
        private void OnDisable()
        {
            _colorService.ColorChangedEvent -= ColorChangeHandler;
            _colorService.ColorSelectedEvent -= ColorSelectionHandler;
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
            _spawnedObjectsController.MakeСlickable(false);
            yield return new WaitForSeconds(_colorSetAsset.DelayBeforeNextColor);
            _colorService.SetNextColor();
        }

        private void ColorChangeHandler(ColorSet correctColorSet, List<ColorSet> retsOfColorSets)
        {
            _spawnedObjectsController.MakeСlickable(true);
            _spawnedObjectsController.ChangeColors(correctColorSet,retsOfColorSets);
        }
    }
}