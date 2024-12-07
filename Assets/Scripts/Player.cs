using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour, Damageable
{

    public enum States { IMMORTAL, ALIVE, DEAD };
    [SerializeField] private States state = States.IMMORTAL;

    public float maxHealth = 100;
    public float health = 100;
    public SceneLoader sceneLoader;

    private bool canTakeDamage = false;
    public AudioSource deathSFX, damageSFX;
    public HealthBar healthBar;
    public DamageFlash damageFlash;



    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        AudioManager.singleton.AddSound(deathSFX);
        AudioManager.singleton.AddSound(damageSFX);

    }

    // Update is called once per frame
    void Update()
    {
        //do nothing if paused 
        if (PauseMenu.paused)
        {
            return;
        }

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

    public void TakeDamage(float damage, string bodyPart)
    {
        if (damageFlash != null)
        {
            damageFlash.DamageFlasher();
        }

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
            damageSFX.Play();
        }
    }

    public void Heal(float amount)
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
            // healFailedSFX.Play();
            return;
        }
        health += amount;
        healthBar.SetHealth(health);
        // healSFX.Play();
    }
    private void Die()
    {
        deathSFX.Play();
        setState(States.DEAD);
        health = 0;
        sceneLoader.GoToScene(0);

        // Reproduce death sound and implement a restart option
        // TODO: Implement restart functionality and sound reproduction
        //Debug.Log("Player died");
    }
}
