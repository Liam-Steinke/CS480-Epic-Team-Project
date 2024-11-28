using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    private GameObject enemyInstance;
    public float timer = 10.0f;
    private float originalTimer = 0f;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        originalTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && timer >= 0f)
        {
            timer -= Time.deltaTime;
            Spawn();
        }
    }
    
    // Resets the timer
    void ResetTimer()
    {
        timer = originalTimer;
    }

    // Sets if the spawner is active or not
    void SetSpawnActive(bool spawnState)
    {
        canSpawn = spawnState;
    }

    // If timer is less or equal to zero, it instantiates a new enemy
    void Spawn()
    {
        if (timer <= 0)
        {
            // Plans to implement a switch statement for different enemies
            enemyInstance = Instantiate(enemy, gameObject.transform);
            enemyInstance.GetComponent<WeaponIK>().SetTarget(player.transform);
            enemyInstance.GetComponent<BaseEnemy>().SetTarget(player);
            ResetTimer();
        }
    }
}
