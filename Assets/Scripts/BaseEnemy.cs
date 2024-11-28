using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, Damageable
{
    // State machine
    private enum States {SEEK, ENGAGE, ATTACK, DIE};
    private States state = States.ENGAGE;

    // Enemy stats
    public float health = 3.0f;
    // Time between subsequent attacks
    public float attackSpeed = 2.5f;
    public float damage = 10f;
    public float range = 100f;
    public float inaccuracy = 0.2f;

    // Internal delay between attacks
    private float attackTimer = 3.0f;

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

    public Rigidbody[] RigidBodies;

    void Start() {
        ToggleRagdoll(false);
        foreach (Target t in GetComponentsInChildren<Target>()) {
            t.enemyParent = this;
        }
    }

    void ToggleRagdoll(bool toggle) {
        RigidBodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rb in RigidBodies) {
            rb.isKinematic = !toggle;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Disgusting state machine
        switch (state) {
            case States.SEEK:
                if (target != null) {
                    agent.SetDestination(target.transform.position);
                    if (!agent.pathPending) {
                            if (agent.remainingDistance <= agent.stoppingDistance) {
                                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
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
                if (attackTimer <= 0)
                {
                    state = States.ATTACK;
                    ChangeAnimation("Idle");
                    createShot();
                    attackTimer = attackSpeed;
                }

                
                
                break;
            case States.ATTACK:
                state = States.SEEK;
                ChangeAnimation("Walk");
                break;
            case States.DIE:
                break;
        }
    }

    // Unused...
    public void Dodge()
    {
        int dodge_chance = Random.Range(0,4);
        int side = Random.Range(0,2);
        if (dodge_chance == 3) {
            if (side == 0){
                transform.position += new Vector3(4, 0);
            }
            else if (side == 1){
                transform.position += new Vector3(-4, 0);
            }
        }
    }
    // Use to transition smoothly between animations
    public void ChangeAnimation(string animation) {
        if (currentAnimation != animation) {
            currentAnimation = animation;
            animator.CrossFade(animation, 0.2f);
        }
        
    }

    // Create shot when shooting
    private void createShot() {
        shootSound.Play();
        muzzleFlash.Activate();
        RaycastHit hit;
        Vector3 miss = new Vector3(Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy));

        Vector3 dir = (target.transform.position - shootPoint.transform.position + miss).normalized;
        if (Physics.Raycast(shootPoint.transform.position, dir, out hit, range)) {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                target.TakeDamage(damage);
            }
        }
    }

    // Enemy takes damage
    public void TakeDamage(float damage, string bodyPart)
    {
        health -= damage;
        print("OUCH");
        if (health <= 0.0f) {
            animator.enabled = false;
            GetComponent<WeaponIK>().enabled = false;
            ToggleRagdoll(true);
            state = States.DIE;
        }
    }
    
    // Turn to face player or other target
    public void LookAtTarget() {
        if (target != null) {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
        }
    }
}
