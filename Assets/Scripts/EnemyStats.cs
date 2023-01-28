using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    public class EnemyStats : Stats
    {
        private void Update()
        {
            if (GetComponent<Health>() != null)
            {
                if (GetComponent<Health>().CurrentHealth <= 0)
                {
                    print("asd");
                    StartCoroutine(AddExperience(1));
                }
            }
        }

        IEnumerator AddExperience(float _exp)
        {
            yield return null;
            MGameManager.instance.playerStats.currentExperience += _exp;
            yield return null;
        }
    }
}
