using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    public class PickableItem : MonoBehaviour, IInteractable
    {
        public void Interact(Player player)
        {
            // Disable collider so picked item will not interfere collisions
            var collider = GetComponent<Collider>();
            collider.enabled = false;

            Debug.Log("Picked up");
        }
    }
}