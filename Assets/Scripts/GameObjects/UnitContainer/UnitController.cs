using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

    public float AttackCooldown;
    public float AttackDamage;
    public bool  hold;

    public LayerMask unitLayer = 1 << 8;    // Unit layer

	NavMeshAgent agent;         // Unit's navmesh agent
    float lastAttack;           // Counter for attack cooldown
    UnitState state;            // Unit FSM, can be either idle, attacking, or moving
    int sightRange;             // Unit's sight range that will make it engage
    int attackRange;            // Range that the unit will actually hit
    bool forceMove;             // Force character to move

    Animator anim;              // Unit's animator
    Mob mob;

	void Start()
	{
        state = UnitState.idle;
		
        agent = GetComponent<NavMeshAgent>();
        anim  = GetComponent<Animator>();
        mob   = GetComponent<Mob>();

        lastAttack = 0;

        // Should put these two together
        sightRange = mob.SightRange;
        attackRange = mob.AttackRange;
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

    void OnEnable() {
        state = UnitState.idle;
    }

    void Idle()
    {
        if ( mob.Target != null && mob.Target.GetComponent<Mob>().Destructable) {
            if (Vector3.Distance(transform.position, mob.Target.transform.position) > attackRange) {
                    Move ( mob.Target.transform.position);
            } else {
                Attacking();
            }
        } else {
            // Detect all nearby objects within the Unit layermask
            var nearbyUnits = Physics.OverlapSphere(transform.position, sightRange, unitLayer);

            for (int i = 0; i < nearbyUnits.Length; i++) {
                if ( mob.Alliance != nearbyUnits[i].GetComponent<Mob>().Alliance) {
                    mob.Target = nearbyUnits[i].gameObject;
                    Move (nearbyUnits[i].transform.position);
                }
            }
        }
    }

    void Moving ()
    {
        // keep aiming until distance less than .1f
        // .1 f might be too close, temp changing to 1.0f
        if ( Vector3.Distance(agent.destination, transform.position) < 1.0f ) {
            anim.SetBool("Attacking", false);
            Stop ();
        }

        // If you have a target
        if ( mob.Target != null && !forceMove) {
            // Stop moving if the target is out of sight range
            if (Vector3.Distance(transform.position, mob.Target.transform.position) > sightRange) {
                anim.SetFloat("Speed", 0);
                anim.SetBool("Attacking", false);
                mob.Target = null;
                state = UnitState.idle;
            // If you're in attack range, attack!
            } else if (Vector3.Distance(transform.position, mob.Target.transform.position) <= attackRange) {
                Attacking();
            // Keep following that target if it's in sight range
            } else {
                Move ( mob.Target.transform.position);
            }
        }
    }

    void Attacking()
    {
        Debug.Log ("Attacking!");

        // For animation
        anim.SetFloat("Speed", 0);
        anim.SetBool("Attacking", true);

        // Look at enemy
        transform.rotation = Quaternion.LookRotation( mob.Target.transform.position - transform.position);

        state = UnitState.attacking;

        if ( ( lastAttack -= Time.deltaTime ) <= 0 ) {
            lastAttack = (AttackCooldown / GetComponent<CharClass>().ASpeedMultiplier);

            // Get UNIT->TARGET->HEALTH -- Redo this, very ugly
            mob.Target.GetComponent<Mob>().Health -= 
                (int)(AttackDamage * GetComponent<CharClass>().ADamageMultiplier);
        }

        // We only leave attacking state if target moves out of position/dies 
        if (Vector3.Distance(transform.position, mob.Target.transform.position) > attackRange) {
            anim.SetBool("Attacking", false);
            Move ( mob.Target.transform.position);
        } else if ( mob.Target == null) {
            anim.SetBool("Attacking", false);
            state = UnitState.idle;
        } else if ( mob.Target.GetComponent<Mob>().Destructable == false) {
            anim.SetBool("Attacking", false);
            mob.Target = null;
            state = UnitState.idle;
        }
    }

    public void Stop ( )
    {
        // stop current movement
        if ( state == UnitState.moving ) {
            agent.Stop();
            anim.SetFloat("Speed", 0);
            state = UnitState.idle;
        }
    }

    public void Resume()
    {
        if ( state == UnitState.idle && agent.destination != null )
            Move ( agent.destination );
    }

    public void Move ( Vector3 _target )
    {
        Move (_target, false);
    }

    public void Move ( Vector3 _target, bool force )
	{
        // Force character to move, so you don't get sent back to attacking state
        if ( forceMove = force )
            anim.SetBool("Attacking", false);

        // Manually moving should override hold -- unit will look at target, but not move (if hold == true)
        transform.rotation = Quaternion.LookRotation(_target - transform.position);

        if ( hold == false )
        {
            agent.destination = _target;
            anim.SetFloat("Speed", 5.0f);
            state = UnitState.moving;
        }
	}
}

