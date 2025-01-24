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
        private bool _pressed;

        public Vector2 InputVector => _touchDistance;

        void Update()
        {
            if (_pressed)
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

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            _pointerId = eventData.pointerId;
            _pointerLastPosition = eventData.position;
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
        }
    }
}
