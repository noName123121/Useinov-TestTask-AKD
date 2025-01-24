using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    public class MobileInputHandler : IInputHandler
    {
        private const float LookInputMultiplier = 10f;
        private const float ItemDetectDistance = 2f;

        private MobileInput _mobileInput;

        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private IInteractable _interactable;

        public void Initialize(MobileInput mobileInput)
        {
            _mobileInput = mobileInput;

            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = true;
        }

        public void ReadInput()
        {
            _moveInput = _mobileInput.GetMoveInput();
            _lookInput = _mobileInput.GetLookInput();

            RaycastHit hit = new RaycastHit();
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
                {
                    // Construct a ray from the current touch coordinates
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    if (Physics.Raycast(ray, out hit, ItemDetectDistance))
                    {
                        if (hit.transform.gameObject.TryGetComponent(out IInteractable interactable))
                        {
                            _interactable = interactable;
                        }
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
            return _lookInput * LookInputMultiplier;
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