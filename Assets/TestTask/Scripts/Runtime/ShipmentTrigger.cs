using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    public class ShipmentTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out Player player))
            {
                if (player.TryShipItem())
                    Debug.Log("Shipped item");
            }
        }
    }
}
