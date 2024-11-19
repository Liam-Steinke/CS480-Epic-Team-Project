using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, BaseEntity
{
    public enum States { IMMORTAL, ALIVE, DEAD};
    [SerializeField] private States state = States.IMMORTAL;

    public int maxHealth = 100;
    public int health = 100;
    public SceneLoader sceneLoader;

    private bool canTakeDamage = false;
    //public AudioSource deathSFX, damageSFX;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
                canTakeDamage = false;
                break;
        }
    }

    public void setState(States newState)
    {
        state = newState;
    }

    public void TakeDamage(int damage, double multiplier)
    {
        if (!canTakeDamage)
        {
            return;
        }

        health -= damage;
        healthBar.SetHealth(health);
        //Debug.Log("Player health: " + health);
        if (health <= 0)
        {
            // We either reproduce damageSFX or deathSFX but not both
            Die();
        }
        else
        {
            //Maybe reproduce a hit sound
            //  damageSFX.Play();
        }
    }

    private void Die()
    {

        setState(States.DEAD);
        health = 0;
        sceneLoader.GoToScene(0);
        //deathSFX.Play();
        // Reproduce death sound and implement a restart option
        // TODO: Implement restart functionality and sound reproduction
        //Debug.Log("Player died");
    }
}
