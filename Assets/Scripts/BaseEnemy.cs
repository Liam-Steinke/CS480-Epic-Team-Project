using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    // State machine
    private enum States {SEEK, ENGAGE, ATTACK, DIE};
    private States state = States.SEEK;

    // Enemy stats
    public int health = 20;
    public float timer = 3.0f;

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
                timer -= Time.deltaTime;
                if(timer <=0)
                {
                    state = States.ATTACK;
                }
                break;
            case States.ATTACK:
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
}
