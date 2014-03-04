using UnityEngine;
using System.Collections;

public class Tier : MonoBehaviour {
    public GameObject[] spawn;

    private static bool _engage;
    public static int currentTier;        // Tier the character is current in;

    public static bool Engage
    {
        get { return _engage; }
        set { _engage = value; }
    }

	void Start () {
        Engage = false;
        currentTier = 1;
	}
	
    void OnTriggerEnter(Collider col) {
        Humans.SpawnPoint = spawn[0].transform.position;

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

    // Leaving a tier = going to the previous tier
    void OnTriggerExit(Collider col) {
        Humans.SpawnPoint = spawn[0].transform.position;

        if (string.Compare (gameObject.name, "Tier1") == 0 && col.CompareTag ("selectable")) {
            TierOne ();
        } else if (string.Compare (gameObject.name, "Tier2") == 0 && col.CompareTag ("selectable")) {
            TierOne ();
        } else if (string.Compare (gameObject.name, "Tier3") == 0 && col.CompareTag ("selectable")) {
            TierTwo ();
        } else if (string.Compare (gameObject.name, "Tier4") == 0 && col.CompareTag ("selectable")) {
            TierThree ();
        } else if (string.Compare (gameObject.name, "Tier5") == 0 && col.CompareTag ("selectable")) {
            TierFour ();
        }
    }

    void TierOne() {
        currentTier = 1;
        Mob.ThreatMultiplier = 1;
        Debug.Log ("Entered Tier 1");

        // If "Engage", continue until player back tracks to tier 1
        Engage = false;
    }

    void TierTwo() {
        currentTier = 2;
        Mob.ThreatMultiplier = 2;
        Debug.Log ("Entered Tier 2");
        Engage = true;

        // Spawn 2 humans upon entering tier 3
        for (int i = 0; i < 5; i++)
            Humans.SpawnHuman(0);
    }

    void TierThree() {
        currentTier = 3;
        Mob.ThreatMultiplier = 3;
        Debug.Log ("Entered Tier 3");

        // Spawn 5 humans upon entering tier 3
        for (int i = 0; i < 5; i++)
            Humans.SpawnHuman(0);
    }

    void TierFour() {
        currentTier = 4;
        Mob.ThreatMultiplier = 4;
        Debug.Log ("Entered Tier 4");

        // Spawn 10 humans upon entering tier 4
        for (int i = 0; i < 10; i++)
            Humans.SpawnHuman(0);
    }

    void TierFive() {
        currentTier = 5;
        Mob.ThreatMultiplier = 5;
        Debug.Log ("Entered Tier 5");

        // Spawn 10 humans upon entering tier 5
        for (int i = 0; i < 20; i++)
            Humans.SpawnHuman(0);
    }
}
