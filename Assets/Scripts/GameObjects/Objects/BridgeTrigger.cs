using UnityEngine;
using System.Collections;

public class BridgeTrigger : MonoBehaviour {
    public GameObject bridge;
    public GameObject bridgeObstacle;
    public GameObject topLeft; // Enable
    public GameObject bridgeSpawn; // Disable

    // Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter() {

        Vector3 targetAngle = new Vector3(0,0,252.0f);

        bridge.transform.eulerAngles = targetAngle;//Vector3.Lerp (bridge.transform.eulerAngles, targetAngle, Time.deltaTime);

        Destroy(bridgeObstacle);

        topLeft.GetComponent<LocationTriggers>().enabled = true;
        bridgeSpawn.GetComponent<LocationTriggers>().enabled = false;

    }
}
