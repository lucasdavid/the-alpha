using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {
    // Attributes
    public string _name;                    // Thing name
    public string _displayName;             // Displayed name (in GUI)
    public int _health;                     // Health
    public bool _destructable;              // Can it be destroyed?
    public int _value;                      // Thing value
    public GameObject _target;              // Target to attack/goto
    public int _sightRange;                 // Sight range
    public int _attackRange;                // Attack range
    public int _alliance;                   // Unit's allegiance, 0 = Zombie, 1 = Human

    Animator anim;
    //NavMeshAgent navAgent;
    public bool big;

    // Mutator Methods
    public string Name {
        get { return _name; }
        set { _name = value; }
    }
    
    public string DisplayName {
        get { return _displayName; }
        set { _displayName = value; }
    }

    public int Health {
        get { return _health; }
        set {
            _health = value; 

            if (Health <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }
    
    public bool Destructable {
        get { return _destructable; }
        set { _destructable = value; }
    }
    
    public int Value {
        get { return _value; }
        set { _value = value; }
    }

    public GameObject Target {
        get { return _target; }
        set { _target = value; }
    }

    public int SightRange {
        get { return _sightRange; }
        set { _sightRange = value; }
    }

    public int AttackRange {
        get { return _attackRange; }
        set { _attackRange = value; }
    }

    public int Alliance {
        get { return _alliance; }
        set { _alliance = value; }
    }

    void Start() {
        anim = GetComponent<Animator>();
        //navAgent = GetComponent<NavMeshAgent>();
        anim.SetBool("Big", big);   
        OnEnable();
    }

    void OnEnable() {
        Health = (int)(100 * GetComponent<CharClass>().HealthMultiplier);
        Target = null;
        collider.enabled = true;
        GetComponent<UnitController>().enabled = true;
        Destructable = true;
    }

    IEnumerator Die() {
        anim.SetBool("Dead", true);                     // Trigger death animation
        collider.enabled = false;                       // Stop enemies from moving towards it
        GetComponent<UnitController>().enabled = false; // Disable attacking
        Destructable = false;                           // Set invincible

        // Very bad place to put this, but it works
        if (Alliance != 0) {                // If a human dies
            Horde.BrainPoints += Value;
            Horde.ThreatLevel += Value;
            Horde.ResetThreatTimer();       // Reset ThreatLevel decrement timer
        } else {                            // If a zombie dies
            Horde.CurrentValue -= Value;
            Horde.ThreatLevel -= Value;
        }
 
        yield return new WaitForSeconds(5.0F);
        this.Recycle();
    }
}
