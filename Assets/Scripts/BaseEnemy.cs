using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    // State machine
    private enum States {SEEK, ENGAGE, ATTACK, DIE};
    private States state = States.ENGAGE;

    // Enemy stats
    public int health = 20;
    public float attackTimer = 3.0f;

    // Navigation agent stuff
    public NavMeshAgent agent;
    public GameObject target;

    // Animation stuff
    public Animator animator;
    private string currentAnimation = "Idle";

    // Update is called once per frame
    void Update()
    {
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
                if (attackTimer <= 0)
                {
                    state = States.ATTACK;
                    ChangeAnimation("Idle");
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
}
