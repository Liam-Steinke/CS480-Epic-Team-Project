using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum States { IMMORTAL, ALIVE, DEAD};
    [SerializeField] private States state = States.IMMORTAL;

    public int maxHealth = 100;
    public int health = 100;

    private bool canTakeDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case States.IMMORTAL:
                canTakeDamage = false;
                break;
            case States.ALIVE:
                canTakeDamage = true;
                break;
            case States.DEAD:
                break;
        }
    }

    public void setState(States newState)
    {
        state = newState;
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage)
        {
            return;
        }

        health -= damage;
        //Debug.Log("Player health: " + health);
        if (health <= 0)
        {
            Die();
        }
        else
        {
            //Maybe reproduce a hit sound
        }
    }

    private void Die()
    {
        // Reproduce death sound and implement a restart option
        // TODO: Implement restart functionality and sound reproduction
        //Debug.Log("Player died");
    }
}
