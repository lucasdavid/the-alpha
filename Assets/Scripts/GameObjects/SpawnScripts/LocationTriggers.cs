using UnityEngine;
using System.Collections;

public class LocationTriggers : MonoBehaviour {
    public GameObject[] spawn;
    public int characterType;
    public float timeLimit;     // Current time limit

    private float _timeLimit;   // Max time limit
    private static bool _engage;
    
    public static bool Engage
    {
        get { return _engage; }
        set { _engage = value; }
    }
    
    void Start () {
        Engage = false;
        _timeLimit = timeLimit;
    }

    void Update() {
        timeLimit -= Time.deltaTime;
    }
    
    void OnTriggerEnter(Collider col) {
        if (Horde.ThreatLevel > Humans.CurrentValue && timeLimit <= 0) {
            timeLimit = _timeLimit;

            Humans.SpawnPoint = spawn[0].transform.position;

            SendUnits();
            StartCoroutine(wait());
        }
    }
    
    // Leaving a tier = going to the previous tier
    void OnTriggerExit(Collider col) {
        if (Horde.ThreatLevel > Humans.CurrentValue && timeLimit <= 0) {
            timeLimit = _timeLimit;
            Humans.SpawnPoint = spawn[1].transform.position;

            SendBackup();
            StartCoroutine(wait());
        }
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(1.0f);
        Engage = false;
    }

    void SendUnits() {
        Engage = true;
        
        // Spawn humans upon entering
        for (int i = 0; i < (5 + Tier.currentTier); i++)
            Humans.SpawnHuman(characterType);

    }

    void SendBackup() {
        Engage = true;
        
        // Spawn humans upon leaving
        for (int i = 0; i < (2 + Tier.currentTier); i++)
            Humans.SpawnHuman(characterType);

    }
}
