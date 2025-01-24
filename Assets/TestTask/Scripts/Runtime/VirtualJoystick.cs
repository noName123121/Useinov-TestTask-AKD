using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Useinov.TestTask.Runtime
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private Image backPanel;
        private Image knob;

        private Vector2 _inputDirection;

        public Vector2 InputVector => _inputDirection;

        /// <summary>
        /// Get the joystick UI
        /// </summary>
        private void Start()
        {
            backPanel = transform.GetComponent<Image>();
            knob = transform.GetChild(0).GetComponent<Image>();
        }

        public void OnDrag(PointerEventData pointerEventData)
        {
            Vector2 position = Vector3.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle
                (backPanel.rectTransform,
                    pointerEventData.position,
                    pointerEventData.pressEventCamera,
                    out position))
            {
                // Get the touch position
                position.x = (position.x / backPanel.rectTransform.sizeDelta.x);
                position.y = (position.y / backPanel.rectTransform.sizeDelta.y);

                // Normalize touch position so knob will not cross the background edge
                if (position.magnitude > 1f)
                    position = position.normalized;

                // Move the knob
                knob.rectTransform.anchoredPosition =
                    new Vector3(position.x * (backPanel.rectTransform.sizeDelta.x / 3),
                        position.y * (backPanel.rectTransform.sizeDelta.y / 3));

                _inputDirection = position;
            }
        }

        /// <summary>
        /// Click on the knob.
        /// </summary>
        /// <param name="pointerEventData">Data from the touch.</param>
        public virtual void OnPointerDown(PointerEventData pointerEventData)
        {
            OnDrag(pointerEventData);
        }

        /// <summary>
        /// Click off the knob.
        /// </summary>
        /// <param name="pointerEventData">Data from the touch.</param>
        public virtual void OnPointerUp(PointerEventData pointerEventData)
        {
            _inputDirection = Vector3.zero;
            knob.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}
