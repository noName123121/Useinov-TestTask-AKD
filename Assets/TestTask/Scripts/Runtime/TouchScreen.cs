using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Useinov.TestTask.Runtime
{
    public class TouchScreen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Vector2 _touchDistance;
        private Vector2 _pointerLastPosition;
        private int _pointerId;
        private bool _isPressed;

        public Vector2 InputDirection => _touchDistance;

        void Update()
        {
            if (_isPressed)
            {
                if (_pointerId >= 0 && _pointerId < Input.touches.Length)
                {
                    _touchDistance = Input.touches[_pointerId].position - _pointerLastPosition;
                    _pointerLastPosition = Input.touches[_pointerId].position;
                }
            }
            else
            {
                _touchDistance = Vector2.zero;
            }
        }

        /// <summary>
        /// Click on the screen.
        /// </summary>
        /// <param name="pointerEventData">Data from the touch.</param>
        public void OnPointerDown(PointerEventData pointerEventData)
        {
            _isPressed = true;
            _pointerId = pointerEventData.pointerId;
            _pointerLastPosition = pointerEventData.position;
        }

        /// <summary>
        /// Click on the screen.
        /// </summary>
        /// <param name="pointerEventData">Data from the touch.</param>
        public void OnPointerUp(PointerEventData pointerEventData)
        {
            _isPressed = false;
        }
    }
}