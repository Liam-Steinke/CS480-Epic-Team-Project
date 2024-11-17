using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Trigger events when a player enters a trigger.
/// </summary>
public class TriggerDamage : MonoBehaviour
{
    public UnityEvent onTrigger;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (onTrigger != null)
            {
                onTrigger.Invoke();
            }
        }
    }
}
