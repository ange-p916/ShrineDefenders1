using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MoreMountains.TopDownEngine
{
    public class PickupController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.transform.root.name.Contains("Coin"))
            {
                MGameManager.instance.coins++;
            }
        }
    }
}