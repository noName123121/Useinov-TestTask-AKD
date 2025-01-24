using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    public class MobileInput : MonoBehaviour
    {
        [SerializeField] private VirtualJoystick _joystick;
        [SerializeField] private TouchScreen _touchScreen;

        public Vector2 GetMoveInput() => _joystick == null ? Vector2.zero : _joystick.InputVector;

        public Vector2 GetLookInput() => _touchScreen == null ? Vector2.zero : _touchScreen.InputVector;
    }
}
