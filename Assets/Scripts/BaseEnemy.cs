using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    // State machine
    private enum States {SEEK, ENGAGE, ATTACK, DIE};
    private States state = States.SEEK;

    // Enemy stats
    public int health = 20;

    // Navigation agent stuff
    //
    // SerializeField lets you look at private
    // variables in the Inspector
    //
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject target;

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case States.SEEK:
                if (target != null) {
                    agent.SetDestination(target.transform.position);
                }
                break;
            case States.ENGAGE:
                break;
            case States.ATTACK:
                break;
            case States.DIE:
                break;
        }
    }
}
