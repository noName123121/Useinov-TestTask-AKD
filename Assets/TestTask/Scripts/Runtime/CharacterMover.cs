using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    [System.Serializable]
    public class MovementSettings
    {
        [SerializeField, Min(0f)] public float MaxSpeed;
        [SerializeField, Min(0f)] public float GravitySpeed;
    }

    public class CharacterMover : IMover
    {
        private CharacterController _characterController;
        private Transform _target;
        private Transform _orientation;
        private MovementSettings _settings;

        public void Initialize(CharacterController characterController, Transform target, Transform orientation, MovementSettings movementSettings = default)
        {
            _characterController = characterController;
            _target = target;
            _orientation = orientation;
            _settings = movementSettings;
        }

        public void Move(Vector2 input)
        {
            if (_characterController == null)
            {
                Debug.LogError("Mover character controller is set to null!");
                return;
            }
            if (_target == null)
            {
                Debug.LogError("Mover target is set to null!");
                return;
            }
            if (_orientation == null)
            {
                Debug.LogError("Mover orientation is set to null!");
                return;
            }

            var direction = _orientation.forward * input.y + _orientation.right * input.x;
            direction = direction.normalized;

            var velocity = direction * _settings.MaxSpeed;
            // Always apply gravity to ensure isGrounded check works
            velocity += Vector3.down * _settings.GravitySpeed;

            _characterController.Move(velocity * Time.deltaTime);
        }
    }
}
