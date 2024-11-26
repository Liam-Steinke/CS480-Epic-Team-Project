using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Damageable point on a game object
//
// Use the "bodyPart" field to add context to damage,
// like adding "head" so enemies do a headshot animation.
//
// Set the player variable to use this for the player hurtbox.
public class Target : MonoBehaviour
{
    public float damageMultiplier = 1f;
    // If facing front of enemy:
    // Set to -1 for far left, 1 for far right.
    // Used to determine pain animations.
    public float damageSide = 0f;
    public string bodyPart = "";
    
    public BaseEnemy enemyParent;
    public Player player;

    // Tell this target's parent to get damaged
    public void TakeDamage(float damage) {
        if (player != null) {
            player.TakeDamage(damage, "");
        } else {
            enemyParent.TakeDamage(damage * damageMultiplier, bodyPart);
        }
    }
}
