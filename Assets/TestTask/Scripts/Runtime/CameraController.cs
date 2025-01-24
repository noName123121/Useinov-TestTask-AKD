using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    [System.Serializable]
    public class CameraControllerSettings
    {
        [SerializeField] public Transform CameraSocket;

        [SerializeField, Min(0f)] public float XSensitivity;
        [SerializeField, Min(0f)] public float YSensitivity;

        [SerializeField, Range(-90f, 90f)] public float MinXClamp;
        [SerializeField, Range(-90f, 90f)] public float MaxXClamp;
    }

    public class CameraController
    {
        private Transform _orientation, _playerCamera;
        private CameraControllerSettings _settings;

        private float _xRotation;
        private float _yRotation;

        public void Initialize(Transform playerCamera, Transform orientation, CameraControllerSettings settings)
        {
            _playerCamera = playerCamera;
            _orientation = orientation;
            _settings = settings;

            if (_playerCamera == null)
                return;

            _xRotation = _playerCamera.transform.eulerAngles.x;
            _yRotation = _playerCamera.transform.eulerAngles.y;
        }

        public void Follow()
        {
            if (_settings.CameraSocket == null)
                return;

            _playerCamera.transform.position = _settings.CameraSocket.position;
        }

        public void Look(Vector2 lookInput)
        {
            if (_orientation == null)
                return;
            if (_playerCamera == null)
                return;

            _yRotation += lookInput.x * Time.deltaTime * _settings.YSensitivity;
            _xRotation -= lookInput.y * Time.deltaTime * _settings.XSensitivity;

            _xRotation = Mathf.Clamp(_xRotation, _settings.MinXClamp, _settings.MaxXClamp);

            _playerCamera.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
            _orientation.rotation = Quaternion.Euler(0f, _yRotation, 0f);
        }
    }
}