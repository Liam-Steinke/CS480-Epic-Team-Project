using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Trigger events when a player enters a trigger.
/// </summary>
public class TriggerDamage : MonoBehaviour
{
    //public UnityEvent onTrigger;
    //public enum HitboxType { PLAYER, ENEMY };
    //public HitboxType type;
    public int baseDamage = 1;
    public double damageMultiplier = 1.0;
    public GameObject hitboxParent; // Call own take damage and handle it
    private Component component;
    private BaseEntity damagedEntity;

    void Start()
    {
        if (hitboxParent.GetComponent("Player") != null)
        {
            damagedEntity =(Player) hitboxParent.GetComponent<Player>();
        }
        else if (hitboxParent.GetComponent("BaseEnemy") != null)
        {
            damagedEntity = (BaseEnemy) hitboxParent.GetComponent<BaseEnemy>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            damagedEntity.TakeDamage(baseDamage, damageMultiplier);
            //if (onTrigger != null)
            //{
            //    onTrigger.Invoke();
            //}
        }
    }
}
