using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graveyard : MonoBehaviour {

    public float healInterval;

    List<Mob> units;
    ParticleSystem effect;

    void Start ()
    {
        units = new List<Mob>();
        effect = GetComponentInChildren<ParticleSystem>();
	}

    void OnTriggerEnter(Collider _collider)
    {
        units.Add( _collider.GetComponent<Mob>() );
        effect.enableEmission = true;
    }

    void OnTriggerExit(Collider _collider)
    {
        units.Remove( _collider.GetComponent<Mob>() );
        effect.enableEmission = units.Count > 0;
    }

    IEnumerator Heal()
    {
        yield return new WaitForSeconds(healInterval);

        foreach ( Mob unit in units )
            unit.Health++;
    }
}
