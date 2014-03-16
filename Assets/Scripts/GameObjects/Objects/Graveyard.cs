using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graveyard : MonoBehaviour {

    public float healInterval;

    ParticleSystem effect;
    List<Mob> units;

    void Start ()
    {
        units = new List<Mob>();
        effect = GetComponentInChildren<ParticleSystem>();
        
        effect.enableEmission = false;
	}

    void OnTriggerEnter(Collider _collider)
    {
        units.Add( _collider.GetComponent<Mob>() );

        // first unit to enter the graveyard, activates the healing
        if ( units.Count == 1 )
        {
            effect.enableEmission = true;
            StartCoroutine ("Heal");
        }
    }

    void OnTriggerExit(Collider _collider)
    {
        units.Remove( _collider.GetComponent<Mob>() );

        // last unit to leave the graveyard, deactivates the healing
        if ( units.Count == 0 )
        {
            effect.enableEmission = false;
            StopCoroutine ("Heal");
        }
    }

    IEnumerator Heal()
    {
        while ( true )
        {
            yield return new WaitForSeconds(healInterval);
            foreach ( Mob unit in units ) unit.Health++;
        }
    }
}
