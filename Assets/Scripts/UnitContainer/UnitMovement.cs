using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour {

    public float AttackCooldown;
    public float AttackDamage;
    public bool hold;

    public LayerMask unitLayer = 1 << 8;    // Unit layer

	NavMeshAgent agent;         // Unit's navmesh agent
    float lastAttack;           // Counter for attack cooldown
    UnitState state;            // Unit FSM, can be either idle, attacking, or moving
    int sightRange;             // Unit's sight range that will make it engage
    int attackRange;            // Range that the unit will actually hit

	void Start()
	{
        state = UnitState.idle;
		agent = GetComponent<NavMeshAgent>();

        lastAttack = 0;

        // Should put these two together
        sightRange = GetComponent<Mob>().SightRange;
        attackRange = GetComponent<Mob>().AttackRange;
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
        if (GetComponent<Mob>().Target != null) {
            if (Vector3.Distance(transform.position, GetComponent<Mob>().Target.transform.position) > attackRange)
                Move (GetComponent<Mob>().Target.transform.position);
            else
                Attacking();
        } else {
            // Detect all nearby objects within the Unit layermask
            var nearbyUnits = Physics.OverlapSphere(transform.position, sightRange, unitLayer);

            for (int i = 0; i < nearbyUnits.Length; i++) {
                if (GetComponent<Mob>().Alliance != nearbyUnits[i].GetComponent<Mob>().Alliance) {
                    GetComponent<Mob>().Target = nearbyUnits[i].gameObject;
                    Move (nearbyUnits[i].transform.position);
                }
            }
        }
    }

    void Moving ()
    {
        // keep aiming until distance less than .1f
        // .1 f might be too close, temp changing to 1.0f
        if ( Vector3.Distance(agent.destination, transform.position) < 1.0f )
            state = UnitState.idle;

        // If you have a target
        if (GetComponent<Mob>().Target != null) {
            // Stop moving if the target is out of sight range
            if (Vector3.Distance(transform.position, GetComponent<Mob>().Target.transform.position) > sightRange) {
                GetComponent<Mob>().Target = null;
                state = UnitState.idle;
            // If you're in attack range, attack!
            } else if (Vector3.Distance(transform.position, GetComponent<Mob>().Target.transform.position) <= attackRange) {
                Attacking();
            // Keep following that target if it's in sight range
            } else {
                Move (GetComponent<Mob>().Target.transform.position);
            }
        }
    }

    void Attacking()
    {
        Debug.Log ("Attacking!");
        state = UnitState.attacking;

        if ( ( lastAttack -= Time.deltaTime ) <= 0 ) {
            // TODO: Take into account ASpeedMultiplier;
            lastAttack = AttackCooldown;

            // Get UNIT->TARGET->HEALTH -- Redo this, very ugly
            GetComponent<Mob>().Target.GetComponent<Mob>().Health -= 
                (int)(AttackDamage * GetComponent<CharClass>().ADamageMultiplier);
        }

        // We only leave attacking state if target moves out of position/dies
        if (Vector3.Distance(transform.position, GetComponent<Mob>().Target.transform.position) > attackRange)
            Move (GetComponent<Mob>().Target.transform.position);
    }

	public void Move(Vector3 _target)
	{
        // Manually moving should override hold -- unit will look at target, but not move (if hold == true)
        transform.rotation = Quaternion.LookRotation(_target - transform.position);

        if ( hold == false )
        {
            agent.destination = _target;
            state = UnitState.moving;
        }
	}
}

