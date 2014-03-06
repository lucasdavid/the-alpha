using UnityEngine;
using System.Collections;

public class Graveyard : MonoBehaviour {
    float timer;

	// Full HP for Alpha in 2 minutes
	void Start () {
        timer = 0;	
	}

    void OnTriggerStay(Collider col) {
        timer -= Time.deltaTime;

        var mob = col.GetComponent<Mob>();

        if ((mob.gameObject.tag == "selectable") && (mob.Health < mob.MaxHealth) && timer <= 0) {
            mob.Health++;
            timer = 0.12f;
        }
    }
}
