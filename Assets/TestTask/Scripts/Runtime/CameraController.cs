using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    [System.Serializable]
    public class CameraControllerSettings
    {
        [SerializeField] public Transform CameraSocket;

        [SerializeField, Min(0f)] public float xSensitivity;
        [SerializeField, Min(0f)] public float ySensitivity;
    }

    public class CameraController
    {
        private const float MIN_X_CLAMP = -90f;
        private const float MAX_X_CLAMP = 90f;

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

            _yRotation += lookInput.x * Time.deltaTime * _settings.ySensitivity;
            _xRotation -= lookInput.y * Time.deltaTime * _settings.xSensitivity;

            _xRotation = Mathf.Clamp(_xRotation, MIN_X_CLAMP, MAX_X_CLAMP);

            _playerCamera.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
            _orientation.rotation = Quaternion.Euler(0f, _yRotation, 0f);
        }
    }
}