using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    public class DesktopInputHandler : IInputHandler
    {
        private const float _lookInputMultiplier = 400f;

        private Vector2 _moveInput;
        private Vector2 _lookInput;

        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void ReadInput()
        {
            _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _lookInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        }

        public Vector2 GetMoveInput()
        {
            return _moveInput;
        }

        public Vector2 GetLookInput()
        {
            return _lookInput * _lookInputMultiplier;
        }

        public GameObject GetInteractionObject()
        {
            throw new System.NotImplementedException();
        }

        public void ConsumeInput()
        {
            _moveInput = Vector2.zero;
            _lookInput = Vector2.zero;
        }
    }
}