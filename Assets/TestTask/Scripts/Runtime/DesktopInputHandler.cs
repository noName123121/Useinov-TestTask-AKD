using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    public class DesktopInputHandler : IInputHandler
    {
        private const float _lookInputMultiplier = 400f;
        private const float ItemDetectDistance = 2f;

        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private IInteractable _interactable;

        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void ReadInput()
        {
            _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _lookInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
                if (Physics.Raycast(ray, out hit, ItemDetectDistance))
                {
                    if (hit.transform.gameObject.TryGetComponent(out IInteractable interactable))
                    {
                        _interactable = interactable;
                    }
                }
            }
        }

        public Vector2 GetMoveInput()
        {
            return _moveInput;
        }

        public Vector2 GetLookInput()
        {
            return _lookInput * _lookInputMultiplier;
        }

        public IInteractable GetInteractable()
        {
            return _interactable;
        }

        public void ConsumeInput()
        {
            _moveInput = Vector2.zero;
            _lookInput = Vector2.zero;
            _interactable = null;
        }
    }
}