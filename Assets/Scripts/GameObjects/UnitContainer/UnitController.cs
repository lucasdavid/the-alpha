using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

    public float AttackCooldown;
    public float AttackDamage;
    public bool  hold;

    public LayerMask unitLayer = 1 << 8;    // Unit layer

    UnitState state;            // Unit FSM, can be either idle, attacking, or moving
    NavMeshAgent agent;         // Unit's navmesh agent
    float lastAttack;           // Counter for attack cooldown
    
    Animator anim;              // Unit's animator
    Mob mob;
    
	void Start()
	{
        state = UnitState.idle;
		
        agent = GetComponent<NavMeshAgent>();
        anim  = GetComponent<Animator>();
        mob   = GetComponent<Mob>();
	}

	void Update ()
	{
        if ( state == UnitState.idle )
            Idle();
        else if ( state == UnitState.moving )
            Moving();
        else if (state == UnitState.chasing)
            Chasing();
        else if ( state == UnitState.attacking )
            Attacking();
    }

    void OnEnable()
    {
        state = UnitState.idle;
    }

    void Idle()
    {
        // Detect all nearby objects within the Unit layermask
        Collider[] nearbyUnits = Physics.OverlapSphere(transform.position, mob.SightRange, unitLayer);

        for (int i = 0; i < nearbyUnits.Length; i++)
            if (mob.Alliance != nearbyUnits[i].GetComponent<Mob>().Alliance)
            {
                Attack(nearbyUnits[i].gameObject);
                break;
            }
    }

    void Attacking()
    {
        // if the target unit is not valid or is dead, stop attacking
        if (mob.Target == null || mob.Target.GetComponent<Mob>().Health <= 0)
            Stop();

        else if (!IsInAttackRange())
            Chase();

        // if cooldown was reached, attack can be performed
        else
        {
            if ((lastAttack += Time.deltaTime) >= AttackCooldown / GetComponent<CharClass>().ASpeedMultiplier)
            {
                lastAttack = 0;

                if (mob.Alliance == 0)
                    GetComponent<ZombieAudioController>().Attack();
                else
                    GetComponent<HumanAudioController>().Attack();

                if ((mob.Target.GetComponent<Mob>().Health -= (int)(AttackDamage * GetComponent<CharClass>().ADamageMultiplier)) <= 0)
                    mob.Target.GetComponent<Mob>().Killer = gameObject;
            }
        }
    }

    void Chasing()
    {
        // if the target unit is not valid, it's dead or it's too far, stop chasing
        if (hold ||
            mob.Target == null ||
            !IsInSightRange()  ||
            mob.Target.GetComponent<Mob>().Health <= 0)
            Stop();

        else if (IsInAttackRange())
            Attack();

        else
        {
            transform.rotation = Quaternion.LookRotation(mob.Target.transform.position - transform.position);
            agent.destination = mob.Target.transform.position;
        }
    }

    void Moving()
    {
        // stop moving when it is close to movement target
        // remider: don't remove these braces, it's necessary for the following else.
        if (mob.Target == null)
        {
            if (Vector3.Distance(agent.destination, transform.position) < 1)
                Stop();
        }
        
        // stop chasing when is really close to target unit
        else if (IsInAttackRange())
            Attack();
    }

    public void Move(Vector3 _target)
    {
        anim.SetBool("Attacking", false);
        anim.SetFloat("Speed", 5);

        mob.Target = null;
        agent.destination = _target;
        transform.rotation = Quaternion.LookRotation(agent.destination - transform.position);

        state = UnitState.moving;
	}

    public void MoveAndAttack(GameObject unit)
    {
        anim.SetBool("Attacking", false);
        anim.SetFloat("Speed", 5);

        mob.Target = unit;
        agent.destination = unit.transform.position;
        transform.rotation = Quaternion.LookRotation(agent.destination - transform.position);

        state = UnitState.moving;
    }

    public void Stop()
    {
        anim.SetFloat("Speed", 0);
        anim.SetBool("Attacking", false);

        agent.Stop();
        state = UnitState.idle;   
    }

    public void Resume()
    {
        if (state == UnitState.idle)
        {
            Move(agent.destination);
            agent.Resume();
        }
    }

    void Chase()
    {
        // prevents the knights from keep their fight sound on.
        GetComponent<AudioController>().Stop();

        if (hold) Stop();
        else
        {
            anim.SetBool("Attacking", false);
            anim.SetFloat("Speed", 5.0f);
            state = UnitState.chasing;
        }
    }

    void Attack(GameObject _target)
    {
        mob.Target = _target;
        Attack();
    }

    void Attack()
    {
        agent.Stop();

        // animation setting
        anim.SetFloat("Speed", 0);
        anim.SetBool("Attacking", true);

        // finite-state machine setting
        state = UnitState.attacking;
    }

    public UnitState State() { return state; }
    bool IsInSightRange() { return IsInRange(mob.Target, mob.SightRange); }
    bool IsInAttackRange() { return  IsInRange(mob.Target, mob.AttackRange); }
    bool IsInRange(GameObject _target, float _range) { return Vector3.Distance(transform.position, _target.transform.position) <= _range; }
}
