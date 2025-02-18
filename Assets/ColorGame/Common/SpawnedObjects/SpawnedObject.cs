using ColorGame.Common.ColorManagement;
using ColorGame.Common.InputManagement.Interfaces;
using ColorGame.Common.Services.Interfaces;
using ColorGame.Common.SpawnedObjects.Interfaces;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace ColorGame.Common.SpawnedObjects
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(MeshRenderer))]
    public class SpawnedObject : MonoBehaviour, IInteractable, IInitializible<Vector3>
    {
        public class  Factory : PlaceholderFactory<SpawnedObject>
        {
            
        }
        
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private const float AnimationDurationNegative = 1f;
        private const float AnimationDurationPositive = 0.5f;
        private const float PunchScale = 0.2f;
        private const float RotationStrength = 2f;
        private const int PunchVibrato = 2;

        private ColorName _myColorName;
        private MeshRenderer _meshRenderer;
        private MaterialPropertyBlock _materialPropertyBlock;
        private bool _isClickable;
       
        private MeshRenderer MeshRenderer => _meshRenderer ??= GetComponent<MeshRenderer>();

        private MaterialPropertyBlock MaterialPropertyBlock =>
            _materialPropertyBlock ??= _materialPropertyBlock = new MaterialPropertyBlock();

        [Inject]
        private IColorService _colorService;

        public void Initialize(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, AnimationDurationNegative).SetEase(Ease.OutBounce);
        }

        public void Interact()
        {
            if (!_isClickable)
            {
                return;
            }

            if (_colorService.ColorSelected(colorName: _myColorName))
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

        public void ChangeColor(ColorSet colorSet)
        {
            _myColorName = colorSet.colorName;
            MeshRenderer.GetPropertyBlock(MaterialPropertyBlock);
            MaterialPropertyBlock.SetColor(BaseColor, colorSet.Color);
            MeshRenderer.SetPropertyBlock(MaterialPropertyBlock);
        }

        private void DoPositiveAction()
        {
            transform.DOPunchScale(Vector3.one * PunchScale, AnimationDurationPositive, PunchVibrato);
        }

        private void DoNegativeAction()
        {
            transform.DOShakeRotation(AnimationDurationNegative, Vector3.one * RotationStrength,
                randomnessMode: ShakeRandomnessMode.Harmonic);
        }
    }
}