using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public GameObject enemy;
    Cooldown spawnTimer = new Cooldown();
    Cooldown spawnCooldown = new Cooldown();
    public List<GameObject> objectPool = new List<GameObject>();
    public Transform theSpawn;
    public int totalAgents = 25;
    public int maxAgentsAliveAtOnce = 5;
    void Awake()
    {
        InitializePool(totalAgents, enemy);

        Spawn(5);
    }
    void Update()
    {
        // Invoke("UpdateAllAgents", 10f);
        Spawner();
    }
    Vector3 spawnInCircle(Vector3 spawnPoint)
    {
        return new Vector3(spawnPoint.x + Random.Range(-10f, 10f), spawnPoint.y, spawnPoint.z + Random.Range(-10f, 10f));
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
                objectPool.Add(newObj);
            }
        }

    }
    void Spawn(int amount)
    {
        //StartCoroutine(UpdateCurrentAgents());
        for (int i = 0; i < amount; i++)
        {
            if (objectPool[i].gameObject.activeInHierarchy == false)
            {
                objectPool[i].transform.position = spawnInCircle(theSpawn.position);

                //if (!objectPool[i].gameObject.GetComponent<Health>().alive)
                //{
                //    objectPool[i].gameObject.GetComponent<NavMeshAgent>().enabled = true;
                //    objectPool[i].gameObject.GetComponent<NavMeshAgent>().speed = 5f;
                //    objectPool[i].gameObject.GetComponent<CapsuleCollider>().enabled = true;
                //    objectPool[i].gameObject.GetComponent<Health>().alive = true;
                //}

                objectPool[i].gameObject.SetActive(true);
                break;
            }
        }
    }
    void Spawner()
    {
        if (!spawnCooldown.cooldown)
        {
            Spawn(maxAgentsAliveAtOnce);
            spawnCooldown.cooldown = true;
        }
        if (spawnCooldown.cooldown)
        {
            //what if timer has passed and everyones alive, will it keep counting or restart, it will restart
            spawnCooldown.cooldowntimer += Time.deltaTime;
            if (spawnCooldown.cooldowntimer > 1f)
            {
                spawnCooldown.cooldown = false;
                spawnCooldown.cooldowntimer = 0f;
            }
        }

    }
}
