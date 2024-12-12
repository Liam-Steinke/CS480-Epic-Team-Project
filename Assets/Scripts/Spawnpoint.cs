using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public GameObject spawnEffect;
    private GameObject enemyInstance;

    //public float timer = 10.0f;
    //private float originalTimer = 0f;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        //originalTimer = timer;
        player = GameObject.Find("HitBox");
    }

    // Update is called once per frame
    void Update()
    {
        //do nothing if paused 
        if (PauseMenu.paused)
        {
            return;
        }
 
        //&& timer >= 0f
        //if (canSpawn)
        //{
        //    //timer -= Time.deltaTime;
        //    Spawn();
        //}
    }

    // Resets the timer
    //public void ResetTimer()
    //{
    //    timer = originalTimer;
    //}

    // Sets if the spawner is active or not
    public void SetSpawnActive(bool spawnState)
    {
        canSpawn = spawnState;

    }

    // If timer is less or equal to zero, it instantiates a new enemy
    public void Spawn()
    {
        enemyInstance = Instantiate(enemy, gameObject.transform);
        enemyInstance.GetComponent<WeaponIK>().SetTarget(player.transform);
        enemyInstance.GetComponent<BaseEnemy>().SetTarget(player);
        spawnEffect.SetActive(true);
        //ResetTimer();
        //if (timer <= 0)
        //{
        //    // Plans to implement a switch statement for different enemies

        //}
    }
}
