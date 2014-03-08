using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {
    // Attributes
    public string _name;                    // Thing name
    public string _displayName;             // Displayed name (in GUI)
    public int _maxHealth;                  // Max Health
    public int _health;                     // Current Health
    public bool _destructable;              // Can it be destroyed?
    public int _value;                      // Thing value
    public GameObject _target;              // Target to attack/goto
    public int _sightRange;                 // Sight range
    public int _attackRange;                // Attack range
    public int _alliance;                   // Unit's allegiance, 0 = Zombie, 1 = Human
    public GameObject _killer;              // Who killed this unit?

    Animator anim;
    AudioController audioContr;

    public bool _big;
    private int _lockUnit;                  // Once a unit dies, lock it
    private static int _threatMultiplier;   // ThreatLevel Multiplier

    public static int ThreatMultiplier {
        get { return _threatMultiplier; }
        set { _threatMultiplier = value; }
    }

    // Mutator Methods
    public string Name {
        get { return _name; }
        set { _name = value; }
    }
    
    public string DisplayName {
        get { return _displayName; }
        set { _displayName = value; }
    }

    public int MaxHealth {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public int Health {
        get { return _health; }
        set {
            _health = value;

            if ( _maxHealth != 0 && _health > _maxHealth )
                _health = _maxHealth;

            if (_health <= 0)
            {
                _lockUnit++;

                // This ensures it only fires once
                if (_lockUnit == 1)
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

    public GameObject Killer {
        get { return _killer; }
        set { _killer = value; }
    }

    void Start ()
    {
        MaxHealth = Health;
        ThreatMultiplier = 1;

        anim  = GetComponent<Animator>();
        audioContr = GetComponent<AudioController>();

        anim.SetBool("Big", _big);   
        OnEnable();
    }

    void OnEnable ()
    {
        Health = ( int ) (100 * GetComponent<CharClass>().HealthMultiplier);

        // Only chase if set to "Engage"
        if ( Alliance != 0      &&
             Name     != "King" &&
            (Tier.Engage || LocationTriggers.Engage) )
        { 
            GetComponent<UnitController>().SetTarget(true); // Force unit to target the Alpha
            Target = Alpha.GetAlpha();
        }
        else
        {
            Target = null;
        }

        collider.enabled = true;
        GetComponent<UnitController>().enabled = true;
        GetComponent<NavMeshAgent>().enabled   = true;
        Destructable = true;

        _lockUnit = 0;
    }

    IEnumerator Die()
    {
        anim.SetBool("Dead", true);                     // Trigger death animation

        try { audioContr.Play((int)AudioController.DefaultSounds.death, true); }
        catch {  }
        
        collider.enabled = false;                       // Stop enemies from moving towards it
        GetComponent<UnitController>().enabled = false; // Disable attacking
        GetComponent<NavMeshAgent>().enabled   = false; // disable collision if living units
        Destructable = false;                           // Set invincible
        Horde.ResetThreatTimer();                       // Reset ThreatLevel decrement timer
        
        // Very bad place to put this, but it works
        // If a human dies
        if (Alliance != 0)
        {
            Horde.ThreatLevel += (Value * ThreatMultiplier);
            Humans.CurrentValue -= Value;

            Horde.BrainPoints += Value;
        }
        // If a zombie dies
        else 
        {
            Horde.ThreatLevel -= Value;
            Horde.CurrentValue -= Value;
        }

        yield return new WaitForSeconds(4.0F);

        // Only spawn if Humans have more value than Zombies
        if ( Killer == Alpha.GetAlpha() )
        {
            switch ( Name )
            {
                case "BasicHuman":
                    Camera.main.GetComponent<CharacterSpawn>().Spawn (0, transform.position, true);
                    break;
                case "ScoutHuman":
                    Camera.main.GetComponent<CharacterSpawn>().Spawn (1, transform.position, true);
                    break;
                case "SoldierHuman":
                    Camera.main.GetComponent<CharacterSpawn>().Spawn (2, transform.position, true);
                    break;
                case "TankHuman":
                    Camera.main.GetComponent<CharacterSpawn>().Spawn (3, transform.position, true);
                    break;
                default:
                    Camera.main.GetComponent<CharacterSpawn>().Spawn (0, transform.position, true);
                    break;
             }
        }

        this.Recycle();

    }
}
