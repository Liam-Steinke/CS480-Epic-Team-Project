using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHurt : MonoBehaviour
{
    public float damage = 1f;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        Target target = other.gameObject.GetComponent<Target>();
        if (target != null) {
            target.TakeDamage(damage);
        }
    }
}
