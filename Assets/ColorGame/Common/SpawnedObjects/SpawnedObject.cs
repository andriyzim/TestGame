using ColorGame.Common.ColorManagement;
using ColorGame.Common.InputManagement.Interfaces;
using ColorGame.Common.ServiceLocator;
using ColorGame.Common.Services.Interfaces;
using ColorGame.Common.SpawnedObjects.Interfaces;
using UnityEngine;

namespace ColorGame.Common.SpawnedObjects
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(MeshRenderer))]
    public class SpawnedObject : MonoBehaviour, IClickable, IInitializible
    {
        [SerializeField]
        
        private ColorName _myColorName;
        private MeshRenderer _meshRenderer;
        private bool _isClickable;
        private IColorService ColorService => Service.Instance.Get<IColorService>();
        private MeshRenderer MeshRenderer =>_meshRenderer ??= GetComponent<MeshRenderer>();

       

        public void Initialize(ColorSet colorSet)
        {
            _myColorName = colorSet.colorName;
            MeshRenderer.material.color = colorSet.Color;
        }

        public void Click()
        {
            if (!_isClickable)
            {
                return;
            }

            if (ColorService.ColorSelected(colorName: _myColorName))
            {
                DoPositiveAction();
            }
            else
            {
                DoNegativeAction();
            }
        }

        public void MakeClickable(bool isClickable)
        {
            _isClickable = isClickable;
        }

        private void DoPositiveAction()
        {
            Debug.Log("PositiveAction {}");
        }

        private void DoNegativeAction()
        {
            Debug.Log("NegativeAction");
        }
    }
}