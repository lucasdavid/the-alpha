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
        set { _health = value; }
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
        Health = (int)(100 * GetComponent<CharClass>().HealthMultiplier);
    }


    void Update() {
        if (Health <= 0) {
            //Animator anim = GetComponent<Animator>();
            //anim.SetBool("Dead", true);
            //gameObject.transform.position = new Vector3(20.0f, 0f, 0);
            Destructable = false;
            this.Recycle();
        }
    }

    void OnEnable() {
        Health = (int)(100 * GetComponent<CharClass>().HealthMultiplier);
        Destructable = true;
        Target = null;
    }
}
