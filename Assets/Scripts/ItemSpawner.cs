using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject spawnedObject;

    public void spawnItem()
    {
        GameObject newItem = Instantiate(spawnedObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
