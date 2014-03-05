﻿using UnityEngine;
using System.Collections;

public class LocationTriggers : MonoBehaviour {
    public GameObject[] spawn;
    
    private static bool _engage;
    
    public static bool Engage
    {
        get { return _engage; }
        set { _engage = value; }
    }
    
    void Start () {
        Engage = false;
    }
    
    void OnTriggerEnter(Collider col) {
        Humans.SpawnPoint = spawn[0].transform.position;
        
        SendUnits();
        StartCoroutine(wait());

    }
    
    // Leaving a tier = going to the previous tier
    void OnTriggerExit(Collider col) {
        Humans.SpawnPoint = spawn[1].transform.position;
        
        SendBackup();
        StartCoroutine(wait());
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(1.0f);
        Engage = false;
    }

    void SendUnits() {
        Engage = true;
        
        // Spawn humans upon entering
        for (int i = 0; i < (5 + Tier.currentTier); i++)
            Humans.SpawnHuman(0);

    }

    void SendBackup() {
        Engage = true;
        
        // Spawn humans upon leaving
        for (int i = 0; i < (2 + Tier.currentTier); i++)
            Humans.SpawnHuman(0);

    }
}
