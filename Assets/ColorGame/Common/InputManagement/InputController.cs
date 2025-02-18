using ColorGame.Common.InputManagement.Interfaces;
using UnityEngine;

namespace ColorGame.Common.InputManagement
{
    public class InputController : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    IInteractable interactableObject = hit.transform.gameObject.GetComponent<IInteractable>();
                    if (interactableObject != null)
                    {
                        interactableObject.Interact();
                    }
                }
            }
        }
    }
}