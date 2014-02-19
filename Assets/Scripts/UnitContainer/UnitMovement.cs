using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour {

    public float AttackCooldown;
    public bool hold;

	NavMeshAgent agent;
    float lastAttack;
    UnitState state;

	void Start()
	{
        state = UnitState.idle;
		agent = GetComponent<NavMeshAgent>();

        lastAttack = 0;
	}

	void Update ()
	{
        if ( state == UnitState.idle )
            Idle();
        else if ( state == UnitState.moving )
            Moving();
        else if ( state == UnitState.attacking )
            Attacking();
	}

    void Idle()
    {
        // enemy is in range
        // move to attacking state
    }

    void Moving ()
    {
        // keep aiming until distance less than .1f
        if ( Vector3.Distance(agent.destination, transform.position) < .1f )
            state = UnitState.idle;
    }

    void Attacking()
    {
        if ( ( lastAttack -= Time.deltaTime ) <= 0 ) {
            lastAttack = AttackCooldown;

            // attack
        }
    }

	public void Move(Vector3 _target)
	{
        if ( hold == false )
        {
            transform.rotation = Quaternion.LookRotation(_target - transform.position);

            agent.destination = _target;
            state = UnitState.moving;
        }
	}
}
