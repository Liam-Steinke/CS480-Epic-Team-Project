using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime = 2.0f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
