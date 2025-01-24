using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    public interface IInputHandler
    {
        void ReadInput();
        Vector2 GetMoveInput();
        Vector2 GetLookInput();
        GameObject GetInteractionObject();
        void ConsumeInput();
    }

    public class MobileInputHandler : IInputHandler
    {
        private const float _lookInputMultiplier = 10f;

        private MobileInput _mobileInput;

        private Vector2 _moveInput;
        private Vector2 _lookInput;

        private bool _isLooking;

        public void Initialize(MobileInput mobileInput)
        {
            _mobileInput = mobileInput;

            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = true;
        }

        public void ReadInput()
        {
            //TODO: Make mobile input here

            _moveInput = _mobileInput.GetMoveInput();
            _lookInput = _mobileInput.GetLookInput();
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