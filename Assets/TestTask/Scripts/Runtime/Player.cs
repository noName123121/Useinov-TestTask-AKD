using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _orientation;
        [SerializeField] private MovementSettings _movementSettings;
        [SerializeField] private CameraControllerSettings _cameraFollowSettings;

        [SerializeField] private MobileInput _mobileInput;

        private Camera _playerCamera;

        private CharacterController _characterController;
        private IInputHandler _inputHandler;
        private IMover _mover;
        private CameraController _cameraController;
        private PickUpHand _pickUpHand;

        public bool TryShipItem()
        {
            return _pickUpHand.TryRemove();
        }

        private void Start()
        {
            // Detect device type
            // It can simulate handheld device when enabled simulation in Editor
            var deviceType = UnityEngine.Device.SystemInfo.deviceType;

            if (deviceType == DeviceType.Desktop)
            {
                var desktopInputHandler = new DesktopInputHandler();
                desktopInputHandler.Initialize();
                _inputHandler = desktopInputHandler;
            }
            else if (deviceType == DeviceType.Handheld)
            {
                var mobileInputHandler = new MobileInputHandler();
                mobileInputHandler.Initialize(_mobileInput);
                _inputHandler = mobileInputHandler;

                _mobileInput.gameObject.SetActive(true);
            }

            _characterController = GetComponent<CharacterController>();
            var characterMover = new CharacterMover();
            characterMover.Initialize(_characterController, transform, _orientation, _movementSettings);
            _mover = characterMover;

            _playerCamera = Camera.main;
            _cameraController = new CameraController();
            _cameraController.Initialize(Camera.main.transform, _orientation, _cameraFollowSettings);

            _pickUpHand = GetComponent<PickUpHand>();
        }

        private void Update()
        {
            _inputHandler.ReadInput();

            // Interact with detected object
            IInteractable interactable = _inputHandler.GetInteractable();
            if (interactable is PickableItem)
            {
                if (_pickUpHand.TryPickUp((PickableItem)interactable))
                {
                    interactable.Interact(this);
                }
            }
            //

            _cameraController.Follow();
            _cameraController.Look(_inputHandler.GetLookInput());

            _mover.Move(_inputHandler.GetMoveInput());

            _inputHandler.ConsumeInput();
        }
    }
}
