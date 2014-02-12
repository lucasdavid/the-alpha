using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {
    // Attributes
    public string _name;                   // Thing name
    public string _displayName;            // Displayed name (in GUI)
    public int _health;                    // Health
    public bool _destructable;             // Can it be destroyed?
    public int _value;                     // Thing value

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
