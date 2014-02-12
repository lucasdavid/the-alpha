using UnityEngine;
using System.Collections;

public class CharClass : MonoBehaviour {
    // Attributes
    public string _name;                   // Class name
    public float _healthMultiplier;        // Health multiplier
    public float _movementMultiplier;      // Movement speed multiplier
    public float _aDamageMultiplier;       // Attack damage multiplier
    public float _aSpeedMultiplier;        // Attack speed multiplier

	// Mutator Methods
	public string Name {
		get { return _name; }
		set { _name = value; }
	}
	
	public float HealthMultiplier {
		get { return _healthMultiplier; }
		set { _healthMultiplier = value; }
	}
	
	public float MovementMultiplier {
		get { return _movementMultiplier; }
		set { _movementMultiplier = value; }
	}

    public float ADamageMultiplier {
        get { return _aDamageMultiplier; }
        set { _aDamageMultiplier = value; }
    }

    public float ASpeedMultiplier {
        get { return _aSpeedMultiplier; }
        set { _aSpeedMultiplier = value; }
    }
}
