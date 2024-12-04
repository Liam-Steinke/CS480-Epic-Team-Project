using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime = 0.5f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
