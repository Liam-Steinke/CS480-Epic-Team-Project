using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BaseEnemy : MonoBehaviour, Damageable
{
    // State machine
    private enum States { SEEK, ENGAGE, ATTACK, DIE };
    private States state = States.ENGAGE;

    // Enemy stats
    public float health = 3.0f;
    // Time between subsequent attacks
    public float attackSpeed = 2.0f;
    public float damage = 1f;
    public float bulletCount = 1;
    public float bulletRange = 100f;
    public bool chasePlayer = false;
    public float inaccuracy = 0.2f;

    // Internal delay between attacks
    private float attackTimer = 2.5f;

    // Navigation agent stuff
    public NavMeshAgent agent;
    public GameObject target;

    // Animation stuff
    public Animator animator;
    private string currentAnimation = "Idle";

    // Shooting stuff
    public Transform shootPoint;
    public GameObject projectile;
    public AudioSource shootSound;
    public ParticleLight muzzleFlash;
    public Tracer tracer;

    // Item dropping on death stuff
    public ItemSpawner[] ItemSpawners;
    public GameObject heldWeapon;

    // Sightline stuff
    private static float DEFAULT_PATIENCE = 3f;
    private float patience = DEFAULT_PATIENCE;
    private bool seeTarget = false;


    public Rigidbody[] RigidBodies;
    public AudioSource deathSound;

    void Start()
    {
        ToggleRagdoll(false);
        foreach (Target t in GetComponentsInChildren<Target>())
        {
            t.enemyParent = this;
        }
        //AudioManager.singleton.AddSound(shootSound);


        // Randomize initial patience and attack delay
        patience += Random.Range(-1, 3);
        attackTimer = attackSpeed + Random.Range(0f, 1f);
    }

    void ToggleRagdoll(bool toggle)
    {
        RigidBodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in RigidBodies)
        {
            rb.isKinematic = !toggle;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //do nothing if paused 
        if (PauseMenu.paused)
        {
            return;
        }

        // Disgusting state machine
        switch (state)
        {
            case States.SEEK:
                if (target != null)
                {
                    LookAtTarget();
                    agent.SetDestination(target.transform.position);
                    if (!agent.pathPending)
                    {
                        if (seeTarget && !chasePlayer)
                        {
                            state = States.ENGAGE;
                            ChangeAnimation("Idle");
                            agent.SetDestination(transform.position);
                            return;
                        }
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                            {
                                state = States.ENGAGE;
                                ChangeAnimation("Idle");
                            }
                        }
                    }
                }
                break;
            case States.ENGAGE:
                attackTimer -= Time.deltaTime;
                LookAtTarget();

                if (patience <= 0)
                {
                    ChangeAnimation("Walk");
                    state = States.SEEK;
                }
                else
                {
                    if (attackTimer <= 0)
                    {
                        if (seeTarget)
                        {
                            state = States.ATTACK;
                            ChangeAnimation("Idle");
                            shoot();
                            attackTimer = attackSpeed + Random.Range(0f, 1f);
                            return;
                        }
                    }
                }






                break;
            case States.ATTACK:
                if (chasePlayer)
                {
                    ChangeAnimation("Walk");
                    state = States.SEEK;
                }
                else
                {
                    state = States.ENGAGE;
                }


                break;
            case States.DIE:
                break;
        }
    }

    // Use to transition smoothly between animations
    public void ChangeAnimation(string animation)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, 0.2f);
        }

    }

    // Create shot when shooting
    protected virtual void shoot()
    {

        shootSound.Play();
        muzzleFlash.Activate();

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 miss = new Vector3(Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy));
            createShot(miss);
        }

    }

    protected void createShot(Vector3 offset)
    {
        RaycastHit hit;
        Tracer trace = Instantiate(tracer, new Vector3(0, 0, 0), Quaternion.identity);


        Vector3 dir = (target.transform.position - shootPoint.transform.position + offset).normalized;
        if (Physics.Raycast(shootPoint.transform.position, dir, out hit, bulletRange))
        {
            //Debug.Log(hit.transform.name);
            trace.AimAt(shootPoint.transform.position, hit.point);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
        else
        {
            trace.AimAt(shootPoint.transform.position, shootPoint.transform.position + (shootPoint.transform.forward * bulletRange));
        }
    }

    // Enemy takes damage
    public void TakeDamage(float damage, string bodyPart)
    {
        health -= damage;
        print("OUCH");
        if (health <= 0.0f)
        {
            if (state != States.DIE)
            {
                foreach (ItemSpawner i in GetComponentsInChildren<ItemSpawner>())
                {
                    i.spawnItem();
                }
                if (heldWeapon != null)
                {
                    heldWeapon.SetActive(false);
                }

                animator.enabled = false;
                GetComponent<WeaponIK>().enabled = false;
                ToggleRagdoll(true);
                gameObject.SendMessageUpwards("OnEnemyKill");
                state = States.DIE;
                Destroy(gameObject, 5f);
                int screamChance = Random.Range(0, 3);
                if (screamChance == 0) {
                    deathSound.Play();
                }
            }
        }
    }

    // Turn to face player or other target
    public void LookAtTarget()
    {
        if (target != null)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

            // Check if player can be seen. Causes chase behavior if patience runs out.
            RaycastHit hit;
            Vector3 dir = (target.transform.position - (transform.position + new Vector3(0, 1.8f, 0))).normalized;
            if (Physics.Raycast(shootPoint.transform.position, dir, out hit, bulletRange))
            {
                // Debug.Log(hit.transform.gameObject.layer);
                if (hit.transform.gameObject.layer == 8)
                {
                    if (!seeTarget)
                    {
                        patience = DEFAULT_PATIENCE + Random.Range(-1, 2);
                        seeTarget = true;
                        attackTimer = attackSpeed + Random.Range(0f, 1f);
                    }

                }
                else
                {
                    patience -= Time.deltaTime;
                    seeTarget = false;
                }
                return;
            }
        }

    }

    // Setter for target variable in case not set in editor
    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}
