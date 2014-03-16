using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

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

    public bool _big;
    private int _lockUnit;                  // Once a unit dies, lock it
    private static int _threatMultiplier;   // ThreatLevel Multiplier

    static int tutorial = 0;

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
            Target = Alpha.GetAlpha();
        else
            Target = null;
        
        collider.enabled = true;
        GetComponent<UnitController>().enabled = true;
        GetComponent<NavMeshAgent>().enabled   = true;
        Destructable = true;

        _lockUnit = 0;
    }

    IEnumerator Die()
    {
        if (tutorial == 0 && Killer == Alpha.GetAlpha())
            Camera
                .main
                .GetComponent<HintController>()
                .Add("Your Alpha has now killed the human...");

        anim.SetBool("Dead", true);                     // Trigger death animation

        collider.enabled = false;                       // Stop enemies from moving towards it
        GetComponent<UnitController>().enabled = false; // Disable attacking
        GetComponent<NavMeshAgent>().enabled   = false; // disable collision if living units
        Destructable = false;                           // Set invincible
        Horde.ResetThreatTimer();                       // Reset ThreatLevel decrement timer
        
        if (Name == "Alpha" || Name == "King")
        {
            Camera
                .main
                .GetComponent<HintController>()
                .Flush()
                .Add(
                    Name == "Alpha"
                    ? "Your Alpha has died. You lose!"
                    : "You have defeated the King. You win!"
                );

            Camera
                .main
                .GetComponent<CameraMovement>()
                .Move(transform.position + 30 * Vector3.down, true);
            Camera
                .main
                .transform
                .LookAt(transform.position);
            Camera
                .main
                .transform
                .RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);

            yield return new WaitForSeconds(10.0f);

            Application.LoadLevel("credits");
        }
        else
        {
            // Only spawn if Humans have more value than Zombies
            if (Killer == Alpha.GetAlpha())
            {
                int type;

                if (name == "BasicHuman")
                    type = 0;
                else if (name == "ScoutHuman")
                    type = 1;
                else if (name == "SoldierHuman")
                    type = 2;
                else if (name == "TankHuman")
                    type = 3;
                else
                    type = 0;

                Camera.main.GetComponent<CharacterSpawn>().Spawn(type, transform.position, true);
            }

            if (tutorial == 0)
            {
                Camera.main.GetComponent<HintController>().Add("... and a new zombie will rise!");
                Camera.main.GetComponent<HintController>().EndTutorial();
                tutorial++;
            }

            // If a human dies
            if (Alliance != 0)
            {
                GetComponent<HumanAudioController>().Hurt();

                Horde.ThreatLevel += (Value * ThreatMultiplier);
                Humans.CurrentValue -= Value;

                if (Horde.BrainPoints + Value <= 99)
                    Horde.BrainPoints += Value;
            }
            // If a zombie dies
            else
            {
                GetComponent<ZombieAudioController>().Hurt();

                Horde.ThreatLevel -= Value;
                Horde.CurrentValue -= Value;

                HudController
                    .main
                    .GetComponent<HudController>()
                    .SignalUnitDied(gameObject);
            }

            this.Recycle();
        }
    }
}
