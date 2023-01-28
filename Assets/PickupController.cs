using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
