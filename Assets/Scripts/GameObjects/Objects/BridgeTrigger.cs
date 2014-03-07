﻿using UnityEngine;
using System.Collections;

public class BridgeTrigger : MonoBehaviour {
    public GameObject bridge;
    public GameObject bridgeObstacle;
	
    // Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter() {
        // Same as Z = 256
        Vector3 targetAngle = new Vector3(0,0,-104.0f);

        bridge.transform.eulerAngles = targetAngle;//Vector3.Lerp (bridge.transform.eulerAngles, targetAngle, Time.deltaTime);

        Destroy(bridgeObstacle);
    }
}
