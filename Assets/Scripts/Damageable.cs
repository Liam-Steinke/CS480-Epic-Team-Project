using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this interface to anything that can be damaged:
// * Player
// * Enemies
// * Breakable objects
// * Etc...
public interface Damageable
{
    public void TakeDamage(float damage, string bodyPart);
 }
