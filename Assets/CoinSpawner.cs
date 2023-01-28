using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    public class CoinSpawner : MMMonoBehaviour
    {
        public List<GameObject> objectPool = new List<GameObject>();
        public ParticleSystem coinsParticles;
        public GameObject coin;
        public Vector3 coinPos = Vector3.zero;

        private void Awake()
        {
            InitializePool(10, coin);    
        }

        private void Update()
        {
			if (GetComponent<Health>() != null)
			{
				if (GetComponent<Health>().CurrentHealth <= 0)
				{
                    print("death");
                    coinsParticles.Play();
                    SpawnCoins();
                }
			}
        }
        public void InitializePool(int amount, GameObject go)
        {
            GameObject newObj;

            if (amount > 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    newObj = Instantiate(go);
                    newObj.gameObject.SetActive(false);
                    newObj.gameObject.transform.position = this.transform.position;
                    objectPool.Add(newObj);
                }
            }

        }

        void SpawnCoins()
        {
            for (int i = 0; i < objectPool.Count; i++)
            {
                if (objectPool[i].gameObject.activeInHierarchy == false)
                {
                    objectPool[i].gameObject.SetActive(true);
                    if (objectPool[i].GetComponent<CoinInit>().hasCoinBeenSet == false)
                    {
                        objectPool[i].transform.position = this.transform.position;
                        objectPool[i].GetComponent<CoinInit>().hasCoinBeenSet = true;
                    }

                    objectPool[i].GetComponent<Rigidbody>().AddForce(1f * new Vector3(
                        UnityEngine.Random.Range(-1,1),
                        UnityEngine.Random.Range(-1, 1),
                        UnityEngine.Random.Range(-1, 1)));
                    
                }
            }
        }

    }

}
