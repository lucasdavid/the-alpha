using UnityEngine;
using System.Collections;

public abstract class Thing {
    // Attributes
	string _name;                   // Thing name
	string _displayName;            // Displayed name (in GUI)
	int _health;                    // Health
	bool _destructable;             // Can it be destroyed?
	int _value;                     // Thing value
    
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
}
