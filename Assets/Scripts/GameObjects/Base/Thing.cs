using UnityEngine;
using System.Collections;

public abstract class Thing {
    // Attributes
    string _name = null;
    string _displayName = null;
    int _health = 0;
    bool _destructable = false;
    int _value = 0;
    
    // Get/Set Methods
    string Name {
        get { return _name; }
        set { _name = value; }
    }
    
    string DisplayName {
        get { return _displayName; }
        set { _name = value; }
    }
    
    int Health {
        get { return _health; }
        set { _health = value; }
    }
    
    bool Destructable {
        get { return _destructable; }
        set { _destructable = value; }
    }
    
    int Value {
        get { return _value; }
        set { _value = value; }
    }
}
