using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnerEvents : MonoBehaviour
{
    public int childCount;
    // Start is called before the first frame update
    void Start()
    {
        childCount = gameObject.transform.childCount;        
    }

    public UnityEvent onStartCombat;

    public void OnStartCombat()
    {
        if (onStartCombat != null)
        {
            onStartCombat.Invoke();
        }
    }

    
    public void OnEnemyKill()
    {
        childCount--;
        if (childCount <= 0)
        {
            OnEndCombat();
        }
    }

    public UnityEvent onEndCombat;

    public void OnEndCombat()
    {
        if (onEndCombat != null)
        {
            onEndCombat.Invoke();
        }
    }
}
