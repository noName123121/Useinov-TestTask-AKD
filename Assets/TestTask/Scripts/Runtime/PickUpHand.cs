using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    public class PickUpHand : MonoBehaviour
    {
        [SerializeField] private Transform _pickUpSocket;

        private PickableItem _currentItem;

        public bool TryPickUp(PickableItem itemToPickUp)
        {
            if (_currentItem != null)
                return false;
            if (itemToPickUp == null)
                return false;

            _currentItem = itemToPickUp;
            AttachItemToSocket();

            return true;
        }

        public bool TryRemove()
        {
            if (_currentItem == null)
                return false;

            Destroy(_currentItem.gameObject);
            _currentItem = null;
            return true;
        }

        private void AttachItemToSocket()
        {
            // Attach item to hands
            _currentItem.transform.parent = _pickUpSocket.transform;
            _currentItem.transform.localPosition = Vector3.zero;
            _currentItem.transform.localRotation = Quaternion.identity;
        }
    }
}