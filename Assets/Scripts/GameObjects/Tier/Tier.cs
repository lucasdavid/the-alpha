using UnityEngine;
using System.Collections;

public class Tier : MonoBehaviour {
    public GameObject[] spawn;

    private static bool _engage;

    public static bool Engage
    {
        get {return _engage;}
        set {_engage = value; }
    }

	void Start () {
        Engage = false;	
	}
	
    void OnTriggerEnter(Collider col) {
        if (string.Compare (gameObject.name, "Tier1") == 0 && col.CompareTag ("selectable")) {
            TierOne ();
        } else if (string.Compare (gameObject.name, "Tier2") == 0 && col.CompareTag ("selectable")) {
            TierTwo ();
        } else if (string.Compare (gameObject.name, "Tier3") == 0 && col.CompareTag ("selectable")) {
            TierThree ();
        } else if (string.Compare (gameObject.name, "Tier4") == 0 && col.CompareTag ("selectable")) {
            TierFour ();
        } else if (string.Compare (gameObject.name, "Tier5") == 0 && col.CompareTag ("selectable")) {
            TierFive ();
        }
           
    }

    void OnTriggerExit(Collider col) {
        Debug.Log ("Exited Tier");
    }

    void TierOne() {
        Mob.ThreatMultiplier = 1;
        Debug.Log ("Entered Tier 1");
        Engage = true;
        Humans.SpawnPoint = spawn[0].transform.position;
        //Humans.SpawnHuman(0);
    }

    void TierTwo() {
        Mob.ThreatMultiplier = 2;
        Debug.Log ("Entered Tier 2");
    }

    void TierThree() {
        Mob.ThreatMultiplier = 3;
        Debug.Log ("Entered Tier 3");
        Engage = true;
    }

    void TierFour() {
        Mob.ThreatMultiplier = 4;
        Debug.Log ("Entered Tier 4");
    }

    void TierFive() {
        Mob.ThreatMultiplier = 5;
        Debug.Log ("Entered Tier 5");
    }
}
